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

        // Driving Settings
        int maxElectricalPower;
        int maxMotorTorque;
        int absoluteMaxAccumulatorCurrent;
        int maxAccumulatorCurrent5s;
        int absoluteMaxMotorRPM;
        int regenRPMThreshold;
        int minAPPSOffset;
        int maxAPPSOffset;
        int minAPPSValue;
        int maxAPPSValue;
        int minBPSValue;
        int maxBPSValue;
        int appsTOP;
        int appsBOTTOM;
        int appsPlausibilityCheckActiviationThreshold;
        int bpsPlausibilityCheckActiviationThreshold;
        int appsPlausibilityCheckRecoveryThreshold;
        int bpsPlausibilityCheckRecoveryThreshold;
        int numberDrivingModes;
        int drivingLoopPeriod;
        int regenSOCThreshold;
        int someBoolFlags;
        int maxElecPowerChecksum;

        DrivingMode drivingMode0;
        DrivingMode drivingMode1;
        DrivingMode drivingMode2;

        //Daq
        int throttleDAQToPreservePerformance;
        int minDAQPeriod;


        public static void Testing()
        {
            Console.WriteLine("Testing the settings class");
        }
    }
}
