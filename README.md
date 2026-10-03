# MicroBundleDomain_Benchmarks

Benchmarks for the domain-side MicroBundle descriptor contract.

The benchmark suite measures the cost of constructing `MicroBundleDescriptor` instances as dependency and provider composition grows.

## Current matrix

- 0, 1, 4, 16, and 64 dependencies/providers
- empty descriptor
- dependencies only
- providers only
- dependencies and providers together
- BenchmarkDotNet memory diagnostics
- .NET 8 runtime

The benchmark intentionally measures descriptor construction and validation/materialization. It does not benchmark FSM_COS composition; that belongs to the composition layer.

## Package under test

`TheSingularityWorkshop.MicroBundleDomain` 0.1.0-alpha.1

## Initial benchmark results

The first Release benchmark was run with BenchmarkDotNet 0.15.2 on .NET 8.0.31, using an Intel Core i5-10400F (6 physical / 12 logical cores) with RyuJIT AVX2.

The baseline is `EmptyDescriptor`. The benchmark constructs a descriptor and, when populated, validates and materializes the supplied dependency/provider collections.

| Count | Scenario | Mean | Allocated |
|---:|---|---:|---:|
| 0 | Empty descriptor | 163.5 ns | 304 B |
| 0 | Dependencies only | 156.0 ns | 304 B |
| 0 | Providers only | 156.2 ns | 304 B |
| 0 | Dependencies + providers | 150.2 ns | 304 B |
| 1 | Empty descriptor | 154.9 ns | 304 B |
| 1 | Dependencies only | 214.1 ns | 552 B |
| 1 | Providers only | 223.7 ns | 552 B |
| 1 | Dependencies + providers | 281.5 ns | 800 B |
| 4 | Empty descriptor | 157.1 ns | 304 B |
| 4 | Dependencies only | 276.4 ns | 768 B |
| 4 | Providers only | 338.7 ns | 768 B |
| 4 | Dependencies + providers | 474.1 ns | 1,232 B |
| 16 | Empty descriptor | 158.9 ns | 304 B |
| 16 | Dependencies only | 405.8 ns | 1,256 B |
| 16 | Providers only | 633.5 ns | 1,256 B |
| 16 | Dependencies + providers | 903.7 ns | 2,208 B |
| 64 | Empty descriptor | 159.7 ns | 304 B |
| 64 | Dependencies only | 959.6 ns | 4,264 B |
| 64 | Providers only | 1,958.7 ns | 4,264 B |
| 64 | Dependencies + providers | 2,761.9 ns | 8,224 B |

### What the first run shows

- Empty descriptor construction stays essentially flat at about 150–160 ns across the matrix.
- Dependency-only construction grows from 214.1 ns at one dependency to 959.6 ns at 64 dependencies.
- Provider-only construction grows from 223.7 ns at one provider to 1,958.7 ns at 64 providers.
- Constructing both collections at 64 entries costs 2,761.9 ns and allocates 8,224 B.
- Allocation scales with composition size, as expected from materializing the supplied collections into the descriptor's read-only storage.
- The benchmark is measuring the domain contract itself: construction, validation, materialization, and read-only wrapping—not downstream FSM_COS composition.

These are the initial baseline measurements for the domain package. Further optimization should be driven by the composition-layer benchmarks and actual runtime requirements rather than by these numbers alone.

## Benchmark environment

- BenchmarkDotNet 0.15.2
- .NET 8.0.31
- Windows 10 22H2
- Intel Core i5-10400F @ 2.90 GHz
- 6 physical cores / 12 logical processors
- X64 RyuJIT AVX2

## How to read this benchmark

The source file is `MicroBundleDomain_Benchmarks.cs`.

The experiment is deliberately small:

1. `[GlobalSetup]` creates the dependency and provider arrays.
2. `[Params(0, 1, 4, 16, 64)]` selects the composition size.
3. `[Benchmark]` constructs the descriptor.
4. BenchmarkDotNet reports time and allocation.

This separation means the setup work is not accidentally included in the measured constructor cost.

### Why the results belong in the package documentation

A benchmark repository is useful to people who already know where to look. A package README is where most readers first encounter a performance claim.

The intended evidence chain is:

~~~text
MicroBundleDomain
      │
      ▼
performance baseline
      │
      ▼
MicroBundleDomain_Benchmarks
      │
      ▼
benchmark source
      │
      ▼
BenchmarkDotNet output
~~~

The package documentation summarizes the result; this repository preserves the experiment.

### Reproduce it

Run the project in Release configuration:

~~~bash
dotnet run -c Release
~~~

The benchmark references `TheSingularityWorkshop.MicroBundleDomain 0.1.0-alpha.1`. Preserve that version when reproducing the historical result.

Benchmark numbers are observations of a particular environment, not immutable guarantees.

### When to rerun

Rerun after changes to:

- `MicroBundleDescriptor`;
- dependency/provider storage;
- validation;
- collection materialization;
- read-only wrapping;
- identity/version representation;
- allocation strategy.

The goal is not to produce a flattering number.

**The goal is to know what the implementation costs.**
