using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace Profiling
{
    class ChartBuilder : IChartBuilder
    {
        public Control Build(List<ExperimentResult> result)
        {
            var control = new ZedGraphControl();
            var pane = control.GraphPane;

            var structList = new PointPairList();
            var classList = new PointPairList();

            foreach (var measurement in result)
            {
                structList.Add(measurement.Size, measurement.StructResult);
                classList.Add(measurement.Size, measurement.ClassResult);
            }

            pane.CurveList.Clear();
            var structCurve = pane.AddCurve("Struct Graph", structList, Color.Aqua, SymbolType.None);
            var classCurve = pane.AddCurve("Class Graph", classList, Color.Black, SymbolType.Plus);

            control.AxisChange();
            control.Invalidate();
            return control;
        }
    }
}