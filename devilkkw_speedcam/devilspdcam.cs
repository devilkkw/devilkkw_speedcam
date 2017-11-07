using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

namespace devilkkw_speedcam
{

    

    public class Devilspdcam : Script
    {

       ///SpeedCamera Script by Devilkkw
	   /// is licensed under the
       /// GNU General Public License v3.0

        string ModName = "SpeedCamera";
        string Developer = "Devilkkw";
        string Version = "1.0";
        bool firstTime = true;
        Ped playerPed;


        float maxSpeed = 30.0F;
        float MaxmaxSpeed = 130.0F;
        float minSpeed = 10.0F;
        string info = "nope";
        GTA.Vehicle LastVehicle;
       

        public Devilspdcam()
        {
            Tick += OnTick;
            KeyDown += OnKeyDown;
            Interval = 1;
        }

    
        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.G)
            {
                if (maxSpeed < MaxmaxSpeed)
                {
                    maxSpeed += 10;
                }
                else maxSpeed = 30;

                UI.ShowSubtitle("~g~Speed Limit seto to:~r~" + maxSpeed,500);

            }

            if (e.KeyCode == Keys.B)
            {


               
                GTA.Vehicle nearVehicles = GTA.World.GetClosestVehicle(playerPed.Position, 200);
                if (nearVehicles != null)
                {
                    
                    string CurrentCarName = nearVehicles.DisplayName;
                    string CurrentCarPlate = nearVehicles.NumberPlate;
                    float CurrentCarSpeedKm = nearVehicles.Speed * 3.6F;
                    float CurrentCarSpeedMp = nearVehicles.Speed * 2.236936F;

                    

                    if (CurrentCarSpeedKm > minSpeed)
                    {

                        if (CurrentCarSpeedKm > maxSpeed)
                        {


                            LastVehicle = nearVehicles;
                            info = string.Format("~g~Model:~w~ {0} ~n~~b~Plate:~w~ {1} ~n~~y~Km/h: ~r~{2}~n~~y~Mph: ~r~{3}", CurrentCarName, CurrentCarPlate, Math.Ceiling(CurrentCarSpeedKm), Math.Ceiling(CurrentCarSpeedMp));

                            
                           


                           

                        }
                        else info = string.Format("~g~Model:~w~ {0} ~n~~b~Plate:~w~ {1} ~n~~y~Km/h: ~g~{2}~n~~y~Mph: ~g~{3}", CurrentCarName, CurrentCarPlate, Math.Ceiling(CurrentCarSpeedKm), Math.Ceiling(CurrentCarSpeedMp));



                    }
                    UI.Notify(info);
                }
            }




            if (e.KeyCode == Keys.T && LastVehicle != null)
            {

               
                LastVehicle.AddBlip();
                UI.ShowSubtitle("~b~Suspect Added~r~",500);

            }


        }



        private void OnTick(object sender, EventArgs e)
        {
            
            if (firstTime)
            {

                
                
                UI.Notify("~r~" + ModName + "~w~ " + Version + " by~g~ " + Developer + "~w~Loaded");
                firstTime = false;
            }
			playerPed = Game.Player.Character;


            


        }




    }
}
