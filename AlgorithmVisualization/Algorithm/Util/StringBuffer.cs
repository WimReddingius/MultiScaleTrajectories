namespace AlgorithmVisualization.Algorithm.Util
{
    public class StringBuffer
    {

        private readonly ThreadSafeStringBuilder buffer;

        public StringBuffer()
        {
            buffer = new ThreadSafeStringBuilder();
        }

        public string Flush()
        {
            var str = buffer.ToString();
            buffer.Clear();
            return str;
        }

        public void Append(string str)
        {
            buffer.Append(str);
        }

    }
}
