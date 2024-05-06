using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace physicsGameConcept
{
    public partial class platformerGame : Form
    {
        //Define player
        //Allow player to jump with space bar
        //Add realistic acceleration
        //Add platforming elements
        //Add Camera Tracking?
        //Add timer
        //Add win condition
        //Add levels + level select?

        bool spaceDown = false;
        bool dDown = false;
        bool aDown = false;

        bool jumpUsed = false;

        Rectangle player = new Rectangle();

        int playerSize = 20;
        int jumpHeight = -20;
        int playerFallSpeed = 0;
        int maxFallSpeed = 25;
        int playerMoveSpeed = 0;
        int playerMaxSpeed = 10;

        int platformHeight = 20;

        int currentFloor = 0;

        SolidBrush playerBrush = new SolidBrush(Color.Green);
        SolidBrush platformBrush = new SolidBrush(Color.SkyBlue);
        SolidBrush npcBrush = new SolidBrush(Color.Red);
        SolidBrush boostBrush = new SolidBrush(Color.Yellow);

        Rectangle leftWall = new Rectangle();
        Rectangle rightWall = new Rectangle();

        List<Floor> floors = new List<Floor>();

        Rectangle floor = new Rectangle();

        //TEST VAR
        int frameCount = 0;

        Stopwatch timer = new Stopwatch();


        public platformerGame()
        {
            InitializeComponent();
            GameInit();
        }

        public void GameInit()
        {
            timer.Reset();
            timer.Start();

            floors.Clear();
            DefineMaps();

            player.X = this.Width / 2 - playerSize;
            player.Y = this.Height - 50;
            player.Width = playerSize;
            player.Height = playerSize;

            currentFloor = 0;

            gameTimer.Start();

            rightWall.Width = 20;
            rightWall.Height = this.Height;
            rightWall.Y = 0;
            rightWall.X = this.Width - 34;

            leftWall.Width = 20;
            leftWall.Height = this.Height;
            leftWall.Y = 0;
            leftWall.X = 0;

            floor.X = 0;
            floor.Y = this.Height - 59;
            floor.Width = this.Width;
            floor.Height = platformHeight;

            Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            for (int j = 0; j < floors[currentFloor].Platforms().Count(); j++)
            {
                e.Graphics.FillRectangle(platformBrush, floors[currentFloor].Platforms()[j]);
            }

            for (int j = 0; j < floors[currentFloor].Boosts().Count(); j++)
            {
                e.Graphics.FillRectangle(boostBrush, floors[currentFloor].Boosts()[j]);
            }

            for (int j = 0; j < floors[currentFloor].NPCs().Count(); j++)
            {
                e.Graphics.FillRectangle(npcBrush, floors[currentFloor].NPCs()[j]);
            }

            e.Graphics.FillRectangle(platformBrush, leftWall);
            e.Graphics.FillRectangle(platformBrush, rightWall);
            e.Graphics.FillRectangle(playerBrush, player);

            if (floors[currentFloor].Checkpoint() == true)
            {
                e.Graphics.FillRectangle(platformBrush, floor);
            }

            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
            timer.Elapsed.Hours, timer.Elapsed.Minutes, timer.Elapsed.Seconds,
            timer.Elapsed.Milliseconds / 10);
            timerLabel.Text = elapsedTime;

            LoadNPCDialog();
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    spaceDown = true;
                    break;

                case Keys.A:
                    aDown = true;
                    break;

                case Keys.D:
                    dDown = true;
                    break;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    spaceDown = false;
                    break;

                case Keys.A:
                    aDown = false;
                    break;

                case Keys.D:
                    dDown = false;
                    break;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            CheckCollisions();
            DecelleratePlayer();
            LoadNextFloor();

            if (spaceDown == true && jumpUsed == false)
            {
                playerFallSpeed = jumpHeight;
                jumpUsed = true;
            }

            if (dDown == true && playerMoveSpeed <= playerMaxSpeed)
            {
                playerMoveSpeed += 1;
            }

            if (aDown == true && playerMoveSpeed >= -playerMaxSpeed)
            {
                playerMoveSpeed -= 1;
            }

            player.X += playerMoveSpeed;
            player.Y += playerFallSpeed;
            Refresh();
        }

        public void CheckCollisions()
        {
            for (int i = 0; i < floors[currentFloor].Platforms().Count(); i++)
            {
                if (player.IntersectsWith(floors[currentFloor].Platforms()[i]) && playerFallSpeed > 0)
                {
                    player.Y = floors[currentFloor].Platforms()[i].Y - playerSize;
                    playerFallSpeed = 0;
                    jumpUsed = false;
                }

                else if (player.IntersectsWith(floors[currentFloor].Platforms()[i]) && playerFallSpeed < 0)
                {
                    player.Y = floors[currentFloor].Platforms()[i].Y + playerSize + platformHeight;
                    playerFallSpeed = 0;
                }
            }

            for (int i = 0; i < floors[currentFloor].Boosts().Count(); i++)
            {
                if (player.IntersectsWith(floors[currentFloor].Boosts()[i]))
                {
                    jumpUsed = false;
                }
            }

            if (player.IntersectsWith(leftWall))
            {
                player.X = leftWall.X + leftWall.Width;
                playerMoveSpeed /= -2;
            }

            if (player.IntersectsWith(rightWall))
            {
                player.X = rightWall.X - playerSize;                
                playerMoveSpeed /= -2;
            }

            if (player.IntersectsWith(floor) && floors[currentFloor].Checkpoint() == true)
            {
                player.Y = floor.Y - playerSize;
                jumpUsed = false;
                playerFallSpeed = 0;
            }
        }

        public void DecelleratePlayer()
        {
            if (playerFallSpeed <= maxFallSpeed)
            {
                playerFallSpeed++;
            }

            if (playerMoveSpeed > 0 && dDown == false)
            {
                playerMoveSpeed--;
            }

            if (playerMoveSpeed < 0 && aDown == false)
            {
                playerMoveSpeed++;
            }
        }

        public void LoadNextFloor()
        {
            if (player.Y < 0)
            {
                currentFloor++;
                player.Y = this.Height - player.Height - 50;
            }

            if (player.Y > this.Height && currentFloor != 0)
            {
                currentFloor--;
                player.Y = 0;
            }

            else if (currentFloor == 0 && player.Y > this.Height)
            {
                player.Y = this.Height - 50;
                playerFallSpeed = 0;
            }
        }

        public void LoadNPCDialog()
        {
            if (floors[currentFloor].NPCs().Count > 0)
            {
                if (player.X > floors[currentFloor].NPCs()[0].X - 200 &&
                    player.X < floors[currentFloor].NPCs()[0].X + 200 &&
                    player.Y > floors[currentFloor].NPCs()[0].Y - 40 &&
                    player.Y < floors[currentFloor].NPCs()[0].Y + 40)
                {
                    npcTextOutput.Visible = true;
                    npcTextOutput.Text = floors[currentFloor].NPCDialog();
                    npcTextOutput.Left = floors[currentFloor].NPCs()[0].X - npcTextOutput.Width / 2;
                    npcTextOutput.Top = floors[currentFloor].NPCs()[0].Y - 20;
                }

                else
                {
                    npcTextOutput.Visible = false;
                }
            }

            else
            {
                npcTextOutput.Visible = false;
            }
        }


        public void DefineMaps()
        {
            List<Rectangle> platforms = new List<Rectangle>();
            List<Rectangle> boosts = new List<Rectangle>();
            List<Rectangle> npcs = new List<Rectangle>();

            //Floor 0
            Floor floor0 = new Floor(platforms, boosts, npcs, true, "You must climb the tower!");

            floor0.Platforms().Add(new Rectangle(0, this.Height / 2 - 50, 100, platformHeight));
            floor0.Platforms().Add(new Rectangle(this.Width / 2 - 50, this.Height / 2 + 50, 100, platformHeight));
            floor0.Platforms().Add(new Rectangle(this.Width - 134, this.Height / 2 + 100, 100, platformHeight));
            floor0.Platforms().Add(new Rectangle(this.Width / 2, this.Height / 2 - 200, 100, platformHeight));

            floor0.NPCs().Add(new Rectangle(this.Width - 100, this.Height - 79, 20, 20));


            floors.Add(floor0);

            //Floor 1
            Floor floor1 = new Floor(platforms, boosts, npcs, false, "");

            floor1.Boosts().Add(new Rectangle(this.Width - 350, this.Height / 2 + 150, 100, platformHeight));
            floor1.Platforms().Add(new Rectangle(this.Width - 134, this.Height / 2 + 100, 100, platformHeight));
            floor1.Platforms().Add(new Rectangle(this.Width / 2, this.Height / 2 - 100, 100, platformHeight));
            floor1.Platforms().Add(new Rectangle(200, this.Height / 2 - 220, 100, platformHeight));

            floors.Add(floor1);

            //Floor 2
            Floor floor2 = new Floor(platforms, boosts, npcs, false, "");

            floor2.Boosts().Add(new Rectangle(20, this.Height / 2 + 100, 40, 40));
            floor2.Boosts().Add(new Rectangle(200, this.Height / 2, 40, 40));
            floor2.Boosts().Add(new Rectangle(400, this.Height / 2 - 100, 40, 40));
            floor2.Boosts().Add(new Rectangle(600, this.Height / 2 - 200, 40, 40));

            floors.Add(floor2);

            //Floor 3
            Floor floor3 = new Floor(platforms, boosts, npcs, false, "");

            floor3.Platforms().Add(new Rectangle(this.Width - 234, this.Height / 2 + 250, 100, platformHeight));
            floor3.Platforms().Add(new Rectangle(20, this.Height / 2 + 100, 100, platformHeight));
            floor3.Platforms().Add(new Rectangle(400, this.Height / 2 - 100, 100, platformHeight));
            floor3.Platforms().Add(new Rectangle(this.Width - 120, this.Height / 2 - 300, 100, platformHeight));

            floors.Add(floor3);

            //Floor 4, Checkpoint
            Floor floor4 = new Floor(platforms, boosts, npcs, true, "There will be checkpoints occasionally!");

            floor4.Platforms().Add(new Rectangle(20, this.Height / 2 + 200, 100, platformHeight));
            floor4.Platforms().Add(new Rectangle(20, this.Height / 2, 100, platformHeight));
            floor4.Platforms().Add(new Rectangle(20, this.Height / 2 - 200, 100, platformHeight));

            floor4.NPCs().Add(new Rectangle(this.Width - 200, this.Height - 79, 20, 20));

            floors.Add(floor4);
            
            //Floor 5
            Floor floor5 = new Floor(platforms, boosts, npcs, false, "");

            floor5.Platforms().Add(new Rectangle(20, this.Height / 2 + 200, 100, platformHeight));
            floor5.Platforms().Add(new Rectangle(20, this.Height / 2, 100, platformHeight));
            floor5.Platforms().Add(new Rectangle(20, this.Height / 2 - 200, 100, platformHeight));

            floors.Add(floor5);
            
            //Floor 6
            Floor floor6 = new Floor(platforms, boosts, npcs, false, "");

            floor6.Platforms().Add(new Rectangle(20, this.Height / 2 + 200, 100, platformHeight));
            floor6.Boosts().Add(new Rectangle(this.Width / 2 - 50, this.Height / 2, 100, platformHeight));
            floor6.Platforms().Add(new Rectangle(this.Width - 120, this.Height / 2 - 180, 100, platformHeight));

            floors.Add(floor6); 
            
            //Floor 7
            Floor floor7 = new Floor(platforms, boosts, npcs, false, "");

            floor7.Platforms().Add(new Rectangle(this.Width / 2, this.Height / 2 + 200, 20, platformHeight));
            floor7.Platforms().Add(new Rectangle(this.Width / 2 - 200, this.Height / 2 + 200, 20, platformHeight));
            floor7.Platforms().Add(new Rectangle(100, this.Height / 2, 20, platformHeight));
            floor7.Boosts().Add(new Rectangle(this.Width - 310, this.Height / 2 -  100, 30, 30));
            floor7.Platforms().Add(new Rectangle(this.Width - 120, this.Height / 2 - 200, 20, platformHeight));

            floors.Add(floor7);
            
            //Floor 8
            Floor floor8 = new Floor(platforms, boosts, npcs, false, "");

            floor8.Platforms().Add(new Rectangle(this.Width / 2, this.Height / 2 + 200, 100, platformHeight));
            floor8.Boosts().Add(new Rectangle(0, this.Height / 2 + 150, 30, 30));
            floor8.Boosts().Add(new Rectangle(this.Width - 50, this.Height / 2, 30, 30));
            floor8.Boosts().Add(new Rectangle(100, this.Height / 2 - 200, 30, 30));

            floors.Add(floor8);
        }
    }
}
