using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Money.Importers.Model
{
    static class ModelHelper
    {
        private readonly static SHA256Managed sha = new SHA256Managed();

        internal static string GetSignature(string raw)
        {
            var rawData = Encoding.UTF8.GetBytes(raw);
            var signature = sha.ComputeHash(rawData);
            var signatureString = Convert.ToBase64String(signature);
            return signatureString;
        }

        internal static string EmptyToNull(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return null;
            }

            return s;
        }
    }
}
