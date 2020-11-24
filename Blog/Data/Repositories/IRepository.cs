using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Models;
using Blog.ViewModel;

namespace Blog.Data.Repositories
{
    public interface IRepository
    {
        Post GetPost(int? id);
        List<Post> GetAllPosts();
        IndexViewModel GetAllPosts(int pageNumber , string category);
        void AddPost(Post post);
        void UpdatePost(Post post);
        void RemovePost(int id);

        Task<bool> SaveChangesAsync();
    }
}
