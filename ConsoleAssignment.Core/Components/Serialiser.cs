using System.IO;

namespace ConsoleAssignment.Core
{
    public class Serialiser
    {
        static ISerialiser serialiser = new XMLSerialiser();
        static void CheckFileFormat(string path)
        {
            string extension = Path.GetExtension(path).ToLower();

            if (extension == ".xml")
            {
                SetSerialiser(new XMLSerialiser());
            }
            else
            {
                SetSerialiser(new JsonSerialiser());
            }
        }
        public static string[] Serialise<T>(T t, string path)
        {
            CheckFileFormat(path);
            return serialiser.Serialise<T>(t, path);
        }

       public static T Deserialise<T>(string path)
       {
            CheckFileFormat(path);
            return serialiser.Deserialise<T>(path);
       }

        public static void SetSerialiser(ISerialiser s)
        {
            serialiser = s;
        }
    }
}
