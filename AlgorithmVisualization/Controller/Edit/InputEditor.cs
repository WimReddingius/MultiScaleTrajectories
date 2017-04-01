using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;

namespace AlgorithmVisualization.Controller.Edit
{
    public abstract class InputEditor<TIn> : UserControl, IInputEditor<TIn> where TIn : Input
    {

        public abstract void LoadInput(TIn input);

        //object has to inherit from Control and IInputEditor
        public static InputEditor<TIn> CreateSimple(object inputEditor)
        {
            var type = inputEditor.GetType();
            var iInputEditorType = typeof(IInputEditor<TIn>);
            var iControlType = typeof(Control);

            if (iControlType.IsAssignableFrom(type) && iInputEditorType.IsAssignableFrom(type))
            {
                var inputEditorWrapperType = typeof(InputEditorWrapper<,>).MakeGenericType(typeof(TIn), type);
                var concrete = (InputEditor<TIn>) Activator.CreateInstance(inputEditorWrapperType, inputEditor);

                return concrete;
            }

            throw new ArgumentOutOfRangeException(nameof(type), "Type provided does not inherit from both Control and IInputEditor");
        }

    }
}
