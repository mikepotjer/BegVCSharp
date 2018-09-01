using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using static System.Console;

namespace DatabaseRelations
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
    /// This class defines the Store table, which is defined as a parent to the Stock table.
    /// </summary>
    public class Store
    {
        [Key]
        public int StoreID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public virtual List<Stock> Inventory { get; set; }
    }

    /// <summary>
    /// This class defines the Stock table, which is defined as child to the Book table.
    /// </summary>
    public class Stock
    {
        [Key]
        public int StockID { get; set; }
        public int OnHand { get; set; }
        public int OnOrder { get; set; }
        public virtual Book Item { get; set; }
    }

    /// <summary>
    /// The database context provides the CRUD operations for the table.
    /// </summary>
    public class BookContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<Stock> Stocks { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // The using clause ensures we get connected to the database, and cleans up when
            // we're done, even if an exception occurs.
            using (var db = new BookContext())
            {
                // Create 2 Book objects.
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

                // Create a store object.
                Store store1 = new Store
                {
                    Name = "Main St Books",
                    Address = "123 Main St",
                    Inventory = new List<Stock>()
                };
                db.Stores.Add(store1);

                // Create 2 stock objects, one for each book object, and add them to the
                // store object.
                Stock store1book1 = new Stock
                {
                    Item = book1,
                    OnHand = 4,
                    OnOrder = 6
                };
                store1.Inventory.Add(store1book1);

                Stock store1book2 = new Stock
                {
                    Item = book2,
                    OnHand = 1,
                    OnOrder = 9
                };
                store1.Inventory.Add(store1book2);

                // Create a second store object.
                Store store2 = new Store
                {
                    Name = "Campus Books",
                    Address = "321 College Ave",
                    Inventory = new List<Stock>()
                };
                db.Stores.Add(store2);

                // Create 2 stock objects, one for each book object, and add them to the
                // second store object.
                Stock store2book1 = new Stock
                {
                    Item = book1,
                    OnHand = 7,
                    OnOrder = 23
                };
                store2.Inventory.Add(store2book1);

                Stock store2book2 = new Stock
                {
                    Item = book2,
                    OnHand = 2,
                    OnOrder = 8
                };
                store2.Inventory.Add(store2book2);

                // Save all of the data generated above.
                db.SaveChanges();

                // Retrieve all the Stores in the database, which will allow us to drill-down to
                // the Stock and Book data.
                var query = from store in db.Stores
                            orderby store.Name
                            select store;

                // Generate a report of all the Store data.
                WriteLine("Bookstore Inventory Report:");
                foreach (var store in query)
                {
                    WriteLine($"\n{store.Name} located at {store.Address}");
                    foreach (Stock stock in store.Inventory)
                    {
                        WriteLine($"- Title: {stock.Item.Title}");
                        WriteLine($"-- Copies in Store: {stock.OnHand}");
                        WriteLine($"-- Copied on Order: {stock.OnOrder}");
                    }
                }

                WriteLine("Press a key to exit...");
                ReadKey();
            }
        }
    }
}
