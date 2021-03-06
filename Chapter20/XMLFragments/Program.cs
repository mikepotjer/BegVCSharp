﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace XMLFragments
{
    class Program
    {
        static void Main(string[] args)
        {
            // XElement and XDocument both inherit from XContainer, so they both support many
            // of the same features.
            XElement xcust =
                new XElement("customers",
                    new XElement("customer",
                        new XAttribute("ID", "A"),
                        new XAttribute("City", "New York"),
                        new XAttribute("Region", "North America"),
                        new XElement("order",
                            new XAttribute("Item", "Widget"),
                            new XAttribute("Price", 100)
                        ),
                        new XElement("order",
                            new XAttribute("Item", "Tire"),
                            new XAttribute("Price", 200)
                        )
                    ),
                    new XElement("customer",
                        new XAttribute("ID", "B"),
                        new XAttribute("City", "Mumbai"),
                        new XAttribute("Region", "Asia"),
                        new XElement("order",
                            new XAttribute("Item", "Oven"),
                            new XAttribute("Price", 501)
                        )
                    )
                );

            string xmlFileName =
                @"D:\Optimal\Visual Studio 2017\Projects\BegVCSharp\Chapter20\XMLFragments\fragment.xml";
            xcust.Save(xmlFileName);

            XElement xcust2 = XElement.Load(xmlFileName);
            WriteLine("Contents of xcust2:");
            WriteLine(xcust2);
            Write("Program finished, press Enter/Return to continue:");
            ReadLine();
        }
    }
}
