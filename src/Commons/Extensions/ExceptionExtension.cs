using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Extensions
{
    public static class ExceptionExtension
    {

        public static string ToCompleteString(this System.Exception ex)
        {
            if (ex == null) return string.Empty;
            StringBuilder sb = new StringBuilder();
            System.Exception ex1 = ex;
            while (ex1 != null)
            {
                sb.Append("Message: ").Append(ex1.Message).AppendLine();
#if !SILVERLIGHT
                sb.Append("Source: ").Append(ex1.Source).AppendLine();
#endif
                sb.Append("StackTrace: ").Append(ex1.StackTrace).AppendLine();
                sb.Append("----------------------------------------------------").AppendLine();
                ex1 = ex1.InnerException;

            }
            return sb.ToString();
        }


    }
}
