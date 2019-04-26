using System;
using System.IO;
using System.Collections.Generic;

namespace DsrBackend.DataAccess
{
    public class FileAccess
    {
        /// <summary>
        /// Returns a list of flight plans read from a file.
        /// </summary>
        /// <param name="filepath">Path to the file containing flight plans.</param>
        /// <returns>List of flight plans.</returns>
        public static List<string> GetFlightPlans(string filepath)
        {
            var list = new List<string>();

            string[] contents;
            try
            {
                contents = File.ReadAllLines(filepath);
                foreach (var content in contents)
                {
                    content.Trim();
                    list.Add(content);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception caught reading file: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }

            return list;
        }

        public static List<string> ParseFlightPlans(string contents)
        {
            var list = new List<string>();

            string[] splContents = contents.Split('\n');

            foreach (var content in splContents)
            {
                content.Trim();
                list.Add(content);
            }

            return list;
        }
    }
}
