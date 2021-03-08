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

             string path = @"E:\MTSummary_Message trace report.csv";

             List<string> sender_names = filter.Parse(path, 1);

             for (int i = 0; i<=10; i++)
             {
                 Console.WriteLine(sender_names[i]);
             }    
            
        }
    }
}
