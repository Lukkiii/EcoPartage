using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EcoPartageCodeFirst
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BloggingContext())
            {
                // Saisit, Crée et Sauvegarde un nouveau User.
                Console.Write("Entrez un Username pour enregistrement : ");
                var name = Console.ReadLine();
                Console.Write("Entrez un Email pour enregistrement : ");
                var email = Console.ReadLine();
                Console.Write("Entrez un Password pour enregistrement : ");
                var password = Console.ReadLine();
                var user = new Users { UserName = name, Email = email, Password = password , Role = null}; 
                db.Users.Add(user);
                db.SaveChanges();
                // Sélectionne et affiche tous les blogs depuis la BD.
                var query = from u in db.Users
                            orderby u.UserName
                            select u;
                Console.WriteLine("Tous les Usrs dans la BD :");
                foreach (var item in query)
                {
                    Console.WriteLine(item.UserName);
                }
                Console.WriteLine("Pressez une touche pour quitter...");
                Console.ReadKey();
            }
        }

        public class Users
        {
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int idUser { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string Role { get; set; }
            public ICollection<Comments> CommentsGiven { get; set; }
            public ICollection<Comments> CommentsRecived { get; set; }
        }
        public class Annonces
        {
            [Key]
            public int idAnnonce { get; set; }
            public string Titre { get; set; }
            public string Description { get; set; }
            public int Points { get; set; }
            public DateTime Date { get; set; }
            public bool Active { get; set; }

            [ForeignKey("Users")]
            public int idUser { get; set; }
            public Users Users { get; set; }
        }

        public class Comments
        {
            [Key]
            public int idComment { get; set; }
            public DateTime Date { get; set; }
            public string Notice { get; set; }
            [ForeignKey("Giver")]
            public int? idUserGiven { get; set; }
            [ForeignKey("Recipient")]
            public int? idUserRecipient { get; set; }
            public Users Giver { get; set; }
            public Users Recipient { get; set; }
        }

        public class Transactions
        {
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int IdTransaction { get; set; }
            [Required]
            [ForeignKey("IdUser")] // §!!!§ vérifier nom de la clé étrangère
            public int UserIdGiver { get; set; }
            [Required]
            [ForeignKey("IdUser")] // §!!!§ vérifier nom de la clé étrangère
            public int UserIdRecipient { get; set; }
            [Required]
            [ForeignKey("IdAnnonce")]
            public int IdAnnonce { get; set; }
            [ForeignKey("Points")] // Table Annonces
            public int AnnoncePoints { get; set; }
            [Required]
            public DateTime DateTransaction { get; set; }

        }
        public class Tags
        {
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int IdTag { get; set; }
            [Required]
            public string CategoryName { get; set; }
        }
        public class AnnoncesTags
        {
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int IdAnnonceTag { get; set; }
            [ForeignKey("Tags")]
            public int IdTag { get; set; }
            //public virtual ICollection<int> Tag { get; set; }
            [ForeignKey("Annonces")]
            public int IdAnnonce { get; set; }
            //public virtual ICollection<int> IdAnnonce { get; set; }
        }
        public class GeographicalSector
        {
            [Required]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            [Key]
            public int IdGeographicalSector { get; set; }
            [ForeignKey("IdAnnonce")]
            public int IdAnnonce { get; set; }
            [Required]
            public int FirstPlace { get; set; } // Le premier lieu est requis, les utres sont optionnels
            public int SecondPlace { get; set; }
            public int ThirdPlace { get; set; }
        }

        public class BloggingContext : DbContext
        {
            public DbSet<Users> Users { get; set; }
            public DbSet<Annonces> Annonces { get; set; }
            public DbSet<Comments> Comments { get; set; }
            public DbSet<Transactions> Transactions { get; set; }
            public DbSet<Tags> Tags { get; set; }
            public DbSet<AnnoncesTags> AnnoncesTags { get; set; }
            public DbSet<GeographicalSector> GeographicalSectors { get; set; }
        }
    }
}
