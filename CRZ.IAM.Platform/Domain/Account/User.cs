using System;
using System.Security.Cryptography;
using System.Text;
using CRZ.Framework.Domain;

namespace CRZ.IAM.Platform.Domain.Account
{
    public class User : IEntity, IAggregation
    {
        public int Id { get; private set; }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }

        public DateTimeOffset? LastLogin { get; private set; }

        public bool Active { get; private set; }

        public string Token { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }

        public DateTimeOffset? UpdatedAt { get; private set; }

        protected User()
        { }

        public User(string name, string email, string username, string password)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Email = email ?? throw new ArgumentNullException(nameof(email));
            Username = string.IsNullOrWhiteSpace(username) ? email : username;
            Password = CreatePasswordHash(password);
            Active = true;
        }

        public User(string name, string email, string password)
            : this(name, email, null, password)
        { }

        public void Login(string token)
        {
            Token = token;
            LastLogin = DateTimeOffset.Now;
            UpdatedAt = DateTimeOffset.Now;
        }

        public void Logout()
        {
            Token = null;
            UpdatedAt = DateTimeOffset.Now;
        }

        public bool ValidatePassword(string password)
        {
            var passwordHash = CreatePasswordHash(password);

            return passwordHash == Password;
        }

        protected string CreatePasswordHash(string password)
        {
            SHA256 sHA256 = SHA256.Create();
            byte[] input = Encoding.ASCII.GetBytes(
                $"{Email}-{password}");
            byte[] output = sHA256.ComputeHash(input);
            string base64 = Convert.ToBase64String(output);

            return base64;
        }
    }
}
