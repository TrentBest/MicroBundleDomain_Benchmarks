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
