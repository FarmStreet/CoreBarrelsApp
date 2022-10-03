using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBarrelsApp
{
    public class GroundPoreEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Объём образца породы
        /// </summary>
        public double VolumeSample { get; set; }

        /// <summary>
        /// Общий объем пор в образце породы
        /// </summary>
        public double VolumeSamplePores { get; set; }

        /// <summary>
        /// Объем открытых пор в образце породы
        /// </summary>
        public double VolumeSampleOpenedPores { get; set; }

        /// <summary>
        /// Объем закрытых пор в образце породы
        /// </summary>
        public double VolumeSampleClosedPores { get; set; }

        /// <summary>
        /// Объем порового пространства, занятый водой
        /// </summary>
        public double VolumeSamplePoresWithWater { get; set; }

        /// <summary>
        /// Межзерновая первичная пористость
        /// </summary>
        public double Porosity { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

        public static GroundPoreEntity CreateGroundPoreByDictionary(Dictionary<string, double> parameters, GroundPoreEntity groundPore)
        {
            groundPore.VolumeSample = parameters["TB_VolumeSample"];
            groundPore.VolumeSamplePores = parameters["TB_VolumeSamplePores"];
            groundPore.VolumeSampleOpenedPores = parameters["TB_VolumeSampleOpenedPores"];
            groundPore.VolumeSampleClosedPores = parameters["TB_VolumeSampleClosedPores"];
            groundPore.VolumeSamplePoresWithWater = parameters["TB_VolumeSamplePoresWithWater"];
            groundPore.Porosity = parameters["TB_Porosity"];

            return groundPore;
        }
    }
}
