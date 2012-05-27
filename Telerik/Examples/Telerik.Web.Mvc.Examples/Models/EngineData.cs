namespace Telerik.Web.Mvc.Examples.Models
{
    public class EngineDataPoint
    {
        public EngineDataPoint(int rpm, double torque, double power)
        {
            RPM = rpm;
            Torque = torque;
            Power = power;
        }

        public int RPM;
        public double Torque;
        public double Power;
    }

    public static class EngineData
    {
        public static readonly EngineDataPoint[] Points = new EngineDataPoint[]
        {
            new EngineDataPoint(1000, 50,  10),
            new EngineDataPoint(1500, 65,  19),
            new EngineDataPoint(2000, 80,  30),
            new EngineDataPoint(2500, 92,  44),
            new EngineDataPoint(3000, 104, 59),
            new EngineDataPoint(3500, 114, 76),
            new EngineDataPoint(4000, 120, 91),
            new EngineDataPoint(4500, 125, 107),
            new EngineDataPoint(5000, 130, 124),
            new EngineDataPoint(5500, 133, 139),
            new EngineDataPoint(6000, 130, 149),
            new EngineDataPoint(6500, 122, 151),
            new EngineDataPoint(7000, 110, 147)
        };
    }
}
