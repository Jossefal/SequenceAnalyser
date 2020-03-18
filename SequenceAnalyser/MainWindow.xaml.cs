using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using SequenceAnalyser.SequenceAnalysing;

namespace SequenceAnalyser
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ChooseFileBtn_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "txt files (*.txt)|*.txt";

            if (openFileDialog.ShowDialog() == true)
            {
                resultDataTextBox.Text = "Обработка данных...";

                AnalyzeSequenceFromFileAsync(openFileDialog.FileName);
            }
        }

        private async void AnalyzeSequenceFromFileAsync(string path)
        {
            DateTime startDateTime = DateTime.Now;

            SequenceData sequenceData = await Task.Run(()=>AnalyzeSequenceFromFile(path));

            if(sequenceData == null)
            {
                MessageBox.Show("Пустая последовательность");
                resultDataTextBox.Text = "Выберите файл";
                return;
            }

            StringBuilder resultData = new StringBuilder();

            resultData.Append("[Начало: " + startDateTime + " --- Конец: " + DateTime.Now + "]");
            resultData.Append("\n\nМинимальное значение: " + sequenceData.minValue);
            resultData.Append("\n\nМаксимальное значение: " + sequenceData.maxValue);
            resultData.Append("\n\nСреднее арифметическое: " + sequenceData.average);
            resultData.Append("\n\nМедиана: " + sequenceData.median);
            resultData.Append("\n\nМаксимальная последовательность, которая увеличивается (" + sequenceData.maxIncreasingSequence.Count + "): " + String.Join(", ", sequenceData.maxIncreasingSequence));
            resultData.Append("\n\nМаксимальная последовательность, которая уменьшается (" + sequenceData.maxDecreasingSequence.Count + "): " + String.Join(", ", sequenceData.maxDecreasingSequence));

            resultDataTextBox.Text = resultData.ToString();
        }

        private SequenceData AnalyzeSequenceFromFile(string path)
        {
            return Sequence.AnalyzeSequence(SequenceLoader.LoadFromTextFile(path));
        }
    }
}
