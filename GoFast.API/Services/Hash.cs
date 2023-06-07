using System.Security.Cryptography;
using System.Text;

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
            var encodedValue = Encoding.UTF8.GetBytes(senha);
            var encryptedPassword = _algorithm.ComputeHash(encodedValue);

            var sb = new StringBuilder();

            foreach (var c in encryptedPassword) 
            {
                sb.Append(c.ToString("X2"));
            }

            return sb.ToString();
        }

        //public bool VerificarSenha(string senhaDigitada, string senhaCadastrada)
        //{
        //    if (string.IsNullOrEmpty(senhaCadastrada))
        //        throw new NullReferenceException("Cadastre uma senha.");

        //    var encryptedPassword = _algorithm.ComputeHash(Encoding.UTF8.GetBytes(senhaDigitada));

        //    var sb = new StringBuilder();

        //    foreach (var caractere in encryptedPassword)
        //    {
        //        sb.Append(caractere.ToString("X2"));
        //    }

        //    return sb.ToString() == senhaCadastrada;
        //}
    }
}
