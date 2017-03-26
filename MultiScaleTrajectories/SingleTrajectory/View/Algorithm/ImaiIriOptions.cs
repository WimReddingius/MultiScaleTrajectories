using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Factory;
using AlgorithmVisualization.Util.Nameable;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.Algorithm.ImaiIri.Simple;
using MultiScaleTrajectories.Algorithm.ImaiIri.Wedges;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ImaiIriOptions : UserControl
    {
        [JsonProperty] public IFactory<ShortcutFinder> ShortcutFinderFactory;
        [JsonProperty] private readonly BindingList<IFactory<ShortcutFinder>> shortcutFinderFactories;

        [JsonConstructor]
        public ImaiIriOptions(BindingList<IFactory<ShortcutFinder>> shortcutFinderFactories)
        {
            InitializeComponent();

            this.shortcutFinderFactories = shortcutFinderFactories;
            PopulateControls();
        }

        public ImaiIriOptions()
        {
            InitializeComponent();

            shortcutFinderFactories = new BindingList<IFactory<ShortcutFinder>>
            {
                new NameableFactory<SimpleShortcutFinder>(SimpleShortcutFinder.Name),
                new NameableFactory<WedgesShortcutFinder>(WedgesShortcutFinder.Name),
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutFinderComboBox.Items.AddRange(shortcutFinderFactories.ToArray());
            shortcutFinderComboBox.SelectedItem = shortcutFinderComboBox.Items[0];
        }

        private void shortcutFinderComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShortcutFinderFactory = (IFactory<ShortcutFinder>)shortcutFinderComboBox.SelectedItem;
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            shortcutFinderComboBox.SelectedItem = ShortcutFinderFactory;
        }

    }
}
