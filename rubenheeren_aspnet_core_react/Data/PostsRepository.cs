using Microsoft.EntityFrameworkCore;

namespace rubenheeren_aspnet_core_react.Data
{
    public class PostsRepository
    {
        //private readonly IConfiguration configuration;

        //public PostsRepository(IConfiguration config)
        //{
        //    configuration = config;
        //}

        public static async Task<List<Post>> GetPostsAsync()
        {
            //var posts = new List<Post>();

            using (var db = new AppDBContext())
            {
                return await db.Posts.ToListAsync();
            }
            
        }

        public static async Task<Post> GetPostByIdAsync(int post_id)
        {
            using (var db = new AppDBContext())
            {
                return await db.Posts.FirstOrDefaultAsync(p => post_id == p.PostId);
            }
        }

        public static async Task<bool> CreatePostAsync(Post postToCreate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    await db.Posts.AddAsync(postToCreate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {

                    return false;
                }
            }
        }

        public static async Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    db.Posts.Update(postToUpdate);
                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {

                    return false;
                }
            }
        }

        public static async Task<bool> DeletePostAsync(int post_id)
        {
            using (var db = new AppDBContext())
            {
                try
                {
                    var p = await GetPostByIdAsync(post_id);

                    db.Remove(p);

                    return await db.SaveChangesAsync() >= 1;
                }
                catch (Exception e)
                {

                    return false;
                }
            }
        }
    }
}
