using System.Collections.Generic;

namespace SequenceAnalyser.SequenceAnalysing
{
    //Класс для обработки последовательности
    class Sequence
    {
        public static SequenceData AnalyzeSequence(List<int> sequence)
        {
            if (sequence.Count == 0)
                return null;

            int increasingStartIndex = 0;
            int increasingEndIndex = 0;

            int decreasingStartIndex = 0;
            int decreasingEndIndex = 0;

            int tmpIncreasingStartIndex = 0;

            int tmpDecreasingStartIndex = 0;

            decimal sum = sequence[0];

            for (int i = 1; i < sequence.Count; i++)
            {
                sum += sequence[i];

                if (sequence[i] >= sequence[i - 1] || i + 1 == sequence.Count)
                {
                    if (i - tmpDecreasingStartIndex > 1 + decreasingEndIndex - decreasingStartIndex)
                    {
                        decreasingStartIndex = tmpDecreasingStartIndex;
                        decreasingEndIndex = i - 1;
                    }

                    tmpDecreasingStartIndex = i;
                }

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

            List<int> maxIncreasingSequence = sequence.GetRange(increasingStartIndex, 1 + increasingEndIndex - increasingStartIndex);
            List<int> maxDecreasingSequence = sequence.GetRange(decreasingStartIndex, 1 + decreasingEndIndex - decreasingStartIndex);
            float average = (float) sum / sequence.Count;

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
