using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public interface IPostService
    {
        public Task<List<Post>> GetPostsAsync();

        public Task<Post> GetPostByIdAsync(Guid postId);


        public Task<bool> CreatePostAsync(Post post);

        public Task<bool> UpdatePostAsync(Post postToUpdate);

        public Task<bool> DeletePostAsync(Guid postId);


    }
}
