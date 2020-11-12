using System;
using System.Globalization;

namespace Crowfounding.Data
{
    public class UiUtil
    {
        public const string defaultFormatDecimal = "0.00";
        public static readonly IFormatProvider cultureUS = new CultureInfo("en-US");

        public static string FormatDecimal(decimal value, string format = defaultFormatDecimal)
        {
            return value.ToString(format, cultureUS);
        }
    }
}
