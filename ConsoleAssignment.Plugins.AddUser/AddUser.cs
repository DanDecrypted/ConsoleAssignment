using System;
using System.Collections.Generic;
using ConsoleAssignment.Core;

namespace ConsoleAssignment.Plugins
{
    public class AddUser : ICommandPlugin, IDescribable
    {
        public AddUser()
        {

        }
        public bool CanHandle(string command)
        {
            return (command.ToLower() == "add");
        }

        public string GetDescription()
        {
            return "Usage: Add [Username] [Email]\n\radds a user to the system. Must be unique username and valid email.";
        }

        public string GetName()
        {
            return "Plugins.AddUser.dll";
        }

        public string[] Handle(string[] args)
        {
            if (args.Length < 2)
            {
                if (args.Length == 1)
                {
                    if (args[0] == "?" || args[0].ToLower() == "help")
                    {
                        return new string[]
                        {
                            GetName() + "\n\rUsage: " + GetDescription()
                        };
                    }
                }

                return new string[] 
                {
                    "Please provide both a username and an email address."
                };
            }

            string username = args[0];
            string email = args[1];

            AddAccountResult result = AccountsRepository.AddAccount(email, username);
            List<string> returns = new List<string>();
            if (result == AddAccountResult.Exists || result == AddAccountResult.InvalidEmailAndUserExists)
            {
                returns.Add(String.Format("Account name \"{0}\" already exists.", username));
            }

            if (result == AddAccountResult.InvalidEmail || result == AddAccountResult.InvalidEmailAndUserExists)
            {
                returns.Add(String.Format("Email \"{0}\" is invalid.", email));
            }

            if (result == AddAccountResult.Successful)
            {
                returns.Add(String.Format("Account has been created for user: {0} email: {1}.", username, email));
            }

            return returns.ToArray();
        }
    }
}
