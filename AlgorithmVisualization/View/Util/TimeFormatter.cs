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
                return "{0:s\\.fff}";
            }
            if (timeSpan.TotalSeconds < 60)
            {
                return "{0:ss\\.fff}";
            }
            if (timeSpan.TotalMinutes < 10)
            {
                return "{0:m\\:ss\\.fff}";
            }
            if (timeSpan.TotalMinutes < 60)
            {
                return "{0:mm\\:ss\\.fff}";
            }
            if (timeSpan.TotalHours < 10)
            {
                return "{0:h\\:mm\\:ss\\.fff}";
            }
            return "{0:hh\\:mm\\:ss\\.fff}";
        }

    }
}
