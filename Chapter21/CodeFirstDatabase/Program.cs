using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using static System.Console;

namespace CodeFirstDatabase
{
    /// <summary>
    /// This class defines the Books table. The Code field is defined as the unique identifier
    /// for each record in the table. The [Key] comes from the DataAnnotations.
    /// </summary>
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        [Key]
        public int Code { get; set; }
    }

    /// <summary>
    /// The database context provides the CRUD operations for the table.
    /// </summary>
    public class BookContext :DbContext
    {
        public DbSet<Book> Books { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The using clause ensures we get connected to the database, and cleans up when
            // we're done, even if an exception occurs.
            using (var db = new BookContext())
            {
                // Create 2 Book objects and save them in the database.
                Book book1 = new Book
                {
                    Title = "Beginning Visual C# 2015",
                    Author = "Perkins, Reid, and Hammer"      
                };
                db.Books.Add(book1);

                Book book2 = new Book
                {
                    Title = "Beginning XML",
                    Author = "Fawcett, Quin, and Ayers"
                };
                db.Books.Add(book2);

                db.SaveChanges();

                // List all the books in the database after they are created.
                var query = from b in db.Books
                            orderby b.Title
                            select b;

                WriteLine("All books in the database:");
                foreach (var b in query)
                {
                    WriteLine($"{b.Title} by {b.Author}, code={b.Code}");
                }

                WriteLine("Press a key to exit...");
                ReadKey();
            }
        }
    }
}
