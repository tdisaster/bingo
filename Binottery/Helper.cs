using Binottery.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Binottery
{
    public class Helper
    {
        public static List<int> FillListFromRange(int smallestNumber, int biggestNumber)
        {
            List<int> listOfNumbers = new List<int>();
            for(int index = smallestNumber; index<=biggestNumber; index++)
            {
                listOfNumbers.Add(index);
            }
            return listOfNumbers;
        }

        internal static void LoadState(string path, State state)
        {
            var lines = File.ReadAllLines(path);



        }

        public static T ReadFromXmlFile<T>(string filePath) where T : new()
        {
            TextReader reader = null;
            try
            {
                var serializer = new XmlSerializer(typeof(T));
                reader = new StreamReader(filePath);
                return (T)serializer.Deserialize(reader);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }
    }
}
