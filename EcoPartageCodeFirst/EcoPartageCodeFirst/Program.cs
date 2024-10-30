using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace EcoPartageCodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Saisit, Crée et Sauvegarde un nouveau Blog.
                Console.Write("Entrez un nom pour votre nouveau Blog : ");
                var name = Console.ReadLine();
                var blog = new Blog { Name = name };
                db.Blogs.Add(blog);
                db.SaveChanges();
                // Sélectionne et affiche tous les blogs depuis la BD.
                var query = from b in db.Blogs
                            orderby b.Name
                            select b;
                Console.WriteLine("Tous les blogs dans la BD :");
                foreach (var item in query)
                {
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine("Pressez une touche pour quitter...");
                Console.ReadKey();
            }
        }
        public class Blog
        {
            public int BlogId { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
            public virtual List<Post> Posts { get; set; }
        }
        public class Post
        {
            public int PostId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public int BlogId { get; set; }
            public virtual Blog Blog { get; set; }
        }
        public class User
        {
            [Key]
            public string Username { get; set; }
            public string DisplayName { get; set; }
        }
        public class BloggingContext : DbContext
        {
            public DbSet<Blog> Blogs { get; set; }
            public DbSet<Post> Posts { get; set; }
            public DbSet<User> Users { get; set; }
        }
    }
}
