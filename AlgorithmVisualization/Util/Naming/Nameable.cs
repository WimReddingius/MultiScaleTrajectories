using Newtonsoft.Json;

namespace AlgorithmVisualization.Util.Naming
{
    public delegate void NameChangedEventHandler(Nameable nameable, string newName);

    public class Nameable : INameable
    {
        //used for data binding
        [JsonIgnore] public object Self => this;

        public event NameChangedEventHandler NameChanged;

        private string _name;
        public string Name
        {
            get { return _name; }
            set {
                _name = value;
                NameChanged?.Invoke(this, value);
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
