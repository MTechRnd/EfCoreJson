using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreJsonApp.BenchmarkTest
{
    public class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddLogger(ConsoleLogger.Default);
            AddValidator(ExecutionValidator.FailOnError);
            AddJob(Job.Default.WithIterationCount(5).WithWarmupCount(5));
            AddExporter(MarkdownExporter.Default, CsvExporter.Default);
            AddColumnProvider(DefaultColumnProviders.Instance);
        }
    }
}
