using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Representation;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.View
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class MSSAlgorithmOptions : UserControl
    {
        [JsonProperty] public MSSComplete ChosenShortcutFinder;
        [JsonProperty] private readonly BindingList<MSSComplete> shortcutFinders;


        [JsonConstructor]
        public MSSAlgorithmOptions(BindingList<MSSComplete> shortcutFinders, MSSComplete ChosenShortcutFinder)
        {
            InitializeComponent();

            this.shortcutFinders = shortcutFinders;

            PopulateControls();

            shortcutSetFactoriesComboBox.SelectedItem = ChosenShortcutFinder;
        }

        public MSSAlgorithmOptions()
        {
            InitializeComponent();

            shortcutFinders = new BindingList<MSSComplete>
            {
                new MSShortcutGraphFinder(),
                new MSShortcutRegionsFinder()
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutSetFactoriesComboBox.DataSource = shortcutFinders;
            shortcutSetFactoriesComboBox.Format += (o, e) => e.Value = ((Nameable)e.Value).Name;

            shortcutSetFactoriesComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutSetFactoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortcutFinder = (MSSComplete)shortcutSetFactoriesComboBox.SelectedItem;
        }
    }
}
