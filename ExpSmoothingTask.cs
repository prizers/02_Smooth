using System.Collections.Generic;

namespace yield
{
    public static class ExpSmoothingTask
    {
        public static IEnumerable<DataPoint> SmoothExponentialy(this IEnumerable<DataPoint> data, double alpha)
        {
            double prev = 0.0;
            bool first = true;
            foreach (var sample in data)
            {
                sample.ExpSmoothedY = first
                    ? sample.OriginalY
                    : sample.OriginalY * alpha + prev * (1.0 - alpha);
                prev = sample.ExpSmoothedY;
                first = false;
                yield return sample;
            }
        }
    }
}
