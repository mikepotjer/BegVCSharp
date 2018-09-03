using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using static System.Console;

namespace GhostStories
{
    /// <summary>
    /// This class defines the structure of the Stories table and its link to the Authors table.
    /// </summary>
    public class Story
    {
        [Key]
        public int StoryID { get; set; }
        public string Title { get; set; }
        public string Rating { get; set; }
        public virtual Author Author { get; set; }
    }

    /// <summary>
    /// This class defines the structure of the Authors table.
    /// </summary>
    public class Author
    {
        [Key]
        public int AuthorID { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
    }

    /// <summary>
    /// This class links the class structures to the tables to define in the database.
    /// </summary>
    public class StoryContext : DbContext
    {
        public DbSet<Story> Stories { get; set; }
        public DbSet<Author> Authors { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Populating the ghost story database, please wait...\n");

            using (var db = new StoryContext())
            {
                // Create Story and Author objects and add them to the tables
                Story story1 = new Story
                {
                    Title = "A House in Aungier Street",
                    Rating = "eerie",
                    Author = new Author
                    {
                        Name = "Sheridan Le Fanu",
                        Nationality = "Irish"
                    }
                };
                db.Stories.Add(story1);

                Story story2 = new Story
                {
                    Title = "The Signalman",
                    Rating = "atmospheric",
                    Author = new Author
                    {
                        Name = "Charles Dickens",
                        Nationality = "English"
                    }
                };
                db.Stories.Add(story2);

                Story story3 = new Story
                {
                    Title = "The Turn of the Screw",
                    Rating = "a bit dull",
                    Author = new Author
                    {
                        Name = "Henry James",
                        Nationality = "American"
                    }
                };
                db.Stories.Add(story3);

                Story story4 = new Story
                {
                    Title = "Number 13",
                    Rating = "mysterious",
                    Author = new Author
                    {
                        Name = "M.R. James",
                        Nationality = "English"
                    }
                };
                db.Stories.Add(story4);

                // Save all the data generated above.
                db.SaveChanges();

                // Retrieve all the story data and sort it.
                var query = from story in db.Stories
                            orderby story.Title
                            select story;

                // Print a report of all the stories in the database.
                WriteLine("Ghost Stories in the database:");
                foreach (var story in query)
                {
                    WriteLine($"{story.Title} by {story.Author.Nationality} author {story.Author.Name}; Rating: {story.Rating}");
                }

                Write("\nPress a key to continue...");
                ReadKey();
            }
        }
    }
}
