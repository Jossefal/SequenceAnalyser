using System;
using System.IO;
using System.Collections.Generic;

namespace SequenceAnalyser.SequenceAnalysing
{
    //Класс для загрузки последовательности из файла
    class SequenceLoader
    {
        public static List<int> LoadFromTextFile(string path)
        {
            if (!File.Exists(path))
                return null;

            List<int> sequence = new List<int>();

            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() != -1)
                {
                    sequence.Add(Convert.ToInt32(sr.ReadLine()));
                }
            }                

            return sequence;
        }
    }
}
