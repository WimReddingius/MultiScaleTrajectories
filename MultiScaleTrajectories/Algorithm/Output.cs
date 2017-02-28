using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiScaleTrajectories.Algorithm
{

    public delegate void CompletedEventHandler();

    abstract class Output
    {

        public event CompletedEventHandler Completed;
        public bool IsComplete { get; protected set; }

        protected Output()
        {
            IsComplete = false;
        }

        public void SetComplete()
        {
            IsComplete = true;
            Completed?.Invoke();
        }

    }
}
