using SNR_Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SNR_Data.Util
{
    public static class CustomTableTypes
    {
        public static DataTable ToBookingChargestbl(this List<BookingChargestbl> charges)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("OtherChargeId", typeof(Int32));
            dt.Columns.Add("Value", typeof(decimal));
            if (charges != null && charges.Count > 0)
            {
                foreach (var item in charges)
                {
                    DataRow row = dt.NewRow();
                    row["OtherChargeId"] = item.otherChargeId;
                    row["Value"] = item.value;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        public static DataTable TotbListTypeBigInt(this Int64?[] LongArray)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Int64));
            if (LongArray != null && LongArray.Length > 0)
            {
                foreach (var item in LongArray)
                {
                    DataRow row = dt.NewRow();
                    row[0] = item;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
     
        public static DataTable TotbListTypeInt(this Int32?[] IntArray)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(Int32));
            if (IntArray != null && IntArray.Length > 0)
            {
                foreach (var item in IntArray)
                {
                    if (item != null)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = item;
                        dt.Rows.Add(row);
                    }
                }
            }
            return dt;
        }

        public static DataTable TotbListTypeString(this string[] strArray)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("strid", typeof(string));
            if (strArray != null && strArray.Length > 0)
            {
                foreach (var item in strArray)
                {
                    if (item != null)
                    {
                        DataRow row = dt.NewRow();
                        row[0] = item;
                        dt.Rows.Add(row);
                    }
                }
            }
            return dt;
        }

        public static long?[] ToNullableIntArray(this Int32[] IntArray)
        {
            long?[] longArray = Array.ConvertAll<int, long?>(IntArray,
                delegate (int i)
                {
                    return (long?)i;
                });
            return longArray;
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
        public static object ToDBNull(this object s)
        {
            if (s == null)
            {
                return DBNull.Value;
            }
            else
            { return s; }
        }

        public static int? ObjToNullableInt(this object s)
        {
            return string.IsNullOrEmpty(s.ToString()) ? (int?)null : Convert.ToInt32(s);
        }
        public static bool ToBoolean(this string s)
        {
            bool i = false;
            bool.TryParse(s, out i);
            return i;
        }
        public static DataTable ToDataTable<T>(this IEnumerable<T> dataList) where T : class
        {
            DataTable convertedTable = new DataTable();
            PropertyInfo[] propertyInfo = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in propertyInfo)
            {
                convertedTable.Columns.Add(prop.Name);
            }
            foreach (T item in dataList)
            {
                var row = convertedTable.NewRow();
                //var values = new object[propertyInfo.Length];
                for (int i = 0; i < propertyInfo.Length; i++)
                {
                    //var test = propertyInfo[i].GetValue(item, null);
                    row[i] = propertyInfo[i].GetValue(item, null);
                }
                convertedTable.Rows.Add(row);
            }
            return convertedTable;
        }
    }

}
