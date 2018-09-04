using System;
using System.Collections.Generic;
using System.Linq;

namespace yield
{
    public static class MovingMaxTask
    {
        private class Accountant
        {
            private LinkedList<Tuple<int, double>> window =
                new LinkedList<Tuple<int, double>>();
            private int index = 0;
            private int width = 0;

            public Accountant(int windowWidth)
            {
                width = windowWidth;
            }

            public void Push(double v)
            {
                while (0 < window.Count && window.Last.Value.Item2 <= v)
                    window.RemoveLast();
                window.AddLast(new Tuple<int, double>(index++, v));
                while (window.First.Value.Item1 < index - width)
                    window.RemoveFirst();
            }

            public double Max
            {
                get => window.First.Value.Item2;
            }
        }

        public static IEnumerable<DataPoint> MovingMax(this IEnumerable<DataPoint> data, int windowWidth)
        {
            var accountant = new Accountant(windowWidth);
            foreach (var sample in data)
            {
                accountant.Push(sample.OriginalY);
                sample.MaxY = accountant.Max;
                yield return sample;
            }
        }
    }
}