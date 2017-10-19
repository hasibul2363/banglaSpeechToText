using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Text.RegularExpressions;

namespace PhoneticConversion
{
    public class BDConverter
    {
        List<PhonaticDTO> oPhonaticDTOList;

        private PhoneticWords phoneticWords;
        public BDConverter() 
        {
            oPhonaticDTOList = ReturnPhonaticDb();
            phoneticWords = new PhoneticWords();
        }



        // This function Receive English character and retruns Corresponding Bangla Char
        // Input string may consist of 1 or more than 1; here we have consider highest 3 character
        // Input: A  OutPut strting[]: আ, া
        // This is used by GetBanglaPhonatic
        // Always Perform DataBase Search
        public string[] GetBanglaChar_fromDB(string english)
        {
            List<string> BanglaChar = new List<string>();
            string conString = "Provider = Microsoft.ACE.OLEDB.12.0; data source = D:\\NLP_DB\\PhonaticMapping.mdb";
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();

            string queryString = "select * from Phonatic_Mapping where English ='" + english + "'";
            OleDbCommand cmd = new OleDbCommand(queryString, con);
            OleDbDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    if (dr[0].ToString() == english)
                    {
                        BanglaChar.Add(dr[1].ToString());
                        BanglaChar.Add(dr[2].ToString());
                    }
                }
            }
            BanglaChar.Add("");
            cmd.Dispose();
            con.Close();
            dr.Dispose();

            return (string[])BanglaChar.ToArray();
        }
        



        // This function Receive English character and retruns Corresponding Bangla Char
        // Input string may consist of 1 or more than 1; here we have consider highest 3 character
        // Input: A  OutPut strting[]: আ, া
        // First Go to DB then Load All the data on List and then perform search

        private string[] GetBanglaChar_fromList(string english)
        {
            List<string> strBanglaList = new List<string>();
            PhonaticDTO oPhonaticDTO1 = new PhonaticDTO();

            List<PhonaticDTO> oPhonaticDTOList = ReturnPhonaticDb();

            foreach (PhonaticDTO oPhonaticDTO in oPhonaticDTOList)
            {
                if (oPhonaticDTO.English == english)
                {
                    strBanglaList.Add(oPhonaticDTO.Bangla1);
                    strBanglaList.Add(oPhonaticDTO.Bangla2);
                    break;
                }

            }

            strBanglaList.Add("");
            return (string[])strBanglaList.ToArray();
        }


        //Receive English string and Returns corresponding Bangla string
        // Input: Amader    Output: আমাদের
        public string GetBanglaPhonatic(string englishWord)
        {

            string bengaliWord = phoneticWords.GetBengaliequivalent(englishWord);
            if (!string.IsNullOrEmpty(bengaliWord))
            {
                return bengaliWord;
            }



            string tempEnglish = englishWord;
            int search_detect = 3;
            int len = 0;
            string findWhat = "";
            string ReturnBangla = "";
            while (tempEnglish != "")
            {
                len = tempEnglish.Length;

                if (len == 2)
                {
                    search_detect = 2;
                }
                if (len == 1)
                {
                    search_detect = 1;
                }

                for (int i = search_detect; i > 0; i--)
                {
                    findWhat = tempEnglish.Substring(len - i, i);
                    string[] strBangla = GetBanglaChar_fromList (findWhat);

                    if (strBangla[0] != "")
                    {
                        tempEnglish = tempEnglish.Remove(len - i, i);

                        if (strBangla[1] != "" && len >= 2)
                        {

                            if (tempEnglish.EndsWith("a"))
                            {
                                ReturnBangla = strBangla[0] + ReturnBangla;
                            }
                            else 
                            {
                                ReturnBangla = strBangla[1] + ReturnBangla;
                            }
                            
                        }
                        else
                        {
                            ReturnBangla = strBangla[0] + ReturnBangla;
                        }
                        break;
                    }


                }




            }

            return ReturnBangla;

        }

        public string GetBanglaPhonaticSentence(string englishSentence)
        {
           
            if (string.IsNullOrEmpty(englishSentence)) return englishSentence;
            var words = englishSentence.Split(' ');
            var bengaliText = string.Empty;
            foreach (var word in words) 
            {
                bengaliText = bengaliText + " " + GetBanglaPhonatic(Utility.SqueezeWord(word));
            }
            return bengaliText;
        }
       
        private List<PhonaticDTO> ReturnPhonaticDb()
        {
            PhonaticDTO oPhonaticDTO = new PhonaticDTO();

            if (oPhonaticDTOList != null) 
            {
                return oPhonaticDTOList;
            }
            oPhonaticDTOList = new List<PhonaticDTO>();

            string conString = "Provider =Microsoft.ACE.OLEDB.12.0; data source = D:\\NLP_DB\\PhonaticMapping.mdb";
            OleDbConnection con = new OleDbConnection(conString);
            con.Open();

            string queryString = "select * from Phonatic_Mapping";
            OleDbCommand cmd = new OleDbCommand(queryString, con);
            OleDbDataReader dr;
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                if (dr.HasRows == true)
                {
                    oPhonaticDTO.English = dr[0].ToString();
                    oPhonaticDTO.Bangla1 = dr[1].ToString();
                    oPhonaticDTO.Bangla2 = dr[2].ToString();

                    oPhonaticDTOList.Add(oPhonaticDTO);
                    oPhonaticDTO = new PhonaticDTO();


                }
            }
            cmd.Dispose();
            con.Close();
            dr.Dispose();

            return oPhonaticDTOList;
        }

    }
}
