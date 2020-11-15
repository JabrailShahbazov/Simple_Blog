using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Models;

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
