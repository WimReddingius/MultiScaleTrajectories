using System;
using System.Windows.Forms;
using AlgorithmVisualization.Algorithm;
using AlgorithmVisualization.View.Util;

namespace AlgorithmVisualization.Controller.Edit
{
    public class SimpleInputEditor<TIn> : InputEditor<TIn> where TIn : Input, new() 
    {
        private readonly IInputEditor<TIn> editor;

        public SimpleInputEditor(IInputEditor<TIn> editor)
        {
            if (!(editor is Control))
                throw new ArgumentOutOfRangeException(nameof(editor), "editor provided does not inherit from both Control");

            this.editor = editor;
            this.Fill((Control)editor);
            Name = editor.Name;
        }

        public override void LoadInput(TIn input)
        {
            editor.LoadInput(input);
        }

    }

}
