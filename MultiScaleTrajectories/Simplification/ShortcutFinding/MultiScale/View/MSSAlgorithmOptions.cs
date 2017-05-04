using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using Newtonsoft.Json;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Compact;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Factory;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation.Simple;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class MSSAlgorithmOptions : UserControl
    {
        [JsonProperty] public MSShortcutSetBuilder ChosenShortcutSetBuilder;
        [JsonProperty] private ShortcutSetFactory chosenShortcutSetFactory; 
        [JsonProperty] private readonly BindingList<MSShortcutSetBuilder> shortcutSetBuilders;
        [JsonProperty] private readonly BindingList<ShortcutSetFactory> shortcutSetFactories;


        [JsonConstructor]
        public MSSAlgorithmOptions(BindingList<ShortcutSetFactory> shortcutSetFactories, ShortcutSetFactory chosenShortcutSetFactory,
            BindingList<MSShortcutSetBuilder> shortcutSetBuilders, MSShortcutSetBuilder ChosenShortcutSetBuilder)
        {
            InitializeComponent();

            this.shortcutSetBuilders = shortcutSetBuilders;
            this.shortcutSetFactories = shortcutSetFactories;

            PopulateControls();

            shortcutSetBuilderComboBox.SelectedItem = ChosenShortcutSetBuilder;
            shortcutSetFactoryComboBox.SelectedItem = chosenShortcutSetFactory;
        }

        public MSSAlgorithmOptions()
        {
            InitializeComponent();

            shortcutSetBuilders = new BindingList<MSShortcutSetBuilder>
            {
                new MSSCompleteCompact(),
                new MSCompleteSimple()
            };

            shortcutSetFactories = new BindingList<ShortcutSetFactory>
            {
                new ShortcutGraphFactory(),
                new ShortcutRegionSetFactory()
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutSetFactoryComboBox.DataSource = shortcutSetFactories;
            shortcutSetFactoryComboBox.Format += (o, e) => e.Value = ((ShortcutSetFactory)e.Value).Name;

            shortcutSetBuilderComboBox.DataSource = shortcutSetBuilders;
            shortcutSetBuilderComboBox.Format += (o, e) => e.Value = ((MSShortcutSetBuilder)e.Value).Name;

            shortcutSetBuilderComboBox_SelectedIndexChanged(null, null);
            shortcutSetFactoryComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutSetBuilderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortcutSetBuilder = (MSShortcutSetBuilder)shortcutSetBuilderComboBox.SelectedItem;
        }

        private void shortcutSetFactoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            chosenShortcutSetFactory = (ShortcutSetFactory) shortcutSetFactoryComboBox.SelectedItem;

            foreach (var builder in shortcutSetBuilders)
            {
                builder.ShortcutSetFactory = chosenShortcutSetFactory;
            }
        }
    }
}
