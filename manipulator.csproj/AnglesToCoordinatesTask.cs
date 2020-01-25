using System;
using System.Drawing;
using NUnit.Framework;

namespace Manipulation
{
    public static class AnglesToCoordinatesTask
    {
        /// <summary>
        /// По значению углов суставов возвращает массив координат суставов
        /// в порядке new []{elbow, wrist, palmEnd}
        /// </summary>
        public static PointF[] GetJointPositions(double shoulder, double elbow, double wrist)
        {
            //cos ОСТРОГО угла
            var elbowPos = new PointF((float) Math.Cos(shoulder) * (float) Manipulator.UpperArm,
                (float) Math.Sin(shoulder) * (float) Manipulator.UpperArm);
            var wristPos = new PointF(
                elbowPos.X + (float) Math.Cos(elbow + shoulder - Math.PI) * (float) Manipulator.Forearm,
                elbowPos.Y + (float) Math.Sin(elbow + shoulder - Math.PI) * (float) Manipulator.Forearm);
            var palmEndPos = new PointF(
                wristPos.X + (float) Math.Cos(wrist + elbow + shoulder - 2 * Math.PI) * (float) Manipulator.Palm,
                wristPos.Y + (float) Math.Sin(wrist + elbow + shoulder - 2 * Math.PI) * (float) Manipulator.Palm);
            return new PointF[]
            {
                elbowPos,
                wristPos,
                palmEndPos
            };
        }
    }
    [TestFixture]
    public class AnglesToCoordinatesTask_Tests
    {
        // Доработайте эти тесты!
        // С помощью строчки TestCase можно добавлять новые тестовые данные.
        // Аргументы TestCase превратятся в аргументы метода.
        [TestCase(Math.PI / 2, Math.PI / 2, Math.PI, Manipulator.Forearm + Manipulator.Palm, Manipulator.UpperArm)]
        [TestCase(0, 0, 0, 90.0f, 0)]
        public void TestGetJointPositions(double shoulder, double elbow, double wrist, double palmEndX, double palmEndY)
        {
            var joints = AnglesToCoordinatesTask.GetJointPositions(shoulder, elbow, wrist);
            Assert.AreEqual(palmEndX, joints[2].X, 1e-5, "palm endX");
            Assert.AreEqual(palmEndY, joints[2].Y, 1e-5, "palm endY");
            //Assert.Fail("TODO: проверить, что расстояния между суставами равны длинам сегментов манипулятора!");
        }
    }
}