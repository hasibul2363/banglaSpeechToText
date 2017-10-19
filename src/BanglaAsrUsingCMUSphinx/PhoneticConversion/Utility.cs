using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PhoneticConversion
{

    
    public class Utility
    {

        public static List<string> getNonSqueezWordList;
        public static string SqueezeWord(string input) 
        {
            input = Regex.Replace(input, "[^0-9a-zA-Z]+", "");
            if (WillPerfromSqueezWord(input))
            {
                //return new string(input.ToCharArray().Distinct().ToArray());
                return  Regex.Replace(input, @"(.)\1{1,}", "$1");
            }
            else
            {
                return input;
            }
        }


        private static  bool WillPerfromSqueezWord(string input)
        {
            var nonSqueezWordList = GetNonSqueezWordList();
            if (nonSqueezWordList.Contains(input.ToLower ()))
            {
                return false ;
            }
            return true;
        }

        private static List<string> GetNonSqueezWordList()
        {
            if (getNonSqueezWordList == null) 
            {
                getNonSqueezWordList = Notepad.ReadAsLineList("NoSqueezeWordList");
            }

            return getNonSqueezWordList;
        }
    }
}
