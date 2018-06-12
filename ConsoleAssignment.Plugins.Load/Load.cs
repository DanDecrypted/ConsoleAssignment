using System;
using System.Collections.Generic;
using System.IO;
using ConsoleAssignment.Core;

namespace ConsoleAssignment.Plugins
{
    public class Load : ICommandPlugin, IDescribable
    {
        public Load()
        {

        }

        public bool CanHandle(string command)
        {
            return (command == "load");
        }

        public string GetDescription()
        {
            return "Usage: Load [Optional:FilePath]\n\rLoads the list of users from the specified file.\n\rIf no file path is given, " +
                   "it will load from the directory of the executable/accounts.txt. \n\rFile must exist.";
        }

        public string GetName()
        {
            return "Plugins.Load.dll";
        }

        public string[] Handle(string[] args)
        {
            List<string> result = new List<string>();
            string path = Environment.CurrentDirectory + "/accounts.json";

            if (args.Length != 0)
            {
                path = Environment.CurrentDirectory + "/" + args[0];
            }

            if (File.Exists(path))
            {
                List<Account> accounts = Serialiser.Deserialise<List<Account>>(path);
                List<AddAccountResult> results = AccountsRepository.AddAccounts(accounts);
                ProcessResults(result, results);
            }
            else
            {
                result.Add(String.Format("File \"{0}\" could not be found.", path));
            }
            return result.ToArray();
        }

        private static void ProcessResults(List<string> result, List<AddAccountResult> results)
        {
            int loaded = 0, invalidEmail = 0, exists = 0;
            foreach (AddAccountResult accountResult in results)
            {
                switch (accountResult)
                {
                    case AddAccountResult.Exists: exists++; break;
                    case AddAccountResult.InvalidEmail: invalidEmail++; break;
                    case AddAccountResult.Successful: loaded++; break;
                }
            }

            result.Add(String.Format("{0} accounts attempted to load", results.Count));

            if (exists > 0)
            {
                result.Add(String.Format("{0} already existed in the system", exists));
            }

            if (invalidEmail > 0)
            {
                result.Add(String.Format("{0} had invalid emails", invalidEmail));
            }

            if (loaded > 0)
            {
                result.Add(String.Format("{0} loaded successfully", loaded));
            }
        }
    }
}
