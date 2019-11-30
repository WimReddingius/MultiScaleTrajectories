using MultiScaleTrajectories.Simplification.MultiScale.Algorithm;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Edit
{
    interface IErrorSampler
    {
        event Action<List<double>> NewSamples;
        string TypeName { get; }
        UserControl Control { get; }
        void LoadInput(MSInput inp);
    }
}
