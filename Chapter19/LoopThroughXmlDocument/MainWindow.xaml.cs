using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Newtonsoft.Json;

namespace LoopThroughXmlDocument
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // This path obviously needs to be changed if your file structure is different.
        private const string booksFile = @"C:\Work\Visual Studio\BegVCSharp\Chapter19\XML and Schemas\Books.xml";

        // Add a field to store the last JSON string that was generated.
        private string lastJson = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonLoop_Click(object sender, RoutedEventArgs e)
        {
            // Load the XML file into a document object for processing.
            XmlDocument document = new XmlDocument();
            document.Load(booksFile);

            // Format the contents of the file for display.
            textBlockResults.Text = FormatText(document.DocumentElement as XmlNode, "", "");
        }

        /// <summary>
        /// A recursive method that generates a string to display the contents of an XML document.
        /// </summary>
        /// <param name="node">An XmlNode to convert to text.</param>
        /// <param name="text">The XML text that has been generated so far.</param>
        /// <param name="indent">The indent string to use for the node being processed.</param>
        /// <returns>A formatted string containing the node information.</returns>
        private string FormatText(XmlNode node, string text, string indent)
        {
            // If the node is just text content, add it to the string as-is, and exit. No indent
            // is needed for this.
            if (node is XmlText)
            {
                text += node.Value;
                return text;
            }

            // Check if an indent has been specified, making sure it isn't null. If it's not
            // empty, make sure it's preceded with a CR & LF.
            if (string.IsNullOrEmpty(indent))
                indent = "";
            else
                text += "\r\n" + indent;

            // If the node is a comment, add the entire comment node to the string as-is, and exit.
            if (node is XmlComment)
            {
                text += node.OuterXml;
                return text;
            }

            // This is a normal node, so add a tag name for it.
            text += "<" + node.Name;

            // If the node has attributes, add them to the tag.
            if (node.Attributes.Count > 0)
                AddAttributes(node, ref text);

            // If there are child nodes, they need to be processed, otherwise we can immediately close
            // the tag for this node.
            if (node.HasChildNodes)
            {
                // This node has child nodes, so we need to close the opening tag and add the children.
                text += ">";

                // Use a recursive call to add each child node, increasing the indent for the child.
                foreach (XmlNode child in node.ChildNodes)
                {
                    text = FormatText(child, text, indent + "  ");
                }

                // If there's only one child node, and it's text or a comment, add the end tag for the
                // parent on the same line. Otherwise the end tag add on the next line and indented.
                if (node.ChildNodes.Count == 1
                    && (node.FirstChild is XmlText || node.FirstChild is XmlComment))
                    text += "</" + node.Name + ">";
                else
                    text += "\r\n" + indent + "</" + node.Name + ">";
            }
            else
                text += " />";

            return text;
        }

        /// <summary>
        /// Adds attribute strings to a tag being processed.
        /// </summary>
        /// <param name="node">The XmlNode currently being processed.</param>
        /// <param name="text">The display string that has been generated so far, passed by reference.</param>
        private void AddAttributes(XmlNode node, ref string text)
        {
            foreach (XmlAttribute xa in node.Attributes)
            {
                text += " " + xa.Name + "='" + xa.Value + "'";
            }
        }

        private void ButtonCreateNode_Click(object sender, RoutedEventArgs e)
        {
            // Load the XML document.
            XmlDocument document = new XmlDocument();
            document.Load(booksFile);

            // Get the root element.
            XmlElement root = document.DocumentElement;

            // Create the new nodes.
            XmlElement newBook = document.CreateElement("book");
            XmlElement newTitle = document.CreateElement("title");
            XmlElement newAuthor = document.CreateElement("author");
            XmlElement newCode = document.CreateElement("code");

            XmlText title = document.CreateTextNode("Beginning Visual C# 2015");
            XmlText author = document.CreateTextNode("Karli Watson et al");
            XmlText code = document.CreateTextNode("314418");

            XmlComment comment = document.CreateComment("The previous edition");

            // Create an attribute for the book node, and add it.
            XmlAttribute pages = document.CreateAttribute("Pages");
            pages.Value = "1000+";
            newBook.Attributes.Append(pages);

            // Insert the elements.
            newBook.AppendChild(comment);
            newBook.AppendChild(newTitle);
            newBook.AppendChild(newAuthor);
            newBook.AppendChild(newCode);

            // Add the text nodes to their respective elements.
            newTitle.AppendChild(title);
            newAuthor.AppendChild(author);
            newCode.AppendChild(code);

            // Add the entire book node to the end of the XML document, and save the results.
            root.InsertAfter(newBook, root.LastChild);
            document.Save(booksFile);

            // Display a message to the user indicating what we did.
            textBlockResults.Text = "A new node has been appended.\r\nClick Loop to see the results.";
        }

        private void ButtonDeleteNode_Click(object sender, RoutedEventArgs e)
        {
            // Load the XML document.
            XmlDocument document = new XmlDocument();
            document.Load(booksFile);

            // Get the root element.
            XmlElement root = document.DocumentElement;

            // Find the node. root is the <books> tag, so its last child will be the last <book> node.
            if (root.HasChildNodes)
            {
                XmlNode book = root.LastChild;

                // Remove the last book node from the document, and save the results.
                root.RemoveChild(book);
                document.Save(booksFile);

                // Display a message to the user indicating what we did.
                textBlockResults.Text = "The last node has been removed.\r\nClick Loop to see the results.";
            }
            else
                textBlockResults.Text = "There are no nodes left to remove from the XML document.";
        }

        private void ButtonXmlToJson_Click(object sender, RoutedEventArgs e)
        {
            // Load the XML document.
            XmlDocument document = new XmlDocument();
            document.Load(booksFile);

            // Convert the XML document to JSON, and display the results.
            string json = JsonConvert.SerializeXmlNode(document);
            textBlockResults.Text = json;

            // Store the JSON string so the JSON to XML button can convert it back to XML.
            lastJson = json;
        }

        private void ButtonJsonToXml_Click(object sender, RoutedEventArgs e)
        {
            // Check if there is a JSON string available to convert.
            if (string.IsNullOrEmpty(lastJson))
            {
                textBlockResults.Text = "There is no JSON string to convert to XML."
                    + "\r\nClick XML > JSON to create a JSON string to convert";
                return;
            }

            try
            {
                // Convert the last JSON string back to an XML document, and display the results.
                XmlDocument document = JsonConvert.DeserializeXmlNode(lastJson);
                textBlockResults.Text = FormatText(document.DocumentElement as XmlNode, "", "");
                
            }
            catch(Exception err)
            {
                textBlockResults.Text = err.Message;
            }
        }
    }
}
