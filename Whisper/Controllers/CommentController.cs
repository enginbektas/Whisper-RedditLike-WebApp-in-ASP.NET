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
using Thread = Whisper.Entities.Thread;

namespace Whisper.Controllers
{
    public class CommentController : Controller
    {
        public IRepository<Community> _communityRepository;
        public IRepository<Entities.Thread> _threadRepository;
        public IRepository<Comment> _commentRepository;
        public IUnitOfWork _unitOfWork;
        public CommentController(IUnitOfWork unitOfWork, IRepository<Comment> commentRepository, IRepository<Community> communityRepository, IRepository<Thread> threadRepository)
        {
            _communityRepository = communityRepository;
            _threadRepository = threadRepository;
            _commentRepository = commentRepository;
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            //this is implemented in Thread/Detail
            return View();
        }

        /// <summary>
        /// Community ekleme sayfası
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(Comment comment)
        {
            try
            {
                if (comment == null)
                {
                    return Json(new { success = false, message = "Comment is null" });
                }
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                comment.UserId = userId;
                await _commentRepository.AddAsync(comment);
                await _unitOfWork.CommitAsync();
                return Json(new { succeeded = true });
            }
            catch (Exception ex)
            {
                return Json(new { message = "Comment is null" });
            }

        }
    }
}
