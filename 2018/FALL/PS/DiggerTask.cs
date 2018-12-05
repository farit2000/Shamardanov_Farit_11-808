using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Digger
{
    //Напишите здесь классы Player, Terrain и другие. 
    class Terrain : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return true;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Terrain.png";
        }
    }

    class Player : ICreature
    {
        public static int XPosition;
        public static int YPosition;
        public CreatureCommand Act(int x, int y)
        {
            XPosition = x;
            YPosition = y;
            int dX = 0;
            int dY = 0;
            if (Game.KeyPressed == Keys.Left && x >= 1)
                dX = -1;
            else if (Game.KeyPressed == Keys.Up && y >= 1)
                dY = -1;
            else if (Game.KeyPressed == Keys.Down && y < Game.MapHeight - 1)
                dY = 1;
            else if (Game.KeyPressed == Keys.Right && x < Game.MapWidth - 1)
                dX = 1;
            if (!(x + dX >= 0 && x + dX < Game.MapWidth &&
            y + dY >= 0 && y + dY < Game.MapHeight))
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };

            if (Game.Map[x + dX, y + dY] != null)
            {
                if (Game.Map[x + dX, y + dY].ToString() == "Digger.Sack")
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            }
            return new CreatureCommand() { DeltaX = dX, DeltaY = dY };
        }
        //проверка на то что в одной клетке находятся и диггер и мешок 
        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "Digger.Gold")
                Game.Scores += 10;
            if (conflictedObject.ToString() == "Digger.Sack" ||
            conflictedObject.ToString() == "Digger.Monster")
            {
                return true;
            }
            return false;
        }

        public int GetDrawingPriority()
        {
            return 2;
        }
        //поле для определения последнего положения(вращения) диггера
        public static string lastRot = "DiggerR.png";
        public string GetImageFileName()
        {
            if (Game.KeyPressed == Keys.Left)
            {
                lastRot = "DiggerL.png";
                return "DiggerL.png";
            }
            else if (Game.KeyPressed == Keys.Up)
            {
                lastRot = "DiggerU.png";
                return "DiggerU.png";
            }
            else if (Game.KeyPressed == Keys.Down)
            {
                lastRot = "DiggerD.png";
                return "DiggerD.png";
            }
            else if (Game.KeyPressed == Keys.Right)
            {
                lastRot = "DiggerR.png";
                return "DiggerR.png";
            }
            else return lastRot;
        }
    }

    class Sack : ICreature
    {
        public int CellCounter = 0;
        //метод реализующий поведение мешков 
        public CreatureCommand Act(int x, int y)
        {
            if (y < Game.MapHeight - 1)
            {
                if (Game.Map[x, y + 1] == null ||
                (CellCounter > 0 && (Game.Map[x, y + 1].ToString() == "Digger.Player"
                || (Game.Map[x, y + 1].ToString() == "Digger.Monster"))))
                {
                    CellCounter++;
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
            }
            if (CellCounter > 1)
            {
                //делаем обнуление что бы при повторном падении мешок не убивал сразу 
                CellCounter = 0;
                return new CreatureCommand() { DeltaX = 0, DeltaY = 0, TransformTo = new Gold() };
            }
            //делаем обнуление что бы при повторном падении мешок не убивал сразу 
            CellCounter = 0;
            return new CreatureCommand() { DeltaX = 0, DeltaY = 0 };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return false;
        }

        public int GetDrawingPriority()
        {
            return 1;
        }

        public string GetImageFileName()
        {
            return "Sack.png";
        }
    }
    //метод поведения золота 
    class Gold : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            return new CreatureCommand();
        }
        //проверка что в одной клетке находиться я диггер и золото 
        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.ToString() == "Digger.Player" ||
            conflictedObject.ToString() == "Digger.Monster";
        }

        public int GetDrawingPriority()
        {
            return 5;
        }

        public string GetImageFileName()
        {
            return "Gold.png";
        }
    }

    class Monster : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            int deltaX = 0;
            int deltaY = 0;
            if (CheckDiggerOnMap())
            {
                MonsterMove(x, y, ref deltaX, ref deltaY);
            }
            else return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            //проверка на выход за пределы карты
            if (!(x + deltaX >= 0 && x + deltaX < Game.MapWidth &&
            y + deltaY >= 0 && y + deltaY < Game.MapHeight))
                return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            //проверка на столкновение с элементами
            if (Game.Map[x + deltaX, y + deltaY] != null)
                if (Game.Map[x + deltaX, y + deltaY].ToString() == "Digger.Terrain" ||
                Game.Map[x + deltaX, y + deltaY].ToString() == "Digger.Sack" ||
                Game.Map[x + deltaX, y + deltaY].ToString() == "Digger.Monster")
                    return new CreatureCommand { DeltaX = 0, DeltaY = 0 };
            return new CreatureCommand() { DeltaX = deltaX, DeltaY = deltaY };
        }
        //метод передвижения монстера
        private static void MonsterMove(int x, int y, ref int deltaX, ref int deltaY)
        {
            if (Player.XPosition == x)
            {
                if (Player.YPosition < y) deltaY = -1;
                else if (Player.YPosition > y) deltaY = 1;
            }
            else if (Player.YPosition == y)
            {
                if (Player.XPosition < x) deltaX = -1;
                else if (Player.XPosition > x) deltaX = 1;
            }
            else
            {
                if (Player.XPosition < x) deltaX = -1;
                else if (Player.XPosition > x) deltaX = 1;
            }
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            return conflictedObject.ToString() == "Digger.Sack" ||
            conflictedObject.ToString() == "Digger.Monster" || conflictedObject.ToString() == "Digger.Bullet";
        }

        public int GetDrawingPriority()
        {
            return 0;
        }

        public string GetImageFileName()
        {
            return "Monster.png";
        }
        //метод проверки диггера на карте
        static public bool CheckDiggerOnMap()
        {
            for (int i = 0; i < Game.MapWidth; i++)
                for (int j = 0; j < Game.MapHeight; j++)
                {
                    if (Game.Map[i, j] != null)
                    {
                        if (Game.Map[i, j].ToString() == "Digger.Player")
                        {
                            //если находим диггера присваиваем его кординаты
                            Player.XPosition = i;
                            Player.YPosition = j;
                            return true;
                        }
                    }
                }
            return false;
        }
    }

    class Bullet : ICreature
    {
        public CreatureCommand Act(int x, int y)
        {
            if (Game.KeyPressed == Keys.Space)
            {
                if ((Game.Map[x + 1, y] == null ||
                (Game.Map[x + 1, y].ToString() == "Digger.Monster")) && Player.lastRot == "DiggerR.png")
                {
                    return new CreatureCommand() { DeltaX = 1, DeltaY = 0 };
                }
                else if ((Game.Map[x - 1, y] == null ||
                (Game.Map[x - 1, y].ToString() == "Digger.Monster")) && Player.lastRot == "DiggerL.png")
                {
                    return new CreatureCommand() { DeltaX = -1, DeltaY = 0 };
                }
                else if ((Game.Map[x, y + 1] == null ||
                (Game.Map[x, y + 1].ToString() == "Digger.Monster")) && Player.lastRot == "DiggerD.png")
                {
                    return new CreatureCommand() { DeltaX = 0, DeltaY = 1 };
                }
                else if ((Game.Map[x, y - 1] == null ||
                (Game.Map[x, y - 1].ToString() == "Digger.Monster")) && Player.lastRot == "DiggerU.png")
                {
                    return new CreatureCommand() { DeltaX = 0, DeltaY = -1 };
                }
                return new CreatureCommand() { };
            }
            return new CreatureCommand() { };
        }

        public bool DeadInConflict(ICreature conflictedObject)
        {
            if (conflictedObject.ToString() == "Digger.Monster")
                return true;
            else return false;
        }

        public int GetDrawingPriority()
        {
            return 10;
        }

        public string GetImageFileName()
        {
            return "Bullet.png";
        }
    }
}