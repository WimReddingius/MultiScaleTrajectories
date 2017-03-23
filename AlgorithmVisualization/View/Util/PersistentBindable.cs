using System.Runtime.Serialization;

namespace AlgorithmVisualization.View.Util
{
    public class PersistentBindable : Bindable
    {

        [OnDeserialized]
        internal void PrefixDisplayNameIfNecessary(StreamingContext context)
        {
            if (!DisplayName.StartsWith("* "))
                DisplayName = "* " + DisplayName;
        }

    }
}
