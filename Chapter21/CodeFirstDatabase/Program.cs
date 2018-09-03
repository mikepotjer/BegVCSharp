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
        [Key]
        public int Code { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
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
                //// Create 2 Book objects and save them in the database.
                //Book book1 = new Book
                //{
                //    Title = "Beginning Visual C# 2015",
                //    Author = "Perkins, Reid, and Hammer"
                //};
                //db.Books.Add(book1);

                //Book book2 = new Book
                //{
                //    Title = "Beginning XML",
                //    Author = "Fawcett, Quin, and Ayers"
                //};
                //db.Books.Add(book2);

                //db.SaveChanges();

                string title;
                string author;
                Book book;

                // Use a loop structure to allow us to enter multiple books in the same run.
                do
                {
                    Write("Enter a book title (leave blank to quit): ");
                    title = ReadLine();

                    if (string.IsNullOrEmpty(title) == false)
                    {
                        // Lookup the title that was entered, to make sure it doesn't already exist.
                        // Use a case-insensitive comparison. We could also check the author, but
                        // we will just require that each title be unique.
                        var checkTitle = from b in db.Books
                                         where (b.Title.ToLower() == title.ToLower())
                                         select b;

                        // If the title already exists, skip it and prompt for another one.
                        if (checkTitle.Count() > 0)
                        {
                            WriteLine($"The title '{title}' already exists in the database.");
                            continue;
                        }

                        // Prompt for an author name. This is a required field, so make sure a value is entered.
                        do
                        {
                            Write($"Enter the author(s) for {title}: ");
                            author = ReadLine();
                        } while (string.IsNullOrEmpty(author));

                        // Add the new book and save it to the database.
                        book = new Book { Title = title, Author = author };
                        db.Books.Add(book);
                        db.SaveChanges();
                    }
                } while (string.IsNullOrEmpty(title) == false);

                // List all the books in the database after they are created.
                var query = from b in db.Books
                            orderby b.Title
                            select b;

                WriteLine("\nAll books in the database:");
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
