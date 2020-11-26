﻿#nullable enable
using System.Threading.Tasks;
using Blog.Data.FileManager;
using Blog.Data.Repositories;
using Blog.Models;
using Blog.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly IFileManager _fileManager;

        public HomeController(IRepository repository, IFileManager fileManager)
        {
            _repository = repository;
            _fileManager = fileManager;
        }



        public IActionResult Index(int pageNumber, string category ,string search)
        {
            if (pageNumber < 1)
            {
                return RedirectToAction("Index", new { pageNumber = 1 , category});
            }

            var vm = _repository.GetAllPosts(pageNumber ,category ,search);

            return View(vm);
        }

        public IActionResult Post(int id) =>
            View(_repository.GetPost(id));

        [HttpGet("/Image/{image}")]
        public IActionResult Image(string image) =>
            new FileStreamResult(_fileManager.ImageStream(image),
                $"image/{image.Substring(image.LastIndexOf('.') + 1)}");


        //public IActionResult Index(string category)
        //{
        //    var posts = string.IsNullOrEmpty(category) ? _repository.GetAllPosts() : _repository.GetAllPosts(category);
        //    return View(posts);
        //}

        //public IActionResult Post(int id)
        //{
        //    var post = _repository.GetPost(id);
        //    return View(post);
        //}

        //[HttpGet("/Image/{image}")]
        //public IActionResult Image(string image)
        //{
        //    var mime = image.Substring(image.LastIndexOf('.')+1);
        //    return new FileStreamResult(_fileManager.ImageStream(image),$"image/{mime}");
        //}

    }
}
