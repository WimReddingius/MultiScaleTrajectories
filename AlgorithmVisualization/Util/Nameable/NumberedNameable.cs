using System;
using Newtonsoft.Json;

namespace AlgorithmVisualization.Util.Nameable
{
    public delegate void BaseNameChangedEventHandler(NumberedNameable nameable, string newBase);

    public abstract class NumberedNameable : Nameable
    {
        internal event BaseNameChangedEventHandler BaseNameChanged;

        [JsonIgnore]
        public override string Name
        {
            get { return NameNumber > 1 ? BaseName + "_" + NameNumber : BaseName; }
            set { throw new NotSupportedException(); }
        }

        private string _baseName;
        public string BaseName
        {
            get { return _baseName;}
            set
            {
                _baseName = value;
                BaseNameChanged?.Invoke(this, value);
            }
        }

        [JsonProperty]
        internal int NameNumber { get; set; }

        protected NumberedNameable()
        {
            NameNumber = 1;
        }

    }
}
