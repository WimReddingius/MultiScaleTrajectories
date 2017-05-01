using System.ComponentModel;
using System.Windows.Forms;
using AlgorithmVisualization.Util.Naming;
using MultiScaleTrajectories.AlgoUtil.DataStructures.Graph;
using MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.Algorithm.Dijkstra.Heap;
using MultiScaleTrajectories.PathFinding.SingleSource.Algorithm.Dijkstra.Heap;
using Newtonsoft.Json;

namespace MultiScaleTrajectories.AlgoUtil.PathFinding.SingleSource.View
{
    [JsonObject(MemberSerialization.OptIn)]
    partial class DijkstraHeapOptions<TNode, TEdge> : UserControl where TNode : Node, new() where TEdge : Edge
    {
        [JsonProperty] public DijkstraHeapFactory<TNode, TEdge> ChosenHeapFactory;
        [JsonProperty] private BindingList<DijkstraHeapFactory<TNode, TEdge>> heapFactories;


        [JsonConstructor]
        public DijkstraHeapOptions(DijkstraHeapFactory<TNode, TEdge> ChosenHeapFactory, BindingList<DijkstraHeapFactory<TNode, TEdge>> heapFactories)
        {
            InitializeComponent();

            this.heapFactories = heapFactories;

            PopulateControls();

            heapTypeComboBox.SelectedItem = ChosenHeapFactory;
        }

        public DijkstraHeapOptions()
        {
            InitializeComponent();

            heapFactories = new BindingList<DijkstraHeapFactory<TNode, TEdge>>
            {
                new BinomialHeapFactory<TNode, TEdge>(),
                new FibonacciHeapFactory<TNode, TEdge>(),
                new DAryAutomaticHeapFactory<TNode, TEdge>(),
                new DAryHeapFactory<TNode, TEdge>(4),
                new PairingHeapFactory<TNode, TEdge>()
            };

            PopulateControls();
        }

        private void PopulateControls()
        {
            heapTypeComboBox.DataSource = heapFactories;
            heapTypeComboBox.Format += (o, e) => e.Value = ((INameable)e.Value).Name;

            heapTypeComboBox_SelectedIndexChanged(null, null);
        }

        private void heapTypeComboBox_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            ChosenHeapFactory = (DijkstraHeapFactory<TNode, TEdge>)heapTypeComboBox.SelectedItem;
        }
    }
}
