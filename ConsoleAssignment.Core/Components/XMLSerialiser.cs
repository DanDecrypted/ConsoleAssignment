using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleAssignment.Core
{
    class XMLSerialiser : ISerialiser
    {
        public T Deserialise<T>(string path)
        {
            T obj = default(T);
            XmlSerializer xmlSerialiser = new XmlSerializer(typeof(T));
            if (File.Exists(path))
            {
                using (FileStream fs = new FileStream(path, FileMode.Open))
                {
                    XmlReader reader = XmlReader.Create(fs);
                    obj = (T)xmlSerialiser.Deserialize(reader);
                }
            }
            return obj;
        }

        public string[] Serialise<T>(T t, string path)
        {
            try
            {
                XmlSerializer xmlSerialiser = new XmlSerializer(typeof(T));
                using (StreamWriter sw = new StreamWriter(path))
                {
                    using (XmlWriter xmlw = XmlWriter.Create(sw))
                    {
                        xmlSerialiser.Serialize(xmlw, t);
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
    }
}
