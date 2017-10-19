using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneticConversion
{



    public class PhoneticWords
    {
        private static Dictionary<string, string> _phoneticWordLists;


        public PhoneticWords() 
        {
            if (_phoneticWordLists == null) PopulatePhoneticWordList();
        }

        private void PopulatePhoneticWordList()
        {
            _phoneticWordLists = new Dictionary<string, string>();
            var phoneticSets = Notepad.ReadAsLineList("phoneticWordConversion");

            foreach (var phoneticSet in phoneticSets)
            {
                if (!string.IsNullOrEmpty(phoneticSet))
                {
                    var words = phoneticSet.Split(' ');
                    _phoneticWordLists.Add(words[0], words[1]);
                }
            }
        }


        public string GetBengaliequivalent(string englishWord) 
        {
            if (_phoneticWordLists.ContainsKey ( englishWord) )
            {
                return _phoneticWordLists[englishWord];
            }
            return "";
        }


    }
}
