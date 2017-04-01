using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace AlgorithmVisualization.Util.Nameable
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

            return lastIndex == -1 ? name : name.Substring(0, lastIndex);
        }

        private static int GetNameNumber(T namedBindable)
        {
            var name = namedBindable.Name;
            var lastIndex = name.LastIndexOf('_');
            var numDigits = name.Length - (lastIndex + 1);

            var suffixNumber = 1;
            if (lastIndex != -1 && numDigits > 0)
            {
                suffixNumber = int.Parse(name.Substring(lastIndex + 1, numDigits));
            }

            return suffixNumber;
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
