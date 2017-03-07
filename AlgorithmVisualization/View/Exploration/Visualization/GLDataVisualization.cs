using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.View.Exploration.Visualization
{
    public class GLDataVisualization<TVis, TData> : DataView<TData> where TVis : GLVisualization, IDataLoader<TData>, new()
    {

        private readonly TVis Visualization;

        public GLDataVisualization()
        {
            Visualization = new TVis();

            FormsUtil.FillContainer(this, Visualization);

            ParentChanged += (o, e) => Visualization.MakeCurrent();
        }

        public override void LoadData(TData data)
        {
            Visualization.LoadData(data);
        }

    }
}
