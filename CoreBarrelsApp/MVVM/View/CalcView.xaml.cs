using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CoreBarrelsApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для CalcView.xaml
    /// </summary>
    public partial class CalcView : UserControl
    {
        public CalcView()
        {
            InitializeComponent();
            ModelHolder.CalcView = this;
            LoadDB();
        }

        public List<TextBox> GetInput()
        {
            return new List<TextBox>() { TB_Ik, TB_Ii, TB_Mk, TB_Dk, TB_Hp, TB_P, TB_Q, TB_q, TB_Kp, TB_I, TB_Mkadd, TB_Z, TB_Vm, TB_Ksh, TB_Vv, TB_Dc, TB_Hk, TB_Db };
        }
        public List<TextBox> GetOutput()
        {
            return new List<TextBox>() { TB_Vkl, TB_Vkv, TB_Vko, TB_Vkmin, TB_Z1, TB_qRes, TB_Kkp, TB_Kko };
        }

        public void LoadDB()
        {
            using (var DB = new AppDbContext())
            {
                CB_GroundCoreListId.ItemsSource = DB.GroundCore.ToList();
            };
        }

        public void Button_Build_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var tbList = GetInput();
                Dictionary<string, double> numberList;

                try
                {
                    BaseMath.ValidateTBFloatNumbers(tbList);
                    numberList = BaseMath.CreateNumberListFromFormList(tbList);

                    BaseMath.ValidateGroundCoreValues(numberList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var itemEntity = GroundCoreEntity.CreateGroundCoreByDictionary(numberList, new GroundCoreEntity());
                db.GroundCore.Add(itemEntity);
                db.SaveChanges();
            }

            LoadDB();
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var item = (GroundCoreEntity)CB_GroundCoreListId.SelectedItem;
                if (item == null) return;

                db.GroundCore.Remove(item);

                db.SaveChanges();
            }

            LoadDB();
        }
        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            BaseMath.ClearInputs(GetInput());
            BaseMath.ClearInputs(GetOutput());
        }

        private void Button_Edit_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var tbList = GetInput();
                Dictionary<string, double> numberList;

                try
                {
                    BaseMath.ValidateTBFloatNumbers(tbList);
                    numberList = BaseMath.CreateNumberListFromFormList(tbList);
                    BaseMath.ValidateGroundCoreValues(numberList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var itemId = (GroundCoreEntity)CB_GroundCoreListId.SelectedItem;
                if (itemId != null)
                {
                    var itemEntity = GroundCoreEntity.CreateGroundCoreByDictionary(numberList, itemId);
                    db.GroundCore.Update(itemEntity);
                }

                db.SaveChanges();
            }

            LoadDB();
        }

        private void Button_DrillCalculator_Click(object sender, RoutedEventArgs e)
        {
            var tbList = GetInput();
            var formResultList = GetOutput();

            try
            {
                BaseMath.ValidateTBFloatNumbers(tbList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var numberList = BaseMath.CreateNumberListFromFormList(tbList);
            BaseMath.FillInputsByNames(formResultList, numberList);
        }

        private void CB_ListId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (sender as ComboBox).SelectedIndex;
            var test2 = (sender as ComboBox).Text;
            //MessageBox.Show(test2, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);


            var item = (GroundCoreEntity)CB_GroundCoreListId.SelectedItem;
            if (item == null) return;

            var itemList = new List<double> { item.UpperCoreLength, item.UpperCompletedLength, item.UpperCoreWeight, item.CoreDiameter, item.CoreCountPerCycle, item.GroundDensity,
                item.VesselVolume, item.WaterVolume, item.KEqualOre, item.ErasionDegree, item.AllowedTechError, item.BoreDepth,
                item.DrillSpeed, item.KExperience, item.WashWaterRiseSpeed, item.BoreDiameter, item.RangeFromSlaughterToCarrier, item.AttitudeCoreToDrillDiameter};
            var tbList = GetInput();

            int i = 0;
            foreach (var tb in tbList)
            {
                tb.Text = itemList[i].ToString();
                i++;
            }

        }
    }
}
