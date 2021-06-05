using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace Assignment4
{
    public enum CryptoAlgorithm { AES,RSA};
    public class Crypto
    {
        AesCryptoServiceProvider aes;
        RSACryptoServiceProvider rsa;

        CryptoAlgorithm mType;

        public string publicKeypathRSA;
        public string privateKeypathRSA;
        
        public void Initialize(CryptoAlgorithm type)
        {
            mType = type;
            if(type == CryptoAlgorithm.AES)
            {
                aes = new AesCryptoServiceProvider();
            }
            else if(type == CryptoAlgorithm.RSA)
            {
                rsa = new RSACryptoServiceProvider();
            }
        }

        public void Terminate()
        {
            if(aes != null)
            {
                aes.Dispose();
            }

            //Do same for rsa
            if (rsa != null)
            {
                rsa.Dispose();
            }
        }

        public byte[] Encrypt(byte[] data)
        {
            if(mType == CryptoAlgorithm.AES)
            {
                if(data == null || data.Length == 0)
                {
                    return null;
                }

                using(var ms = new MemoryStream())
                {
                    using(var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                //Here you will have to do for rsa
                byte[] cipherbytes;
                using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
                {
                    rsa.PersistKeyInCsp = false;
                    string publicKey = File.ReadAllText(publicKeypathRSA);
                    rsa.FromXmlString(publicKey);
                    cipherbytes = rsa.Encrypt(data, false);
                }
                return cipherbytes;
            }
        }

        public void ExportPrivateKey(string privateKeyPath)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.PersistKeyInCsp = false;
                string privateKey = rsa.ToXmlString(true);
                using(var w = new StreamWriter(privateKeyPath)) { w.WriteLine(privateKey);}
            }
        }



        public void ExportPublicKey(string publicKeyPath)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
            {
                rsa.PersistKeyInCsp = false;
                string publicKey = rsa.ToXmlString(false);
                using (var w = new StreamWriter(publicKeyPath)) { w.WriteLine(publicKey); }
            }
        }

        public void ExportSharedKey(string path)
        {
            File.WriteAllBytes(path, aes.Key);
        }

        public void ExportIV(string path) 
        {
            File.WriteAllBytes(path, aes.IV);
        }

        public void ImportPrivateKey(string path)
        {
            if(mType == CryptoAlgorithm.RSA)
            {
                //using(StreamReader sr = new StreamReader(path)) 
                //{
                //    string xmlString = sr.ReadToEnd();//Get the string from file
                //    rsa.FromXmlString(xmlString); //set the new keys using the string
                //}
                string xmlString = File.ReadAllText(path);
                rsa.FromXmlString(xmlString);
            }
            else
            {
                byte[] Sharedkey = File.ReadAllBytes(path);
                aes.Key = Sharedkey;//setting new keys using retrieved bytes.
            }
        }

        public void ImportPublicKey(string path)
        {
            if(mType == CryptoAlgorithm.RSA)
            {
                string xmlString = File.ReadAllText(path);
                rsa.FromXmlString(xmlString);
            }
            else
            {
                byte[] IV = File.ReadAllBytes(path);
                aes.Key = IV;
            }
        }

        public byte[] Decrypt(byte[] data)
        {
            if (mType == CryptoAlgorithm.AES)
            {
                if (data == null || data.Length == 0)
                {
                    return null;
                }

                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(data, 0, data.Length);
                        cs.FlushFinalBlock();
                    }
                    return ms.ToArray();
                }
            }
            else
            {
                //Here you will have to do for rsa
                byte[] plain;
                using(RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(1024))
                {
                    rsa.PersistKeyInCsp = false;
                    byte[] cipher = File.ReadAllBytes(privateKeypathRSA);
                    rsa.FromXmlString(Encoding.UTF8.GetString(cipher));
                    plain = rsa.Decrypt(data, false);
                }
                return plain;
            }
        }
    }
}
