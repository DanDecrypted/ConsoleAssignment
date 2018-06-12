using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAssignment.Core
{
    public class AccountsRepository
    {
        public static List<Account> Accounts { get; set; } = new List<Account>();

        public static AddAccountResult AddAccount(string email, string username)
        {
            AddAccountResult returns = AddAccountResult.Undefined;

            if (AccountExists(username))
            {
                returns = AddAccountResult.Exists;
            }

            if (!Utils.IsValidEmail(email))
            {
                if (returns == AddAccountResult.Exists)
                {
                    returns = AddAccountResult.InvalidEmailAndUserExists;
                }
                else
                {
                    returns = AddAccountResult.InvalidEmail;
                }
            }

            if (!AccountExists(username) && Utils.IsValidEmail(email))
            {
                Accounts.Add(new Account(email, username));
                returns = AddAccountResult.Successful;
            }

            return returns;
        }

        public static AddAccountResult AddAccount(Account account)
        {
            return AddAccount(account.Email, account.Username);
        }

        public static List<AddAccountResult> AddAccounts(List<Account> accounts)
        {
            List<AddAccountResult> returns = new List<AddAccountResult>();
            foreach (Account account in accounts)
            {
                returns.Add(AddAccount(account));
            }
            return returns;
        }

        public static Account GetAccountForUsername(string username)
        {
            Account returns = null;
            foreach (Account account in Accounts)
            {
                if (account.Username == username)
                {
                    returns = account;
                    break;
                }
            }
            return returns;
        }

        public static bool AccountExists(string username)
        {
            bool exists = false;
            foreach (Account account in Accounts)
            {
                if (account.Username == username)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        public static bool AccountExists(Account account)
        {
            return AccountExists(account.Username);
        }
    }
}
