using System;

namespace AlgorithmVisualization.View.Util
{
    class TimeFormatter
    {

        public static string Format(TimeSpan timeSpan)
        {
            return string.Format(GetTimeFormat(timeSpan), timeSpan);
        }

        public static string GetTimeFormat(TimeSpan timeSpan)
        {
            if (timeSpan.TotalSeconds < 10)
            {
                return "{0:s\\.ff}";
            }
            if (timeSpan.TotalSeconds < 60)
            {
                return "{0:ss\\.ff}";
            }
            if (timeSpan.TotalMinutes < 10)
            {
                return "{0:m\\:ss\\.ff}";
            }
            if (timeSpan.TotalMinutes < 60)
            {
                return "{0:mm\\:ss\\.ff}";
            }
            if (timeSpan.TotalHours < 10)
            {
                return "{0:h\\:mm\\:ss\\.ff}";
            }
            return "{0:hh\\:mm\\:ss\\.ff}";
        }

    }
}
