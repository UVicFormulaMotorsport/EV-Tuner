using System;

namespace EV_Tuner
{
    public class Settings
    {
        int serviceTaskManagerPeriod;
        int taskManagerPeriod;
        int maxTaskPeriod;
        int maxServiceTaskPeriod;
        int minTaskPeriod;

        // Driivng Settings
        int maxElectricalPower;
        int maxMotorTorque;
        int absoluteMaxAccumulatorCurrent;
        int maxAccumulatorCurrent5s;
        int absoluteMaxMotorRPM;


        public static void Testing()
        {
            Console.WriteLine("Testing the settings class");
        }
    }
}
