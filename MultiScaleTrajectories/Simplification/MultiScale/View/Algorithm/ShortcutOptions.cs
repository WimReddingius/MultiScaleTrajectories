using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.Geometry;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision.Graph;
using MultiScaleTrajectories.Simplification.MultiScale.Algorithm.ImaiIri.ShortestPathProvision.Region;
using MultiScaleTrajectories.Simplification.ShortcutFinding.MultiScale.Algorithm.Algorithms;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.Simplification.MultiScale.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ShortcutOptions : UserControl
    {
        [JsonProperty] private readonly BindingList<ShortcutProvider> shortcutProviders;
        [JsonProperty] private readonly BindingList<ShortestPathProvider> shortestPathroviders;

        [JsonProperty] public ShortcutProvider ChosenShortcutProvider;
        [JsonProperty] public ShortestPathProvider ChosenShortestPathProvider;


        [JsonConstructor]
        public ShortcutOptions(BindingList<ShortcutProvider> shortcutProviders, ShortcutProvider ChosenShortcutProvider,
            BindingList<ShortestPathProvider> shortestPathroviders, ShortestPathProvider ChosenShortestPathProvider)
        {
            InitializeComponent();

            this.shortcutProviders = shortcutProviders;
            this.shortestPathroviders = shortestPathroviders;

            PopulateControls();

            shortestPathProviderComboBox.SelectedItem = ChosenShortestPathProvider;
            shortcutProviderComboBox.SelectedItem = ChosenShortcutProvider;
        }

        public ShortcutOptions()
        {
            InitializeComponent();

            shortcutProviders = new BindingList<ShortcutProvider>
            {
                new ShortcutsOnDemand(new MSSChinChan()),
                new ShortcutPreprocessor(new MSSChinChan()),
                new ShortcutPreprocessor(new MSSBruteForce()),
                new ShortcutPreprocessor(new MSSConvexHull())
            };

            shortestPathroviders = new BindingList<ShortestPathProvider>
            {
                new BasicShortestPathProvider(new DijkstraOnDemand<DataNode<TPoint2D>, WeightedEdge>()),
                new BasicShortestPathProvider(new DijkstraStandard<DataNode<TPoint2D>, WeightedEdge>()),
                new ShortcutGraphDijkstra(),
                new ShortcutRegionsShortestPath()
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutProviderComboBox.DataSource = shortcutProviders;
            shortcutProviderComboBox.Format += (o, e) => e.Value = ((ShortcutProvider)e.Value).Name;

            shortestPathProviderComboBox.DataSource = shortestPathroviders;
            shortestPathProviderComboBox.Format += (o, e) => e.Value = ((ShortestPathProvider)e.Value).Name;

            shortestPathProviderComboBox_SelectedIndexChanged(null, null);
            shortcutProviderComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortcutProvider = (ShortcutProvider) shortcutProviderComboBox.SelectedItem;
            
            shortcutProviderOptions.Fill(ChosenShortcutProvider.OptionsControl);
        }

        private void shortestPathProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortestPathProvider = (ShortestPathProvider)shortestPathProviderComboBox.SelectedItem;

            shortestPathProviderOptions.Fill(ChosenShortestPathProvider.OptionsControl);
        }

    }
}
