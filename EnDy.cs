using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace psw
{
    public static class EnDy
    {
        public static string Decrypt(string cipherString, bool useHashing , string key)
        {
            byte[] keyArray;

            //get the byte code of the string
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            ////System.Configuration.AppSettingsReader settingsReader =
            ////                                    new AppSettingsReader();
            //////Get your key from config file to open the lock!
            ////string key = (string)settingsReader.GetValue("SecurityKey",
            ////                                             typeof(String));

            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

                //release any resource held by the MD5CryptoServiceProvider
                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 

            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }

        public static string Encrypt(string toEncrypt, bool useHashing , string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            ////System.Configuration.AppSettingsReader settingsReader =
            ////                                    new AppSettingsReader();
            ////// Get the key from config file
            ////string key = (string)settingsReader.GetValue("SecurityKey",
            ////                                                 typeof(String));

            
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key

            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
            keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
    
        public static void WriteFile(string host ,string username, string pass , string fname)
        {
            host = EnDy.Encrypt(host, false, "012345678901234567890123");
            username = EnDy.Encrypt(username, false, "012345678901234567890123");
            pass = EnDy.Encrypt(pass, false, "012345678901234567890123");
            host = EnDy.Encrypt(host + "||" + username + "||" + pass, false, "012345678901234567890123");

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(fname, true))
            {
                file.Write(string.Format("{0}{1}", host,"\n"));
            }
        }

    }
}
