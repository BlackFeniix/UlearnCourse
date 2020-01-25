using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class ManipulatorTask
    {

        /// <summary>
        /// Возвращает массив углов (shoulder, elbow, wrist),
        /// необходимых для приведения эффектора манипулятора в точку x и y 
        /// с углом между последним суставом и горизонталью, равному angle (в радианах)
        /// См. чертеж manipulator.png!
        /// </summary>
        public static double[] MoveManipulatorTo(double x, double y, double angle)
        {
            // Используйте поля Forearm, UpperArm, Palm класса Manipulator
            var wristPoint = new PointF {X = (float) (x - Math.Sin(angle)), Y = (float) (y - Math.Cos(angle))};
            var sholderWristSide = Math.Sqrt(wristPoint.X * wristPoint.X + wristPoint.Y * wristPoint.Y);
            
            var elbowAngle = TriangleTask.GetABAngle(Manipulator.UpperArm, Manipulator.Forearm, sholderWristSide);
            var shoulderAngle = TriangleTask.GetABAngle(Manipulator.UpperArm, sholderWristSide, Manipulator.Forearm) +
                                Math.Atan2(wristPoint.Y, wristPoint.X);
            var wristAngle = -angle - elbowAngle - shoulderAngle;
            if (double.IsNaN(wristAngle) || double.IsNaN(elbowAngle) || double.IsNaN(shoulderAngle))
                return new[] { double.NaN, double.NaN, double.NaN };
            return new[] {shoulderAngle, elbowAngle, wristAngle};
        }
    }

    [TestFixture]
    public class ManipulatorTask_Tests
    {
        [TestCase(15, 14, Math.PI/2, new double[] {Double.NaN, Double.NaN, Double.NaN})]
        public void TestMoveManipulatorTo(double x, double y, double angle, double[] angleMas)
        {
            Assert.AreEqual(ManipulatorTask.MoveManipulatorTo(x,y,angle), angleMas);
        }
    }
}