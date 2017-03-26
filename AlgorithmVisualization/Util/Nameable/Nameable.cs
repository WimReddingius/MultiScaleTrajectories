using Newtonsoft.Json;

namespace AlgorithmVisualization.Util.Nameable
{
    public class Nameable : INameable
    {
        //used for data binding
        [JsonIgnore] public object Self => this;

        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }

    }
}
