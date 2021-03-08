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

            filter.ExchangeLogFilePath = @"E:\Programy\ExchangeLogFilter\MTSummary_Message trace report.csv";
            filter.VendorFilePath = @"E:\Programy\ExchangeLogFilter\Vendors.csv";
            filter.BlockedWordsFilePath = @"E:\Programy\ExchangeLogFilter\BlockedWords.csv";

            filter.VendorList = filter.loadCSV(filter.VendorFilePath, 0);

            List<string> sender_names = filter.Filter(1);

            for (int i = 0; i <= 10; i++)
            {
                Console.WriteLine(sender_names[i]);
            }
        }
    }
}
