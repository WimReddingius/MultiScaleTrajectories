namespace MultiScaleTrajectories.Controller
{
    interface IDataLoader<T>
    {
        void LoadData(T data);
    }
}
