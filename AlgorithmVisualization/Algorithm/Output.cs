using AlgorithmVisualization.Algorithm.Experiment;

namespace AlgorithmVisualization.Algorithm
{

    public delegate void LoggedEventHandler(string str);

    public abstract class Output
    {
        public event LoggedEventHandler Logged;

        public string LogString;
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
