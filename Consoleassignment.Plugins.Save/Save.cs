using System;
using System.Collections.Generic;
using ConsoleAssignment.Core;

namespace ConsoleAssignment.Plugins
{
    public class Save : ICommandPlugin, IDescribable
    {
        public Save()
        { }
        public bool CanHandle(string command)
        {
            return (command.ToLower() == "save");
        }
        public string GetDescription()
        {
            return "Usage: Save [Optional:FilePath]\n\rSaves the current list of users to the specified file. \n\rIf no file path is given " +
                   "it will save in the directory of the executable/accounts.txt.\n\rIf the file specified or the default file exist, " +
                   "they will be overwritten.";
        }

        public string GetName()
        {
            return "Plugins.Save.dll";
        }

        public string[] Handle(string[] args)
        {
            string path = Environment.CurrentDirectory + "/accounts.json";
            if (args.Length != 0)
            {
                if (args[0] == "?" || args[0].ToLower() == "help")
                {
                    return new string[]
                    {
                        GetName() + "\n\r Usage: " + GetDescription()
                    };
                }
                else
                {
                    path = Environment.CurrentDirectory + "/" + args[0];
                }
            }

            List<string> result = new List<string>(Serialiser.Serialise(AccountsRepository.Accounts, path))
            {
                String.Format("{0} accounts were saved to {1}", AccountsRepository.Accounts.Count, path)
            };
            return result.ToArray();
        }
    }
}
