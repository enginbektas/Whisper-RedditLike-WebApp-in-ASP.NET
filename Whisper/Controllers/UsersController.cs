using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Whisper.Data.Repository;
using Whisper.Data.UoW;
using Whisper.Entities;
using Whisper.Entities.Enums;
using Whisper.Models.UserModels;

namespace Whisper.Controllers
{
    public class UsersController : Controller
    {
        private readonly IRepository<User> _userRepository;
        private UserManager<User> _userManager;
        private SignInManager<User> _signInManager;
        private RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IRepository<User> userRepository, RoleManager<Role> roleManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepository = userRepository;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index(string errorMessage)
        {
            var allUsers = _userManager.Users;
            return View(new UsersViewModel() { Users = allUsers });
        }

        /// <summary>
        /// Kullanıcı Detay sayfası
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Detail(string id, string userName)
        {
            User user = null;
            if (id != null)
                user = _userRepository.Table.Include(x => x.Threads).FirstOrDefaultAsync(x => x.Id == id).Result;
            else
                user = _userRepository.Table.Include(x => x.Threads).FirstOrDefaultAsync(x => x.UserName == userName).Result;
            return View(new UsersViewModel() { User = user });
        }


        /// <summary>
        /// Login ekranına ilk götüren metod
        /// </summary>
        /// <returns></returns>
        /// 
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View(new UserLoginViewModel());
        }


        /// <summary>
        /// Login işlemini yapan method. Başarılı ise home'a, başarısız ise hata mesajı ile logine geri gönderir.
        /// Kullanıcı database'de mevcut mu kontrolünü şu an için UserName ile sağlıyoruz
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                model.ErrorMessage = "Email veya parola yanlış";
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);
            //var roles = ((ClaimsIdentity)User.Identity).Roles();
            if (result.Succeeded)
            {
                return Redirect("~/Home/Index");
            }
            model.ErrorMessage = "Email veya parola yanlış";
            ModelState.AddModelError("", "Email veya parola yanlış");

            return View(model);
        }

        public async Task<IActionResult> Register()
        {
            return View(new UserLoginViewModel());
        }

        /// <summary>
        /// Kullanıcı ekleme işlemini yapan method.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Register(UserLoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            User user = new User() { UserName = model.UserName, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {

                return Redirect("~/Users/Login");
            }

            ModelState.AddModelError("", "Bilinmeyen hata, lütfen tekrar deneyiniz.");
            model.ErrorMessage = "Girmiş olduğunuz bilgilerden bazıları zaten mevcut.";
            return View(model);
        }

        /// <summary>
        /// Kullanıcı bilgilerini editleme sayfasına götüren method
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]

        public async Task<IActionResult> Edit(string id, string userName)
        {
            var user = await _userRepository.Table.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                user = await _userRepository.Table.FirstOrDefaultAsync(x => x.UserName == userName);
            }
            var model = new UsersViewModel();

            if (!User.IsInRole("Admin"))
            {
                if (user.UserName != User.Identity.Name)
                {
                    model.ErrorMessage = "Buna yetkiniz yok.";
                    return View(model);
                }
            }

            if (user == null)
            {
                model.ErrorMessage = "Kullanıcı bulunamadı";
                return View(model);
            }
            model.User = user;

            return View(model);
        }



        [HttpPost]

        public async Task<IActionResult> Edit(User user)
        {
            var myUser = await _userRepository.Table.FirstOrDefaultAsync(x => x.Id == user.Id);
            if (myUser == null)
            {
                myUser = await _userRepository.Table.FirstOrDefaultAsync(x => x.UserName == user.UserName);
            }
            myUser.Email = user.Email;
            _userRepository.Update(myUser);
            await _unitOfWork.CommitAsync();
            return Json(new { Succeeded = true, userId = user.Id });
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Users");
        }
        public IActionResult Accessdenied()
        {
            return View();
        }
    }
}
