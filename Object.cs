using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    class Object
    {

    }
    public class Point
    {
        public int x, y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Enemy
    {
        public int x, y, Stt, Drt, Move, X, Y, Next, Move2, MaxMove2;
        public bool isAlive;
        public static int MAXEnemy = 6, t = 0;//Bien static
        public static Image[,] imgEnemy = new Image[,]
            { { Properties.Resources.EWU1, Properties.Resources.EWU2, Properties.Resources.EWU3 }
            , { Properties.Resources.EWR1, Properties.Resources.EWR2, Properties.Resources.EWR3 }
            , { Properties.Resources.EWD1, Properties.Resources.EWD2, Properties.Resources.EWD3 }
            , { Properties.Resources.EWL1, Properties.Resources.EWL2, Properties.Resources.EWL3 } };
        public static int cntEnemy = 0, curEnemy = 0;//Bien static

        public Enemy()
        {
            isAlive = false;
        }

        public void Init(int x, int y)
        {
            X = x;
            Y = y;
            GetRdDrt();
            this.x = 48 * x + Form1.StartX - 4;
            this.y = 48 * y + Form1.StartY - 6; // Trừ bớt 2 để ko bị chạm vào cây
            Stt = 0;
            Move = 0;
        }

        public void GetRdDrt()
        {
            Move2 = 0;
            MaxMove2 = Form1.r.Next(5) + 3;
            do
            {
                Drt = Form1.r.Next(4);
            } while (Form1.Map[Y + Form1.Drts[Drt].y, X + Form1.Drts[Drt].x] <= Form1.typeWall);
        }

        public string getDirectString()
        {
            if (Drt == 0) return "Go up";
            else if (Drt == 1) return "Turn Right";
            else if (Drt == 2) return "Go Down";
            else  return "Turn Left";
        }
    }


    public class Bullet
    {
        public static int[,] offset = new int[4, 2] { { -17, -28 }, { -8, -17 }, { -17, -8 }, { -28, -17 } };
        public int x, y, mx, my;
        public int Drt, Next;
        public bool isAlive;
        public Image img;
        public static Image[] imgBull = new Image[] { Properties.Resources.BulletUp, Properties.Resources.BulletRight, Properties.Resources.BulletUp, Properties.Resources.BulletRight };

        public Bullet()
        {
            isAlive = false;
        }

        public void init(int x, int y, int direction)
        {
            isAlive = true;
            Drt = direction;
            this.x = x + offset[Drt, 0];
            this.y = y + offset[Drt, 1];
            img = imgBull[Drt];
            mx = 12 * Form1.Drts[Drt].x;
            my = 12 * Form1.Drts[Drt].y;
        }
    }

    public class Item
    {
        public int x, y, Type, offset;
        public bool isAlive;
        public Image img;
        public static int MAXType = 4, MAXItem = 6;
        private static Image[] imgItem = new Image[] { Properties.Resources.ItemAmmo, Properties.Resources.ItemHp, Properties.Resources.ItemApple, Properties.Resources.ItemShield };
        public static int cntItem = 0, curItem = 0;

        public Item()
        {
            isAlive = false;
        }

        public void Init(int x, int y, int type)
        {
            offset = 5;
            this.x = x * 48 + Form1.StartX;
            this.y = y * 48 + Form1.StartY;
            isAlive = true;
            Type = type;
            img = imgItem[Type];
        }
    }


}
