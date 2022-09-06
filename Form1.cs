using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public const int StartX = -24, StartY = 48, StartXC = StartX + 24, StartYC = StartY + 24;//Start XC va YC danh cho nhan vat
        public static Random r = new Random();
        public static int[,] Map = new int[,]
            { { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 }
            , { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }
            , { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }
            , { 0, 2, 2, 0, 2, 2, 2, 2, 2, 2, 2, 0, 2, 2, 2, 2, 0 }
            , { 0, 2, 2, 2, 2, 2, 1, 2, 1, 2, 2, 2, 2, 0, 2, 2, 0 }
            , { 0, 2, 2, 2, 1, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 2, 0 }
            , { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }
            , { 0, 2, 2, 2, 1, 1, 1, 2, 1, 1, 1, 2, 2, 2, 2, 1, 0 }
            , { 0, 2, 0, 2, 2, 2, 1, 2, 1, 2, 2, 2, 0, 2, 2, 2, 0 }
            , { 0, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }
            , { 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 0 }
            , { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 } };
        public static int typeWall = 1, cntESP = 0;
        public static Point[] Drts = new Point[] { new Point(0, -1), new Point(1, 0), new Point(0, 1), new Point(-1, 0) };// cua enemy
        private static int SpawnStt, tSpawn, xSpawn, ySpawn;
        private static int HardLevel;




        //======================================Main Hero======================================
        private static bool isPressedArrow, isMove, isImmunity, isHasShield, isDraw, isDead;
        private static Image img;
        private static Image[,] imageWalk = {//4 direction thai: len xuong trai phai,moi direction co 8 trang thai dat trong loop8
           { Properties.Resources.StandUp,Properties.Resources.WalkUp1,Properties.Resources.WalkUp2,Properties.Resources.WalkUp1,Properties.Resources.StandUp,Properties.Resources.WalkUp3,Properties.Resources.WalkUp4,Properties.Resources.WalkUp3 },
           { Properties.Resources.StandRight,Properties.Resources.WalkRight1,Properties.Resources.WalkRight2,Properties.Resources.WalkRight1,Properties.Resources.StandRight,Properties.Resources.WalkRight3,Properties.Resources.WalkRight4,Properties.Resources.WalkRight3 },
           { Properties.Resources.StandDown,Properties.Resources.WalkDown1,Properties.Resources.WalkDown2,Properties.Resources.WalkDown1,Properties.Resources.StandDown,Properties.Resources.WalkDown3,Properties.Resources.WalkDown4,Properties.Resources.WalkDown3 },
           { Properties.Resources.StandLeft,Properties.Resources.WalkLeft1,Properties.Resources.WalkLeft2,Properties.Resources.WalkLeft1,Properties.Resources.StandLeft,Properties.Resources.WalkLeft3,Properties.Resources.WalkLeft4,Properties.Resources.WalkLeft3 },
        };

        private static int x, y, Drt, Stt, SttDead;
        private static int curExp = 0, maxExp = 5, Level = 1, curHp = 6, maxHp = 6;
        private static Timer tDead = new Timer() { Interval = 100 };
        private static int curBull, cntBull, MAXBull, timeImmu, tIM;
        private static int MAXBULLET = 6, MAXIMMU = 16;
        private static Bullet[] Bull;
        private delegate bool CheckCollision(int x, int y);
        private static CheckCollision[] CheckBullet = new CheckCollision[] { new CheckCollision(CheckCollisionUp), new CheckCollision(CheckCollisionRight), new CheckCollision(CheckCollisionDown), new CheckCollision(CheckCollisionLeft) };
        private static int xLU, yLU, sLU, tLU;
        private static bool isLU;
        private static int enk;



        //======================================Enemy======================================
        private static Point[] ESP = new Point[150];
        private static int tSpawnEnemy, levelEnemy, damEnemy, timeEnemy, tEnemy;
        private static Enemy[] enemy;
        // enemy Spawn status 
        private static Image[] imgSpawn = new Image[] { Properties.Resources.Spawn1, Properties.Resources.Spawn2, Properties.Resources.Spawn3, Properties.Resources.Spawn2, Properties.Resources.Spawn3, Properties.Resources.Spawn4 };


        //======================================Init map graphic=========================================
        private static Image imgBG = Properties.Resources.BG, imgTopTree = Properties.Resources.TopTree;//backgorund image
        private static Image[] imgWall = new Image[] { Properties.Resources.DownTree, Properties.Resources.Stump };//nhan vat khong di qua duoc
        private static int[] xTree = new int[10], yTree = new int[10];
        private static Bitmap bmp;
        private static Graphics g, G;//do hoa de map vao picturebox
        //control
        private static Keys[] Key = new Keys[4] { Keys.Up, Keys.Right, Keys.Down, Keys.Left };//len,sang phai,xuong,sang trai
        private static Action[] Action_Move, Action_EatItem;
        //-------------------------------
        //cordinate of all object :hero,enemy
        private static int t, X, Y, X1, Y1, X2, Y2, tmpInt, tmpX, tmpY, t2, t3, tmpInt2;
        //Loop event 
        private static int[] Loop8 = new int[] { 1, 2, 3, 4, 5, 6, 7, 0 }, Loop3 = new int[] { 1, 2, 0 }, Loop4 = new int[] { 1, 2, 3, 0 }, Loop5 = new int[] { 1, 2, 3, 4, 0 };//loop8 cua nhan vat,loop4 cua enemy,Loop5 trang thai spawn
        //continue tree
        private static int cntTree = 0, tSpawnItem;
        private static Point[] points = new Point[150]; //10x15 o vuong trong Map[,] 
        //Character UI
        private static System.Drawing.Point pntLevel = new System.Drawing.Point(104, 67);
        //trạng thái levelup
        private static Image[] imgLU = new Image[] { Properties.Resources.LevelUp1, Properties.Resources.LevelUp2, Properties.Resources.LevelUp3, Properties.Resources.LevelUp4, Properties.Resources.LevelUp5, Properties.Resources.LevelUp6 };

        private static int[] numTextHp, numTextExp, numTextMaxHp, numTextMaxExp, xTextHp, xTextMaxHp;
        private static Font fntLevel = new Font("Microsoft Sans Serif", 9.75F);
        private static Brush br = new SolidBrush(Color.FromArgb(236, 29, 42)), br2 = new SolidBrush(Color.FromArgb(40, 173, 85));
        private static Rectangle rectHp = new Rectangle() { X = 102, Y = 23, Width = 195, Height = 10 }, rectExp = new Rectangle() { X = 102, Y = 47, Width = 0, Height = 10 };
        private static int xSlash, ySlashHp, ySlashExp, yTextHp, yTextExp;
        private static Image[] imgNum = new Image[] { Properties.Resources.Num0, Properties.Resources.Num1, Properties.Resources.Num2, Properties.Resources.Num3, Properties.Resources.Num4, Properties.Resources.Num5, Properties.Resources.Num6, Properties.Resources.Num7, Properties.Resources.Num8, Properties.Resources.Num9 };
        private static Image imgSlash = Properties.Resources.Slash;


        //HỆ THÔNG ITEM
        private static Item[] ItemSpawn;
        private static List<Point> ListSpawn = new List<Point>();
        public static int[,] MapItem = new int[12, 17];
        private static int[] nexItem = new int[] { 1, 2, 3, 4, 5, 0 };


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }



        public Form1()
        {
            InitializeComponent();
            this.timer1.Enabled = true;
            int levelForm2 = Form2.level;
            HardLevel = levelForm2;
            switch (levelForm2)
            {
                case 0:
                    timer1.Interval = 30;
                    lbLevel.Text = "Level: Easy";
                    break;
                case 1:
                    timer1.Interval = 20;
                    lbLevel.Text = "Level: Normal";

                    break;
                case 2:
                    timer1.Interval = 10;
                    lbLevel.Text = "Level: Hard";

                    break;
            }
            numTextHp = new int[3];
            numTextMaxHp = new int[3];
            numTextExp = new int[3];
            numTextMaxExp = new int[3];
            numTextHp[0] = curHp / 100;
            numTextHp[1] = (curHp / 10) % 10;
            numTextHp[2] = curHp % 10;
            numTextMaxHp[0] = maxHp / 100;
            numTextMaxHp[1] = (maxHp / 10) % 10;
            numTextMaxHp[2] = maxHp % 10;
            numTextExp[0] = 0;
            numTextExp[1] = 0;
            numTextExp[2] = 0;
            numTextMaxExp[0] = maxExp / 100;
            numTextMaxExp[1] = (maxExp / 10) % 10;
            numTextMaxExp[2] = maxExp % 10;
            xTextHp = new int[3]; xTextMaxHp = new int[3];
            for (int i = 0; i < 3; i++) xTextHp[i] = 151 + 13 * i;
            xSlash = xTextHp[2] + 13;
            for (int i = 0; i < 3; i++) xTextMaxHp[i] = xSlash + 12 + 13 * i;
            ySlashHp = 18; yTextHp = 19;
            ySlashExp = 42; yTextExp = 43;
            timeEnemy = 30;
            tEnemy = 0;
            damEnemy = 2;
            levelEnemy = 1;
            SpawnStt = 6;
            tSpawn = 0;
            InitMyHero(1, 1);
            isDead = false;
            enemy = new Enemy[6];
            for (int i = 0; i < 6; i++)
            {
                enemy[i] = new Enemy();
                enemy[i].Next = i + 1;
            }
            enemy[5].Next = 0;
            tSpawnEnemy = 0;
            tSpawnItem = 0;

            Action_Move = new Action[4];
            //Action_Move: tinh toan lai x va y qua 4 di chuyen
            Action_Move[0] = Move_Up;
            Action_Move[1] = Move_Right;
            Action_Move[2] = Move_Down;
            Action_Move[3] = Move_Left;
            //Action_EatItem: cho nhan vat su dung vat pham
            Action_EatItem = new Action[4];
            Action_EatItem[0] = BuffAmo;
            Action_EatItem[1] = BuffHP;
            Action_EatItem[2] = Increease_Exp;
            Action_EatItem[3] = BuffShield;
            //map background vao pictureBox
            bmp = new Bitmap(imgBG);
            g = Graphics.FromImage(bmp);
            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 16; j++)
                {
                    if (Map[i, j] == 0) //load 1 nua than cay va luu vi tri cua cay de them phan dinh
                    {
                        g.DrawImage(imgWall[0], 48 * j + StartX, 48 * i + StartY, 48, 48);
                        xTree[cntTree] = 48 * j + StartX;
                        yTree[cntTree] = 48 * i + StartY - 48;
                        cntTree++;
                    }
                    else if (Map[i, j] == 1)//load goc cay
                    {
                        g.DrawImage(imgWall[1], 48 * j + StartX, 48 * i + StartY, 48, 48);
                    }


                    else//tim khoang trong (Map[x,y]=2) de load item hoac enemy
                    {
                        ListSpawn.Add(new Point(j, i));
                        ESP[cntESP] = new Point(j, i);//them khoang trong vao mang enemy
                        cntESP++;//continue add
                    }
                }
            }

            pictureBox1.BackgroundImage = bmp;
            pictureBox1.Paint += pictureBox1_Paint;
            KeyDown += Form1_KeyDown;//them su kien nhan phim
            KeyUp += Form1_KeyUp;//them su kien nha phim

            ItemSpawn = new Item[Item.MAXItem];
            for (int i = 0; i < Item.MAXItem; i++) ItemSpawn[i] = new Item();

        }

        //REDRAW CHO HỆ THỐNG
        private void pictureBox1_Paint(object sender, PaintEventArgs e)//bat dau ve vao picturebox
        {
            G = e.Graphics;
            //Vẽ giao diện nhân vật
            G.DrawString(Level.ToString("00"), fntLevel, Brushes.Aqua, pntLevel);
            G.FillRectangle(br, rectHp);
            G.FillRectangle(br2, rectExp);
            for (int i = 0; i < 3; i++)
            {
                G.DrawImage(imgNum[numTextHp[i]], xTextHp[i], yTextHp, 15, 18);
                G.DrawImage(imgNum[numTextExp[i]], xTextHp[i], yTextExp, 15, 18);
                G.DrawImage(imgNum[numTextMaxHp[i]], xTextMaxHp[i], yTextHp, 15, 18);
                G.DrawImage(imgNum[numTextMaxExp[i]], xTextMaxHp[i], yTextExp, 15, 18);
            }
            G.DrawImage(imgSlash, xSlash, ySlashHp, 13, 19);
            G.DrawImage(imgSlash, xSlash, ySlashExp, 13, 19);


            //VẼ ITEM
            if (Item.cntItem > 0) for (int i = 0; i < Item.MAXItem; i++) if (ItemSpawn[i].isAlive) G.DrawImage(ItemSpawn[i].img, ItemSpawn[i].x, ItemSpawn[i].y - ItemSpawn[i].offset, 48, 48);
            //ve trang thai spawn
            if (SpawnStt < 6) G.DrawImage(imgSpawn[SpawnStt], xSpawn, ySpawn, 48, 48);

            if (Enemy.cntEnemy > 0) for (int i = 0; i < Enemy.MAXEnemy; i++) if (enemy[i].isAlive) G.DrawImage(Enemy.imgEnemy[enemy[i].Drt, enemy[i].Stt], enemy[i].x, enemy[i].y, 56, 56);//ve enemy
            if (isDraw) G.DrawImage(img, x - 25, y - 25, 51, 51);//ve Hero
            //vẽ đạn cho hero
            if (cntBull > 0) for (int i = 0; i < MAXBull; i++) if (Bull[i].isAlive) G.DrawImage(Bull[i].img, Bull[i].x, Bull[i].y, 35, 35);
            //trạng thái levelup
            if (isLU) G.DrawImage(imgLU[sLU], xLU, yLU, 84, 15);
            for (int i = 0; i < cntTree; i++)//ve not phan dinh cua cay
                G.DrawImage(imgTopTree, xTree[i], yTree[i], 48, 48);
        }


        //===================khoi tao hero===================
        private void InitMyHero(int x1, int y1)
        {
            isPressedArrow = isMove = false;
            isDraw = true;
            Stt = 0;
            Drt = 2;
            enk = 0;

            //LAM SAU
            MAXBull = 1;
            curBull = cntBull = 0;
            Bull = new Bullet[MAXBULLET];
            for (int i = 0; i < MAXBULLET; i++)
            {
                Bull[i] = new Bullet();
                Bull[i].Next = i + 1;
            }
            Bull[MAXBull - 1].Next = 0;

            x = 48 * x1 + StartXC;
            y = 48 * y1 + StartYC;
            img = imageWalk[Drt, Stt];
        }


        //===================Move Action===================
        private void MoveUnit()
        {
            if (isMove)
            {
                Action_Move[Drt]();//phụ trách phần di chuyển
                Stt = Loop8[Stt];
                img = imageWalk[Drt, Stt];//phụ trách animation
                tmpX = (x - StartX) / 48;
                tmpY = (y - StartY) / 48;
                if (Map[tmpY, tmpX] > 2)
                {
                    Action_EatItem[Map[tmpY, tmpX] - 3]();
                    Map[tmpY, tmpX] = 2;
                    ItemSpawn[MapItem[tmpY, tmpX]].isAlive = false;
                    ListSpawn.Add(new Point(tmpX, tmpY));
                    Item.cntItem--;
                }
            }
        }


        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyData)
            {
                case Keys.Up: Drt = 0; isPressedArrow = true; break;
                case Keys.Right: Drt = 1; isPressedArrow = true; break;
                case Keys.Down: Drt = 2; isPressedArrow = true; break;
                case Keys.Left: Drt = 3; isPressedArrow = true; break;

                //LAM SAU
                case Keys.Space:
                    //Console.WriteLine("FIRE BULLET!!");

                    if (cntBull < MAXBull)
                    {
                        while (Bull[curBull].isAlive)
                        {
                            curBull = Bull[curBull].Next;
                        }
                        Bull[curBull].init(x, y, Drt);
                        curBull = Bull[curBull].Next;
                        cntBull++;
                        Console.WriteLine("FIRE BULLET!!");

                    }
                    break;
            }
            if (isPressedArrow && !isMove)
            {
                isMove = true;
            }


        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Key[Drt])
            {
                img = imageWalk[Drt, 0];
                isMove = false;
                isPressedArrow = false;
            }
            Console.WriteLine("Status of Hero: " + Stt + "  Direct of Hero: " + Drt);

            Console.WriteLine("X: " + X + " Y:" + Y + " X1: " + X1 + " Y1: " + Y1 + " X2: " + X2 + " Y2: " + Y2 + " x:" + x + " y: " + y);

        }




        private void Move_Up()
        {
            X1 = (x + 1) / 48; X2 = (x + 47) / 48; Y = (y - 77) / 48;
            if (Map[Y, X1] > typeWall && Map[Y, X2] > typeWall) y -= 6;
            else if (Map[Y, (x + 5) / 48] > typeWall) x -= 6;
            else if (Map[Y, (x + 43) / 48] > typeWall) x += 6;
        }

        private void Move_Right()
        {
            X = (x + 53) / 48; Y1 = (y - 71) / 48; Y2 = (y - 25) / 48;
            if (Map[Y1, X] > typeWall && Map[Y2, X] > typeWall) x += 6;
            else if (Map[(y - 67) / 48, X] > typeWall) y -= 6;
            else if (Map[(y - 29) / 48, X] > typeWall) y += 6;
        }

        private void Move_Down()
        {
            X1 = (x + 1) / 48; X2 = (x + 47) / 48; Y = (y - 19) / 48;
            if (Map[Y, X1] > typeWall && Map[Y, X2] > typeWall) y += 6;
            else if (Map[Y, (x + 5) / 48] > typeWall) x -= 6;
            else if (Map[Y, (x + 43) / 48] > typeWall) x += 6;
        }

        private void Move_Left()
        {
            X = (x - 5) / 48; Y1 = (y - 71) / 48; Y2 = (y - 25) / 48;
            if (Map[Y1, X] > typeWall && Map[Y2, X] > typeWall) x -= 6;
            else if (Map[(y - 67) / 48, X] > typeWall) y -= 6;
            else if (Map[(y - 29) / 48, X] > typeWall) y += 6;
        }


        private void UpdateHp()
        {
            numTextHp[0] = curHp / 100;
            numTextHp[1] = (curHp / 10) % 10;
            numTextHp[2] = curHp % 10;
            rectHp.Width = curHp * 195 / maxHp;
        }

        private void UpdateMaxHp()
        {
            curHp = maxHp;
            numTextHp[0] = numTextMaxHp[0] = curHp / 100;
            numTextHp[1] = numTextMaxHp[1] = (curHp / 10) % 10;
            numTextHp[2] = numTextMaxHp[2] = curHp % 10;
            rectHp.Width = 195;
        }

        private void UpdateExp()
        {
            numTextExp[0] = curExp / 100;
            numTextExp[1] = (curExp / 10) % 10;
            numTextExp[2] = curExp % 10;
            rectExp.Width = curExp * 180 / maxExp;
        }

        private void UpdateMaxExp()
        {
            curExp = 0;
            numTextExp[0] = numTextExp[1] = numTextExp[2] = 0;
            numTextMaxExp[0] = maxExp / 100;
            numTextMaxExp[1] = (maxExp / 10) % 10;
            numTextMaxExp[2] = maxExp % 10;
            rectExp.Width = 0;
        }

       
        private void LoseHP(int dmg)
        {
            if (curHp > dmg)
            {
                curHp -= dmg;
                UpdateHp();
            }
            else
            {
                curHp = 0;
                UpdateHp();
                isDraw = false;
                SttDead = 0;
                isDead = true;
                isMove = false;
                isPressedArrow = false;
                KeyDown -= Form1_KeyDown;
                KeyUp -= Form1_KeyUp;
                tDead.Enabled = true;
                MessageBox.Show("Game overr!\nYour level: "+ Level+"\nEnemy Killed: "+enk);
                SaveGame();
                timer1.Enabled = false;
            }
        }

        //USE ITEM

        private void Increease_Exp()
        {
            curExp++;
            if (curExp == maxExp)
            {
                xLU = x - 41;
                yLU = y - 45;
                tLU = sLU = 0;
                isLU = true;
                Level++;
                maxHp += 2;
                UpdateMaxHp();
                maxExp += 5;
                UpdateMaxExp();
            }
            else
            {
                UpdateExp();
            }
        }

        private void BuffAmo()
        {
            if (MAXBull < MAXBULLET)
            {
                Bull[MAXBull - 1].Next = MAXBull;
                Bull[MAXBull].Next = 0;
                MAXBull++;
            }
        }

        private void BuffHP()
        {
            if (curHp < maxHp)
            {
                curHp++;
                UpdateHp();
            }
        }

        private void BuffShield()
        {
            isHasShield = true;
        }

        //level up
        private void LevelUpText()
        {
            tLU = 1 - tLU;//tLU = 0 orr 1
            yLU -= 2;
            if (tLU == 0)
            {
                sLU++;

                if (sLU == 6)//6 lần để chữ bay lên :)))
                {
                    isLU = false;
                }
            }
        }



        private void SpawnEnemy()
        {
            if (Enemy.cntEnemy < Enemy.MAXEnemy)
            {
                tSpawnEnemy++;
                if (tSpawnEnemy == 80)//80*Interval milisec spawn 1
                {
                    while (enemy[Enemy.curEnemy].isAlive)//nếu có 1 con bị chết thì dừng lại ở toạ độ con đó
                    {
                        Enemy.curEnemy = enemy[Enemy.curEnemy].Next;//(I)
                    }
                    tSpawnEnemy = 0;
                    tmpInt = r.Next(cntESP);//lấy toạ độ từ điểm trống
                    tmpX = ESP[tmpInt].x;
                    tmpY = ESP[tmpInt].y;
                    xSpawn = 48 * tmpX + StartX;//toa do x,y spawn smoke khi ve
                    ySpawn = 48 * tmpY + StartY;
                    enemy[Enemy.curEnemy].Init(tmpX, tmpY);
                    Console.WriteLine("==============================================================");
                    Console.WriteLine("Enemy: " + Enemy.cntEnemy + " Spawn");
                    Console.WriteLine("X cordinate in Map: " + enemy[Enemy.curEnemy].X + "Y cordinate in Map: " + enemy[Enemy.curEnemy].Y);
                    Console.WriteLine("X cordinate in PictureBox: " + enemy[Enemy.curEnemy].x + "Y cordinate in PictureBox: " + enemy[Enemy.curEnemy].y);
                    Console.WriteLine("Direct: " + enemy[Enemy.curEnemy].getDirectString());
                    Console.WriteLine("==============================================================");

                    Enemy.cntEnemy++;//cộng cho đến khi ctnEnemy == MAXEnemy
                    SpawnStt = 0;


                }
            }

            if (SpawnStt < 6) //tổng 6x3x30 => min = 18 ml
            {
                tSpawn = Loop3[tSpawn];//Thoi gian spawn (Duration)
                if (tSpawn == 0)
                {
                    SpawnStt++;
                    if (SpawnStt == 6)
                    {
                        enemy[Enemy.curEnemy].isAlive = true;//set bang true 
                        Enemy.curEnemy = enemy[Enemy.curEnemy].Next;//,tang curEnemy  muc dich cho (I)
                    }
                }
            }
        }


        private void MoveEnemy()
        {
            Enemy.t = Loop8[Enemy.t];//cần phải có cái này đỡ không là nó chạy ghê lắm
            for (int i = 0; i < Enemy.MAXEnemy; i++)
            {
                if (enemy[i].isAlive)
                {
                    if (enemy[i].Move < 8)
                    {
                        enemy[i].x += 6 * Drts[enemy[i].Drt].x;//luc nay khong bi cham vao tuong khi moi hoi sinh
                        enemy[i].y += 6 * Drts[enemy[i].Drt].y;//move 6px 1 lan
                        enemy[i].Move++;
                        //xử lí bullet
                        if (cntBull > 0) for (int k = 0; k < MAXBull; k++)
                            {
                                if (Bull[k].isAlive)
                                {
                                    if (IsCollisionBullet(enemy[i].x, enemy[i].y, Bull[k].x - Bullet.offset[Bull[k].Drt, 0], Bull[k].y - Bullet.offset[Bull[k].Drt, 1]))
                                    {
                                        enemy[i].isAlive = false;
                                        enk++;
                                        Enemy.cntEnemy--;
                                        Bull[k].isAlive = false;
                                        cntBull--;
                                        Increease_Exp();
                                        lbEnemy.Text = "Enemy kill: " + enk;
                                        //LExplode.Add(new Explode(enemy[i].x - 6, enemy[i].y - 8));
                                        break;
                                    }
                                }
                            }
                        if (!isDead && !isImmunity && IsCollisionHero(enemy[i].x, enemy[i].y))
                        {
                            isImmunity = true;//trạng thái của nv
                            tIM = timeImmu = 0; //set time miễn nhiễm = 0
                            if (isHasShield) isHasShield = false;//mất giáp 
                            else LoseHP(damEnemy);//trừ máu nhân vật
                        }
                    }
                    else
                    {
                        enemy[i].Move = 0;
                        enemy[i].X += Drts[enemy[i].Drt].x;
                        enemy[i].Y += Drts[enemy[i].Drt].y;
                        enemy[i].Move2++;
                        if (Map[enemy[i].Y + Drts[enemy[i].Drt].y, enemy[i].X + Drts[enemy[i].Drt].x] <= typeWall || enemy[i].Move2 == enemy[i].MaxMove2) //Neu khong có MaxMove2 nó sẽ mãi ko chuyển hướng mà đi theo 1 quỹ đạo
                            enemy[i].GetRdDrt();//đổi maxMOVE2 1 lần ở objject
                    }
                    if (Enemy.t == 0)
                    {
                        enemy[i].Stt = Loop3[enemy[i].Stt];//tTime.Interval* loop3.count (thoi gian de enemy doi trang thai)
                    }
                }
            }
        }


       
        private bool IsCollisionHero(int xe, int ye)
        {
            if (xe <= x && x <= xe + 56 && ye <= y && y <= ye + 56) return true;
            return false;
        }

        private bool IsCollisionBullet(int xe, int ye, int xb, int yb)
        {
            if (xe <= xb && xb <= xe + 56 && ye <= yb && yb <= ye + 56) return true;
            return false;
        }

        private void MoveBullet()
        {
            for (int i = 0; i < MAXBull; i++)
            {
                if (Bull[i].isAlive)
                {
                    if (CheckBullet[Bull[i].Drt](Bull[i].x, Bull[i].y))//kiểm tra xem chạm tường hay khôg
                    {
                        Bull[i].isAlive = false; //chạm tường thì đạn mất
                        cntBull--;
                    }
                    else
                    {
                        Bull[i].x += Bull[i].mx; //không thì bay tiếp (12px 1 lần)
                        Bull[i].y += Bull[i].my;
                    }
                }
            }
        }

        private static bool CheckCollisionUp(int x, int y)
        {
            if (Map[(y - 48) / 48, (x + 31) / 48] <= typeWall) return true;
            if (Map[(y - 48) / 48, (x + 51) / 48] <= typeWall) return true;
            return false;
        }

        private static bool CheckCollisionRight(int x, int y)
        {
            if (Map[(y - 41) / 48, (x + 60) / 48] <= typeWall) return true;
            if (Map[(y - 21) / 48, (x + 60) / 48] <= typeWall) return true;
            return false;
        }

        private static bool CheckCollisionDown(int x, int y)
        {
            if (Map[(y - 12) / 48, (x + 31) / 48] <= typeWall) return true;
            if (Map[(y - 12) / 48, (x + 51) / 48] <= typeWall) return true;
            return false;
        }

        private static bool CheckCollisionLeft(int x, int y)
        {
            if (Map[(y - 41) / 48, (x + 24) / 48] <= typeWall) return true;
            if (Map[(y - 21) / 48, (x + 24) / 48] <= typeWall) return true;
            return false;
        }

        private void SpawnItem()
        {
            if (Item.cntItem < Item.MAXItem)
            {
                tSpawnItem++;
                if (tSpawnItem == 30)
                {
                    while (ItemSpawn[Item.curItem].isAlive)
                    {
                        Item.curItem = nexItem[Item.curItem];
                    }
                    tSpawnItem = 0;
                    tmpInt = r.Next(ListSpawn.Count);
                    tmpX = ListSpawn.ElementAt(tmpInt).x;
                    tmpY = ListSpawn.ElementAt(tmpInt).y;
                    tmpInt2 = r.Next(Item.MAXType);
                    ItemSpawn[Item.curItem].Init(tmpX, tmpY, tmpInt2);
                    Map[tmpY, tmpX] = tmpInt2 + 3;
                    MapItem[tmpY, tmpX] = Item.curItem;
                    ListSpawn[tmpInt] = null;
                    ListSpawn.RemoveAt(tmpInt);
                    Item.curItem = nexItem[Item.curItem];
                    Item.cntItem++;
                }
            }
            if (Item.cntItem > 0)
            {
                t2 = Loop3[t2];
                if (t2 == 1) for (int i = 0; i < Item.MAXItem; i++) ItemSpawn[i].offset = 5 - ItemSpawn[i].offset;
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            SpawnEnemy();
            SpawnItem();
            MoveUnit();
            if (isLU) LevelUpText();
            if (isImmunity)
            {
                tIM = 1 - tIM;//tIM chi co the bang 0 hoac 1
                if (tIM == 0)
                {
                    timeImmu++;
                    isDraw = !isDraw;
                    if (timeImmu == MAXIMMU) //du 16 lan nhap nhay
                    {
                        isImmunity = false;//thi thoat khoi trang thai imuty
                    }
                }
            }
            tEnemy++;
            if (tEnemy == 33)//lồng thêm vòng này ở ngoài => 33*30*10 = 
            {
            tEnemy = 0;
            timeEnemy--;
            lbCount.Text = "Count Down: " + timeEnemy.ToString("00");
                if (timeEnemy == 0)
                {
                    
                    timeEnemy = 31;
                    levelEnemy++;
                    lbEnemyLevel.Text = "Enemy's Level: " + levelEnemy.ToString("00");
                    damEnemy++;
                    //MAXBull++;
                }
            }
            if (Enemy.cntEnemy > 0) MoveEnemy();



            if (cntBull > 0) MoveBullet();

            pictureBox1.Invalidate();
        }


        public void SaveGame() {
            string HardLevelText = "";
            switch (HardLevel) {
                case 0:
                    HardLevelText = "Easy";
                    break;
                case 1:
                    HardLevelText = "Normal";
                    break;
                case 2:
                    HardLevelText = "Hard";
                    break;
            }

            DateTime aDate = DateTime.Now;
            History h = new History(enk,Level,HardLevelText,aDate);
            if (new storyDAO().insertPre(h) > 0) {
                Console.WriteLine("Save successfully!");
            }

        }
    }
}
