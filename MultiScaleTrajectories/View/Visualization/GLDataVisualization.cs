﻿using MultiScaleTrajectories.Controller;

namespace MultiScaleTrajectories.View.Visualization
{
    class GLDataVisualization<TVis, TData> : DataControl<TData> where TVis : GLVisualization, IDataLoader<TData>, new()
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
