using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace RegexWithEnglish
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = @"..\..\5000 + 2700 важных английских слов.txt"; 
            string resultFilePath = @"..\..\5000 + 2700 важных английских слов в удобной форме.txt";
            
            if (!File.Exists(filePath))
                return; 
            if (!File.Exists(resultFilePath))
                File.Create(resultFilePath);

            string content = null;
            content = (new StreamReader(filePath)).ReadToEnd();

            Regex regex = new Regex(
                @"(?:(\d+)\s+)?([A-Za-z][A-Za-z\-\.\s]*[A-Za-z\-\.](?:\s+\((?:[A-Za-z\-\.\s\:](?:\s*\,?\s+)?)+\))?)\s+" //слово
                + @"\s*(?:\(|\[)((?:[\w\:\(\)\-\.]+(?:\s*\,?\s+)?)+)(?:\)|\])\s+" // транскрипция
                + @"\s*((?:[А-Яа-яё\:\(\)\.][А-Яа-яё\:\(\)\-\.\s]*[А-Яа-яё\:\(\)\.](?:\s*\,\s+)?)+)"); // перевод
            MatchCollection matchCollection = regex.Matches(content, 0);

            method(resultFilePath, matchCollection);

            Console.ReadKey();
        }

        static void method(string resultFilePath, MatchCollection matchCollection)
        {
            string result = "";
            StreamWriter sw = new StreamWriter(resultFilePath, false, Encoding.Unicode);
            sw.AutoFlush = true;
            foreach (Match match in matchCollection)
            {
                foreach (Group group in match.Groups)
                {
                    if (!match.Groups[0].Equals(group))
                    {
                        if (match.Groups[3].Equals(group)) 
                            result += "[" + group.Value + "]" + "\t";
                        else
                            result += group.Value + "\t";
                    }
                }
                sw.WriteLine(result);
                result = "";
            }
            
        }

        
    }
}
