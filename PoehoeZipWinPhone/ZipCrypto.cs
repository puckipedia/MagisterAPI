using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poehoe;
using System.IO;
using Ionic.Zip;

namespace PoehoeZipWin32
{
    public class ZipCrypto : ICrypto
    {
        public byte[] Encrypt(byte[] Data, byte[] Key)
        {
            MemoryStream ZipStream = new MemoryStream(20);
            using (ZipOutputStream ZipOutput = new ZipOutputStream(ZipStream))
            {
                ZipOutput.Encryption = EncryptionAlgorithm.PkzipWeak;
                ZipOutput.Password = Encoding.UTF8.GetString(Key, 0, Key.Length);
                ZipOutput.PutNextEntry("content");
                ZipOutput.Write(Data, 0, Data.Length);
            }
            string EnvelopedResult = String.Format(
                @"<s:Envelope xmlns:s=""http://schemas.xmlsoap.org/soap/envelope/""><s:Body><data>{0}</data></s:Body></s:Envelope>",
                 BitConverter.ToString(ZipStream.ToArray()).Replace("-", ""));
            byte[] payload = Encoding.UTF8.GetBytes(EnvelopedResult);
            return payload;
        }

        public static byte[] ToByteArray(string HexString)
        {
            try
            {
                int NumberChars = HexString.Length;
                byte[] bytes = new byte[NumberChars / 2];
                for (int i = 0; i < NumberChars; i += 2)
                {
                    bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
                }
                return bytes;
            }
            catch (Exception E)
            {
                return null;
            }
        }

        public byte[] Decrypt(byte[] Data, byte[] Key)
        {
            string StringContent = Encoding.UTF8.GetString(Data, 0, Data.Length);
            var ByteArray = ToByteArray(StringContent.Replace("<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body><data>", "").Replace("</data></s:Body></s:Envelope>", ""));

            MemoryStream s = new MemoryStream(ByteArray);
            using (ZipInputStream InputStream = new ZipInputStream(s))
            {
                InputStream.Password = Encoding.UTF8.GetString(Key, 0, Key.Length);
                InputStream.GetNextEntry();

                return Encoding.Unicode.GetBytes(new StreamReader(InputStream, Encoding.Unicode).ReadToEnd());
            }
        }
    }
}
