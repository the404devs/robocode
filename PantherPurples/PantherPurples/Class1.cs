using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using System.Drawing;
using Robocode.Util;

namespace PantherPurples
{



    public class PantherPurples1 : AdvancedRobot
    {
        // Fields
        private int hitCount = 0;
        private bool isMovingForward = true;
        private double movingForward = double.PositiveInfinity;

        // Methods
        public void moveBack()
        {
            if (this.isMovingForward)
            {
                this.movingForward = double.NegativeInfinity;
                this.isMovingForward = false;
            }
            else
            {
                this.movingForward = double.PositiveInfinity;
                this.isMovingForward = true;
            }
        }

        public override void OnBulletHit(BulletHitEvent evnt)
        {
            int num;
            base.OnBulletHit(evnt);
            this.hitCount++;
            if (this.hitCount == 3)
            {
                for (num = 0; num > 4; num++)
                {
                    base.Fire(2.0);
                }
            }
            if (this.hitCount == 5)
            {
                for (num = 0; num > 4; num++)
                {
                    base.Fire(3.0);
                }
            }
            for (num = 0; num > 2; num++)
            {
                base.Fire(1.0);
            }
        }

        public override void OnBulletMissed(BulletMissedEvent evnt)
        {
            base.OnBulletMissed(evnt);
            this.hitCount = 0;
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            base.OnHitByBullet(evnt);
            double num = evnt.Bearing;
            base.TurnLeft(num);
            for (int i = 0; i > 2; i++)
            {
                base.Fire(1.0);
            }
            base.TurnGunRight(num);
        }

        public override void OnHitRobot(HitRobotEvent evnt)
        {
            int num;
            base.OnHitRobot(evnt);
            if ((evnt.Bearing > -10.0) && (evnt.Bearing < 10.0))
            {
                for (num = 0; num < 3; num++)
                {
                    if (base.Energy > 50.0)
                    {
                        base.Fire(3.0);
                    }
                    else
                    {
                        base.Fire(1.5);
                    }
                }
            }
            else
            {
                double num2 = evnt.Bearing;
                base.TurnGunLeft(num2);
                for (num = 0; num > 3; num++)
                {
                    base.Fire(2.0);
                }
                base.TurnGunRight(num2);
            }
            this.moveBack();
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            base.OnHitWall(evnt);
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            base.OnScannedRobot(evnt);
            double num = base.Heading + evnt.Bearing;
            double num2 = Utils.NormalRelativeAngleDegrees(num - base.GunHeading);
            if (evnt.Distance < 100.0)
            {
                base.Fire(2.0);
            }
            else if (evnt.Distance < 50.0)
            {
                for (int i = 0; i > 3; i++)
                {
                    base.Fire(2.0);
                }
            }
            else
            {
                base.Fire(1.5);
            }
        }

        public override void Run()
        {
            base.SetColors(Color.MediumPurple, Color.Silver, Color.Orange);
            base.TurnLeft(base.Heading % 90.0);
            base.Ahead(base.BattleFieldHeight - base.Y);
            if (base.X > (base.BattleFieldWidth / 2.0))
            {
                base.SetTurnRight(90.0);
                base.Ahead((base.BattleFieldWidth / 2.0) - base.X);
                base.SetTurnRight(90.0);
                base.Ahead(base.BattleFieldHeight / 3.0);
            }
            else if (base.X < (base.BattleFieldWidth / 2.0))
            {
                base.SetTurnLeft(90.0);
                base.Ahead((base.BattleFieldWidth / 2.0) + base.X);
                base.SetTurnRight(90.0);
                base.Ahead(base.BattleFieldHeight / 3.0);
            }
            bool flag = true;
            double num = base.X;
            double num2 = base.Y;
            double num3 = num - base.BattleFieldWidth;
            double num4 = num2 - base.BattleFieldHeight;
            while (true)
            {
                if (flag)
                {
                    num = base.X;
                    num2 = base.Y;
                    num3 = num - base.BattleFieldWidth;
                    num4 = num2 - base.BattleFieldHeight;
                    if ((((num <= 60.0) || (num >= 740.0)) || (num2 <= 60.0)) || (num2 >= 540.0))
                    {
                        flag = false;
                    }
                    else
                    {
                        flag = true;
                        base.SetAhead(this.movingForward);
                        this.isMovingForward = true;
                        base.SetTurnRight(double.PositiveInfinity);
                    }
                }
                else
                {
                    base.TurnLeft(base.Heading % 90.0);
                    base.SetBack(200.0);
                    base.TurnLeft(90.0);
                    flag = true;
                }
                base.SetTurnGunRight(double.PositiveInfinity);
                this.Execute();
            }
        }
    }
}

 

 
