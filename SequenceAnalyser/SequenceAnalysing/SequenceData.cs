using System.Collections.Generic;

namespace SequenceAnalyser.SequenceAnalysing
{
    //Класс для хранения данных о последовательности
    class SequenceData
    {
        //Минимальное число
        public int minValue;
        //Максимальное число
        public int maxValue;
        //Среднее арифметическое
        public float average;
        //Медиана
        public float median;
        //Максимальная последовательность идущих подряд чисел, которая увеличивается
        public List<int> maxIncreasingSequence = new List<int>();
        //Максимальная последовательность идущих подряд чисел, которая уменьшается
        public List<int> maxDecreasingSequence = new List<int>();

        public SequenceData(int minValue, int maxValue, float average, float median, List<int> maxIncreasingSequence, List<int> maxDecreasingSequence)
        {
            this.minValue = minValue;
            this.maxValue = maxValue;
            this.average = average;
            this.median = median;
            this.maxIncreasingSequence = new List<int>(maxIncreasingSequence);
            this.maxDecreasingSequence = new List<int>(maxDecreasingSequence);
        }
    }
}
