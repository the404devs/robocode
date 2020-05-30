using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using Robocode.Util;

namespace StarPower
{
    public class StarPower1 : Robot
    //May 12th, 2017 against Victory (ironic, because we had the victory)
    //Combination of Walls and Corners
    {
        int turnDirection = 1;
        //Custom Functions

        void sweep()
        {
            int x = 4;
            while (x > 0)
            {
                this.TurnGunRight(100);
                this.TurnGunLeft(100);
                x--;
            }
        }

        void fullSweep()
        {
            this.TurnGunRight(360);
        }

        void colourFlash()
        {
            Random r = new Random();
            int colour = r.Next(1, 6);
            if (colour == 1)
            {
                this.SetColors(Color.White, Color.Green, Color.Blue);
            }
            else if (colour == 2)
            {
                this.SetColors(Color.Red, Color.Blue, Color.Purple);
            }
            else if (colour == 3)
            {
                this.SetColors(Color.Green, Color.Purple, Color.White);
            }
            else if (colour == 4)
            {
                this.SetColors(Color.Blue, Color.White, Color.Red);
            }
            else if (colour == 5)
            {
                this.SetColors(Color.Purple, Color.Red, Color.Green);
            }
        }
        //End Functions

        public override void Run()
        {
            base.Run();
            this.SetAllColors(Color.Gold);
            colourFlash();
            this.TurnLeft(this.Heading);
            colourFlash();
            this.Ahead(this.BattleFieldHeight - this.Y);
            colourFlash();
            this.TurnRight(90);
            colourFlash();
            this.Ahead(this.BattleFieldWidth - this.X);
            colourFlash();
            this.TurnRight(90);
            colourFlash();

            while (true)
            {
                sweep();
                colourFlash();
                this.TurnGunRight(90);
                colourFlash();
                this.Ahead(this.BattleFieldHeight);
                colourFlash();
                this.TurnGunLeft(90);
                colourFlash();
                this.TurnRight(90);
                colourFlash();
                sweep();
                colourFlash();
                this.TurnGunRight(90);
                colourFlash();
                this.Ahead(this.BattleFieldWidth);
                colourFlash();
                this.TurnGunLeft(90);
                colourFlash();
                this.TurnRight(90);
                colourFlash();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            base.OnScannedRobot(evnt);
            if (evnt.Distance < 50)
            {
                this.Fire(3);
            }
            else if (evnt.Distance < 100)
            {
                this.Fire(2.5);
            }
            else
            {
                this.Fire(2);
            }
        }

        public override void OnHitRobot(HitRobotEvent evnt)//Code when the collide with the enemy
        {
            double lastBearing = evnt.Bearing;
            base.OnHitRobot(evnt);
            if (evnt.Bearing >= 0)
            {
                turnDirection = 1;
            }
            else
            {
                turnDirection = -1;
            }
            TurnRight(evnt.Bearing);

            // Determine a shot that won't kill the robot...
            // We want to ram him instead for bonus points
            if (evnt.Energy > 16)
            {
                Fire(3);
            }
            else if (evnt.Energy > 10)
            {
                Fire(2);
            }
            else if (evnt.Energy > 4)
            {
                Fire(1);
            }
            else if (evnt.Energy > 2)
            {
                Fire(.5);
            }
            else if (evnt.Energy > .4)
            {
                Fire(.1);
            }
            Ahead(40); // Ram him again!
            //this.TurnRight(-lastBearing);
        }
        
    }
    /*************************************************************************************************************************************************/
    public class StarPower3 : Robot
    //The Bergade Buster
    //May 18th, 2017 against BJCBergade
    //Walls killer, I guess
    {
        //Custom Functions
        void colourFlash()//Our signature flashing colour sequence
        {
            Random r = new Random();
            int colour = r.Next(1, 6);
            if (colour == 1)
            {
                this.SetColors(Color.White, Color.Green, Color.Blue);
            }
            else if (colour == 2)
            {
                this.SetColors(Color.Red, Color.Blue, Color.Purple);
            }
            else if (colour == 3)
            {
                this.SetColors(Color.Green, Color.Purple, Color.White);
            }
            else if (colour == 4)
            {
                this.SetColors(Color.Blue, Color.White, Color.Red);
            }
            else if (colour == 5)
            {
                this.SetColors(Color.Purple, Color.Red, Color.Green);
            }
        }

        void sweep()//because the robot needs to do something, or else we'll be disabled
        {
            int x = 2;
            while (x > 0)
            {
                this.TurnGunRight(1);//turn the gun back and forth just a tiny amount
                colourFlash();
                this.TurnGunLeft(1);
                colourFlash();
                x--;
            }
        }
        //End Functions

        public override void Run()//Code that first runs when the battle starts
        {
            base.Run();
            this.SetAllColors(Color.Gold);
            colourFlash();//Change colours
            this.TurnLeft(this.Heading);//Turn towards the top of the map
            colourFlash();
            this.Ahead(this.BattleFieldHeight - this.Y);//Move to the top
            colourFlash();
            this.TurnRight(90);//Turn right
            colourFlash();
            this.Ahead(this.BattleFieldWidth - this.X - 100);//Move towards the special corner, stop 100px away from it


            while (true)//Infinite loop
            {
                sweep();//"Sweep" the gun back and forth 1 degree, only because we need to do something
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)//Code when we see the enemy
        {
            base.OnScannedRobot(evnt);
            if (this.Y >= this.BattleFieldHeight - 36)//Make sure we're at the top of the field
            {
                this.Fire(3);//Shoot full strength at enemy
                Out.WriteLine("Bang!");
            }
        }

        public override void OnHitRobot(HitRobotEvent evnt)//Code when the collide with the enemy
        {
            base.OnHitRobot(evnt);

            if (this.Y >= this.BattleFieldHeight - 36 && this.X >= this.BattleFieldWidth - 136)
            //Check if we are in position by the top right corner.
            //Minus 36, because the robot measures 36x36.
            //Minus 136, because we stopped 100px from the corner, and the robot is 36x36
            {
                this.Back(30);//Back up a bit
                this.TurnRight(evnt.Bearing);//Turn towards enemy
                this.Fire(3);//Fire
                Out.WriteLine("Bang!");
            }
        }
    }
    /*************************************************************************************************************************************************/
   /* public class StarPowerExtra : Robot
    //Aimbot
    {
        //Variables
        int movement = 1;
        Random r = new Random();//Create a variable to store random values
        //Custom Functions
        void colourFlash()//Colour changing code
        {            
            int colour = r.Next(1, 6);
            if (colour == 1)
            {
                this.SetColors(Color.White, Color.Green, Color.Blue);
            }
            else if (colour == 2)
            {
                this.SetColors(Color.Red, Color.Blue, Color.Purple);
            }
            else if (colour == 3)
            {
                this.SetColors(Color.Green, Color.Purple, Color.White);
            }
            else if (colour == 4)
            {
                this.SetColors(Color.Blue, Color.White, Color.Red);
            }
            else if (colour == 5)
            {
                this.SetColors(Color.Purple, Color.Red, Color.Green);
            }
        }

        void sweep()
        {
            int x = 4;
            while (x > 0)
            {
                this.TurnGunRight(10);
                colourFlash();
                x--;
            }
        }

        //End Functions

        public override void Run()
        {
            base.Run();
            colourFlash();//Change colours
            this.TurnLeft(this.Heading);//Face the top of the battlefield
            colourFlash();
            this.Ahead((this.BattleFieldHeight / 2) - this.Y);
            //this.Ahead(this.BattleFieldHeight - this.Y);//Move to the top
            colourFlash();
            this.TurnLeft(90);//Turn left
            colourFlash();
            this.Back((this.BattleFieldWidth / 2) - this.X);
            //this.Ahead(this.BattleFieldWidth - this.X);//Move to the top right corner
            colourFlash();

            while (true)
            {
                //this.TurnLeft(5);//By default turn left in a circle
                sweep();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)//Code for when we see the enemy
        {
            /*double realBearing = evnt.BearingRadians + (this.Heading * 0.0174533);//Convert our heading to radians, this'll give us the true bearing to the bad guy
            double enemyVelocity = evnt.Velocity * Math.Sin(evnt.HeadingRadians - realBearing);//This is their speed
            double gunTurn = 0;
            this.TurnRadarRight(this.RadarHeading);//This could be a problem
            //Random speed here

            if (evnt.Distance > 150)//If they're far away
            {
                gunTurn = Utils.NormalRelativeAngle(realBearing - (this.GunHeading * 0.0174533) + (enemyVelocity / 22));
                this.TurnGunRight(gunTurn * 57.2958);
                this.TurnGunRight(Utils.NormalRelativeAngle(realBearing - (this.Heading * 0.0174533) + (enemyVelocity / this.Velocity)));
                this.Ahead((evnt.Distance - 140) * movement);
                this.Fire(3);
            }
            else//If they're close
            {
                gunTurn = Utils.NormalRelativeAngle(realBearing - (this.GunHeading * 0.0174533) + (enemyVelocity / 15));
                this.TurnGunRight(gunTurn * 57.2958);
                this.TurnLeft(-90 - evnt.Bearing);
                this.Ahead((evnt.Distance - 140) * movement);
                this.Fire(3);
            }
            }

         public override void OnHitWall(HitWallEvent evnt)
         {
            base.OnHitWall(evnt);
            movement = -movement;
             }
            //real code
            double absoluteBearing = Heading + evnt.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);
            if (Math.Abs(bearingFromGun) <= 3)
            {
                TurnGunRight(bearingFromGun);
                if (GunHeat == 0)
                {
                    Fire(Math.Min(3 - Math.Abs(bearingFromGun), Energy - .1));
                }
            }
            else
            {
                TurnGunRight(bearingFromGun);
            }

            if (bearingFromGun == 0)
            {
                Scan();
            }
             }
            /*double angleToEnemy = (this.Heading * 0.0174533) + evnt.BearingRadians;

            // Subtract current radar heading to get the turn required to face the enemy, be sure it is normalized
            double radarTurn = Utils.NormalRelativeAngle(angleToEnemy - (this.RadarHeading));

            // Distance we want to scan from middle of enemy to either side
            // The 36.0 is how many units from the center of the enemy robot it scans.
            double extraTurn = Math.Min(Math.Atan(36.0 / evnt.Distance), Rules.RADAR_TURN_RATE_RADIANS);

            // Adjust the radar turn so it goes that much further in the direction it is going to turn
            // Basically if we were going to turn it left, turn it even more left, if right, turn more right.
            // This allows us to overshoot our enemy so that we get a good sweep that will not slip.
            if (radarTurn < 0)
            {
                radarTurn -= extraTurn;
            }
            else
            {
                radarTurn += extraTurn;
            }

            //Turn the radar
            this.TurnRadarRight(radarTurn);
            this.Fire(3);
           }

            /*this.TurnRight(evnt.Bearing);//Turn towards the enemy
            colourFlash();
            this.Ahead(2);//Move ahead a tiny bit

            if (evnt.Distance < 25)//Determine the distance to the enemy, and change firing strength accordingly
            {
                this.Fire(3);
            }
            else if (evnt.Distance < 500)
            {
                this.Fire(2.5);
            }
            else
            {
                this.Fire(2);
            }
        

        public override void OnHitRobot(HitRobotEvent evnt)//Code when we collide with the enemy
        {
            base.OnHitRobot(evnt);
            this.Fire(3);//Fire full strength
        }
    }*/

    /*************************************************************************************************************************************************/
    public class StarPowerInvisible : Robot
    //This is a joke
    {
        int turnDirection = 1;
        //Custom Functions
        void colourFlash()//Colour changing code
        {
            this.SetAllColors(Color.MintCream);
            /*Random r = new Random();//Create a variable to store random values
            int colour = r.Next(1, 6);
            if (colour == 1)
            {
                this.SetColors(Color.White, Color.Green, Color.Blue);
            }
            else if (colour == 2)
            {
                this.SetColors(Color.Red, Color.Blue, Color.Purple);
            }
            else if (colour == 3)
            {
                this.SetColors(Color.Green, Color.Purple, Color.White);
            }
            else if (colour == 4)
            {
                this.SetColors(Color.Blue, Color.White, Color.Red);
            }
            else if (colour == 5)
            {
                this.SetColors(Color.Purple, Color.Red, Color.Green);
            }*/
        }

        void sweep()
        {
            int x = 4;
            while (x > 0)
            {
                this.TurnGunRight(100);
                this.TurnGunLeft(100);
                x--;
            }
        }

        //End Functions

        public override void Run()
        {
            base.Run();
            colourFlash();//Change colours
            this.TurnLeft(this.Heading);//Face the top of the battlefield
            colourFlash();
            //this.Ahead((this.BattleFieldHeight / 2) - this.Y);
            this.Ahead(this.BattleFieldHeight - this.Y);//Move to the top
            colourFlash();
            this.TurnLeft(90);//Turn left
            colourFlash();
            //this.Back((this.BattleFieldWidth / 2) - this.X);
            this.Ahead(this.BattleFieldWidth - this.X);//Move to the top right corner
            colourFlash();

            while (true)
            {
                colourFlash();//Change colours
                TurnRight(5 * turnDirection);
                //sweep();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)//Code for when we see the enemy
        {
            if (evnt.Bearing >= 0)
            {
                turnDirection = 1;
            }
            else
            {
                turnDirection = -1;
            }

            TurnRight(evnt.Bearing);
            Ahead(evnt.Distance + 5);
            Scan(); // Might want to move ahead again!
            /*// Calculate exact location of the robot
            double absoluteBearing = Heading + evnt.Bearing;
            double bearingFromGun = Utils.NormalRelativeAngleDegrees(absoluteBearing - GunHeading);

            // If it's close enough, fire!
            if (Math.Abs(bearingFromGun) <= 3)
            {
                TurnGunRight(bearingFromGun);
                // We check gun heat here, because calling Fire()
                // uses a turn, which could cause us to lose track
                // of the other robot.
                if (GunHeat == 0)
                {
                    Fire(Math.Min(3 - Math.Abs(bearingFromGun), Energy - .1));
                }
            }
            else
            {
                // otherwise just set the gun to turn.
                // Note:  This will have no effect until we call scan()
                TurnGunRight(bearingFromGun);
            }
            // Generates another scan event if we see a robot.
            // We only need to call this if the gun (and therefore radar)
            // are not turning.  Otherwise, scan is called automatically.
            if (bearingFromGun == 0)
            {
                Scan();
            }

            /*this.TurnRight(evnt.Bearing);//Turn towards the enemy
            this.Ahead(10);//Move ahead a tiny bit

            if (evnt.Distance < 50)//Determine the distance to the enemy, and change firing strength accordingly
            {
                this.Fire(3);
            }
            else if (evnt.Distance < 100)
            {
                this.Fire(2.5);
            }
            else
            {
                this.Fire(2);
            }*/
        }

        public override void OnHitRobot(HitRobotEvent evnt)//Code when we collide with the enemy
        {
            base.OnHitRobot(evnt);
            if (evnt.Bearing >= 0)
            {
                turnDirection = 1;
            }
            else
            {
                turnDirection = -1;
            }
            TurnRight(evnt.Bearing);

            // Determine a shot that won't kill the robot...
            // We want to ram him instead for bonus points
            if (evnt.Energy > 16)
            {
                Fire(3);
            }
            else if (evnt.Energy > 10)
            {
                Fire(2);
            }
            else if (evnt.Energy > 4)
            {
                Fire(1);
            }
            else if (evnt.Energy > 2)
            {
                Fire(.5);
            }
            else if (evnt.Energy > .4)
            {
                Fire(.1);
            }
            Ahead(40); // Ram him again!
        }
    }
    /*******************************************************************************************************************************************/
    public class StarPower2 : Robot
    {
        //Custom Functions
        void colourFlash()//Colour changing code
        {
            Random r = new Random();//Create a variable to store random values
            int colour = r.Next(1, 6);
            if (colour == 1)
            {
                this.SetColors(Color.White, Color.Green, Color.Blue);
            }
            else if (colour == 2)
            {
                this.SetColors(Color.Red, Color.Blue, Color.Purple);
            }
            else if (colour == 3)
            {
                this.SetColors(Color.Green, Color.Purple, Color.White);
            }
            else if (colour == 4)
            {
                this.SetColors(Color.Blue, Color.White, Color.Red);
            }
            else if (colour == 5)
            {
                this.SetColors(Color.Purple, Color.Red, Color.Green);
            }
        }

        void move()//because the robot needs to do something, or else we'll be disabled
        {
            this.Ahead(20);
            this.Back(20);
        }
        //End Functions

        //Variables
        Boolean kill = false;
        //End variables 

        public override void Run()
        {
            base.Run();
            this.SetAllColors(Color.Gold);
            colourFlash();
            this.TurnLeft(this.Heading);
            colourFlash();
            this.Ahead((this.BattleFieldHeight / 2) - this.Y);
            colourFlash();
            this.TurnRight(90);
            colourFlash();
            this.Ahead((this.BattleFieldWidth / 2) - this.X);
            colourFlash();
            this.TurnGunRight(20);
            //this.TurnRadarRight(20);
            kill = true;


            while (true)
            {
                this.TurnRight(10);//////////
                //this.TurnLeft(1);
                colourFlash();
                //move();
            }
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)//Code for when we see the enemy
        {
            if (kill == true)
            {
                
                if (evnt.Distance < this.BattleFieldHeight * (this.BattleFieldWidth / 2))
                {
                    this.TurnRight(evnt.Bearing + 5);//Turn towards the enemy
                    //this.Ahead(0.7);//Move ahead a tiny bit

                    //this.Fire(2.5);
                    if (evnt.Distance < 50)//Determine the distance to the enemy, and change firing strength accordingly
                    {
                        this.Fire(3);
                    }
                    else if (evnt.Distance < 100)
                    {
                        this.Fire(2.5);
                    }
                    else
                    {
                        this.Fire(2);
                    }
                }
                else if (evnt.Distance > this.BattleFieldHeight * (this.BattleFieldWidth / 2))
                {
                    this.TurnRight(evnt.Bearing - 5);//Turn towards the enemy
                    //this.Ahead(0.7);//Move ahead a tiny bit

                    if (evnt.Distance < 50)//Determine the distance to the enemy, and change firing strength accordingly
                    {
                        this.Fire(3);
                    }
                    else if (evnt.Distance < 100)
                    {
                        //this.Fire(2.5);
                    }
                    else
                    {
                        //this.Fire(2);
                    }
                }
            }
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            base.OnHitRobot(evnt);
            this.TurnLeft(evnt.Bearing);
            this.Fire(3);
        }
    }
    /********************************************************************************************************************************/
    public class StarPower4 : Robot
    {
        private int moveDirection = 1;

        public override void Run()
        {
            base.Run();
            this.IsAdjustRadarForRobotTurn = true;
            this.SetColors(Color.Red, Color.Blue, Color.Gray);
            this.IsAdjustGunForRobotTurn = true;
            this.TurnRadarRight(double.PositiveInfinity);
            this.Scan();////
            while (true)
            {
                
            }
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            base.OnHitWall(evnt);
            moveDirection = -moveDirection;
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            
            base.OnScannedRobot(evnt);
            double num3 = 0;
            double num = evnt.Bearing + base.Heading;
            double num2 = evnt.Velocity * Math.Sin(evnt.Heading - num);
            //base.TurnRadarLeft(base.RadarTurnRemainingRadians());
            if (evnt.Distance > 150.0)
            {
                num3 = (num - base.GunHeading) + (num2 / 20.0);
                this.TurnGunRight(num3);
                
                this.TurnRight((num - base.Heading + (num2 / this.Velocity)));
                this.Ahead((evnt.Distance - 140.0) * moveDirection);
                this.Fire(2.0);
            }
            else
            {
                num3 = (num - base.GunHeading) + (num2 / 12.0);
                this.TurnGunRight(num3);
                this.TurnLeft(-90.0 - evnt.Bearing);
                this.Ahead((evnt.Distance - 140.0) * moveDirection);
                this.Fire(10.0);
            }
        }
        /*// Fields
        private int moveDirection = 1;

        // Methods
        public override void OnHitWall(HitWallEvent evnt)
        {
            base.OnHitWall(evnt);
            this.moveDirection = -this.moveDirection;
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            double num3;
            base.OnScannedRobot(evnt);
            double num = evnt.get_BearingRadians() + base.get_HeadingRadians();
            double num2 = evnt.get_Velocity() * Math.Sin(evnt.get_HeadingRadians() - num);
            base.SetTurnRadarLeftRadians(base.get_RadarTurnRemainingRadians());
            if (evnt.get_Distance() > 150.0)
            {
                num3 = (num - base.get_GunHeadingRadians()) + (num2 / 20.0);
                base.SetTurnGunRightRadians(num3);
                base.SetTurnRightRadians((num - base.get_HeadingRadians()) + (num2 / base.get_Velocity()));
                base.SetAhead((evnt.get_Distance() - 140.0) * this.moveDirection);
                base.SetFire(2.0);
            }
            else
            {
                num3 = (num - base.get_GunHeadingRadians()) + (num2 / 12.0);
                base.SetTurnGunRightRadians(num3);
                base.SetTurnLeft(-90.0 - evnt.get_Bearing());
                base.SetAhead((evnt.get_Distance() - 140.0) * this.moveDirection);
                base.SetFire(10.0);
            }
        }

        public override void OnWin(WinEvent evnt)
        {
            base.OnWin(evnt);
            base.TurnGunLeft(double.PositiveInfinity);
            base.FireBullet(0.1);
        }

        public override void Run()
        {
            base.Run();
            this.SetAdjustRadarForBodyTurn(true);
            base.SetColors(Color.Red, Color.Blue, Color.Gray);
            this.SetAdjustGunForBodyTurn(true);
            base.TurnRadarRightRadians(double.PositiveInfinity);
            base.Scan();
            while (true)
            {
            }
        }

        private void SetAdjustGunForBodyTurn(bool independent)
        {
        }

        private void SetAdjustRadarForBodyTurn(bool independent)
        {
        }

        // Properties
        public double getHeadingRadians { get; set; }*/
    }
}



