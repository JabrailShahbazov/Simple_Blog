using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;

namespace Blog.Data.Repositories
{
    public class Repository : IRepository
    {
        private readonly AppDbContext _appDbContext;
        public Repository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Post GetPost(int? id)
        {
            return _appDbContext.Posts.FirstOrDefault(x => x.Id == id);
        }

        public List<Post> GetAllPosts()
        {
            return _appDbContext.Posts.ToList();
        } 
        
        public List<Post> GetAllPosts(string category)
        {
            //Func<Post, bool> inCategory = (post) => post.Category.ToLower().Equals(category.ToLower());
            //return _appDbContext.Posts.Where(posts=> inCategory(posts)).ToList();

            return _appDbContext.Posts.Where(post => post.Category.ToLower().Equals(category.ToLower()))
                .ToList();
        }

        public void AddPost(Post post)
        {
            _appDbContext.Posts.AddAsync(post);

        }

        public void UpdatePost(Post post)
        {
            _appDbContext.Posts.Update(post);
        }

        public void RemovePost(int id)
        {
            _appDbContext.Posts.Remove(GetPost(id));
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _appDbContext.SaveChangesAsync() > 0)
            {
                return true;
            }

            return false;
        }
    }
}
