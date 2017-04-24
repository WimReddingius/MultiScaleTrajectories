using System;
using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.View.Util;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ImaiIriOptions : UserControl
    {
        [JsonProperty] private readonly BindingList<ShortcutProvider> shortcutProviders;
        [JsonProperty] private readonly BindingList<ShortcutShortestPath> shortestPathProviders;
        [JsonProperty] public ShortcutProvider ChosenShortcutProvider;
        [JsonProperty] public ShortcutShortestPath ChosenShortestPathProvider;
        //[JsonProperty] public ShortcutProvider ChosenShortcutProvider;
        //[JsonProperty] public ShortcutShortestPath ChosenShortestPathProvider;


        [JsonConstructor]
        public ImaiIriOptions(BindingList<ShortcutProvider> shortcutProviders, BindingList<ShortcutShortestPath> shortestPathProviders, 
            ShortcutProvider ChosenShortcutProvider, ShortcutShortestPath ChosenShortestPathProvider)
        {
            InitializeComponent();

            this.shortcutProviders = shortcutProviders;
            this.shortestPathProviders = shortestPathProviders;

            PopulateControls();

            shortcutFinderComboBox.SelectedItem = ChosenShortcutProvider;
            shortestPathProviderComboBox.SelectedItem = ChosenShortestPathProvider;
        }

        public ImaiIriOptions()
        {
            InitializeComponent();

            shortcutProviders = new BindingList<ShortcutProvider>
            {
                new EFShortcutProvider(new EFBruteForce()),
                new SFShortcutProvider(new SFChinChan()),
                new EFShortcutProvider(new EFConvexHullEnhanced()),
                //new EFShortcutProvider(new EFConvexHullIncremental()),
            };

            shortestPathProviders = new BindingList<ShortcutShortestPath>
            {
                new ShortcutShortestPathRanges(),
                new ShortcutShortestPathSimple(new DijkstraHeapSlow<DataNode<Point2D>, WeightedEdge>()),
                new ShortcutShortestPathSimple(new DijkstraHeapFast<DataNode<Point2D>, WeightedEdge>())
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            shortcutFinderComboBox.DataSource = shortcutProviders;
            shortcutFinderComboBox.Format += (o, e) => e.Value = ((ShortcutProvider) e.Value).Name;

            shortestPathProviderComboBox.DataSource = shortestPathProviders;
            shortestPathProviderComboBox.Format += (o, e) => e.Value = ((ShortcutShortestPath)e.Value).Name;

            shortcutFinderComboBox_SelectedIndexChanged(null, null);
            shortestPathProviderComboBox_SelectedIndexChanged(null, null);
        }

        private void shortcutFinderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortcutProvider = (ShortcutProvider)shortcutFinderComboBox.SelectedItem;
        }

        private void shortestPathProviderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChosenShortestPathProvider = (ShortcutShortestPath) shortestPathProviderComboBox.SelectedItem;

            var simple = ChosenShortestPathProvider as ShortcutShortestPathSimple;
            if (simple != null)
            {
                var algorithm = simple.Algorithm;
                shortcutShortestPathOptions.Fill(algorithm.OptionsControl);
            }
        }
    }
}
