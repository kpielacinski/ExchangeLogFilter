using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExchangeLogFilter
{
    public class ExchangeLogFilter
    {
        public string ExchangeLogFilePath { get; set; }
        public string VendorFilePath { get; set; }
        public string BlockedAliasesFilePath { get; set; }
        public string BlockedDomainsFilePath { get; set; }
        public string ExportFilePath { get; set; }

        public List<string> VendorList { get; set; }
        public List<string> BlockedWordsList { get; set; }
        public List<string> BlockedDomainsList { get; set; }

        public List<string> Filter(int column_index)
        {
            List<string> FilteredList = new List<string>();
            List<string> NotFilteredList = new List<string>();

            VendorList = loadCSV(VendorFilePath, 0);
            BlockedWordsList = loadCSV(BlockedAliasesFilePath, 0);
            BlockedDomainsList = loadCSV(BlockedDomainsFilePath, 0);

            NotFilteredList = loadCSV(ExchangeLogFilePath, 1);
            NotFilteredList = RemoveDuplicates(NotFilteredList);

            foreach (string mail in NotFilteredList)
            {
                try
                {
                    string contact = mail;
                    contact = contact.ToLower().Replace("\"", "");

                    var alias = contact.Substring(0, contact.IndexOf("@"));
                    var domain = contact.Substring(contact.IndexOf("@") + 1);

                    if (!FilteredList.Contains(contact))
                    {
                        if (!ContainsBlockedStrings(domain, VendorList)
                            && !ContainsBlockedStrings(alias, BlockedWordsList)
                            && !ContainsBlockedStrings(domain, BlockedDomainsList)
                            && !alias.Any(char.IsDigit)
                            && alias.Length > 3
                            && !domain.Contains(alias))
                        {
                            FilteredList.Add(contact);
                        }
                    }
                }
                catch (Exception ex)
                {

                }
            }
            return FilteredList;
        }

        private List<string> loadCSV(string path, int column_index)
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

        public void ExportToCSV(List<string> list)
        {
            var csv = new StringBuilder();

            for (int i = 0; i < list.Count(); i++)
            {
                var newLine = list[i];
                csv.AppendLine(newLine);
            }

            File.WriteAllText(this.ExportFilePath, csv.ToString(), Encoding.UTF8);
        }
        private List<string> RemoveDuplicates(List<string> listWithDuplicates)
        {
            List<string> listWithoutDuplicates = new List<string>();
            foreach(string item in listWithDuplicates)
            {
                if(!listWithoutDuplicates.Contains(item))
                {
                    listWithoutDuplicates.Add(item);
                }
            }
            return listWithoutDuplicates;
        }
    }
}
