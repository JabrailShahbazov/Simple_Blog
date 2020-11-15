using System.Threading.Tasks;
using Blog.Data.Repositories;
using Blog.Models;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public IActionResult Index()
        {
            
            return View(_repository.GetAllPosts());
        }

        public IActionResult Post(int id)
        {
            var post = _repository.GetPost(id);
            return View(post);
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
