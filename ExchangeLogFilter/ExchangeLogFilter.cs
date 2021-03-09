using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ExchangeLogFilter
{
    public class ExchangeLogFilter
    {

        // dorobić robienie unikatowej listy z logu, a potem dopiero sprawdzanie - złożonośc algorytmu spadnie
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
            using (var reader = new StreamReader(ExchangeLogFilePath))
            {
                while (!reader.EndOfStream)
                {
                    try
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        string contact = values[column_index];
                        contact = contact.ToLower().Replace("\"", "");

                        var alias = contact.Substring(0, contact.IndexOf("@"));
                        var domain = contact.Substring(contact.IndexOf("@") + 1);
                        var domainToDot = domain.Substring(0, domain.IndexOf("."));

                        if (!FilteredList.Contains(contact))
                        {
                            if (!ContainsBlockedStrings(domain, VendorList)
                                && !ContainsBlockedStrings(alias, BlockedWordsList)
                                && !ContainsBlockedStrings(domain, BlockedDomainsList)
                                && !alias.Any(char.IsDigit)
                                && (alias.Length > 3)
                                && (alias != domainToDot))

                            {
                                FilteredList.Add(contact);
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return FilteredList;
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
    }
}
