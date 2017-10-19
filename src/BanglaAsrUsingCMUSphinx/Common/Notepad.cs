using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Notepad
    {
        public static string[] ReadAsLineArray(string txtFileNameWithPath) 
        {
            var lines = ReadAsLineList(txtFileNameWithPath);
            return lines.ToArray();
        }

        public static string[] ReadAsLineArrayAndAddWordList(string txtFileNameWithPath)
        {
            List<string>  lines = ReadAsLineList(txtFileNameWithPath);
            var wordList = GetWordList(lines) ; //.OrderBy(p=>p).ToList ();

           lines.AddRange(wordList);
            return lines.ToArray();
        }


        public static List<string> ReadAsLineList(string txtFileNameWithPath)
        {
            var sr = new StreamReader(txtFileNameWithPath+".txt");
            var lines = new List<string>();
            string line = string.Empty;
            while ((line = sr.ReadLine()) != null)
            {
                lines.Add(line);
            }

            sr.Close();
            sr.Dispose();
            return lines;
        }


        private static List<string> _wordList;
        private static List<string> GetWordList(List<string> sentences) 
        {
            if (_wordList == null) _wordList = new List<string>();

            
            foreach (var sentence in sentences )
            {
                var words = sentence.Split(' ');
                foreach (var word in words) 
                {
                    if (!string.IsNullOrEmpty(word)) 
                    {
                        if (!_wordList.Contains(word)) 
                        {
                            _wordList.Add(word);
                        }
                    }
                }
            }
            
            


            return _wordList;
        }


    }
}
