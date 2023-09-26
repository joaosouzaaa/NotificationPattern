using Benchmark;
using BenchmarkDotNet.Running;

await Task.Delay(15 * 1000);
var summary = BenchmarkRunner.Run<NotificationPatternBenchmark>();
Console.ReadLine();