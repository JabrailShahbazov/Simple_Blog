using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;
using Blog.ViewModel;
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
        public IndexViewModel GetAllPosts(int pageNumber, string category)
        {
            int pageSize = 5;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _appDbContext.Posts.AsQueryable();

            if (!String.IsNullOrEmpty(category))
            {
                query = query.Where(post => post.Category.ToLower().Equals(category.ToLower()));
            }

            int postsCount = query.Count();

            return new IndexViewModel
            {
                PageNumber = pageNumber,
                NextPage = postsCount > skipAmount + pageSize,
                Category = category,
                Posts = query
                    .Skip(skipAmount)
                    .Take(pageSize)
                    .ToList()
            };
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
