namespace MultiScaleTrajectories.Controller.Util
{
    interface IDataLoader<T>
    {
        void LoadData(T output);
    }
}
