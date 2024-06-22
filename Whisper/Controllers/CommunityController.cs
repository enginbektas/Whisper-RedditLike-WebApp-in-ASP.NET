using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Whisper.Data.Repository;
using Whisper.Data.UoW;
using Whisper.Entities;
using Whisper.Models;

namespace Whisper.Controllers
{
    public class CommunityController : Controller
    {
        public IRepository<Community> _communityRepository;
        public IRepository<User> _userRepository;
        public IUnitOfWork _unitOfWork;
        public CommunityController(IUnitOfWork unitOfWork, IRepository<Community> communityRepository, IRepository<User> userRepository)
        {
            _communityRepository = communityRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }
        public IActionResult Index()
        {
            var communities = _communityRepository.GetAllAsync().Result.ToList();
            var model = new CommunityViewModel()
            { Communities = communities };
            return View(model);
        }

        /// <summary>
        /// Community ekleme sayfasına yönlendiren fonksiyon
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var model = new CommunityViewModel() { Community = new Community() };
            return View(model);
        }
        /// <summary>
        /// Community ekleme sayfası
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Community community)
        {
            try
            {
                if (community == null)
                {
                    return Json(new { success = false, message = "Community is null" });
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                community.CreatorId = userId;

                //if community title already exists, dont create
                var communityExists = _communityRepository.Table.Any(x => x.Name == community.Name);
                if (communityExists)
                {
                    return Json(new { success = false, message = "Community already exists" });
                }
                await _communityRepository.AddAsync(community);
                await _unitOfWork.CommitAsync();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return Json(new { message = "Community is null" });
            }

        }

        public async Task<IActionResult> Detail(string id)
        {
            try
            {
                var community = _communityRepository.Table.Include(x => x.Creator).Include(x => x.Threads).Include(x => x.Moderators).FirstOrDefault(x => x.Id == id);
                


                if (community == null)
                {
                    return Json(new { success = false, message = "Community is null" });
                }
                var model = new CommunityViewModel() { Community = community };

                return View(model);
            }
            catch (Exception ex)
            {
                return Json( new { message = "Error" } );
            }

        }
        [HttpPost]
        public async Task<IActionResult> AddModerator(string userName, string communityName)
        {
            try
            {
                var user = _userRepository.Table.FirstOrDefault(x => x.UserName == userName);

                if (user == null)
                {
                    return Json(new { success = false, message = "User is null" });
                }
                //add user as a mod to community

                var community = _communityRepository.Table.Include(x => x.Moderators).FirstOrDefault(x => x.Name == communityName);
                if (community == null)
                {
                    return Json(new { success = false, message = "Community is null" });
                }
                community.Moderators.Add(user);
                await _unitOfWork.CommitAsync();
                return Json(new { success = true, message = "Moderator added" });

            }
            catch (Exception ex)
            {
                return Json(new { message = "Error" });
            }

        }
    }
}
