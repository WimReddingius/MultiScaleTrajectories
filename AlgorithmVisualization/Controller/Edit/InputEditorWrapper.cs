using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.Util.Factory;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller.Edit
{
    public class InputEditorWrapper<TIn, TEdit> : InputEditor<TIn>
        where TIn : Input, new() 
        where TEdit : Control, IInputEditor<TIn>
    {
        private readonly TEdit editor;

        public InputEditorWrapper() : this(new object[] {})
        {
        }

        public InputEditorWrapper(params object[] args) : this(new Factory<TEdit>().Create(args))
        {
        }

        public InputEditorWrapper(TEdit editor)
        {
            this.editor = editor;
            this.Fill(editor);
            Name = editor.Name;
        }

        public override void LoadInput(TIn input)
        {
            editor.LoadInput(input);
        }

    }

}
