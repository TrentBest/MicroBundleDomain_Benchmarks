```

BenchmarkDotNet v0.15.2, Windows 10 (10.0.19045.6466/22H2/2022Update)
Intel Core i5-10400F CPU 2.90GHz, 1 CPU, 12 logical and 6 physical cores
.NET SDK 10.0.401
  [Host]   : .NET 8.0.31 (8.0.3126.42015), X64 RyuJIT AVX2 [AttachedDebugger]
  .NET 8.0 : .NET 8.0.31 (8.0.3126.42015), X64 RyuJIT AVX2

Job=.NET 8.0  Runtime=.NET 8.0  

```
| Method                   | Count | Mean       | Error    | StdDev   | Ratio | RatioSD | Gen0   | Gen1   | Allocated | Alloc Ratio |
|------------------------- |------ |-----------:|---------:|---------:|------:|--------:|-------:|-------:|----------:|------------:|
| **EmptyDescriptor**          | **0**     |   **163.5 ns** |  **0.75 ns** |  **0.63 ns** |  **1.00** |    **0.01** | **0.0484** |      **-** |     **304 B** |        **1.00** |
| DependenciesOnly         | 0     |   156.0 ns |  1.70 ns |  1.59 ns |  0.95 |    0.01 | 0.0484 |      - |     304 B |        1.00 |
| ProvidersOnly            | 0     |   156.2 ns |  0.69 ns |  0.61 ns |  0.95 |    0.01 | 0.0484 |      - |     304 B |        1.00 |
| DependenciesAndProviders | 0     |   150.2 ns |  1.37 ns |  1.28 ns |  0.92 |    0.01 | 0.0484 |      - |     304 B |        1.00 |
|                          |       |            |          |          |       |         |        |        |           |             |
| **EmptyDescriptor**          | **1**     |   **154.9 ns** |  **1.01 ns** |  **0.95 ns** |  **1.00** |    **0.01** | **0.0484** |      **-** |     **304 B** |        **1.00** |
| DependenciesOnly         | 1     |   214.1 ns |  0.94 ns |  0.79 ns |  1.38 |    0.01 | 0.0880 |      - |     552 B |        1.82 |
| ProvidersOnly            | 1     |   223.7 ns |  0.87 ns |  0.77 ns |  1.44 |    0.01 | 0.0880 |      - |     552 B |        1.82 |
| DependenciesAndProviders | 1     |   281.5 ns |  1.02 ns |  0.85 ns |  1.82 |    0.01 | 0.1273 |      - |     800 B |        2.63 |
|                          |       |            |          |          |       |         |        |        |           |             |
| **EmptyDescriptor**          | **4**     |   **157.1 ns** |  **0.92 ns** |  **0.82 ns** |  **1.00** |    **0.01** | **0.0484** |      **-** |     **304 B** |        **1.00** |
| DependenciesOnly         | 4     |   276.4 ns |  1.64 ns |  1.46 ns |  1.76 |    0.01 | 0.1221 |      - |     768 B |        2.53 |
| ProvidersOnly            | 4     |   338.7 ns |  1.00 ns |  0.84 ns |  2.16 |    0.01 | 0.1221 |      - |     768 B |        2.53 |
| DependenciesAndProviders | 4     |   474.1 ns |  7.49 ns |  8.92 ns |  3.02 |    0.06 | 0.1960 |      - |    1232 B |        4.05 |
|                          |       |            |          |          |       |         |        |        |           |             |
| **EmptyDescriptor**          | **16**    |   **158.9 ns** |  **0.74 ns** |  **0.69 ns** |  **1.00** |    **0.01** | **0.0484** |      **-** |     **304 B** |        **1.00** |
| DependenciesOnly         | 16    |   405.8 ns |  2.70 ns |  2.40 ns |  2.55 |    0.02 | 0.1998 |      - |    1256 B |        4.13 |
| ProvidersOnly            | 16    |   633.5 ns |  3.67 ns |  3.25 ns |  3.99 |    0.03 | 0.1993 |      - |    1256 B |        4.13 |
| DependenciesAndProviders | 16    |   903.7 ns |  4.77 ns |  4.46 ns |  5.69 |    0.04 | 0.3519 | 0.0010 |    2208 B |        7.26 |
|                          |       |            |          |          |       |         |        |        |           |             |
| **EmptyDescriptor**          | **64**    |   **159.7 ns** |  **0.67 ns** |  **0.62 ns** |  **1.00** |    **0.01** | **0.0484** |      **-** |     **304 B** |        **1.00** |
| DependenciesOnly         | 64    |   959.6 ns |  5.88 ns |  5.21 ns |  6.01 |    0.04 | 0.6790 |      - |    4264 B |       14.03 |
| ProvidersOnly            | 64    | 1,958.7 ns | 17.61 ns | 16.47 ns | 12.27 |    0.11 | 0.6790 |      - |    4264 B |       14.03 |
| DependenciesAndProviders | 64    | 2,761.9 ns | 17.92 ns | 14.96 ns | 17.30 |    0.11 | 1.3084 | 0.0114 |    8224 B |       27.05 |
