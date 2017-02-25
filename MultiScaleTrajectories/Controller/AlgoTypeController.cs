using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    abstract class AlgoTypeController
    {
        public InputController InputController;
        public List<ViewTypeController> ViewControllers;
        public List<object> Algorithms;

        public ViewTypeController CurrentViewType;
        public object CurrentAlgorithm;


        public AlgoTypeController()
        {
            ViewControllers = new List<ViewTypeController>();
            Algorithms = new List<object>();
        }

        public virtual void SetAlgorithm(object algorithm)
        {
            CurrentAlgorithm = algorithm;
        }

        public virtual void SetViewType(ViewTypeController viewType)
        {
            CurrentViewType = viewType;
        }

        public abstract override string ToString();

    }
}
