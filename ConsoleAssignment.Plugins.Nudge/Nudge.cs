using System;
using System.Collections.Generic;
using System.Net.Mail;
using ConsoleAssignment.Core;

namespace ConsoleAssignment.Plugins
{
    public class Nudge : ICommandPlugin, IDescribable
    {
        public Nudge()
        {

        }
        public bool CanHandle(string command)
        {
            return (command == "nudge");
        }

        public string GetDescription()
        {
            return "Usage: Nudge [Username] [Optional:Message]\n\rSends an email to the specified user with the subject of 'nudge'.\n\r" +
                   "Message body is optional. \n\rUsername must exist.";
        }

        public string GetName()
        {
            return "Plguins.Nudge.dll";
        }

        public string[] Handle(string[] args)
        {
            var messages = new List<string>();
            if (!ArgumentsAreValid(args, messages))
            {
                return messages.ToArray();
            }

            SplicedContainer splicedUsernameAndMessage = Utils.SplicedContainerForIndexOfAray(args, 0);
            string username = splicedUsernameAndMessage.Spliced;
            string message = String.Join(" ", splicedUsernameAndMessage.RemainingArray);

            Account to = AccountsRepository.GetAccountForUsername(username);
            if (to == null)
            {
                return new string[] 
                {
                    String.Format("Username \"{0}\" could not be found in the system. Have you loaded the save file?", username)
                };
            }

            List<string> result = new List<string>();
            try
            {
                MailMessage mm = new MailMessage()
                {
                    From = new MailAddress("d.scott@wpcsoft.com", "Dan Scott"),
                    Subject = "Nudge",
                    Body = message
                };

                mm.To.Add(new MailAddress(to.Email, to.Username));
                using(SmtpClient client = new SmtpClient("exchange.wpcsoft.com"))
                {
                    client.Send(mm);
                }

                return new string[] 
                {
                    String.Format("Message \"{0}\" was successfully sent to {1}.", mm.Body.ToString(), to.Email)
                };
            }
            catch (InvalidOperationException e)
            {
                result.Add("Server host is undefined. Error: " + e.Message);
            }
            catch (SmtpFailedRecipientException e)
            {
                result.Add("Recepient does not have a mailbox. Error: " + e.Message);
            }
            catch (SmtpException e)
            {
                result.Add("Smtp sever host could not be found. Error: " + e.Message);
            }
            catch (Exception e)
            {
                result.Add("Error: " + e.Message.ToString());
            }
            return result.ToArray();
        }

        private bool ArgumentsAreValid(string[] args, List<string> messages)
        {
            if (args.Length == 0)
            {
                messages.Add("No account given to nudge");
                return false;
            }
            else if (args[0] == "?" || args[0].ToLower() == "help")
            {
                messages.Add($"{GetName()}\n\rUsage: {GetDescription()}");
                return false;
            }
            return true;
        }
    }
}
