This repository consists of two main modules:

- [AlgorithmVisualization](https://github.com/WimReddingius/MultiScaleTrajectories/wiki/AlgorithmVisualization): an algorithm-agnostic UI and set of tools that can be used for running, configuring and visualizing algorithms.

- [MultiScaleTrajectories](https://github.com/WimReddingius/MultiScaleTrajectories/wiki/MultiScaleTrajectories): a set of algorithms and UI plugins for the `AlgorithmVisualization` module, for a problem called *progressive curve simplification*. This is a problem that comes from cartography, where one wants to simplify a line feature (e.g. a river or road) over multiple levels of detail, such that zooming in progressively reveals more detail without producing any visual artifacts. For more information, see [our paper](https://www.sciencedirect.com/science/article/abs/pii/S0925772120300146) (Pre-print found [here](https://arxiv.org/abs/1806.02647)).

# Running the latest build
If you simply want to run the latest build, extract all contents of `build_DD_MM_YYY.zip` to a folder, and run `MultiScaleTrajectories.exe`.

You can import one of the included datasets, to start running the various progressive simplification algorithms.

# Build Instructions (Visual Studio)

Make sure all references are pointing to the right DLL's in the `packages` folder and make sure to right click `MultiScaleTrajectories -> Set as StartUp Project` in the solution explorer. 

# Dependencies
  - [Json.NET](https://www.newtonsoft.com/json) - Versatile JSON library for .NET
  - [GMap.NET](https://github.com/radioman/greatmaps) - Overlaying animal trajectories over Google maps
  - [OpenTK](https://opentk.net/) - OpenGL bindings for C#
  - [AlgoKit](https://github.com/pgolebiowski/algo-kit) - heap implementions for use in Dijkstra's algorithm
