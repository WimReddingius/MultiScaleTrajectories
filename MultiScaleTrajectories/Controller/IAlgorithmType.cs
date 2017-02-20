using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiScaleTrajectories.Controller
{
    interface IAlgorithmType
    {
        UserControl GetInputControl();

        GLControl GetVisualizationControl();

        List<object> GetAlgorithms();

        void SetAlgorithm(object algorithm);

        void ClearInput();

        void VisualizeInput();

        void VisualizeOutput();

        string SerializeInput();

        void DeSerializeInput(string inputString);

        string ToString();
    }
}
