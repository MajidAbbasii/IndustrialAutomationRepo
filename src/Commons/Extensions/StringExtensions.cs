using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using static Commons.Constants;

namespace Commons.Extensions
{
    public static class StringExtensions
    {
       


        public static bool IsValidNationalCode(this string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode)) return false;
            if (nationalCode.Length != 10) return false;
            switch (nationalCode)
            {
                case "0000000000":
                case "1111111111":
                case "2222222222":
                case "3333333333":
                case "4444444444":
                case "5555555555":
                case "6666666666":
                case "7777777777":
                case "8888888888":
                case "9999999999":
                    return false;
                default:
                    int code = 0;
                    char ch;
                    for (int i = 0; i < 9; i++)
                    {
                        ch = nationalCode[i];
                        if (ch < '0') return false;
                        if (ch > '9') return false;

                        int v = ch - 48;
                        code += v * (10 - i);
                    }
                    int r = code % 11;
                    if (r > 1) r = 11 - r;
                    ch = nationalCode[9];
                    if (r == (ch - 48)) return true;
                    break;
            }

            return false;
        }
        private static string GetDefaultEncryptDecryptKey()
        {
            return "!@#$%^&*()|':;=+-_";
        }

        public static string NormalizeTextChars(this string strText, bool persian2Arabic = true)
        {
            if (string.IsNullOrEmpty(strText)) return strText;
            char arabicKaf = (char)1603;
            char arabicYa = (char)1610;

            char persianKaf = (char)1705;
            char persianYa = (char)1740;

            if (persian2Arabic)
            {
                string result = strText.Replace(persianKaf, arabicKaf);
                result = result.Replace(persianYa, arabicYa);
                return result;
            }
            else
            {
                string result = strText.Replace(arabicKaf, persianKaf);
                result = result.Replace(arabicYa, persianYa);
                return result;
            }
        }

        public static string ToPersianDateString(this string value)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;
            string[] dateValues = value.Split('/');
            StringBuilder result = new StringBuilder();
            int day = System.Convert.ToInt32(dateValues[2]);
            switch (day)
            {
                case 3:
                    result.Append("سوم");
                    break;
                case 23:
                    result.Append("بیست و سوم");
                    break;
                case 30:
                    result.Append("سی ام");
                    break;
                default:
                    result.Append(day.ToPersianText()).Append("م");
                    break;
            }
            result.Append(" ");
            int moon = System.Convert.ToInt32(dateValues[1]);
            switch (moon)
            {
                case 1:
                    result.Append("فروردین");
                    break;
                case 2:
                    result.Append("اردیبهشت");
                    break;
                case 3:
                    result.Append("خرداد");
                    break;
                case 4:
                    result.Append("تیر");
                    break;
                case 5:
                    result.Append("مرداد");
                    break;
                case 6:
                    result.Append("شهریور");
                    break;
                case 7:
                    result.Append("مهر");
                    break;
                case 8:
                    result.Append("آبان");
                    break;
                case 9:
                    result.Append("آذر");
                    break;
                case 10:
                    result.Append("دی");
                    break;
                case 11:
                    result.Append("بهمن");
                    break;
                case 12:
                    result.Append("اسفند");
                    break;
            }
            result.Append(" ماه ");
            int year = System.Convert.ToInt32(dateValues[0]);
            result.Append(year.ToPersianText());



            return result.ToString();
        }

        public static bool IsDigit(this string value, int length = 0)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (length != 0 && value.Length != length) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsDigit(value[i])) return false;
            }
            return true;
        }

        public static bool IsLetter(this string value, int length = 0)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (length != 0 && value.Length != length) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (!char.IsLetter(value[i])) return false;
            }
            return true;
        }

        public static bool IsText(this string value, int length = 0)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;
            if (length != 0 && value.Length != length) return false;
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsWhiteSpace(value[i])) continue;
                if (!char.IsLetter(value[i])) return false;
            }
            return true;
        }


        public static bool LessThan(this string self, string value)
        {
            if (self == null && value == null) return false;
            if (value == null) return false;
            if (self == null) return true;
            return self.CompareTo(value) < 0;
        }

        public static bool LessThanEqual(this string self, string value)
        {
            if (self == null && value == null) return true;
            if (value == null) return false;
            if (self == null) return true;
            return self.CompareTo(value) <= 0;
        }

        public static bool GreaterThan(this string self, string value)
        {
            if (self == null && value == null) return false;
            if (value == null) return true;
            if (self == null) return false;
            return self.CompareTo(value) > 0;
        }

        public static bool GreaterThanEqual(this string self, string value)
        {
            if (self == null && value == null) return true;
            if (value == null) return true;
            if (self == null) return false;
            return self.CompareTo(value) >= 0;
        }

        public static string ToRtf(this string value)
        {
            if (value.StartsWith("{\\rtf1\\")) return value;
            string text = "{\\rtf1\\fbidis\\ansi\\deff0{\\fonttbl{\\f0\\fnil\\fcharset178 B Nazanin;}}\\viewkind4\\uc1\\lang1065\\f0\\fs28 " + value + "}0";
            text = text.Replace("\r\n", @"\par");
            text = text.Replace("\n", @"\par");
            var sb = new System.Text.StringBuilder();
            foreach (var c in text)
            {
                if (c <= 0x7f)
                    sb.Append(c);
                else if (c <= 0xFF)
                    sb.Append("\\'" + Convert.ToUInt32(c).ToString("X"));
                else
                    sb.Append("\\u" + Convert.ToUInt32(c) + "?");
            }
            return sb.ToString();

        }

        

        public static string[] ToSmsParameters(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return null;
            string sms = value.Trim(' ', '*');
            string[] result = sms.Split('*');
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = result[i].Trim();
                if (!result[i].IsDigit()) return null;
            }
            return result;
        }

        public static bool IsValidLegalNationalCode(this string nationalCode)
        {
            if (string.IsNullOrWhiteSpace(nationalCode))
            {
                return false;
            }
            if (nationalCode.Length != 11)
                return false;
            int checkDigit = nationalCode[10] - 48;
            int dgt10th = nationalCode[9] - 48;

            int calc1th = (nationalCode[0] - 48 + dgt10th + 2) * 29;
            int calc2th = (nationalCode[1] - 48 + dgt10th + 2) * 27;
            int calc3th = (nationalCode[2] - 48 + dgt10th + 2) * 23;
            int calc4th = (nationalCode[3] - 48 + dgt10th + 2) * 19;
            int calc5th = (nationalCode[4] - 48 + dgt10th + 2) * 17;
            int calc6th = (nationalCode[5] - 48 + dgt10th + 2) * 29;
            int calc7th = (nationalCode[6] - 48 + dgt10th + 2) * 27;
            int calc8th = (nationalCode[7] - 48 + dgt10th + 2) * 23;
            int calc9th = (nationalCode[8] - 48 + dgt10th + 2) * 19;
            int calc10th = (nationalCode[9] - 48 + dgt10th + 2) * 17;

            int getdigit = (calc1th + calc2th + calc3th + calc4th + calc5th + calc6th + calc7th + calc8th + calc9th + calc10th) % 11;
            if (getdigit > 9)
                getdigit = getdigit % 10;
            if (checkDigit == getdigit)
                return true;
            return false;
        }

#if !SILVERLIGHT

        public static IDictionary<string, object> GetPropertyValues(this object value)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            if (value != null)
            {
                System.Type type = value.GetType();
                if (!(type.IsValueType || type == typeof(string)))
                {
                    var properties = type.GetProperties();
                    foreach (var p in properties)
                    {
                        object v = p.GetValue(value, null);
                        result.Add(p.Name, v);
                    }
                }
            }

            return result;
        }
        public static string Compress(this string text)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(text);
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            using (System.IO.Compression.DeflateStream zip = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Compress, true))
            {
                zip.Write(buffer, 0, buffer.Length);
            }

            ms.Position = 0;
            System.IO.MemoryStream outStream = new System.IO.MemoryStream();

            byte[] compressed = new byte[ms.Length];
            ms.Read(compressed, 0, compressed.Length);

            byte[] gzBuffer = new byte[compressed.Length + 4];
            System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length);
            System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4);
            return Convert.ToBase64String(gzBuffer);
        }

        public static string Decompress(this string compressedText)
        {
            byte[] gzBuffer = Convert.FromBase64String(compressedText);
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                int msgLength = BitConverter.ToInt32(gzBuffer, 0);
                ms.Write(gzBuffer, 4, gzBuffer.Length - 4);

                byte[] buffer = new byte[msgLength];

                ms.Position = 0;
                using (System.IO.Compression.DeflateStream zip = new System.IO.Compression.DeflateStream(ms, System.IO.Compression.CompressionMode.Decompress))
                {
                    zip.Read(buffer, 0, buffer.Length);
                }

                return Encoding.UTF8.GetString(buffer);
            }
        }

        public static string Encrypt(this string value, string key = null)
        {
            try
            {
                System.Text.Encoding encode = System.Text.Encoding.UTF8;
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                if (string.IsNullOrEmpty(key)) key = GetDefaultEncryptDecryptKey();
                string strTempKey = key;
                byteHash = objHashMD5.ComputeHash(encode.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = encode.GetBytes(value);
                return Convert.ToBase64String(objDESCrypto.CreateEncryptor().
                    TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string Decrypt(this string value, string key = null)
        {
            try
            {
                System.Text.Encoding encode = System.Text.Encoding.UTF8;
                TripleDESCryptoServiceProvider objDESCrypto =
                    new TripleDESCryptoServiceProvider();
                MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
                byte[] byteHash, byteBuff;
                if (string.IsNullOrEmpty(key)) key = GetDefaultEncryptDecryptKey();
                string strTempKey = key;
                byteHash = objHashMD5.ComputeHash(encode.GetBytes(strTempKey));
                objHashMD5 = null;
                objDESCrypto.Key = byteHash;
                objDESCrypto.Mode = CipherMode.ECB; //CBC, CFB
                byteBuff = Convert.FromBase64String(value);
                string strDecrypted = encode.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
                objDESCrypto = null;
                return strDecrypted;
            }
            catch (Exception ex)
            {
                return "Wrong Input. " + ex.Message;
            }
        }

        public static string ToHash(this string value, int number = 1)
        {
            if (number < 1) return value;
            System.Text.Encoding encode = System.Text.Encoding.UTF8;
            MD5CryptoServiceProvider objHashMD5 = new MD5CryptoServiceProvider();
            byte[] byteHash = objHashMD5.ComputeHash(encode.GetBytes(value));
            if (number > 1)
            {
                int n = 1;
                while (n < number)
                {
                    byteHash = objHashMD5.ComputeHash(byteHash);
                    n++;
                }
            }
            return Convert.ToBase64String(byteHash);

        }

        public static string ToUtf8(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return value;
            var buffer = System.Text.Encoding.UTF8.GetBytes(value);
            return System.Text.Encoding.UTF8.GetString(buffer);

        }

        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }

        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }
        


#endif

    }
}
