using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
using Robocode;
using System.Drawing;

namespace BJCBergade
{
    public class BJC1 : Robot
    {
        public override void OnBulletHit(BulletHitEvent evnt)
        {
            this.OnBulletHit(evnt);
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            this.OnHitByBullet(evnt);
            this.Ahead(this.BattleFieldHeight - this.X);
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            this.OnHitWall(evnt);
            this.TurnLeft(90.0);
            this.TurnGunRight(evnt.Bearing);
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            int num;
            this.OnScannedRobot(evnt);
            for (num = 0; num < 2; num++)
            {
                this.Fire(2.0);
            }
            for (num = 0; num < 10; num++)
            {
                this.Fire(1.0);
            }
        }

        public override void Run()
        {
            this.Run();
            this.SetColors(Color.Pink, Color.Black, Color.Pink);
            this.TurnLeft(this.Heading);
            while (true)
            {
                this.Ahead(this.BattleFieldHeight + this.X);
                this.TurnGunLeft(360.0);
            }
        }

        public void Stop()
        {
            this.Stop();
        }
    }

    public class BJC2 : Robot
    {
        public override void Run()
        {
            this.Run();
            this.SetColors(Color.Blue, Color.Red, Color.Black);
            this.TurnLeft(this.Heading);
            this.Ahead(this.BattleFieldHeight - this.Y);
        }
    }


    public class BJC3 : Robot
    {
        public override void OnBulletHit(BulletHitEvent evnt)
        {
            this.OnBulletHit(evnt);
        }

        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            this.OnHitByBullet(evnt);
            this.Ahead(this.BattleFieldHeight - this.X);
        }

        public override void OnHitWall(HitWallEvent evnt)
        {
            this.OnHitWall(evnt);
            this.TurnLeft(90.0);
            this.TurnGunRight(evnt.Bearing);
        }

        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            int num;
            this.OnScannedRobot(evnt);
            for (num = 0; num < 2; num++)
            {
                this.Fire(2.0);
            }
            for (num = 0; num < 10; num++)
            {
                this.Fire(1.0);
            }
        }

        public override void Run()
        {
            this.Run();
            this.SetColors(Color.Pink, Color.Black, Color.Pink);
            this.TurnLeft(this.Heading);
            while (true)
            {
                this.Ahead(this.BattleFieldHeight + this.X);
                this.TurnGunLeft(360.0);
            }
        }

        public void Stop()
        {
            this.Stop();
        }
    }   
}

