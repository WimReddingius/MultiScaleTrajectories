using System.Collections.Generic;

namespace MultiScaleTrajectories.Controller.Util
{
    abstract class CompoundDataLoader<T>
    {
        public List<IDataLoader<T>> DataLoaders;
        public T Data;

        protected CompoundDataLoader()
        {
            DataLoaders = new List<IDataLoader<T>>();
        }

        public virtual void LoadData(T data)
        {
            Data = data;
            foreach (IDataLoader<T> control in DataLoaders)
            {
                control.LoadData(data);
            }
        }

    }
}
