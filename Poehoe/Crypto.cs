using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    public class Crypto
    {
        public static byte[] StringToByteArrayFastest(string hex)
        {
            if (hex.Length % 2 == 1)
                throw new Exception("The binary key cannot have an odd number of digits");

            byte[] arr = new byte[hex.Length >> 1];

            for (int i = 0; i < hex.Length >> 1; ++i)
            {
                arr[i] = (byte)((GetHexVal(hex[i << 1]) << 4) + (GetHexVal(hex[(i << 1) + 1])));
            }

            return arr;
        }
        public static int GetHexVal(char hex)
        {
            int val = hex;
            return val - (val < 58 ? 48 : 55);
        }

        public static ICrypto ZipCrypto
        {
            get;
            set;
        }

        public static string ZipKey
        {
            get;
            set;
        }

        public static string AesKey
        {
            get;
            set;
        }

        public static ICrypto AesCrypto
        {
            get;
            set;
        }

        public static byte[] EncryptRequest(Request Req, string Key = null)
        {
            Key = Key ?? ZipKey;

            byte[] ReqData = Encoding.Unicode.GetBytes(Req.Payload);
            byte[] KeyData = Encoding.UTF8.GetBytes(Key);

            return ZipCrypto.Encrypt(ReqData, KeyData);
        }

        public static string DecryptResponse(string Data, string Key = null)
        {
            return DecryptResponse(StringToByteArrayFastest(Data), Key ?? ZipKey);
        }

        public static string DecryptResponse(byte[] Data, string Key = null)
        {
            var b = ZipCrypto.Decrypt(Data, Encoding.UTF8.GetBytes(Key ?? ZipKey));
            return Encoding.Unicode.GetString(b, 0, b.Length);
        }

        public static string CryptPass(string User)
        {
            return Convert.ToBase64String(AesCrypto.Encrypt(Encoding.Unicode.GetBytes(User), StringToByteArrayFastest(AesKey)));
        }
    }
}
