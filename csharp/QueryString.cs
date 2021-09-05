using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryFileToolFramework
{
    class Program
    {
        static List<string> QueryFile(string queryString, string nowFolder)
        {
            var fileList = Directory.GetFiles(nowFolder);
            var filteredList = fileList.Where(f => f.Contains(queryString)).ToList();
            var subFolder1 = Directory.GetDirectories(nowFolder);
            var subFolder2 = subFolder1
                .Where(f => (!f.Contains("System Volume Information")) &&
                (!f.Contains("RECYCLE.BIN"))).ToList();
            subFolder2.ForEach(f =>
            filteredList.AddRange(QueryFile(queryString, nowFolder = f))
            );
            return filteredList;
        }
        static void Main(string[] args)
        {
            if (args.Length != 0)
            {
                var startTime = DateTime.Now.Ticks;
                var result = QueryFile(args[0], Directory.GetCurrentDirectory());
                long durningTime = DateTime.Now.Ticks - startTime;
                double durningTimeToSec = durningTime / 1000_0000.0;
                result.ForEach(f => Console.WriteLine(f));
                Console.WriteLine();
                Console.WriteLine($"查找共找到{result.Count}条结果，用时{durningTimeToSec}秒");
                Console.ReadLine();
            }
        }
    }
}
