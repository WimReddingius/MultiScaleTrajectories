using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using MultiScaleTrajectories.Algorithm.DataStructures.Graph;
using MultiScaleTrajectories.Algorithm.Geometry;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.BruteForce;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Bidirectional;
using MultiScaleTrajectories.ImaiIri.EpsilonFinding.Algorithm.ConvexHull.Incremental;
using MultiScaleTrajectories.ImaiIri.ShortcutFinding.Algorithm.ChinChan;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortcutProvision;
using MultiScaleTrajectories.MultiScale.Algorithm.ImaiIri.ShortestPathProvision;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Concrete;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.MultiScale.View.Algorithm
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class ImaiIriOptions : UserControl
    {
        public event Action ShortcutProviderChanged;

        [JsonProperty] private Type shortcutFinderType => chosenShortcutProvider.GetType();
        [JsonProperty] private Type shortestPathProviderType => ChosenShortestPathProvider.GetType();

        private readonly BindingList<ShortcutProvider> shortcutProviders;
        private readonly BindingList<ShortcutShortestPath> shortestPathProviders;

        private ShortcutProvider chosenShortcutProvider;
        public ShortcutProvider ChosenShortcutProvider
        {
            get { return chosenShortcutProvider; }
            set { chosenShortcutProvider = value; ShortcutProviderChanged?.Invoke(); }
        }     
        public ShortcutShortestPath ChosenShortestPathProvider;



        [JsonConstructor]
        public ImaiIriOptions(Type shortcutFinderType, Type shortestPathProviderType) : this()
        {
            shortcutFinderComboBox.SelectedItem = shortcutProviders.ToList().Find(s => s.GetType() == shortcutFinderType);
            shortestPathProviderComboBox.SelectedItem = shortestPathProviders.ToList().Find(s => s.GetType() == shortestPathProviderType);
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

            shortestPathProviders = new BindingList<ShortcutShortestPath>
            {
                new ShortcutShortestPathConcrete<DijkstraSimple<DataNode<Point2D>, WeightedEdge>>(),
                new ShortcutShortestPathConcrete<DijkstraFibonacciHeap<DataNode<Point2D>, WeightedEdge>>()
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
        }
    }
}
