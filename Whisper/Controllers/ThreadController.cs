using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Whisper.Data.Repository;
using Whisper.Data.UoW;
using Whisper.Entities;
using Whisper.Models;


namespace Whisper.Controllers
{
    public class ThreadController : Controller
    {
        public IRepository<Community> _communityRepository;
        public IRepository<Thread> _threadRepository;
        public IUnitOfWork _unitOfWork;
        public ThreadController(IUnitOfWork unitOfWork, IRepository<Community> communityRepository, IRepository<Thread> threadRepository)
        {
            _communityRepository = communityRepository;
            _threadRepository = threadRepository;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //this is implemented in Community/Detail
            return View();
        }

        /// <summary>
        /// Community ekleme sayfasına yönlendiren fonksiyon
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add(string CommunityId)
        {
            var community = _communityRepository.Table.Include(x => x.Threads).FirstOrDefault(x => x.Id == CommunityId);
            var model = new ThreadViewModel() { Community = community, Thread = new Thread() };
            return View(model);
        }
        /// <summary>
        /// Community ekleme sayfası
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Thread thread)
        {
            try
            {
                if (thread == null)
                {
                    return Json(new { success = false, message = "Community is null" });
                }
                //Get Current User id from User
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                thread.UserId = userId;
                await _threadRepository.AddAsync(thread);
                await _unitOfWork.CommitAsync();
                return Json(new { succeeded = true, threadId = thread.Id });
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
                var thread = _threadRepository.Table.Include(x => x.Comments).ThenInclude(x => x.User).OrderByDescending(x => x.CreatedAt)
                                                    .Include(x => x.Community)
                                                    .Include(x => x.User).FirstOrDefault(x => x.Id == id);

                if (thread == null)
                {
                    return Json(new { success = false, message = "Community is null" });
                }
                var model = new ThreadViewModel() { Thread = thread };

                return View(model);
            }
            catch (Exception ex)
            {
                return Json(new { message = "Error" });
            }

        }
    }
}
