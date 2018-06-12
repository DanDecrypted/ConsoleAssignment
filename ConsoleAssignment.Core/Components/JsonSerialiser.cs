using Newtonsoft.Json;
using System;
using System.IO;

namespace ConsoleAssignment.Core
{
    public class JsonSerialiser : ISerialiser
    {
        public JsonSerialiser()
        {
        }

        public string[] Serialise<T>(T t, string path)
        {
            JsonSerializer serialiser = new JsonSerializer();
            try
            {
                using (StreamWriter sw = new StreamWriter(path))
                {
                    using (JsonWriter jw = new JsonTextWriter(sw))
                    {
                        serialiser.Serialize(jw, t);
                    }
                }
            }
            catch (Exception e)
            {
                return new string[]
                {
                    e.Message.ToString()
                };
            }
            return new string[]
            {
                "Successfully serialised object " + t.GetType().Name +  " to " + path
            };
        }

        public T Deserialise<T>(string path)
        {
            T obj = default(T);
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                obj = JsonConvert.DeserializeObject<T>(json);
            }
            return obj;
        }
    }
}
