using System;
using System.Collections.Generic;
using System.IO;

namespace ExchangeLogFilter
{
    class Program
    {
        static void Main(string[] args)
        {
            ExchangeLogFilter filter = new ExchangeLogFilter();

            filter.ExchangeLogFilePath = @"E:\Programy\ExchangeLogFilter\ExchangeLog.csv";
            filter.VendorFilePath = @"E:\Programy\ExchangeLogFilter\Vendors.csv";
            filter.BlockedAliasesFilePath = @"E:\Programy\ExchangeLogFilter\BlockedAliases.csv";
            filter.BlockedDomainsFilePath = @"E:\Programy\ExchangeLogFilter\BlockedDomains.csv";
            filter.ExportFilePath = @"E:\Programy\ExchangeLogFilter\FilteredMails.csv";

            filter.VendorList = filter.loadCSV(filter.VendorFilePath, 0);
            filter.BlockedWordsList = filter.loadCSV(filter.BlockedAliasesFilePath, 0);
            filter.BlockedDomainsList = filter.loadCSV(filter.BlockedDomainsFilePath, 0);

            List<string> sender_names = filter.Filter(1);

            filter.ExportToCSV(sender_names);

            /*try
            {
                for (int i = 0; i <= sender_names.Count; i++)
                {
                    Console.WriteLine(sender_names[i]);
                }
            }
            catch(Exception e)
            {

            }*/
        }
    }
}
