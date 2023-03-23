using Microsoft.EntityFrameworkCore;

namespace rubenheeren_aspnet_core_react.Data
{
    public class AppDBContext:DbContext
    {
        public DbSet<Post> Posts { get; set; }  // Table
        //public IConfiguration configuration { get; }

        //public AppDBContext(IConfiguration config)
        //{
        //    configuration = config;
        //}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string? conString = configuration.GetConnectionString("DBConn"); //Microsoft.Extensions.Configuration.GetConnectionString(configuration, "DbConn");
            optionsBuilder.UseSqlite(conString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            Post[] postsToSeed = new Post[6];

            for (int i= 1;i<= 6;i++)
            {
                postsToSeed[i - 1] = new Post
                {
                    PostId = i,
                    Title = "Title for post " + i,
                    Content = "This is Content " + i,
                    CreatedTime = DateTime.Now
                };
            }
            modelBuilder.Entity<Post>().HasData(postsToSeed);
            
        }
    }
}
