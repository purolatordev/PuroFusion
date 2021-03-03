using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Configuration;

/// <summary>
/// Summary description for ClsEncryptDecrypt
/// </summary>
public class ClsEncryptDecrypt
{



    // public string StringValue { get; set; }
    protected static string _Key = "";

    public ClsEncryptDecrypt()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string EncryptString(string inputString)
    {
        MemoryStream memStream = null;
        try
        {
            byte[] key = { };
            byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
            //string encryptKey = "aXb2uy4z"; // MUST be 8 characters
            string encryptKey = EncryptionKey;
            key = Encoding.UTF8.GetBytes(encryptKey);
            byte[] byteInput = Encoding.UTF8.GetBytes(inputString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            memStream = new MemoryStream();
            ICryptoTransform transform = provider.CreateEncryptor(key, IV);
            CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(byteInput, 0, byteInput.Length);
            cryptoStream.FlushFinalBlock();

        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }
        return Convert.ToBase64String(memStream.ToArray());
    }

    public string DecryptString(string inputString)
    {
        MemoryStream memStream = null;
        try
        {
            byte[] key = { };
            byte[] IV = { 12, 21, 43, 17, 57, 35, 67, 27 };
            //string encryptKey = "aXb2uy4z"; // MUST be 8 characters
            //string encryptKey = ConfigurationManager.AppSettings["AESKey"].ToString();
            string encryptKey = EncryptionKey;
            key = Encoding.UTF8.GetBytes(encryptKey);
            byte[] byteInput = new byte[inputString.Length];
            byteInput = Convert.FromBase64String(inputString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            memStream = new MemoryStream();
            ICryptoTransform transform = provider.CreateDecryptor(key, IV);
            CryptoStream cryptoStream = new CryptoStream(memStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(byteInput, 0, byteInput.Length);
            cryptoStream.FlushFinalBlock();
        }
        catch (Exception ex)
        {
            //Response.Write(ex.Message);
        }

        Encoding encoding1 = Encoding.UTF8;
        return encoding1.GetString(memStream.ToArray());
    }

    protected static string EncryptionKey
    {
        get
        {
            if (String.IsNullOrEmpty(_Key))
            {
                _Key = ConfigurationManager.AppSettings["AESKey"].ToString();
            }

            return _Key;
        }
    }
}