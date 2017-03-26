﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;

namespace AlgorithmVisualization.View.Util.Nameable
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
            var baseName = item.BaseName;
            var maxNameNumber = MaxNameNumberConflictingWith(baseName);

            if (maxNameNumber != -1)
            {
                item.NameNumber = maxNameNumber + 1;
            }
        }

        private List<T> GetConflicts(string baseName)
        {
            return this.ToList().FindAll(bindable => bindable.BaseName == baseName);
        }

        private int MaxNameNumberConflictingWith(string baseName)
        {
            var conflicts = GetConflicts(baseName);

            if (conflicts.Count == 0)
                return -1;

            return conflicts.Max(i => i.NameNumber);
        }

        private static string GetBaseName(T namedBindable)
        {
            var name = namedBindable.Name;
            var lastIndex = name.LastIndexOf('_');

            return lastIndex == -1 ? name : name.Substring(0, lastIndex);
        }

        private static int GetSuffixNumber(T namedBindable)
        {
            var name = namedBindable.Name;
            var lastIndex = name.LastIndexOf('_');
            var numDigits = name.Length - (lastIndex + 1);

            var suffixNumber = -1;
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