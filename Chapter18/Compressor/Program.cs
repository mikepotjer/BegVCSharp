using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using static System.Console;

namespace Compressor
{
    class Program
    {
        /// <summary>
        /// Compresses and saves the specified text in the specified file.
        /// </summary>
        /// <param name="filename">The name of the file in which the data is to be compressed and saved.</param>
        /// <param name="data">A string to be compressed and saved in the file.</param>
        static void SaveCompressedFile(string filename, string data)
        {
            // Create a file stream for the file to store the compressed results.
            FileStream fileStream = new FileStream(filename, FileMode.Create, FileAccess.Write);

            // Create a GZip compression stream to compress the file.
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Compress);

            // Create a stream writer and write the data into the compressed file.
            StreamWriter writer = new StreamWriter(compressionStream);
            writer.Write(data);
            writer.Close();
        }

        /// <summary>
        /// Loads compressed data from the specified file.
        /// </summary>
        /// <param name="filename">The name of a compressed file containing the data to extract.</param>
        /// <returns>The contents of the compressed file.</returns>
        static string LoadCompressedFile(string filename)
        {
            // Create a file stream for the compressed file to read.
            FileStream fileStream = new FileStream(filename, FileMode.Open, FileAccess.Read);

            // Create a GZip stream to decompress the contents of the file.
            GZipStream compressionStream = new GZipStream(fileStream, CompressionMode.Decompress);

            // Create a stream reader and read the contents of the compressed file.
            StreamReader reader = new StreamReader(compressionStream);
            string data = reader.ReadToEnd();
            reader.Close();

            return data;
        }

        static void Main(string[] args)
        {
            try
            {
                string filename = "CompressedFile.txt";

                // Get the string to compress.
                WriteLine("Enter a string to compress (will be repeated 100 times):");
                string sourceString = ReadLine();

                // Repeat the string to inflate the size of the data to compress.
                StringBuilder sourceStringMultiplier = new StringBuilder(sourceString.Length * 100);
                for (int i = 0; i < 100; i++)
                {
                    sourceStringMultiplier.Append(sourceString);
                }
                sourceString = sourceStringMultiplier.ToString();

                // Display the original size of the data, and save it in the compressed file.
                WriteLine($"Source data is {sourceString.Length} bytes long.");
                SaveCompressedFile(filename, sourceString);

                // Report the results of compressing the data.
                WriteLine($"\nData saved to {filename}.");
                FileInfo compressedFileData = new FileInfo(filename);
                WriteLine($"Compressed file is {compressedFileData.Length} bytes long.");

                // Read the data from the compressed file, removing the duplicated data to just
                // display the original string.
                string recoveredString = LoadCompressedFile(filename);
                recoveredString = recoveredString.Substring(0, recoveredString.Length / 100);
                WriteLine($"\nRecovered data: {recoveredString}");

                ReadKey();
            }
            catch (IOException ex)
            {
                WriteLine("An IO exception has been thrown!");
                WriteLine(ex.ToString());
                ReadKey();
                return;
            }
        }
    }
}
