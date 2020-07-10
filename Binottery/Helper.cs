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
        public enum GameStage
        {
            Begining = 0,
            FirstNumber,
            SecondNumber,
            ThirdNumber,
            FourthNumber,
            EndOfGame
        }

        public static List<int> FillListFromRange(int smallestNumber, int biggestNumber)
        {
            List<int> listOfNumbers = new List<int>();
            for(int index = smallestNumber; index<=biggestNumber; index++)
            {
                listOfNumbers.Add(index);
            }
            return listOfNumbers;
        }

        public static void WriteToXmlFile(string path, State state)
        {
            using (XmlWriter writer = XmlWriter.Create("theGarage.xml"))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement("State");

                writer.WriteStartElement("GeneratedNumbers");
                //
                for (int rowNumber = 0; rowNumber < Constants.MatrixNumberOfRows; rowNumber++)
                {
                    for (int columnNumber = 0; columnNumber < Constants.MatrixNumberOfColumns; columnNumber++)
                    {
                        writer.WriteString(state.GeneratedNumbers[columnNumber, rowNumber].ToString());
                        writer.WriteString(",");
                    }
                }
                writer.WriteEndElement();

                writer.WriteStartElement("WinningNumbers");
                //
                writer.WriteEndElement();

                writer.WriteStartElement("UserNumbers");
                //
                writer.WriteEndElement();

                writer.WriteEndElement();

            }
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
