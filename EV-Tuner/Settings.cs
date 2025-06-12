using System;

namespace EV_Tuner
{
    public class Settings
    {
        // This is not a comprehensive list of variables yet, might need add more.

        public int serviceTaskManagerPeriod { get; set; }
        public int taskManagerPeriod { get; set; }
        public int maxTaskPeriod { get; set; }
        public int maxServiceTaskPeriod { get; set; }
        public int minTaskPeriod { get; set; }

        // Driving Settings
        public int maxElectricalPower { get; set; }
        public int maxMotorTorque { get; set; }
        public int absoluteMaxAccumulatorCurrent { get; set; }
        public int maxAccumulatorCurrent5s { get; set; }
        public int absoluteMaxMotorRPM { get; set; }
        public int regenRPMThreshold { get; set; }
        public int minAPPSOffset { get; set; }
        public int maxAPPSOffset { get; set; }
        public int minAPPSValue { get; set; }
        public int maxAPPSValue { get; set; }
        public int minBPSValue { get; set; }
        public int maxBPSValue { get; set; }
        public int appsTOP { get; set; }
        public int appsBOTTOM { get; set; }
        public int appsPlausibilityCheckActiviationThreshold { get; set; }
        public int bpsPlausibilityCheckActiviationThreshold { get; set; }
        public int appsPlausibilityCheckRecoveryThreshold { get; set; }
        public int bpsPlausibilityCheckRecoveryThreshold { get; set; }
        public int numberDrivingModes { get; set; }
        public int drivingLoopPeriod { get; set; }
        public int regenSOCThreshold { get; set; }
        public int someBoolFlags { get; set; }
        public int maxElecPowerChecksum { get; set; } // needs to be added to UI

        // driving modes aren't getting serialized for now.
        DrivingMode drivingMode0;
        DrivingMode drivingMode1;
        DrivingMode drivingMode2;

        //Daq
        public int throttleDAQToPreservePerformance { get; set; }
        public int minDAQPeriod { get; set; }
    }
}
