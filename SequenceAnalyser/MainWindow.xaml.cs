using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

                AnalyzeSequenceAsync(openFileDialog.FileName);
            }
        }

        private async void AnalyzeSequenceAsync(string path)
        {
            chooseFileBtn.IsEnabled = false;

            Stopwatch stopWatch = Stopwatch.StartNew();

            SequenceData sequenceData = await Task.Run(()=>AnalyzeSequenceFromFile(path));

            if(sequenceData == null)
            {
                MessageBox.Show("Пустая последовательность");
                resultDataTextBox.Text = "Выберите файл";
                return;
            }

            stopWatch.Stop();
            TimeSpan timeSpan = stopWatch.Elapsed;

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}:{3:00}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds / 10);

            StringBuilder resultData = new StringBuilder();

            resultData.Append("[Время выполнения: " + elapsedTime + "]");
            resultData.Append("\n\nМинимальное значение: " + sequenceData.minValue);
            resultData.Append("\n\nМаксимальное значение: " + sequenceData.maxValue);
            resultData.Append("\n\nСреднее арифметическое: " + sequenceData.average);
            resultData.Append("\n\nМедиана: " + sequenceData.median);
            resultData.Append("\n\nМаксимальная последовательность, которая увеличивается (" + sequenceData.maxIncreasingSequence.Count + "): " + String.Join(", ", sequenceData.maxIncreasingSequence));
            resultData.Append("\n\nМаксимальная последовательность, которая уменьшается (" + sequenceData.maxDecreasingSequence.Count + "): " + String.Join(", ", sequenceData.maxDecreasingSequence));

            resultDataTextBox.Text = resultData.ToString();

            chooseFileBtn.IsEnabled = true;
        }

        private SequenceData AnalyzeSequenceFromFile(string path)
        {
            List<int>
            return Sequence.AnalyzeSequence(SequenceLoader.LoadFromTextFile(path));
        }
    }
}
