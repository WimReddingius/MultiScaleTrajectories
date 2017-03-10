using AlgorithmVisualization.Algorithm.Experiment;

namespace AlgorithmVisualization.Algorithm
{

    public delegate void LoggedEventHandler(string str);

    public abstract class Output
    {
        public event LoggedEventHandler Logged;

        public Statistics Statistics;
        public string LogString;


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
