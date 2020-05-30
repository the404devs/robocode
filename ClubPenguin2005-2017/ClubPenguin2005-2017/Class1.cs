using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;
using System.Drawing;


namespace ClubPenguin2005_2017
{

    public class LanSchool:Robot
    {
        //Functions
        void colourFlash()
        {
            //this.Fire(1);
            this.SetColors(System.Drawing.Color.White, System.Drawing.Color.Green, System.Drawing.Color.Blue);
            this.SetColors(System.Drawing.Color.Red, System.Drawing.Color.Blue, System.Drawing.Color.Purple);
            this.SetColors(System.Drawing.Color.Green, System.Drawing.Color.Purple, System.Drawing.Color.White);
            this.SetColors(System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.Red);
            this.SetColors(System.Drawing.Color.Purple, System.Drawing.Color.Red, System.Drawing.Color.Green);

        }
        void shoot()
        { 
            this.Fire(1); 
        }
        
        public override void Run()//Starts the tank, only 1 run tank is allowed
        {
            base.Run();
            this.TurnLeft(this.Heading);
            while (true)
            {
                this.TurnLeft(1);
            }


        }//end of Run()


    }//end of Glenn44

    public class Glenn45:Robot
    {
        public override void Run()
        {
            base.Run();
            this.TurnLeft(this.Heading);
            //this.Ahead(400);
            this.Ahead(this.BattleFieldHeight - this.Y);
        }
        
    }//end of glenn45

    public class DankTank : Robot
    {
        public override void Run()
        {
            base.Run();
            this.TurnLeft(this.Heading);
            while (true)
            {
                this.Ahead(5);
            
            }

        }
        public override void OnHitWall(HitWallEvent evnt)
        {
            base.OnHitWall(evnt);
            this.TurnLeft(180);
        }
        public override void OnHitByBullet(HitByBulletEvent evnt)
        {
            base.OnHitByBullet(evnt);
            this.TurnRight(evnt.Bearing);
            this.Fire(3);
        }

    }
}
