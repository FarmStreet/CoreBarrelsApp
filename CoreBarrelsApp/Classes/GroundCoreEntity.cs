using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public class GroundCoreEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// lk - длина поднятого керна, м
        /// </summary>
        public double UpperCoreLength { get; set; }

        /// <summary>
        /// lи - длина пройденного интервала, м
        /// </summary>
        public double UpperCompletedLength { get; set; }

        /// <summary>
        /// mк - фактическая масса поднятого керна
        /// </summary>
        public double UpperCoreWeight { get; set; }

        /// <summary>
        /// dк - диаметр керна
        /// </summary>
        public double CoreDiameter { get; set; }

        /// <summary>
        /// hр - проходка за рейс
        /// </summary>
        public double CoreCountPerCycle { get; set; }

        /// <summary>
        /// ρ - плотность породы
        /// </summary>
        public double GroundDensity { get; set; }

        /// <summary>
        /// Q - Объем соответственно мерного сосуда, дм3
        /// </summary>
        public double VesselVolume { get; set; }

        /// <summary>
        /// q - Объем доливаемой воды, дм3
        /// </summary>
        public double WaterVolume { get; set; }

        /// <summary>
        /// Кр - коэффициент равномерности оруденения (отношение среднего содержания компонента в руде к максимальному)
        /// </summary>
        public double KEqualOre { get; set; }

        /// <summary>
        /// И - степень избирательности истирания (доля перетертого керна, приходящая на рудный материал)
        /// </summary>
        public double ErasionDegree { get; set; }

        /// <summary>
        /// mк доп - допустимая техническая погрешность опробования, отн.ед
        /// </summary>
        public double AllowedTechError { get; set; }

        /// <summary>
        /// z - глубина скважины в момент отбора шлама, м
        /// </summary>
        public double BoreDepth { get; set; }

        /// <summary>
        ///  kш=0,80÷0,85 - опытный поправочный коэффициент к скорости подъема частиц шлама размером 0,4-0,5 мм при нормальном глинистом растворе
        /// </summary>
        public double KExperience { get; set; }

        /// <summary>
        ///  vв - скорость восходящего потока промывочной жидкости, м/ч.
        /// </summary>
        public double WashWaterRiseSpeed { get; set; }

        /// <summary>
        /// vм - механическая скорость бурения, м/ч
        /// </summary>
        public double DrillSpeed { get; set; }

        /// <summary>
        /// Dc - диаметр скважины, см
        /// </summary>
        public double BoreDiameter { get; set; }

        /// <summary>
        /// hк - расстояние от забоя до груноноски
        /// </summary>
        public double RangeFromSlaughterToCarrier { get; set; }

        /// <summary>
        /// Dб.г - Отношение диаметра керна к диаметру бурильной головки 
        /// </summary>
        public double AttitudeCoreToDrillDiameter { get; set; }
        public override string ToString()
        {
            return Id.ToString();
        }
        public static GroundCoreEntity CreateGroundCoreByDictionary(Dictionary<string, double> parameters, GroundCoreEntity groundCore)
        {
            groundCore.UpperCoreLength = parameters["TB_Ik"];
            groundCore.UpperCompletedLength = parameters["TB_Ii"];
            groundCore.UpperCoreWeight = parameters["TB_Mk"];
            groundCore.CoreDiameter = parameters["TB_Dk"];
            groundCore.CoreCountPerCycle = parameters["TB_Hp"];
            groundCore.GroundDensity = parameters["TB_P"];
            groundCore.VesselVolume = parameters["TB_Q"];
            groundCore.WaterVolume = parameters["TB_q"];
            groundCore.KEqualOre = parameters["TB_Kp"];
            groundCore.ErasionDegree = parameters["TB_I"];
            groundCore.AllowedTechError = parameters["TB_Mkadd"];
            groundCore.BoreDepth = parameters["TB_Z"];
            groundCore.KExperience = parameters["TB_Ksh"];
            groundCore.WashWaterRiseSpeed = parameters["TB_Vv"];
            groundCore.DrillSpeed = parameters["TB_Vm"];
            groundCore.BoreDiameter = parameters["TB_Dc"];
            groundCore.RangeFromSlaughterToCarrier = parameters["TB_Hk"];
            groundCore.AttitudeCoreToDrillDiameter = parameters["TB_Db"];

            return groundCore;
        }
    }
}
