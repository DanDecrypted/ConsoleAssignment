using System;

namespace ConsoleAssignment.Core
{
    public class Account
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public Account()
        { }

        public Account(string email, string username)
        {
            Username = username;
            ChangeEmail(email);
        }

        public void ChangeEmail(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentNullException(nameof(value));
            }
            else if (!Utils.IsValidEmail(value))
            {
                throw new ArgumentException("Email is invalid");
            }
            Email = value;
        }
    }
}
