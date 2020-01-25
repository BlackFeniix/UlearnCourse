namespace Mazes
{
    public static class DiagonalMazeTask
    {
        public static void MoveToXInSeries(Robot robot, int stepsForX)
        {
            for (int i = 0; i < stepsForX; i++)
                robot.MoveTo(Direction.Right);
        }

        public static void MoveToYInSeries(Robot robot, int stepsForY)
        {
            for (int i = 0; i < stepsForY; i++)
                robot.MoveTo(Direction.Down);
        }

        public static void MoveToExit(Robot robot, int distanceX, int distanceY, int stepsForX, int stepsForY)
        {
            while (robot.X != distanceX && robot.Y != distanceY)
            {
                if (stepsForX > stepsForY)
                {
                    MoveToXInSeries(robot, stepsForX);
                    robot.MoveTo(Direction.Down);
                }
                else
                {
                    MoveToYInSeries(robot, stepsForY);
                    robot.MoveTo(Direction.Right);
                }
            }
            if (stepsForX > stepsForY)
                MoveToXInSeries(robot, stepsForX);
            else
                MoveToYInSeries(robot, stepsForY);
        }

        public static void MoveOut(Robot robot, int width, int height)
		{
            width -= 3;
            height -= 3;
            int stepsForX;
            int stepsForY;
            if (width/height>0)
            {
                stepsForX = width / height;
                stepsForY = 1;
            }
            else
            {
                stepsForY = height / width;
                stepsForX = 1;
            }
            MoveToExit(robot, width+1, height+1, stepsForX, stepsForY);
        }
	}
}