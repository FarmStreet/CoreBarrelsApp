using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public class CoreBarrel
    {
        // TODO добавить нормальные данные
        public int Id { get; set; }
        public double B { get; set; }
        public double H { get; set; }
        public double D { get; set; }
        public double Z { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
        public static CoreBarrel CreateCoreBarrelByDictionary(Dictionary<string, double> parameters, CoreBarrel coreBarrel)
        {
            coreBarrel.B = parameters["TB__B"];
            coreBarrel.H = parameters["TB__H"];
            coreBarrel.D = parameters["TB__D"];
            coreBarrel.Z = parameters["TB__Z"];

            return coreBarrel;
        }
    }
}
