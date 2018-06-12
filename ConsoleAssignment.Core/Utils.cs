using System.Text.RegularExpressions;

namespace ConsoleAssignment.Core
{
    public class Utils
    {
        public static bool IsValidEmail(string email)
        {
            //This insane regex is General Email Regex (RFC 5322 Official Standard see: http://emailregex.com/)
            string insaneRegex = @"(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*|""" +
                           @"(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*""" +
                           @")@(?:(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?|\[(?:(?:25[0-5]|" +
                           @"2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-z0-9-]*[a-z0-9]" +
                           @":(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])";
            return Regex.IsMatch(email, insaneRegex);
        }

        public static SplicedContainer SplicedContainerForIndexOfAray(string[] args, int index)
        {
            SplicedContainer splicedContainer = new SplicedContainer();
            splicedContainer.RemainingArray = new string[args.Length - 1];
            for (int i = 0; i < args.Length; i++)
            {
                if (i == index)
                {
                    splicedContainer.Spliced = args[i];
                }
                else
                {
                    splicedContainer.RemainingArray[i > index ? i - 1 : i] = args[i];
                }
            }
            return splicedContainer;
        }
    }
}
