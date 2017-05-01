using System;
using System.ComponentModel;
using System.Windows.Forms;
using MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.Algorithm.Representation;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.ShortcutFinding.SingleScale.View
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class SSShortcutFinderOptions : UserControl
    {
        [JsonProperty] public SSSComplete ChosenShortcutFinder;
        [JsonProperty] private readonly BindingList<SSSComplete> shortcutFinders;


        [JsonConstructor]
        public SSShortcutFinderOptions(BindingList<SSSComplete> shortcutFinders, SSSComplete ChosenShortcutSetFactory)
        {
            InitializeComponent();

            this.shortcutFinders = shortcutFinders;

            PopulateControls();

            shortcutSetFactoriesComboBox.SelectedItem = ChosenShortcutSetFactory;
        }

        public SSShortcutFinderOptions()
        {
            InitializeComponent();

            shortcutFinders = new BindingList<SSSComplete>
            {
                new SSShortcutGraphFinder(),
                new SSShortcutRegionsFinder()
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutSetFactoriesComboBox.DataSource = shortcutFinders;
            shortcutSetFactoriesComboBox.Format += (o, e) => e.Value = ((SSSComplete)e.Value).Name;

            shortcutSetFactoriesComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutSetFactoriesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortcutFinder = (SSSComplete)shortcutSetFactoriesComboBox.SelectedItem;
        }
    }
}
