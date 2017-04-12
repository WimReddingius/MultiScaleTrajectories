using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.ConvexHull;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.ConvexHull.Bidirectional;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.ChinChan;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ImaiIriOptions : UserControl
    {
        public event Action ShortcutProviderChanged;

        [JsonProperty] private Type shortcutFinderType => shortcutProvider.GetType();
        private readonly BindingList<ShortcutProvider> shortcutProviders;
        private ShortcutProvider shortcutProvider;
        public ShortcutProvider ShortcutProvider
        {
            get { return shortcutProvider; }
            set { shortcutProvider = value; ShortcutProviderChanged?.Invoke(); }
        }


        [JsonConstructor]
        public ImaiIriOptions(Type shortcutFinderType) : this()
        {
            shortcutFinderComboBox.SelectedItem = shortcutProviders.ToList().Find(s => s.GetType() == shortcutFinderType);
        }

        public ImaiIriOptions()
        {
            InitializeComponent();

            shortcutProviders = new BindingList<ShortcutProvider>
            {
                new EFShortcutProvider<EFBruteForce>(),
                new SFShortcutProvider<SFChinChan>(),
                new EFShortcutProvider<EFConvexHullIncremental>(),
                new EFShortcutProvider<EFConvexHullEnhanced>(),
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutFinderComboBox.DataSource = shortcutProviders;
            shortcutFinderComboBox.Format += (o, e) => e.Value = ((ShortcutProvider) e.Value).Name;

            shortcutFinderComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutFinderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShortcutProvider = (ShortcutProvider)shortcutFinderComboBox.SelectedItem;
        }

    }
}
