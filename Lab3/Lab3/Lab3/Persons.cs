using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Lab3
{
    class Persons : ObservableCollection<Person>
    {
        public void Load()
        {
            try
            {
                System.Xml.Serialization.XmlSerializer reader = new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));
                System.IO.StreamReader file = new System.IO.StreamReader("SerializedData.xml");
                List<Person> buffer = (List<Person>)reader.Deserialize(file);
                file.Close();
                foreach (var p in buffer)
                {
                    Add(p);
                }
            }
            catch (Exception) { }
        }
        public void Dump()
        {
            List<Person> buffer = new List<Person>(this);
            var writer = new System.Xml.Serialization.XmlSerializer(typeof(List<Person>));
            var wfile = new System.IO.StreamWriter("SerializedData.xml");
            writer.Serialize(wfile, buffer);
            wfile.Close();
        }
    }
}
