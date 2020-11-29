using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using ModelLayer.Model;
using ModelLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace BusinessLayer.Business
{
    
        public sealed class PasswordHasher : IPasswordHasher<Users>
        {
            private const int SaltSize = 16; // 128 bit 
            private const int KeySize = 32; // 256 bit

            public PasswordHasher(IOptions<HashingOptions> options) //IOptions<HashingOptions> options
        {
          //  HashingOptions options = new HashingOptions() { Iterations = 10000 };
                Options = options.Value;
            }

            private HashingOptions Options { get; }

            

        public string HashPassword(Users user, string password)
        {
            using (var algorithm = new Rfc2898DeriveBytes(
               password,
               SaltSize,
               Options.Iterations))
            {
                var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                var salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Options.Iterations}.{salt}.{key}";
            }
        }

        public PasswordVerificationResult VerifyHashedPassword(Users user, string hashedPassword, string providedPassword)
        {
            char split = (char)3;
            var parts = hashedPassword.Split('.', split);

            if (parts.Length != 3)
            {
               return PasswordVerificationResult.Failed;
                //throw new FormatException("Unexpected hash format. " +
                //  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != Options.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              providedPassword,
              salt,
              iterations
              ))
            {
                var keyToCheck = algorithm.GetBytes(KeySize);

                var verified = keyToCheck.SequenceEqual(key);

                if (verified == true) { return PasswordVerificationResult.Success; }
               else { return PasswordVerificationResult.Failed; }
                // return (verified, needsUpgrade);
            }
        }
    }
    

  
}
