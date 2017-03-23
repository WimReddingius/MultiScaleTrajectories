using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Factory;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.Algorithm.ImaiIri.Fast;
using MultiScaleTrajectories.Algorithm.ImaiIri.Slow;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ImaiIriOptions : UserControl
    {

        [JsonProperty]
        public IFactory<ShortcutFinder> ShortcutFinderFactory;

        [JsonProperty]
        private readonly BindingList<IFactory<ShortcutFinder>> shortcutFinderFactories;

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
                new BindableFactory<SlowShortcutFinder> { DisplayName = "Slow" },
                new BindableFactory<FastShortcutFinder> { DisplayName = "Fast" }
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
