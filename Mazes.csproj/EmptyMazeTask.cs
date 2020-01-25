namespace Mazes
{
	public static class EmptyMazeTask
	{
        private static void MoveToRightSide(Robot robot, int steps)
        {
            for (int i = 0; i < steps; i++)
                robot.MoveTo(Direction.Right);
        }

        private static void MoveToBottomSide(Robot robot, int steps)
        {
            for (int i = 0; i < steps; i++)
                robot.MoveTo(Direction.Down);
        }

        public static void MoveOut(Robot robot, int width, int height)
		{
            int destinationX = width - 3;
            int destinationY = height - 3;
            MoveToRightSide(robot, destinationX);
            MoveToBottomSide(robot, destinationY);
        }
	}
}