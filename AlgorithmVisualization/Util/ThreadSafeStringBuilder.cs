using System.Text;

namespace AlgorithmVisualization.Util
{
    public class ThreadSafeStringBuilder
    {

        private readonly StringBuilder stringBuilder;
        private readonly object thisLock;

        public ThreadSafeStringBuilder()
        {
            stringBuilder = new StringBuilder();
            thisLock = new object();
        }

        public void Append(string str)
        {
            lock (thisLock)
            {
                stringBuilder.Append(str);
            }
        }

        public void Clear()
        {
            lock (thisLock)
            {
                stringBuilder.Clear();
            }
        }

        public override string ToString()
        {
            lock (thisLock)
            {
                return stringBuilder.ToString();
            }
        }

    }
}
