using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poehoe
{
    public interface ICrypto
    {
        byte[] Encrypt(byte[] Data, byte[] Key);
        byte[] Decrypt(byte[] Data, byte[] Key);
    }
}
