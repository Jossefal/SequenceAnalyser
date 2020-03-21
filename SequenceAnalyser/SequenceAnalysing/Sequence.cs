using System.Collections.Generic;

namespace SequenceAnalyser.SequenceAnalysing
{
    //Класс для анализа последовательности
    class Sequence
    {
        public static SequenceData AnalyzeSequence(List<int> sequence)
        {
            if (sequence.Count == 0)
                return null;

            //Индексы первого и последнего элемента максимальной последовательности, которая увеличивается, соответственно
            int increasingStartIndex = 0;
            int increasingEndIndex = 0;

            //Индексы первого и последнего элемента максимальной последовательности, которая уменьшается, соответственно
            int decreasingStartIndex = 0;
            int decreasingEndIndex = 0;

            //Индекс первого элемента текущей последовательности, которая увеличивается
            int tmpIncreasingStartIndex = 0;

            //Индекс первого элемента текущей последовательности, которая уменьшается
            int tmpDecreasingStartIndex = 0;

            //Сума всех чисел последовательности, для нахождения среднего арифметического
            decimal sum = sequence[0];

            for (int i = 1; i < sequence.Count; i++)
            {
                sum += sequence[i];

                /*Если текущая последовательность, уоторая уменьшается, прерывается
                 сравниваем ее длину с максимальной*/
                if (sequence[i] >= sequence[i - 1] || i + 1 == sequence.Count)
                {
                    if (i - tmpDecreasingStartIndex > 1 + decreasingEndIndex - decreasingStartIndex)
                    {
                        decreasingStartIndex = tmpDecreasingStartIndex;
                        decreasingEndIndex = i - 1;
                    }

                    tmpDecreasingStartIndex = i;
                }

                /*Если текущая последовательность, которая увеличивается, прерывается
                 сравниваем ее длину с максимальной*/
                if (sequence[i] <= sequence[i - 1] || i + 1 == sequence.Count)
                {
                    if (i - tmpIncreasingStartIndex > 1 + increasingEndIndex - increasingStartIndex)
                    {
                        increasingStartIndex = tmpIncreasingStartIndex;
                        increasingEndIndex = i - 1;
                    }

                    tmpIncreasingStartIndex = i;
                }
            }

            //Получаем максимальные последовательности
            List<int> maxIncreasingSequence = sequence.GetRange(increasingStartIndex, 1 + increasingEndIndex - increasingStartIndex);
            List<int> maxDecreasingSequence = sequence.GetRange(decreasingStartIndex, 1 + decreasingEndIndex - decreasingStartIndex);

            //Находим среднее арифметическое
            float average = (float) sum / sequence.Count;

            //Сортируем последовательность и находим медиану и минимальное/максимальное значение
            sequence.Sort();

            float median;

            if (sequence.Count % 2 == 0)
            {
                median = (sequence[sequence.Count / 2 - 1] + sequence[sequence.Count / 2]) / 2f;
            }
            else
                median = sequence[sequence.Count / 2];

            return new SequenceData(sequence[0], sequence[sequence.Count - 1], average, median, maxIncreasingSequence, maxDecreasingSequence);
        }
    }
}
