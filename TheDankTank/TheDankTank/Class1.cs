using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Robocode;



namespace TheDankTank
{
    public class WeegeeTank : Robot
    {
        //Functions
        void colourFlash()
        {
            this.SetColors(System.Drawing.Color.White, System.Drawing.Color.Green, System.Drawing.Color.Blue);
            this.SetColors(System.Drawing.Color.Red, System.Drawing.Color.Blue, System.Drawing.Color.Purple);
            this.SetColors(System.Drawing.Color.Green, System.Drawing.Color.Purple, System.Drawing.Color.White);
            this.SetColors(System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.Red);
            this.SetColors(System.Drawing.Color.Purple, System.Drawing.Color.Red, System.Drawing.Color.Green);
        }

        public override void Run()//Starts the tank, only 1 run tank is allowed
        {
            while (true)
            {
                colourFlash();
            }


        }//end of Run()
        public override void OnScannedRobot(ScannedRobotEvent evnt)
        {
            base.OnScannedRobot(evnt);
            this.Ahead(100);
            if (evnt.Distance < 100)
            {
                this.Fire(3);
            }
            else if (evnt.Distance < 200)
            {
                this.Fire(2);
            }
            else
            {
                //this.Fire(1);
            }
            // this.Fire(3);
        }
    }
    /*public class WeegeeTank:Robot
    {
        //Functions
        void colourFlash()
        {
            this.SetColors(System.Drawing.Color.White, System.Drawing.Color.Green, System.Drawing.Color.Blue); 
            this.SetColors(System.Drawing.Color.Red, System.Drawing.Color.Blue, System.Drawing.Color.Purple);
            this.SetColors(System.Drawing.Color.Green, System.Drawing.Color.Purple, System.Drawing.Color.White);
            this.SetColors(System.Drawing.Color.Blue, System.Drawing.Color.White, System.Drawing.Color.Red);
            this.SetColors(System.Drawing.Color.Purple, System.Drawing.Color.Red, System.Drawing.Color.Green);
        }
        public override void Run()
        {
            while (true)
            {
                colourFlash();
            }
        }
    }*/
}
