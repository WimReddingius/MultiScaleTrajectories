using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller.Edit
{
    public class InputEditorWrapper<TIn, TEdit> : InputEditor<TIn>
        where TIn : Input, new() 
        where TEdit : Control, IInputEditor<TIn>, new()
    {
        private readonly TEdit editor;

        public InputEditorWrapper()
        {
            editor = new TEdit();

            this.Fill(editor);
            Name = editor.Name;
        }

        public override void LoadInput(TIn input)
        {
            editor.LoadInput(input);
        }

    }

}
