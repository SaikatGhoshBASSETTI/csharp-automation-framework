using NUnit.Framework;

// Forces NUnit to run all test fixtures and methods in parallel by default across available CPU cores
[assembly: Parallelizable(ParallelScope.Children)]
[assembly: LevelOfParallelism(4)]