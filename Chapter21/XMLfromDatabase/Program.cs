using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace XMLfromDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a database context instance to read from the database.
            using (var db = new BookContext())
            {
                // Retrieve the store data from the database.
                var query = from store in db.Stores
                            orderby store.Name
                            select store;

                foreach (var s in query)
                {
                    // Use Linq to XML to transform the query results to XML.
                    XElement storeElement = new XElement("store",
                            new XAttribute("name", s.Name),
                            new XAttribute("address", s.Address),
                        from stock in s.Stocks
                        select new XElement("stock",
                                new XAttribute("StockID", stock.StockID),
                                new XAttribute("onHand", stock.OnHand),
                                new XAttribute("onOrder", stock.OnOrder),
                            new XElement("book",
                                new XAttribute("title", stock.Book.Title),
                                new XAttribute("author", stock.Book.Author)
                                )   // end book
                            )   // end stock
                        );  // end store

                    WriteLine(storeElement);
                }

                WriteLine("Program finished, press a key to continue...");
                ReadKey();
            }
        }
    }
}
