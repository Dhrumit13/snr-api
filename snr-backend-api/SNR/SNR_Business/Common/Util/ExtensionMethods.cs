using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SNR_Business.Common.Util
{
    public static class ExtensionMethods
    {
        public static bool ToBool(this string s)
        {
            bool r = false;
            s = s.Trim();
            if (s != string.Empty && s != "")
            {
                bool b;
                if (bool.TryParse(s, out b))
                    r = b;
                else if (s == "0" || s.ToLower() == "false")
                    r = false;
                else if (s == "1" || s.ToLower() == "true")
                    r = true;
            }
            return r;
        }

        public static byte ToByte(this string s)
        {
            byte i = 0;
            byte.TryParse(s, out i);
            return i;
        }

        public static Int16 ToInt16(this string s)
        {
            Int16 i = 0;
            Int16.TryParse(s, out i);
            return i;
        }
        public static Int32 ToInt32(this string s)
        {
            Int32 i = 0;
            Int32.TryParse(s, out i);
            return i;
        }

        public static UInt16 ToUInt16(this string s)
        {
            UInt16 i = 0;
            UInt16.TryParse(s, out i);
            return i;
        }

        public static int ToInt(this string s)
        {
            int i = 0;
            int.TryParse(s, out i);
            return i;
        }

        public static long ToLong(this string s)
        {
            long i = 0;
            long.TryParse(s, out i);
            return i;
        }

        public static decimal ToDecimal(this string s)
        {
            decimal i = 0;
            decimal.TryParse(s, out i);
            return i;
        }

        public static int ConvertDecimalToInt(this decimal d)
        {
            return Math.Floor(d).ToString().ToInt();
        }

        public static Single? ToNullableSingle(this string s)
        {
            Single? r = null;
            Single i;
            if (Single.TryParse(s, out i))
                r = i;
            return r;
        }

        public static UInt16 ConvertDecimalToUInt16(this decimal d)
        {
            return Math.Floor(d).ToString().ToUInt16();
        }

        public static int ConvertDoubleToInt(this double d)
        {
            return Math.Floor(d).ToString().ToInt();
        }

        public static byte? ToNullableByte(this string s)
        {
            byte? r = null;
            byte i;
            if (byte.TryParse(s, out i))
                r = i;
            return r;
        }

        public static bool? ToNullableBool(this string s)
        {
            bool? r = null;
            s = s.Trim();
            if (s != string.Empty && s != "")
            {
                bool b;
                if (bool.TryParse(s, out b))
                    r = b;
                else if (s == "0" || s.ToLower() == "false")
                    r = false;
                else if (s == "1" || s.ToLower() == "true")
                    r = true;
            }
            return r;
        }

        public static Int16? ToNullableInt16(this string s)
        {
            Int16? r = null;
            Int16 i;
            if (Int16.TryParse(s, out i))
                r = i;
            return r;
        }

        public static int? ToNullableInt(this string s)
        {
            int? r = null;
            int i;
            if (int.TryParse(s, out i))
                r = i;
            return r;
        }

        public static int ToNonNullableInt(this string s, int defaultIntValue = 0)
        {
            int r;
            if (int.TryParse(s, out r))
            {
                return r;
            }
            return defaultIntValue;
        }

        public static long? ToNullableLong(this string s)
        {
            long? r = null;
            long i;
            if (long.TryParse(s, out i))
                r = i;
            return r;
        }

        public static decimal? ToNullableDecimal(this string s)
        {
            decimal? r = null;
            decimal i;
            if (decimal.TryParse(s, out i))
                r = i;
            return r;
        }

        public static DateTime? ToNullableDateTime(this string s)
        {
            DateTime? r = null;
            DateTime i;
            if (DateTime.TryParse(s, out i))
                r = i;
            return r;
        }

        public static DateTime? ToNullableDateTimeParseExact(this string s)
        {
            DateTime? r = null;
            try
            {
                DateTime i = DateTime.ParseExact(s.Trim().Substring(0, 24), "ddd MMM d yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                r = i;
            }
            catch (Exception ex)
            {
                r = null;
                throw ex;
            }
            return r;
        }

        public static bool IsNullOrEmpty(this DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNull(this DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool IsNullOrEmpty(this DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool IsNullOrEmpty(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string ByteToBase64(this byte[] imageBytes)
        {
            string Base64string = "";
            if (imageBytes != null && imageBytes.Length > 0)
                Base64string = Convert.ToBase64String(imageBytes);

            return Base64string;
        }
        public static byte[] Base64ToByteArr(this string Base64string)
        {
            byte[] ByteArray = null;
            if (!Base64string.IsNullOrEmpty())
                ByteArray = Convert.FromBase64String(Base64string);

            return ByteArray;
        }
        public static string ToBase64(this string str)
        {
            string base64 = string.Empty;
            byte[] ByteArray = null;
            if (!str.IsNullOrEmpty())
            {
                ByteArray = Encoding.Unicode.GetBytes(str);
                base64 = Convert.ToBase64String(ByteArray);
            }
            return base64;
        }
        public static DateTime ObjToDateTime(this object s)
        {
            return Convert.ToDateTime(s);
        }
        public static DateTime? ObjToNullableDateTime(this object s)
        {
            if (string.IsNullOrEmpty(s.ToString()))
            {
                return null;
            }
            else
            {
                return Convert.ToDateTime(s);
            }
        }
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public static string GetUserName(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst(ClaimTypes.Name)?.Value;
        }

        public static string GetClientID(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("ClientId")?.Value;
        }
        public static string GetUserGroupIds(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("UserGroupIDs")?.Value;
        }
        public static string GetUserAgent(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            return principal.FindFirst("Agent")?.Value;
        }

        public static Int16? ObjToNullableInt16(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (Int16?)null : Convert.ToInt16(s);
        }
        public static Int32? ObjToNullableInt32(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (Int32?)null : Convert.ToInt32(s);
        }
        public static Int64? ObjToNullableLong(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (Int64?)null : Convert.ToInt64(s);
        }
        public static decimal? ObjToNullableDecimal(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (decimal?)null : Convert.ToDecimal(s);
        }
        public static float? ObjToNullableFloat(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (float?)null : Convert.ToSingle(s);
        }
        public static Double? ObjToNullableDouble(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (Double?)null : Convert.ToDouble(s);
        }
        public static bool? ObjToNullableBool(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (bool?)null : Convert.ToBoolean(s);
        }

        public static string ObjToString(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? "" : s.ToString();
        }

        public static byte? ObjToNullableByte(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (Byte?)null : Convert.ToByte(s);
        }

        public static byte ObjToByte(this object s)
        {
            return Convert.ToByte(s);
        }
        public static Int16 ObjToInt16(this object s)
        {
            return Convert.ToInt16(s);
        }
        public static Int32 ObjToInt32(this object s)
        {
            return Convert.ToInt32(s);
        }
        public static Int64 ObjToLong(this object s)
        {
            return Convert.ToInt64(s);
        }
        public static decimal ObjToDecimal(this object s)
        {
            return Convert.ToDecimal(s);
        }
        public static Double ObjToDouble(this object s)
        {
            return Convert.ToDouble(s);
        }
        public static bool ObjToBool(this object s)
        {
            return Convert.ToBoolean(s);
        }
      
        public static T GetValueFromDescription<T>(this string description)
        {
            description = description.ToLower();
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description.ToLower() == description.ToLower())
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name.ToLower() == description.ToLower())
                        return (T)field.GetValue(null);
                }
            }
            throw new ArgumentException("Not found.", "description");
        }

        public static int GetEnumFromDescription(string description, Type enumType)
        {
            foreach (var field in enumType.GetFields())
            {
                DescriptionAttribute attribute
                    = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute == null)
                    continue;
                if (attribute.Description == description)
                {
                    return (int)field.GetValue(null);
                }
            }
            return 0;
        }

        public static string BytesToString(this byte[] bytes)
        {
            using (var stream = new MemoryStream(bytes))
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        public static AuthenticationHeaderValue ToBasicAuthHeaderValue(this string username, string password)
        {
            return new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}")));
        }

        public static double CelsiusToFahrenheit(this double c)
        {
            return c * 9 / 5 + 32;
        }
        public static double FahrenheitToCelsius(this double f)
        {
            return (f - 32) * 5 / 9;
        }
        public static double? ConvertTemperatureUnit(this double? f)
        {
            return (f - 32) * 5 / 9;
        }

    }
}
