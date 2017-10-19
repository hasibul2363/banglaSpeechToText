using System;
using System.Collections.Generic;
using System.Text;

namespace PhoneticConversion
{
    public class PhonaticDTO
    {
        private string strEnglish;
        private string strBangla1;
        private string strBangla2;
        public string English
        {
            get
            {
                return strEnglish;
            }
            set
            {
                strEnglish = value;
            }
        }

        public string Bangla1
        {
            get
            {
                return strBangla1;
            }
            set
            {
                strBangla1 = value;
            }
        }

        public string Bangla2
        {
            get
            {
                return strBangla2;
            }
            set
            {
                strBangla2 = value;
            }
        }

    }
}
