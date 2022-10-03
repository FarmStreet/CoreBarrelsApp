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
    public partial class CalcPoreView : UserControl
    {
        public CalcPoreView()
        {
            InitializeComponent();
            ModelHolder.CalcPoreView = this;
            LoadDB();
        }

        public List<TextBox> GetInput()
        {
            return new List<TextBox>() { TB_VolumeSample, TB_VolumeSamplePores, TB_VolumeSampleOpenedPores, TB_VolumeSampleClosedPores, TB_VolumeSamplePoresWithWater, TB_Porosity };
        }
        public List<TextBox> GetOutput()
        {
            return new List<TextBox>() { TB_KGeneral, TB_KOpened, TB_KClosed, TB_KEffective, TB_KEffective, TB_KSecondary };
        }

        public void LoadDB()
        {
            using (var DB = new AppDbContext())
            {
                CB_GroundPoreListId.ItemsSource = DB.GroundPore.ToList();
            };
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            BaseMath.ClearInputs(GetInput());
            BaseMath.ClearInputs(GetOutput());
            TB_GotPores.Text = "";
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
                    BaseMath.ValidatePoreValues(numberList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var itemEntity = GroundPoreEntity.CreateGroundPoreByDictionary(numberList, new GroundPoreEntity());
                db.GroundPore.Add(itemEntity);

                db.SaveChanges();
            }

            LoadDB();
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var item = (GroundPoreEntity)CB_GroundPoreListId.SelectedItem;
                if (item == null) return;

                db.GroundPore.Remove(item);

                db.SaveChanges();
            }

            LoadDB();
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
                    BaseMath.ValidatePoreValues(numberList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var itemId = (GroundPoreEntity)CB_GroundPoreListId.SelectedItem;
                if (itemId != null)
                {
                    var itemEntity = GroundPoreEntity.CreateGroundPoreByDictionary(numberList, itemId);
                    db.GroundPore.Update(itemEntity);
                }

                db.SaveChanges();
            }

            LoadDB();
        }

        private void Button_GetPoresResult_Click(object sender, RoutedEventArgs e)
        {
            var tbList = GetInput();
            var tbResultList = GetOutput();

            try
            {
                BaseMath.ValidateTBFloatNumbers(tbList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Dictionary<string, double> numberList = BaseMath.CreateNumberListFromFormList(tbList);

            try
            {
                BaseMath.ValidatePoreValues(numberList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // методы с большой буквы, локальные с малеьнкой, классовые с большой
            BaseMath.FillInputsByNames(tbResultList, numberList);
            TB_GotPores.Text = GroundPore.GetGroundAnalyse(numberList["TB_VolumeSample"], numberList["TB_VolumeSamplePores"]);
        }

        private void CB_ListId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (sender as ComboBox).SelectedIndex;
            var test2 = (sender as ComboBox).Text;
            //MessageBox.Show(test2, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);

            var item = (GroundPoreEntity)CB_GroundPoreListId.SelectedItem;
            if (item == null) return;

            var itemList = new List<double> { item.VolumeSample, item.VolumeSamplePores, item.VolumeSampleOpenedPores, item.VolumeSampleClosedPores, item.VolumeSamplePoresWithWater, item.Porosity };
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
