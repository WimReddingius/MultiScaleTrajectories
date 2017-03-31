using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace AlgorithmVisualization.Util.Nameable
{
    public class NamingList<T> : BindingList<T> where T : NumberedNameable
    {
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
            item.BaseNameChanged += (o, e) => ValidateNameNumber(item);
        }

        private void ValidateNameNumber(T item)
        {
            var maxNameNumber = MaxNameNumberConflictingWith(item);

            if (maxNameNumber != -1)
            {
                item.NameNumber = maxNameNumber + 1;
            }
            else
            {
                item.NameNumber = 1;
            }
        }

        private List<T> GetConflicts(T item)
        {
            return this.ToList().FindAll(bindable => bindable.BaseName == item.BaseName && bindable != item);
        }

        private int MaxNameNumberConflictingWith(T item)
        {
            var conflicts = GetConflicts(item);

            if (conflicts.Count == 0)
                return -1;

            return conflicts.Max(i => i.NameNumber);
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
