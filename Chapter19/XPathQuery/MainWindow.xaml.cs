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

namespace XPathQuery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private XmlDocument document;

        public MainWindow()
        {
            InitializeComponent();

            // Populate the XML document to search.
            document = new XmlDocument();
            document.Load(@"C:\Work\Visual Studio\BegVCSharp\Chapter19\XML and Schemas\Elements.xml");

            // Initialize the text block with the entire contents of the document.
            Update(document.DocumentElement.SelectNodes("."));
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

        /// <summary>
        /// Updates the text block control with the contents of the node list retrieved by the most
        /// recent search.
        /// </summary>
        /// <param name="nodes">A list of XML nodes matching the search criteria.</param>
        private void Update(XmlNodeList nodes)
        {
            // Check if a node list has been retrieved.
            if (nodes == null || nodes.Count == 0)
            {
                textBlockResult.Text = "The query yielded no results.";
                return;
            }

            // Format the contents of the node list for display, and copy it to the results control.
            string text = "";
            foreach (XmlNode node in nodes)
            {
                text = FormatText(node, text, "") + "\r\n";
            }
            textBlockResult.Text = text;
        }

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Perform the specified search, and output the results.
                XmlNodeList nodes = document.DocumentElement.SelectNodes(textBoxQuery.Text);
                Update(nodes);
            }
            catch(Exception err)
            {
                textBlockResult.Text = err.Message;
            }
        }
    }
}
