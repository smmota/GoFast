using System.Security.Cryptography;

namespace GoFast.API.Services
{
    public class Hash
    {
        private HashAlgorithm _algorithm;

        public Hash(HashAlgorithm algorithm)
        {
            _algorithm = algorithm;
        }

        public string CriptografarSenha(string senha) 
        {
            return string.Empty;
        }
    }
}
