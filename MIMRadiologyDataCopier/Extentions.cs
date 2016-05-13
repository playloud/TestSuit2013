using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Windows.Forms;

namespace MIMRadiologyDataCopier
{
	public static class Extentions
	{
		public static decimal ToDecimal(this string str)
		{
			decimal value = 0;
			if (decimal.TryParse(str, out value))
				return value;
			return 0;
		}


		// PSH 05/13/15 : for screenshots
		//public static byte[] ToByteArray(this Image image, ImageFormat format)
		//{
		//	using (MemoryStream ms = new MemoryStream())
		//	{
		//		image.Save(ms, format);
		//		return ms.ToArray();
		//	}
		//}

		public static string RefineVisitLabel(this string visitLabel)
		{
			string number = visitLabel.Replace("Visit", "").Trim();
			if (number.Contains(".") && !number.StartsWith("0"))
				return string.Format("Visit 0{0}", number);
			return visitLabel;
		}

		public static bool IsValidNum(this string str)
		{
			if (str.IsNullOrEmptyOrWhiteSpace())
				return false;

			foreach (char c in str.Trim())
				if (c < 46 || c > 57)
					return false;
			decimal temp;
			return decimal.TryParse(str, out temp);
		}

		public static bool Available(this DataRow[] rows)
		{
			if (rows != null && rows.Length > 0)
				return true;
			return false;
		}


		public static void DumpPopup(this IEnumerable linqs)
		{
			StringBuilder sbuf = new StringBuilder();
			foreach (var linq in linqs)
			{
				sbuf.AppendLine(linq.ToString());
			}
			MessageBox.Show(sbuf.ToString());
		}


		public static bool IsNullOrEmptyOrWhiteSpace(this string str)
		{
			if (string.IsNullOrEmpty(str))
				return true;

			if (string.IsNullOrWhiteSpace(str))
				return true;

			return false;
		}

		public static bool IsNumberString(this string str)
		{

			foreach (char c in str.Trim())
				if (c < 46 || c > 57)
					return false;

			decimal temp;

			return decimal.TryParse(str, out temp);
		}

		public static bool IsZeroString(this string str)
		{
			decimal value;
			if (decimal.TryParse(str, out value))
				if (value == 0)
					return true;
			return false;
		}

		public static bool IsIntString(this string str)
		{
			int temp;
			if (int.TryParse(str, out temp))
				return true;
			return false;
		}

		public static bool IsHyphenString(this string str)
		{
			if (str.Trim().Length == 1 && str.Contains("-"))
				return true;
			else if (str.Trim().Length == str.Trim().Where(a => a == '-').Count())
				return true;
			else
				return false;
		}

		public static bool IsQuestionmarkString(this string str)
		{
			var query = str.Trim().Where(a => a != '?');
			if (query.Any())
				return false;
			return true;
		}

		public static bool IsIn(this string str, params string[] arrStr)
		{
			if (arrStr.Contains(str))
				return true;
			return false;
		}

		public static bool IsNotIn(this string str, params string[] arrStr)
		{
			if (arrStr.Contains(str))
				return false;
			return true;
		}

		public static string F(this string s, params object[] args)
		{
			return string.Format(s, args);
		}

		// // PSH 06/23/15 : project specific,
		public static bool IsInvolved(this string s)
		{
			if (s.IsNullOrEmptyOrWhiteSpace())
				return false;
			if (s == "NI")
				return false;
			return true;
		}

		// PSH 06/23/15 : Unified Response
		public static string UR(this string response)
		{
			if (response.Length == 2)
				return response;

			if (response.Contains("(") && response.Contains(")"))
			{
				int i = response.IndexOf("(");
				int j = response.IndexOf(")");
				return response.Substring(i + 1, j - i - 1);
			}

			if (response.ToLower().Contains("progressive disease"))
				return "PD";

			return response;
		}

		public static string XMLUR(this string numberResponse)
		{
			switch (numberResponse)
			{
				case "100" :
					return "CR";
				case "50":
					return "PR";
				case "0":
					return "SD";
				case "-1":
					return "PD";
				case "-3" :
					return "NE";
				case "-4":
					return "NI";
				default:
					return null;
			}
			return null;
		}

		public static void Visualize(this DataTable dt, string title)
		{
			Form form = new Form();
			form.Text = title;

			form.Size = new Size(800, 600);

			DataGridView dgv = new DataGridView();
			dgv.DataSource = dt;
			dgv.Dock = DockStyle.Fill;
			
			form.Controls.Add(dgv);
			form.ShowDialog();
		}

		public static string GetValueByRowCondtion(this DataTable dt, string rowCondition, string fieldName)
		{
			DataRow[] rows = dt.Select(rowCondition);
			foreach (DataRow row in rows)
				return row[fieldName].ToString();
			return null;
		}

		public static string GetRowConditionByOtherField(this DataTable dt, string otherCondition)
		{
			DataRow[] rows = dt.Select(otherCondition);
			if (rows.Length > 0)
				return "TRow={0}".F(rows[0]["TRow"].ToString());
			return null;
		}

		public static string ToDateFormat(this string str)
		{
			if (str == null)
				return null;
			
			DateTime dtTemp = DateTime.Now;

			if (DateTime.TryParse(str, out dtTemp))
				return dtTemp.ToShortDateString();
			
			// to support 20140826
			CultureInfo enUS = new CultureInfo("en-US");
			if (DateTime.TryParseExact(str, "yyyyMMdd", enUS, DateTimeStyles.None, out dtTemp))
				return dtTemp.ToShortDateString();

			return null;
		}

		public static DateTime ToDateTime(this string str)
		{
			if (str == null)
				return DateTime.MinValue;
			DateTime dtTemp = DateTime.MinValue;
			if (DateTime.TryParse(str, out dtTemp))
				return dtTemp;
			return dtTemp;
		}

		public static DateTime ToDateTime(this string str, string format)
		{
			if (str == null)
				return DateTime.MinValue;
			DateTime dtTemp = DateTime.MinValue;
			if (DateTime.TryParse(str, out dtTemp))
				return dtTemp;

			CultureInfo enUS = new CultureInfo("en-US");
			if (DateTime.TryParseExact(str, format, enUS, DateTimeStyles.None, out dtTemp))
				return dtTemp;

			return dtTemp;
		}

		public static DateTime ToDaysAfterDate(this string str, int days)
		{
			return str.ToDateTime().AddDays(days);
		}

		// read visit label => gets the number
		public static int? TIndex(this string str)
		{
			string value = str.ToLower().Replace("visit", "");
			int iIndex;
			if (int.TryParse(value, out iIndex))
				return iIndex;
			return null;
		}

	}
	

}
