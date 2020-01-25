using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Profiling
{
    internal class Program
    {
        public static void Run(IRunner caller, IProfiler profiler, int repetitionsCount, IChartBuilder builder)
        {
            var result = profiler.Measure(caller, repetitionsCount);
            var chart = builder.Build(result);
            var form = new Form {ClientSize = new Size(800, 600)};
            chart.Dock = DockStyle.Fill;
            form.Controls.Add(chart);
            Application.Run(form);
        }

        [STAThread]
        public static void Main()
        {
            /*var arrayData = Generator.GenerateArrayRunner();
            var callData = Generator.GenerateCallRunner();
            var declarationData = Generator.GenerateDeclarations();*/
            // File.WriteAllText(@"C:\Users\Nikolay\Desktop\Ulearn_Projects\Profiling.csproj\codeCallData.cs", callData, Encoding.Default);
            // File.WriteAllText(@"C:\Users\Nikolay\Desktop\Ulearn_Projects\Profiling.csproj\codeArrayData.cs", arrayData, Encoding.Default);
            // File.WriteAllText(@"C:\Users\Nikolay\Desktop\Ulearn_Projects\Profiling.csproj\declarationData.cs", declarationData, Encoding.Default);

            //Run(new ArrayRunner(), new ProfilerTask(), 100000, new ChartBuilder());
            Run(new CallRunner(), new ProfilerTask(), 7000000, new ChartBuilder());
        }
    }
}