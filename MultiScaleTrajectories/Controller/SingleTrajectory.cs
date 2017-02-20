using MultiScaleTrajectories.Algorithm;
using MultiScaleTrajectories.Algorithm.SingleTrajectory;
using MultiScaleTrajectories.View.SingleTrajectory;
using System.Collections.Generic;
using System.Windows.Forms;
using OpenTK;
using MultiScaleTrajectories.Algorithm.SingleTrajectory.ShortcutShortestPath;
using Newtonsoft.Json;
using System;

namespace MultiScaleTrajectories.Controller
{
    class SingleTrajectory : IAlgorithmType
    {
        STInputControl InputControl;
        STVisualization Visualization;
        STInput Input;
        STOutput Output;
        IAlgorithm<STInput, STOutput> Algorithm;

        public SingleTrajectory()
        {
            Input = new STInput();
            InputControl = new STInputControl(Input);
            Visualization = new STVisualization(Input);
        }

        public void SetAlgorithm(object algorithm)
        {
            Algorithm = (IAlgorithm<STInput, STOutput>) algorithm;
        }

        public void ClearInput()
        {
            LoadInput(new STInput());
        }

        public void VisualizeInput()
        {
            Visualization.VisualizeInput(Input);
        }

        public void VisualizeOutput()
        {
            Output = Algorithm.Compute(Input);
            Visualization.VisualizeOutput(Output);
        }

        public void DeSerializeInput(string inputString)
        {
            LoadInput(JsonConvert.DeserializeObject<STInput>(inputString));
        }

        public string SerializeInput()
        {
            return JsonConvert.SerializeObject(Input);
        }

        public UserControl GetInputControl()
        {
            return InputControl;
        }

        public GLControl GetVisualizationControl()
        {
            return Visualization;
        }

        public List<object> GetAlgorithms()
        {
            List<object> algorithms = new List<object>();
            algorithms.Add(new ShortcutShortestPath());
            return algorithms;
        }

        private void LoadInput(STInput Input)
        {
            this.Input = Input;
            InputControl.LoadInput(this.Input);
            Visualization.VisualizeInput(this.Input);
        }

        public override string ToString()
        {
            return "Single Trajectory";
        }

    }
}
