using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using TheSingularityWorkshop.MicroBundleDomain;

namespace MicroBundleDomain_Benchmarks;

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net80)]
public class DescriptorBenchmarks
{
    [Params(0, 1, 4, 16, 64)]
    public int Count { get; set; }

    private MicroBundleDependency[] dependencies = null!;
    private MicroBundleProvider[] providers = null!;

    [GlobalSetup]
    public void Setup()
    {
        dependencies = new MicroBundleDependency[Count];
        providers = new MicroBundleProvider[Count];

        for (var i = 0; i < Count; i++)
        {
            var id = (ulong)(i + 1);
            dependencies[i] = new MicroBundleDependency(id);
            providers[i] = new MicroBundleProvider($"provider-{id}");
        }
    }

    [Benchmark(Baseline = true)]
    public MicroBundleDescriptor EmptyDescriptor()
        => new(1, "0.1.0-alpha.1");

    [Benchmark]
    public MicroBundleDescriptor DependenciesOnly()
        => new(1, "0.1.0-alpha.1", dependencies: dependencies);

    [Benchmark]
    public MicroBundleDescriptor ProvidersOnly()
        => new(1, "0.1.0-alpha.1", providers: providers);

    [Benchmark]
    public MicroBundleDescriptor DependenciesAndProviders()
        => new(
            1,
            "0.1.0-alpha.1",
            dependencies: dependencies,
            providers: providers);
}
