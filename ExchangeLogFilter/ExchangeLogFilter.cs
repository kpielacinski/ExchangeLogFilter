using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ExchangeLogFilter
{
    public class ExchangeLogFilter
    {

        public List<string> Parse(string path, int column_index)
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

                        values[column_index] = values[column_index].Replace("\"", "");
                        
                        listA.Add(values[column_index]);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            return listA;
        }

        private bool isVendor(string name)
        {
            return true;
        }
    }
}
