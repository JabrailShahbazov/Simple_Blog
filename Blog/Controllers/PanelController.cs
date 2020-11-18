using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace Blog.Controllers
{
    [Authorize(Roles = "admin")]
    public class PanelController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFileManager _fileManager;

        public PanelController(IRepository repository , IFileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
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
                return View(new PostViewModel());
            }
            else
            {
                var post = _repository.GetPost(id);
                return View(new PostViewModel
                {
                    Id = post.Id,
                    Title = post.Title,
                    Body = post.Body,
                    CurrentImage = post.Image,
                });
            }
           
        }
        [HttpPost]
        public async Task<IActionResult> Edit(PostViewModel vm)
        {
            var post =new Post
            {
                Id = vm.Id,
                Title = vm.Title,
                Body = vm.Body,
            };

            if (vm.Image==null)
            {
                post.Image = vm.CurrentImage;

            }
            else
            {
                post.Image = await _fileManager.SaveImage(vm.Image);
            }

            if (post.Id > 0)
                _repository.UpdatePost(post);
            else
                _repository.AddPost(post);

            if (await _repository.SaveChangesAsync())
                return RedirectToAction("Index");
            else
                return View();
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
