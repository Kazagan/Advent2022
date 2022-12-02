``` ini

BenchmarkDotNet=v0.13.2, OS=ubuntu 22.04
Intel Core i7-8565U CPU 1.80GHz (Whiskey Lake), 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.110
  [Host]     : .NET 6.0.10 (6.0.1022.47701), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.10 (6.0.1022.47701), X64 RyuJIT AVX2


```
|       Method |     Mean |   Error |  StdDev | Rank |    Gen0 |    Gen1 | Allocated |
|------------- |---------:|--------:|--------:|-----:|--------:|--------:|----------:|
|       Stream | 181.4 μs | 1.90 μs | 1.78 μs |    1 | 17.8223 |  8.5449 |   73.3 KB |
|    ReadLines | 211.9 μs | 1.87 μs | 1.75 μs |    2 | 37.8418 | 12.9395 | 155.05 KB |
| LinqSolution | 288.7 μs | 4.59 μs | 4.29 μs |    3 | 47.8516 |       - | 196.59 KB |
