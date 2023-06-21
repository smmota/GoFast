using GoFast.API.Interfaces.Services;
using System.Security.Cryptography;
using System.Text;

namespace GoFast.API.Services
{
    public class HashService : IHashService
    {
        private HashAlgorithm _algorithm;

        public HashService()
        {
            _algorithm = SHA512.Create(); 
        }

        public string CriptografarSenha(string senha) 
        {
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();

            foreach (var c in encryptedPassword) 
            {
                sb.Append(c.ToString("X2"));
            }

            return sb.ToString();
        }
    }
}
