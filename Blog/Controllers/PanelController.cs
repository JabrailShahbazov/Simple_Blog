using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data.Repositories;
using Blog.Models;
using Microsoft.AspNetCore.Authorization;

namespace Blog.Controllers
{
    [Authorize(Roles = "admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repository;

        public PanelController(IRepository repository)
        {
            _repository = repository;
        }


        public IActionResult Index()
        {
            
            return View(_repository.GetAllPosts());
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return View(new Post());
            }
            else
            {
                var post = _repository.GetPost(id);
                return View(post);
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Post post)
        {
            if (post.Id > 0)
                _repository.UpdatePost(post);
            else
                _repository.AddPost(post);

            if (await _repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View(post);
        }

        [HttpGet]
        public async Task<IActionResult> Remove(int? id)
        {
            if (id != null) _repository.RemovePost((int) id);
            await _repository.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
