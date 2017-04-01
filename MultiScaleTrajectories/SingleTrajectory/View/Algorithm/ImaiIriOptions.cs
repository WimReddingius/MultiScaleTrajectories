using System.ComponentModel;
using System.Runtime.Serialization;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Nameable;
using MultiScaleTrajectories.Algorithm.ImaiIri;
using MultiScaleTrajectories.Algorithm.ImaiIri.BruteForce;
using MultiScaleTrajectories.Algorithm.ImaiIri.ChinChan;
using MultiScaleTrajectories.SingleTrajectory.Algorithm.ImaiIri;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.SingleTrajectory.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ImaiIriOptions : UserControl
    {
        [JsonProperty] public INameableFactory<ShortcutFinder> ShortcutFinderFactory;
        [JsonProperty] private readonly BindingList<INameableFactory<ShortcutFinder>> shortcutFinderFactories;
        [JsonIgnore] private ImaiIriAlgorithm algorithm;

        [JsonConstructor]
        public ImaiIriOptions()
        {
            InitializeComponent();
        }

        public ImaiIriOptions(ImaiIriAlgorithm algorithm) : this()
        {
            shortcutFinderFactories = new BindingList<INameableFactory<ShortcutFinder>>
            {
                new NameableFactory<SimpleShortcutFinder>(SimpleShortcutFinder.Name),
                new NameableFactory<ChinChanShortcutFinder>(ChinChanShortcutFinder.Name),
            };

            this.algorithm = algorithm;

            PopulateControls();
        }

        public void CustomOnDeserialized(ImaiIriAlgorithm algo)
        {
            algorithm = algo;
            PopulateControls();
            shortcutFinderComboBox.SelectedItem = ShortcutFinderFactory;
        }

        private void PopulateControls()
        {
            shortcutFinderComboBox.DataSource = shortcutFinderFactories;
            shortcutFinderComboBox.Format += (o, e) => e.Value = ((INameableFactory<ShortcutFinder>) e.Value).Name;

            shortcutFinderComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutFinderComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ShortcutFinderFactory = (INameableFactory<ShortcutFinder>)shortcutFinderComboBox.SelectedItem;
            algorithm.Name = algorithm.AlgoName + " - " + ShortcutFinderFactory.Name;
        }

    }
}
