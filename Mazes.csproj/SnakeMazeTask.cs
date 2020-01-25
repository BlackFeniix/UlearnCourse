namespace Mazes
{
	public static class SnakeMazeTask
	{
        private static void MoveToRightOrLeft(Robot robot, int stepsForX, bool direction)
        {
            for (int i = 0; i < stepsForX; i++)
            {
                if (direction)
                    robot.MoveTo(Direction.Right);
                else
                    robot.MoveTo(Direction.Left);
            }
        }

        private static void MoveToBottomSide(Robot robot, int stepsForY, int StepsForX)
        {
            bool directionX = true;
            for (int i = 0; i < stepsForY; i+=2)
            {
                MoveToRightOrLeft(robot, StepsForX, directionX);
                robot.MoveTo(Direction.Down);
                robot.MoveTo(Direction.Down);
                directionX = !directionX;
            }
            MoveToRightOrLeft(robot, StepsForX, directionX);
        }

        public static void MoveOut(Robot robot, int width, int height)
		{
            MoveToBottomSide(robot, height - 3, width - 3);
		}
	}
}