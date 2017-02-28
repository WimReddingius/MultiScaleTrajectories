using System.Collections.Generic;
using System.Linq;

namespace MultiScaleTrajectories.Controller.Util
{
    class CompoundDataLoader<T> : IDataLoader<T>
    {
        public List<IDataLoader<T>> DataLoaders;

        public CompoundDataLoader()
        {
            DataLoaders = new List<IDataLoader<T>>();
        }

        public CompoundDataLoader(List<IDataLoader<T>> dataLoaders)
        {
            DataLoaders = dataLoaders;
        }

        public void LoadData(T data)
        {
            foreach (IDataLoader<T> control in DataLoaders)
            {
                control.LoadData(data);
            }
        }

    }
}
