using System.Collections.Generic;
using MultiScaleTrajectories.Algorithm;

namespace MultiScaleTrajectories.Controller
{
    interface IAlgoController<TIn, TOut> : IAlgoView where TOut : Output, new() where TIn : Input, new()
    {

        List<IAlgorithm<TIn, TOut>> Algorithms { get; }

        List<DataViewController<TOut>> OutputControllers { get; }

        DataViewController<TIn> InputController { get;  }

        AlgorithmRunnable<TIn, TOut> Run { get; set; }

    }
}
