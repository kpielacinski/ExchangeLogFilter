using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExchangeLogFilter
{
    public class ExchangeLogFilter
    {
        public string VendorFilePath { get; set; }
        public string ExchangeLogFilePath { get; set; }
        public string BlockedWordsFilePath { get; set; }

        public List<string> VendorList { get; set; }
        public List<string> BlockedWordsList { get; set; }


        public List<string> Filter(int column_index)
        {
            List<string> listA = new List<string>();
            using (var reader = new StreamReader(ExchangeLogFilePath))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        string contact = values[column_index];
                        contact = contact.Replace("\"", "");
                        
                        if(!ContainsBlockedStrings(contact, VendorList))
                        {
                            listA.Add(contact);
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return listA;
        }

        public List<string> loadCSV(string path, int column_index)
        {
            List<string> listA = new List<string>();
            using (var reader = new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        string row = values[column_index];

                        listA.Add(row);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return listA;
        }

        private bool ContainsBlockedStrings(string name, List<string> blockedWordsList)
        {
            bool containsBlockedWord = false;

            foreach (string blockedWord in blockedWordsList)
            {
                if (name.Contains(blockedWord))
                {
                    containsBlockedWord = true;
                    break;
                }
                else
                {
                    containsBlockedWord = false;
                }
            }
            return containsBlockedWord;
        }
    }
}
