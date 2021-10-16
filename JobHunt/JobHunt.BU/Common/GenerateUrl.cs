using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JobHunt.BU.Common
{
    public static class GenerateUrl
    {
        public static string GenerateSlug(string TieuDe, int Id)
        {
            string phrase = string.Format("{0}-{1}", TieuDe, Id);

            string str = RemoveSign4VietnameseString(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            //str = str.Substring(0, str.Length <= 90 ? str.Length : 90).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens 
            return str;
        }

        public static string GenerateSlugNMG(string TieuDe, string Id)
        {
            string phrase = string.Format("{0}-{1}", TieuDe, Id);
            string str = RemoveSign4VietnameseString(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 90 ? str.Length : 90).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            var idRandom = RandomString(4);
            var idRandom1 = RandomString(8);
            return (str + '-' + idRandom + '-' + idRandom1).ToLower();
        }

        public static string ConvertUrlCap2(string TieuDe, int key1)
        {
            string phrase = string.Format("{0}", TieuDe);
            string str = RemoveSign4VietnameseString(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 90 ? str.Length : 90).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens
            return str;
        }

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
            {
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            }
            return str;
        }


        private static readonly string[] VietnameseSigns = new string[]
        {

            "aAeEoOuUiIdDyY",

            "áàạảãâấầậẩẫăắằặẳẵ",

            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

            "éèẹẻẽêếềệểễ",

            "ÉÈẸẺẼÊẾỀỆỂỄ",

            "óòọỏõôốồộổỗơớờợởỡ",

            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

            "úùụủũưứừựửữ",

            "ÚÙỤỦŨƯỨỪỰỬỮ",

            "íìịỉĩ",

            "ÍÌỊỈĨ",

            "đ",

            "Đ",

            "ýỳỵỷỹ",

            "ÝỲỴỶỸ"
        };
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
