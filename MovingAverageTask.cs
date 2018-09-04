using System.Collections.Generic;

namespace yield
{
    public static class MovingAverageTask
    {
        public static IEnumerable<DataPoint> MovingAverage(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var window = new Queue<double>();
            double sum = 0.0;
            foreach (var sample in data)
            {
                if (windowWidth <= window.Count) sum -= window.Dequeue();
                window.Enqueue(sample.OriginalY);
                sum += sample.OriginalY;
                sample.AvgSmoothedY = sum / window.Count;
                yield return sample;
            }
        }
    }
}
