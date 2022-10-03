using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    // Helper под функции, Entity под свойства
    public class GroundPore
    {
        /// <summary>
        /// алгоритм подсчета коэффициента общей пористости
        /// </summary>
        /// <returns>
        /// Коэффициент общей пористости
        /// </returns>
        public static double GetKGeneralPores(double volumeSample, double volumeSamplePores)
        {
            return Math.Round(volumeSamplePores / volumeSample, 2);
        }

        /// <summary>
        /// алгоритм подсчета коэффициента открытой пористости
        /// </summary>
        /// <returns>
        /// Коэффициент открытой пористости
        /// </returns>
        public static double GetKOpenPores(double volumeSample, double volumeSampleOpenedPores)
        {
            return Math.Round(volumeSampleOpenedPores / volumeSample, 2);
        }

        /// <summary>
        /// алгоритм подсчета коэффициента закрытой пористости
        /// </summary>
        /// <returns>
        /// Коэффициент закрытой пористости
        /// </returns>
        public static double GetKClosedPores(double volumeSample, double volumeSampleClosedPores)
        {
            return Math.Round(volumeSampleClosedPores / volumeSample, 2);
        }

        /// <summary>
        /// алгоритм подсчета коэффициента эффективной пористости
        /// </summary>
        /// <returns>
        /// Коэффициент эффективной пористости
        /// </returns>
        public static double GetKEffectivePores(double volumeSample, double volumeSampleOpenedPores, double volumeSamplePoresWithWater)
        {
            return Math.Round((volumeSampleOpenedPores / volumeSamplePoresWithWater) / volumeSample, 2);
        }

        /// <summary>
        /// Алгоритм подсчета коэффициента вторичной пористости
        /// </summary>
        /// <returns>
        /// Коэффициент пористости вторичной породы
        /// </returns>
        public static double GetKSecondPores(double volumeSample, double volumeSamplePores, double porosity)
        {
            return Math.Round((GetKGeneralPores(volumeSample, volumeSamplePores) - porosity) / (1 - porosity), 2);
        }

        /// <summary>
        /// Алгоритм анализа пористости породы
        /// </summary>
        /// <returns>
        /// Уровень пористости породы
        /// </returns>
        public static string GetGroundAnalyse(double volumeSample, double volumeSamplePores)
        {
            double percentGeneralPores = GetKGeneralPores(volumeSample, volumeSamplePores) * 100;

            string resultMessage = "Низкая";
            if (percentGeneralPores >= 5.0) resultMessage = "Пониженная";
            if (percentGeneralPores >= 10.0) resultMessage = "Средняя";
            if (percentGeneralPores >= 15.0) resultMessage = "Повышенная";
            if (percentGeneralPores >= 20.0) resultMessage = "Высокая";

            return resultMessage + " пористость породы.";
        }
    }
}
