using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace AlgorithmVisualization.Util.Naming
{
    public class NamingList<T> : BindingList<T> where T : Nameable
    {
        public NameChangedEventHandler ItemNameChanged;

        public new void Add(T item)
        {
            if (!Contains(item))
            {
                RegisterItem(item);
                ValidateNameNumber(item);
                base.Add(item);
            }
        }

        private void RegisterItem(T item)
        {
            item.NameChanged += (o, s) =>
            {
                ValidateNameNumber(item);
                ItemNameChanged?.Invoke(o, s);
            };
        }

        private void ValidateNameNumber(T item)
        {
            var maxNameNumber = MaxNameNumberConflictingWith(item);

            var newName = GetBaseName(item);
            if (maxNameNumber != -1)
            {
                newName += "_" + (maxNameNumber + 1);
            }

            if (!newName.Equals(item.Name))
                item.Name = newName;
        }

        private List<T> GetConflicts(T item)
        {
            var baseName = GetBaseName(item);
            return this.ToList().FindAll(bindable => GetBaseName(bindable) == baseName && bindable != item);
        }

        private int MaxNameNumberConflictingWith(T item)
        {
            var conflicts = GetConflicts(item);

            if (conflicts.Count == 0)
                return -1;

            return conflicts.Max(GetNameNumber);
        }

        private static string GetBaseName(T namedBindable)
        {
            var name = namedBindable.Name;
            var lastIndex = name.LastIndexOf('_');
            var numDigits = name.Length - (lastIndex + 1);

            if (lastIndex != -1 && numDigits > 0)
            {
                var numberStr = name.Substring(lastIndex + 1, numDigits);
                int number;
                if (int.TryParse(numberStr, out number))
                    return name.Substring(0, lastIndex);
            }

            return name;
        }

        private static int GetNameNumber(T namedBindable)
        {
            var name = namedBindable.Name;
            var lastIndex = name.LastIndexOf('_');
            var numDigits = name.Length - (lastIndex + 1);

            if (lastIndex != -1 && numDigits > 0)
            {
                int nameNumber;
                var numberStr = name.Substring(lastIndex + 1, numDigits);
                if (int.TryParse(numberStr, out nameNumber))
                    return nameNumber;
            }

            return 1;
        }

        [OnDeserialized]
        internal void OnDeserialized(StreamingContext context)
        {
            foreach (var item in this)
            {
                RegisterItem(item);
            }
        }

    }
}
