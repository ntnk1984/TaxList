using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using IT4U;

/// <summary>
/// Summary description for SQLConvert
/// </summary>
public class SQLConvert
{
    public static string SQLType(object o)
    {
        string s = "";

        if (o == null) return "null";

        switch (o.GetType().ToString())
        {
            case "System.String":
                s = "N'" + iString.Format_Input(o.ToString()) + "'";
                break;
            case "System.Int32":
                s = "" + int.Parse(string.Format("{0}", o)).ToString() + "";
                break;
            case "System.Double":
                s = "" + Double.Parse(string.Format("{0}", o)).ToString() + "";
                break;
            case "System.Decimal":
                s = "" + Decimal.Parse(string.Format("{0}", o)).ToString() + "";
                break;
            case "System.Single":
                s = "" + float.Parse(string.Format("{0}", o)).ToString() + "";
                break;
            case "System.DateTime":
                s = sqlConvertDate(o);
                break;
            default:
                break;
        }

        return s;
    }

    public static string sqlConvertDate(object o)
    {
        DateTime d = (DateTime)o;

        if (d == null) return "null";

        if (d == new DateTime(1900, 1, 1)) return "null";

        return "N'" + (d.Year.ToString("####") + "0000").Substring(0, 4) + ("00" + d.Month.ToString("##")).Substring(d.Month.ToString("##").Length, 2) + ("00" + d.Day.ToString("##")).Substring(d.Day.ToString("##").Length, 2) + "'";
    }

    public static string ToStringDate(DateTime d)
    {
        return "N'" + ("00" + d.Day.ToString("##")).Substring(d.Day.ToString("##").Length, 2) + "/" + ("00" + d.Month.ToString("##")).Substring(d.Month.ToString("##").Length, 2) + "/" + (d.Year.ToString("####") + "0000").Substring(0, 4) + "'";
    }

   

    //public static string Encrypt(string clearText, string Password, string sKey)
    //{
    //    var textEncoder = new UTF8Encoding();
    //    byte[] bKey = textEncoder.GetBytes(sKey);

    //    byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(clearText);
    //    PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, bKey);
    //    byte[] encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
    //    return Convert.ToBase64String(encryptedData);
    //}

   

    //public static string Decrypt(string cipherText, string Password, string sKey)
    //{
    //    var textEncoder = new UTF8Encoding();
    //    byte[] bKey = textEncoder.GetBytes(sKey);

    //    byte[] cipherBytes = Convert.FromBase64String(cipherText);
    //    PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, bKey);
    //    byte[] decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
    //    return System.Text.Encoding.Unicode.GetString(decryptedData);
    //}


}