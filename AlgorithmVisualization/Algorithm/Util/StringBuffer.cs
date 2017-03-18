namespace AlgorithmVisualization.Algorithm.Util
{
    public class StringBuffer
    {

        private string buffer;

        public StringBuffer()
        {
            buffer = "";
        }

        //is this completely thread safe?
        public string Flush()
        {
            var str = buffer;
            buffer = "";
            return str;
        }

        public void Append(string str)
        {
            buffer += str;
        }

    }
}
