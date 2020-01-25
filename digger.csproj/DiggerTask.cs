using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие.
    public class Terrain : ICreature
    {
        public int X, Y;
        
        private string imageFileName="Terrain.png";
        private int drawingPriority=1;
        private bool deadInConflict =true;
        public string GetImageFileName()
        {
           return imageFileName;
        }

        public int GetDrawingPriority()
        {
            return  drawingPriority;
        }

        public CreatureCommand Act(int x, int y)
        {
            return  new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return deadInConflict;
        }
    }

    public class Player : ICreature
    {
        private string imageFileName="Digger.png";
        private int drawingPriority = 2;
        private bool deadInConflict =false;

        public string GetImageFileName()
        {
            return imageFileName;
        }

        public int GetDrawingPriority()
        {
            return  drawingPriority;
        }

        public CreatureCommand Act(int x, int y)
        {
            //
            var player= new CreatureCommand();
            var key = Game.KeyPressed;
            
            if (key == Keys.Up && y-1>=0 && !(Game.Map[x, y-1] is Sack))
            {
                player.DeltaY = -1;
            }
            
            if (key == Keys.Down && y+1<Game.MapHeight && !(Game.Map[x, y+1] is Sack))
            {
                player.DeltaY = 1;
            }
            if (key == Keys.Left && x-1>=0 && !(Game.Map[x - 1, y] is Sack))
            {
                player.DeltaX = -1;
            }
            if (key == Keys.Right && x+1<Game.MapWidth && !(Game.Map[x + 1, y] is Sack))
            {
                player.DeltaX = 1;
            }
            return player;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return (conflictedObject is Sack || conflictedObject is Monster) || deadInConflict;
        }
    }

    public class Sack : ICreature
    {
        private string imageFileName="Sack.png";
        private int drawingPriority = 2;
        private bool deadInConflict =false;
        private int itsFall = 0;
        public string GetImageFileName()
        {
            return imageFileName;
        }

        public int GetDrawingPriority()
        {
            return drawingPriority;
        }

        public CreatureCommand Act(int x, int y)
        {
            var sack=new CreatureCommand();
            if ( y + 1 < Game.MapHeight && Game.Map[x, y+1]==null)
            {
                itsFall++;
                sack.DeltaY = 1;
                return new CreatureCommand() {DeltaY = 1};
            }

            if ( y + 1 < Game.MapHeight && ((Game.Map[x, y+1] is Player || Game.Map[x, y+1] is Monster) &&
                                            itsFall > 0 || Game.Map[x, y+1]==null))
            {
                itsFall++;
                return  new CreatureCommand() {DeltaY = 1};
            }
            else
            {
                if (itsFall > 1)
                {
                    itsFall = 0;
                    sack.TransformTo = new Gold();
                }
                else
                {
                    itsFall = 0;
                    return new CreatureCommand();
                }
            }
            
            if (itsFall > 1 && (y + 1 < Game.MapHeight && Game.Map[x, y + 1] != null || y + 1 == Game.MapHeight)) {
                sack.TransformTo = new Gold();
            } 
            return sack;
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return deadInConflict;
        }
    }

    public class Gold : ICreature
    {
        private string imageFileName="Gold.png";
        private int drawingPriority = 2;
        private bool deadInConflict =false;
        public string GetImageFileName()
        {
            return imageFileName;
        }

        public int GetDrawingPriority()
        {
            return drawingPriority;
        }

        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            switch (conflictedObject)
            {
                case Player _:
                    Game.Scores += 10;
                    return true;
                case Monster _:
                    return true;
                default:
                    return deadInConflict;
            }
        }
    }

    public class Monster : ICreature
    {
        private string imageFileName="Monster.png";
        private int drawingPriority = 2;
        private bool deadInConflict =false;

        private int offsetX;
        private int offsetY;
        private int[] FindDigger()
        {
            for (var i = 0; i < Game.MapWidth; i++)
            for (var j = 0; j <Game.MapHeight; j++)
            {
                if (Game.Map[i, j] is Player)
                    return new int[] { i, j};
            }
            return null;
        }
        public string GetImageFileName()
        {
            return imageFileName;
        }

        public int GetDrawingPriority()
        {
            return drawingPriority;
        }

        public CreatureCommand Act(int x, int y)
        {
            var monster=new CreatureCommand();
            var diggerLocation = FindDigger();
            if (diggerLocation!=null)
            {
                offsetX = diggerLocation[0] - x;
                offsetY = diggerLocation[1] - y;
            }
            
            if (diggerLocation != null && HasOpportunityToWalk(x, y))
            {
                return Walk();
            }
            else
            {
                return Idle();
            }
        } 
        
        private bool HasOpportunityToWalk(int x, int y) 
        {
            return CheckTop(x, y) || CheckBottom(x, y) ||
                   CheckLeft(x, y) || CheckRight(x, y);
        }

        private bool CheckTop(int x, int y)
        {
            return y - 1 >= 0 && offsetY != 0 && CheckNextCreature(x, y - 1, 0, -1);
        }

        private bool CheckBottom(int x, int y)
        {
            return y + 1 < Game.MapHeight && offsetY != 0 && CheckNextCreature(x, y + 1, 0, 1);
        }

        private bool CheckLeft(int x, int y)
        {
            return x - 1 >= 0 && offsetX != 0 && CheckNextCreature(x - 1, y, -1, 0);
        }

        private bool CheckRight(int x, int y)
        {
            return x + 1 < Game.MapWidth && offsetX != 0 && CheckNextCreature(x + 1, y, 1, 0);
        }

        private bool CheckNextCreature(int x, int y, int dx, int dy)
        {
            var nextObject = Game.Map[x, y];
            if (nextObject == null || nextObject is Gold || nextObject is Player)
            {
                offsetX = dx;
                offsetY = dy;
                return true;
            }
            
            return false;
        }

        private CreatureCommand Walk()
        {
            if (offsetX != 0)
            {
                return new CreatureCommand() { DeltaX = offsetX };
            }
            else if (offsetY != 0)
            {
                return new CreatureCommand() { DeltaY = offsetY };
            }
            else
            {
                return new CreatureCommand();
            }
        }

        private CreatureCommand Idle()
        {
            return new CreatureCommand();
        }
        
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject is Monster)
                return true;
            if (conflictedObject is Sack)
                return true;
            return deadInConflict;
        }
    }
}
