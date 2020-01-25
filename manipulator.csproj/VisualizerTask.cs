﻿using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Manipulation
{
	public static class VisualizerTask
	{
		public static double X = 220;
		public static double Y = -100;
		public static double Alpha = 0.05;
		public static double Wrist = 2 * Math.PI / 3;
		public static double Elbow = 3 * Math.PI / 4;
		public static double Shoulder = Math.PI / 2;

		public static Brush UnreachableAreaBrush = new SolidBrush(Color.FromArgb(255, 255, 230, 230));
		public static Brush ReachableAreaBrush = new SolidBrush(Color.FromArgb(255, 230, 255, 230));
		public static Pen ManipulatorPen = new Pen(Color.Black, 3);
		public static Brush JointBrush = Brushes.Gray;

		public static void KeyDown(Form form, KeyEventArgs key)
		{
			// TODO: Добавьте реакцию на QAWS и пересчитывать Wrist
			if (key.KeyCode == Keys.Q)
			{
				Shoulder += 0.1;
			}
			if (key.KeyCode == Keys.A)
			{
				Shoulder -= 0.05;

			}
			if (key.KeyCode == Keys.W)
			{
				Elbow += 0.05;
			}
			if (key.KeyCode == Keys.S)
			{
				Elbow -= 0.05;
			}

			Wrist = -Alpha - Shoulder - Elbow;
			form.Invalidate(); // 
		}


		public static void MouseMove(Form form, MouseEventArgs e)
		{
			// TODO: Измените X и Y пересчитав координаты (e.X, e.Y) в логические.
			var shoulderPos = new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
			var newPoint = ConvertWindowToMath(new PointF {X = e.X, Y = e.Y}, shoulderPos);
			X = newPoint.X;
			Y = newPoint.Y;
			UpdateManipulator();
			form.Invalidate();
		}

		public static void MouseWheel(Form form, MouseEventArgs e)
		{
			// TODO: Измените Alpha, используя e.Delta — размер прокрутки колеса мыши
			if (e.Delta != 0)
			{
				Alpha += e.Delta;
			}
			UpdateManipulator();
			form.Invalidate();
		}

		public static void UpdateManipulator()
		{
			// Вызовите ManipulatorTask.MoveManipulatorTo и обновите значения полей Shoulder, Elbow и Wrist, 
            // если они не NaN. Это понадобится для последней задачи.
            bool isNan = false;
            var mas= ManipulatorTask.MoveManipulatorTo(X, Y, Alpha);
            foreach (var angle in mas)
            {
	            if (angle == Double.NaN)
		            isNan = true;
            }

            if (!isNan)
            {
	             Shoulder = mas[0];
	             Elbow = mas[1];
	             Wrist = mas[2];
            }
		}

		public static void DrawManipulator(Graphics graphics, PointF shoulderPos)
		{
			var joints = AnglesToCoordinatesTask.GetJointPositions(Shoulder, Elbow, Wrist);

			graphics.DrawString(
                $"X={X:0}, Y={Y:0}, Alpha={Alpha:0.00}", 
                new Font(SystemFonts.DefaultFont.FontFamily, 12), 
                Brushes.DarkRed, 
                10,
                10);
			var elbowPointInWindow = ConvertMathToWindow(joints[0], shoulderPos);
			var wristPointInWindow = ConvertMathToWindow(joints[1], shoulderPos);
			var palmEndPointInWindow = ConvertMathToWindow(joints[2], shoulderPos);
			
			DrawReachableZone(graphics, ReachableAreaBrush, UnreachableAreaBrush, shoulderPos, joints);
			DrawLineAndEllipse(graphics, ManipulatorPen, shoulderPos, elbowPointInWindow);
			DrawLineAndEllipse(graphics, ManipulatorPen, elbowPointInWindow, wristPointInWindow);
			DrawLineAndEllipse(graphics, ManipulatorPen, wristPointInWindow, palmEndPointInWindow);
			// Нарисуйте сегменты манипулятора методом graphics.DrawLine используя ManipulatorPen.
			// Нарисуйте суставы манипулятора окружностями методом graphics.FillEllipse используя JointBrush.
			// Не забудьте сконвертировать координаты из логических в оконные
		}

		private static void DrawLineAndEllipse(Graphics graphics, Pen manipulatorPen, PointF first, PointF second)
		{
			graphics.DrawLine(ManipulatorPen,first.X, first.Y, second.X, second.Y);
			graphics.FillEllipse(JointBrush, second.X-5, second.Y-5, 10,10);

		}
		private static void DrawReachableZone(
            Graphics graphics, 
            Brush reachableBrush, 
            Brush unreachableBrush, 
            PointF shoulderPos, 
            PointF[] joints)
		{
			var rmin = Math.Abs(Manipulator.UpperArm - Manipulator.Forearm);
			var rmax = Manipulator.UpperArm + Manipulator.Forearm;
			var mathCenter = new PointF(joints[2].X - joints[1].X, joints[2].Y - joints[1].Y);
			var windowCenter = ConvertMathToWindow(mathCenter, shoulderPos);
			graphics.FillEllipse(reachableBrush, windowCenter.X - rmax, windowCenter.Y - rmax, 2 * rmax, 2 * rmax);
			graphics.FillEllipse(unreachableBrush, windowCenter.X - rmin, windowCenter.Y - rmin, 2 * rmin, 2 * rmin);
		}

		public static PointF GetShoulderPos(Form form)
		{
			return new PointF(form.ClientSize.Width / 2f, form.ClientSize.Height / 2f);
		}

		public static PointF ConvertMathToWindow(PointF mathPoint, PointF shoulderPos)
		{
			return new PointF(mathPoint.X + shoulderPos.X, shoulderPos.Y - mathPoint.Y);
		}

		public static PointF ConvertWindowToMath(PointF windowPoint, PointF shoulderPos)
		{
			return new PointF(windowPoint.X - shoulderPos.X, shoulderPos.Y - windowPoint.Y);
		}

	}
}