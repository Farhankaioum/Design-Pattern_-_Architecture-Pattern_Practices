using Cosmonaut;
using Cosmonaut.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetBook.Domain;

namespace TweetBook.Services
{
    public class CosmosPostService : IPostService
    {
        private readonly ICosmosStore<CosmosPostDto> _cosmosStore;

        public CosmosPostService(ICosmosStore<CosmosPostDto> cosmosStore)
        {
            this._cosmosStore = cosmosStore;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            var cosmosPost = new CosmosPostDto
            {
                Id = post.Id.ToString(),
                Name = post.Name
            };
           var response = await _cosmosStore.AddAsync(cosmosPost);
            return response.IsSuccess;
        }

        public async Task<bool> DeletePostAsync(Guid postId)
        {
            var post = await _cosmosStore.FindAsync(postId.ToString());
            if (post == null)
            {
                return false;
            }
            return true;
        }

        public async Task<Post> GetPostByIdAsync(Guid postId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Post>> GetPostsAsync()
        {
            var posts = await _cosmosStore.Query().ToListAsync();
            return posts.Select(x => new Post { Id = Guid.Parse(x.Id), Name = x.Name }).ToList();
        }

        public async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
