using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DVLD_Buisness
{
    public class clshashing
    {
      public static string  ComputeHash(string Input)
        {
            using(SHA256 sHA256 = SHA256.Create())
            {
                byte[] hashBates = sHA256.ComputeHash(Encoding.UTF8.GetBytes(Input));

                return BitConverter.ToString(hashBates).Replace("-","").ToLower();
            }
        }
    }
}
