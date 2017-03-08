namespace AlgorithmVisualization.Algorithm
{

    public delegate void LogEventHandler(string str);

    public abstract class Output
    {

        public string LogString;
        public event LogEventHandler Logged;

        public Statistics Statistics;


        protected Output()
        {
            Statistics = new Statistics();
        }

        public void LogLine(string str)
        {
            var line = str + "\n";
            LogString += line;
            Logged?.Invoke(line);
        }

    }
}
