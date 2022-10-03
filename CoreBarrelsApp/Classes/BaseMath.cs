using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CoreBarrelsApp
{
    public class BaseMath
    {
        public static Dictionary<string, double> CreateNumberListFromFormList(List<TextBox> tbList)
        {
            var numberList = new Dictionary<string, double>();
            foreach (var item in tbList) numberList.Add(item.Name, Double.Parse(item.Text));
            return numberList;
        }

        /// <summary>
        /// Проверяет поля на соответствие положительным дробным числам
        /// </summary>
        /// <param name="tbList">Список полей для валидации</param>
        /// <exception cref="Exception">Пустые поля; Любые знаки кроме чисел и запятых; отрицательные и равные нулю</exception>
        public static void ValidateTBFloatNumbers(List<TextBox> tbList, bool withZero = false)
        {
            if (tbList.Any(tb => String.IsNullOrEmpty(tb.Text))) throw new Exception("Не заполнены поля");
            if (tbList.Any(tb => !double.TryParse(tb.Text, out double number))) throw new Exception("Используйте только запятые и числа");
            if (tbList.Any(tb => Double.Parse(tb.Text) < 0)) throw new Exception("Числа должны быть положительные");
            if (tbList.Any(tb => Double.Parse(tb.Text) == 0) && withZero == false) throw new Exception("Числа должны быть положительные");
            if (tbList.Any(tb => Double.Parse(tb.Text) >= 9999)) throw new Exception("Числа должны быть не больше 9999!");
        }

        /// <summary>
        /// Проверяет валидность данных о грунтовых понрах
        /// </summary>
        /// <param name="tbList">Список чисел и их названия</param>
        /// <exception cref="Exception">Пустые поля; Любые знаки кроме чисел и запятых; отрицательные и равные нулю</exception>
        public static void ValidatePoreValues(Dictionary<string, double> numbersList)
        {
            if (numbersList["TB_VolumeSample"] < numbersList["TB_VolumeSamplePores"]) throw new Exception("Объём пор не должен превышать объём образца породы");
            if (numbersList["TB_VolumeSamplePores"] < (numbersList["TB_VolumeSampleOpenedPores"] + numbersList["TB_VolumeSampleClosedPores"])) throw new Exception("Объём открытых и закрытых пор должен быть меньше или равен общему объёму пор");
            if (numbersList["TB_VolumeSamplePores"] < numbersList["TB_VolumeSamplePoresWithWater"]) throw new Exception("Объём заполненного водой пространства должен быть меньше равен объёму пор");
            if (numbersList["TB_Porosity"] <= 1) throw new Exception("Межзерновая пористость должна быть больше единицы");
        }

        public static void ValidateGroundCoreValues(Dictionary<string, double> numbersList)
        {
        }

        public static void ValidateCoreBarrel(Dictionary<string, double> numbersList)
        {

            if (numbersList["TB__B"] < 10 || numbersList["TB__B"] > 15) throw new Exception("Значение B должно быть между 10 и 15");
            if (numbersList["TB__H"] < 24.8 || numbersList["TB__H"] > 30) throw new Exception("Значение H должно быть между 24.8 и 30");
            if (numbersList["TB__D"] < 120 || numbersList["TB__D"] > 140) throw new Exception("Значение D должно быть между 120 и 140");
            if (numbersList["TB__Z"] < 10 || numbersList["TB__Z"] > 13) throw new Exception("Значение Z должно быть между 10 и 13");
        }
        public static double getFunctionByName(string name, Dictionary<string, double> parameters)
        {
            if (name == "TB_Vkl") return GroundCore.GetCoreQuitPercent(parameters["TB_Ik"], parameters["TB_Ii"]);
            if (name == "TB_Vkv") return GroundCore.GetWeightCoreQuitPercent(parameters["TB_Mk"], parameters["TB_Dk"], parameters["TB_Hp"], parameters["TB_P"]);
            if (name == "TB_Vko") return GroundCore.GetVolumeCoreQuitPercent(parameters["TB_Hp"], parameters["TB_Dk"], parameters["TB_Q"], parameters["TB_q"]);
            if (name == "TB_Vkmin") return GroundCore.GetMinimalCoreOutput(parameters["TB_Kp"], parameters["TB_I"], parameters["TB_Mkadd"]);
            if (name == "TB_Z1") return GroundCore.GetBoreDepthFrom(parameters["TB_Z"], parameters["TB_Vm"], parameters["TB_Ksh"], parameters["TB_Vv"]);
            if (name == "TB_qRes") return GroundCore.GetSludgeSpeed(parameters["TB_Vm"], parameters["TB_Dc"]);
            if (name == "TB_Kkp") return GroundCore.GetKCoreInput(parameters["TB_Dk"], parameters["TB_Hk"]);
            if (name == "TB_Kko") return GroundCore.GetKCoreFilter(parameters["TB_Dk"], parameters["TB_Db"]);

            if (name == "TB_KGeneral") return GroundPore.GetKGeneralPores(parameters["TB_VolumeSample"], parameters["TB_VolumeSamplePores"]);
            if (name == "TB_KOpened") return GroundPore.GetKOpenPores(parameters["TB_VolumeSample"], parameters["TB_VolumeSampleOpenedPores"]);
            if (name == "TB_KClosed") return GroundPore.GetKClosedPores(parameters["TB_VolumeSample"], parameters["TB_VolumeSampleClosedPores"]);
            if (name == "TB_KEffective") return GroundPore.GetKEffectivePores(parameters["TB_VolumeSample"], parameters["TB_VolumeSampleOpenedPores"], parameters["TB_VolumeSamplePoresWithWater"]);
            if (name == "TB_KSecondary") return GroundPore.GetKSecondPores(parameters["TB_VolumeSample"], parameters["TB_VolumeSamplePores"], parameters["TB_Porosity"]);

            return 0;
        }

        public static void FillInputsByNames(List<TextBox> formList, Dictionary<string, double> numberList)
        {
            foreach (var item in formList) item.Text = Convert.ToString(getFunctionByName(item.Name, numberList));
        }

        public static void ClearInputs(List<TextBox> formList)
        {
            foreach (var item in formList) item.Text = "";
        }
    }
}
