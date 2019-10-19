This repository consists of two main modules:

- `AlgorithmVisualization`: an algorithm-agnostic UI and set of tools that can be used for running, configuring and visualizing algorithms.
- `MultiScaleTrajectories`: a set of algorithms and UI plugins for the `AlgorithmVisualization` module, for a problem called *progressive curve simplification*. This is a problem that comes from cartography, where one wants to simplify a line feature (e.g. a river or road) over multiple levels of detail, such that zooming in progressively reveals more detail without producing any visual artifacts. For more information, see [my thesis](https://iverb.me/research/thesis.pdf).

# AlgorithmVisualization

## Configuration

In the top-right corner, you can select which algorithm type you wish to run. For each of these algorithm types, it is possible to have differents kinds of implementations, but they all accept the same kind of input.

 Each of these algorithm types can be instantiated to create a configuration for that specific type. New configurations can be added, and configurations can be saved to/loaded from disk. This is useful for restoring an experimental setup, as well as exploring the output/statistics gathered from the experiments themselves. To prevent losing your configuration, the tool will save the state of your configuration when you close the tool and will restore this state the next time you start it up.
 
 Each configuration contains the following:

 - Algorithm library: a set of configurations for specific implementations of the algorithm type.
 - Input library: a set of inputs which can be used to run these algorithms. Inputs can be saved to/loaded from disk or imported using a custom importer. Furthermore, differents kinds of custom input editors may be defined for visualizing and editing the input.
 - Workload: a set of outputs and statistics produced by running one or more algorithm run configurations. A run configuration is composed of:
   - A specific input chosen from the input library.
   - A specific implementation configuration chosen from the algorithm library
   - An amount of iterations for which to repeat running the given algorithm on the given input. This is useful for gathering multiple samples.
 
   Run configurations can be executed by selecting one or multiple of them (indicated in blue) and clicking `Compute`. Note that you can select multiple configurations uing CTRL+CLICK or SHIFT+CLICK on the run configuration name, akin to selecting files/folders in the Windows file explorer. Running multiple configurations at the same time will execute each run configuration in a separate thread. Using the same way of selecting multiple run configurations, it is also possible to clear the output of multiple run configurations at the same time by clicking the `Reset` button. Based on the color of the `Amount` column, you can tell what the status of a run configuration is. It is either idle (red), in progress (yellow), or green (completed all iterations).

   As alluded to earlier, when the algorithm type configuration is stored to disk, the algorithm workload configuration as well as any produced workload output/statistics are saved.

## Output Visualization

In the main panel on the left, visualizations can be shown for the output of the run configurations. This can either be the output of the algorithm or (live) statistics that were gathered. The type of visualization can be chosen by hovering your mouse over the top edge and selecting the type of visualization in the dropdown menu on the left. 

By default, the run configurations that are visualized inside of a visualization are chosen automatically, but you can disable this by clicking the `Choose` button and selecting one or more configurations. You can go back to automatically selecting the run configurations shown by a visualization by clicking the `Auto` button.

Finally, by clicking the `Split` button, it is possible to show multiple visualizations at the same time by splitting the visualization panel either vertically or horizontally. This can be done as often as you want to show many different visualizations simultaneously. Alternatively, by selecting the `Unsplit` option, a visualization can be removed and a neighboring visualization panel will be expanded to fill the space.

The following pre-defined visualizations are available for any algorithm type:

- Statistics: table containing running time and iterations completed, algorithm configuration, and input/output statistics. Can show statistics of multiple runs simultaneously by expanding to multiple columns.
- Log: raw logging generated by the algorithm for one specific run.
 
# MultiScaleTrajectories

For context on the algorithms and techniques described below, see [my thesis](https://iverb.me/research/thesis.pdf).

## Algorithm types and implementations

- `Single Trajectory Multi-Scale Simplification`: simplifying an input curve at various levels of details using a given set of error criteria.
  - `H - Optimal - Cubic`. progressive. Optimal progressive simplification algorithm running in using weighted Imai-Iri simplification.
  - `H - Optimal - Quartic`. progressive. A slower version of the optimal progressive simplification algorithm, which uses a separate run of Dijkstra's algorithm for computing the shortcut weight of every shortcut on every scale, as opposed to running Dijkstra only once for every shortcut sharing the same source node.
  - `H - Imai Iri Bottom Up`. Progressive simplification heuristic using Imai-Iri which starts at the finest scale, and restricts simplification of the next (coarser) scale to strictly contain a subset of the resulting simplification to ensure the resulting simplification is progressive.
  - `H - Imai Iri Bottom Up - Cao`. The same simplification heuristic, but ensures that the resulting simplification is progressive by re-using the simplification of the previous (finer) scale as input for the simplification of the next (coarser) scale (as proposed by Cao et al.). Faster, but will not guarantee that the simplification holds to the error criterion.
  - `H - Imai Iri Top Down`. A similar heuristic which instead starts at the coarsest scale, and for each edge `(pi, pj)` in the resulting simplification, recursively computes a simplification on the next (finer) scale between `pi` and `pj` which are then concatenated together.
  - `H - Douglas Peucker Bottom Up`. Similar bottom-up heuristic as with Imai-Iri, but replaces the simplification sub-routine with the heuristic by Douglas and Peucker. This algorithm also re-uses the resulting simplifications as input for simplifying at the next scale (Cao et al.), but unlike `H - Imai Iri Bottom Up - Cao`, this algorithm does not drop compliance with the error criterion due to the nature of the Douglas-Peucker heuristic.
  - `H - Douglas Peucker Top Down`. Similar top-down heuristic as with Imai-Iri, but replaces the simplification sub-routine with the heuristic by Douglas and Peucker.
  - `Imai-Iri`. Non-progressive simplification using Imai-Iri, where each scale is simplified independently. Acts as a baseline.

  For each of these algorithms except for the ones using Douglas-Peucker, it is possible to configure how the shortcut graphs are computed and how shortest paths are found in them. The configuration options are in line with the configuration options outlined below.
  
- `Shortcut finding - Multi Scale`: finding all sets of shortcuts given a set of errors. All algorithms listed below use the Hausdorff distance as error measure.
  - `Brute force`. Naive implementation which explicitly calculates the Hausdorff distance for each shortcut independently.
  - `Chin Chan`. Chin and Chan's algorithm for constructing the shortcut graph. Commonly used in unison with Imai-Iri when simplifying for the Hausdorff distance.
  - `Convex Hulls`. Uses convex hulls of contiguouses sequences of the input curve to incrementally determine the exact Hausdorff distance of every shortcut with a common source node `pi`. This is done using extreme point queries on a left-leaning red-black trees annotated with the point furthest from `pi`.
   
   The following options are available for each of these algorithms:
     - `Shortcut Set Builder`. This is the means of representing the shortcuts over the various scales.
        - `Simple`. A simple representation which explicitly stores a shortcut graph representation for each scale.
        - `Compact - min level`. Compact representation for reducing memory pressure when simplifying for many scales. For this representation, we use the fact that if a shortcut occurs on scale `x`, it must also occur in all scales `y` where `y > x`, since these scales will allow for a higher error. This representation takes advantage of this by simply maintaining a dictionary which maps each shortcut to the first scale it occurs in.
        - `Compact - min error`. Compact representation which uses a dictionary to map every shortcut to its associated error. This representation is useful if you have many scales, where for each scale you can trivially determine whether a shortcut is present by checking if the shortcut error stored in the dictionary is smaller than the maximum allowed error of the scale. Computing the exact error of a shortcut is typically more expensive than determining whether a shortcut is valid for a pre-defined error. 
        Of the shortcut finding algorithms listed above, only `Convex Hulls` allows for computing the exact Hausdorff distance of every shortcut, so only that algorithm can take advantage of this representation.

    - `Shortcut Representation`. The shortcut graph representation that is provided when an interfacing algorithm requests all shortcuts on a given scale.
        - `Graph`. Explicit graph representation with nodes and edges.
        - `Intervals`. Implicit graph representation with a set of intervals `[px, py]` for every point `pi`, such that for any `x <= j <= y`, `(pi, pj)` is a shortcut.
- `Shortcut path finding`: finding the shortest path in a pre-computed set of shortcuts, either represented using a graph:
  - `Graph - BFS.` Breadth-first search.
  - `Graph - Dijkstra`. Dijkstra's algorithm. For the priority queue, the following heap types can be used:
    - `Binomial Heap`
    - `Fibonacci Heap`
    - `Automatic D-ary heap`
    - `4-ary Heap`
    - `Pairing Heap`

  or a set of shortcut intervals:
  
  - `Intervals - BFS`. Breadth-first search.
  - `Intervals - Range Queries`. This algorithm constructs a left-leaning red-black tree where each node `p` is annotated with the next node in a shortest path from `p` to the target node. We then use the shorcut intervals to perform range queries on this tree to find shortest path in expected `O(n log n)` time.

## Input editors

### Trajectory editor
TODO

### Trajectory importer
TODO

### Error distribution generator
Cumulative?

### Shortcut graph generator
TODO

## Output Visualization

### Simplifications
TODO

### Shortcut graphs
TODO

# Running the latest build
If you simply want to run the latest build, extract all contents of `build_DD_MM_YYY.zip` to a folder, and run `MultiScaleTrajectories.exe`.

You can import `Griffon Vulture NABU Moessingen.csv`, a simple flight trajectory of a griffon vulture across Europe, to start running the various progressive simplification algorithms.

# Build Instructions (Visual Studio)

Make sure all references are pointing to the right DLL's in the `packages` folder and make sure to right click `MultiScaleTrajectories -> Set as StartUp Project` in the solution explorer. 

# Dependencies
  - [Json.NET](https://www.newtonsoft.com/json) - Versatile JSON library for .NET
  - [GMap.NET](https://github.com/radioman/greatmaps) - Overlaying animal trajectories over Google maps
  - [OpenTK](https://opentk.net/) - OpenGL bindings for C#
  - [AlgoKit](https://github.com/pgolebiowski/algo-kit) - heap implementions for use in Dijkstra's algorithm
