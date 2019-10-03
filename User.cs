using System;
using System.Security.Cryptography;

namespace Commands.NET
{
    /// <summary>
    /// Represents the base user of an application, with password encrypting methods.
    /// </summary>
    public abstract class User
    {
        private const int ITERATIONS = 2000;
        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// The password of the user.
        /// </summary>
        public string Password { get; protected set; }

        /// <summary>
        /// Constructs or loads a user of your application.
        /// </summary>
        /// <param name="name">The name of the user.</param>
        /// <param name="pass">The password of the user.</param>
        /// <param name="load"><c>true</c> if loading the profile, <c>false</c> if new user.</param>
        protected User(string name, string pass, bool load)
        {
            Name = name;
            Password = load ? pass : EncodeMdp(pass);
        }

        /// <summary>
        /// Checks if the given password corresponds to the saved password of the user.
        /// </summary>
        /// <param name="pass">The password to check.</param>
        /// <returns><c>true</c> if the passwords are equal, otherwise <c>false</c>.</returns>
        public bool CheckMdp(string pass)
        {
            byte[] hashBytes = Convert.FromBase64String(Password);
            byte[] salt = new byte[16];
            Array.ConstrainedCopy(hashBytes, 0, salt, 0, 16);
            using Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(pass, salt, ITERATIONS);
            byte[] hash = rfc2898DeriveBytes.GetBytes(16);
            for (int i = 0; i < 16; i++)
                if (hashBytes[i + 16] != hash[i])
                    return false;
            return true;
        }

        private string EncodeMdp(string pass)
        {
            byte[] salt = new byte[16];
            using RNGCryptoServiceProvider rNGCryptoServiceProvider = new RNGCryptoServiceProvider();
            rNGCryptoServiceProvider.GetBytes(salt);
            byte[] hash = new Rfc2898DeriveBytes(pass, salt, ITERATIONS).GetBytes(16);
            byte[] tmp = new byte[32];
            Array.ConstrainedCopy(salt, 0, tmp, 0, 16);
            Array.ConstrainedCopy(hash, 0, tmp, 16, 16);
            return Convert.ToBase64String(tmp);
        }
    }
}
