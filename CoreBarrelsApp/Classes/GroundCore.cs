using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public class GroundCore
    {

        /// <summary>
        /// Bк.л. Отдает процент выхода керна
        /// </summary> 
        public static double GetCoreQuitPercent(double upperCoreLength, double upperCompletedLength)
        {
            return Math.Round(upperCoreLength / upperCompletedLength * 100);
        }

        /// <summary>
        /// Вк.в. Отдает процент выхода керна при весовом способе
        /// </summary>
        public static double GetWeightCoreQuitPercent(double UpperCoreWeight, double CoreDiameter, double CoreCountPerCycle, double GroundDensity)
        {
            return Math.Round(4 * UpperCoreWeight / (Math.PI * Math.Pow(CoreDiameter, 2) * CoreCountPerCycle * GroundDensity) * 100);
        }

        /// <summary>
        /// Вк.о. Отдает процент выхода керна при обьёмном способе
        /// </summary>    
        public static double GetVolumeCoreQuitPercent(double coreCountPerCycle, double coreDiameter, double vesselVolume, double waterVolume)
        {
            return 124 * coreCountPerCycle * (vesselVolume - waterVolume) / Math.Pow(coreDiameter, 2);
        }

        /// <summary>
        /// Bк.min минимально допустимый выход керна 
        /// </summary>
        public static double GetMinimalCoreOutput(double kEqualOre, double erasionDegree, double allowedTechError)
        {
            return ((1 - kEqualOre) * erasionDegree) / ((1 - kEqualOre) * erasionDegree + kEqualOre * allowedTechError);
        }

        /// <summary>
        /// z1 - Глубина скважины
        /// </summary>
        public static double GetBoreDepthFrom(double boreDepth, double averageSpeed, double kExperience, double washWaterRiseSpeed)
        {
            return (boreDepth - ((boreDepth * averageSpeed * kExperience) / washWaterRiseSpeed));
        }

        /// <summary>
        /// q - скорость поступления шлама в промывочную жидкость, кг/ч
        /// </summary>
        public static double GetSludgeSpeed(double averageSpeed, double boreDiameter)
        {
            return 0.2 * averageSpeed * Math.Pow(boreDiameter, 2);
        }

        /// <summary>
        /// Kк.п. Коэффициент керноприема
        /// </summary>
        public static double GetKCoreInput(double coreDiameter, double rangeFromSlaughterToCarrier)
        {
            return coreDiameter / rangeFromSlaughterToCarrier;
        }

        /// <summary>
        /// Кк.о. Коэффициент керноприема
        /// </summary>
        public static double GetKCoreFilter(double coreDiameter, double attitudeCoreToDrillDiameter)
        {
            return coreDiameter / attitudeCoreToDrillDiameter;
        }
    }


}
