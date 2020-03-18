using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;

namespace SequenceAnalyser.SequenceAnalysing
{
    //Класс для загрузки последовательности из файла
    class SequenceLoader
    {
        public static List<int> LoadFromTextFile(string path)
        {
            if (!File.Exists(path))
                return null;

            StreamReader sr = null;
            List<int> sequence = new List<int>();

            try
            {
                sr = new StreamReader(path);

                while (sr.Peek() != -1)
                {
                    sequence.Add(Convert.ToInt32(sr.ReadLine()));
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }

            return sequence;
        }
    }
}
