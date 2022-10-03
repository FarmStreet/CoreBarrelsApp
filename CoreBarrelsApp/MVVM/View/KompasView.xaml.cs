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
    /// Логика взаимодействия для KompasView.xaml
    /// </summary>
    public partial class KompasView : UserControl
    {
        public KompasView()
        {
            InitializeComponent();
            ModelHolder.KompasView = this;
            LoadDB();
        }
        public List<TextBox> GetInput()
        {
            return new List<TextBox>() { TB__B, TB__H, TB__D, TB__Z };
        }
        public List<TextBox> GetOutput()
        {
            return new List<TextBox>() { };
        }
        public void LoadDB(string name = "all")
        {
            using (var DB = new AppDbContext())
            {
                CB_CoreBarrelListId.ItemsSource = DB.CoreBarrel.ToList();
            };
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            BaseMath.ClearInputs(GetInput());
            BaseMath.ClearInputs(GetOutput());
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
                    BaseMath.ValidateCoreBarrel(numberList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var itemEntity = CoreBarrel.CreateCoreBarrelByDictionary(numberList, new CoreBarrel());
                db.CoreBarrel.Add(itemEntity);

                db.SaveChanges();
            }

            LoadDB();
        }
        private void Button_Delete_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new AppDbContext())
            {
                var item = (CoreBarrel)CB_CoreBarrelListId.SelectedItem;
                if (item == null) return;

                db.CoreBarrel.Remove(item);

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
                    BaseMath.ValidateCoreBarrel(numberList);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var itemId = (CoreBarrel)CB_CoreBarrelListId.SelectedItem;
                if (itemId != null)
                {
                    var itemEntity = CoreBarrel.CreateCoreBarrelByDictionary(numberList, itemId);
                    db.CoreBarrel.Update(itemEntity);
                }

                db.SaveChanges();
            }

            LoadDB();
        }


        private void Button_Kompas_Click(object sender, RoutedEventArgs e)
        {
            var tbList = GetInput();
            Dictionary<string, double> numberList;

            try
            {
                BaseMath.ValidateTBFloatNumbers(tbList);
                numberList = BaseMath.CreateNumberListFromFormList(tbList);
                BaseMath.ValidateCoreBarrel(numberList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            numberList.Add("TB__D_VNSH", 120 + (numberList["TB__D"] - 120));
            numberList.Add("TB__D_VNSH_2", 120 + (numberList["TB__D"] - 120));
            numberList.Add("TB__D_VNTR", 110 + (numberList["TB__D"] - 120));
            numberList.Add("TB__D_VNTR2", 100 + (numberList["TB__D"] - 120));

            numberList.Add("TB__D_1", 11.67 + ((numberList["TB__D"] - 120) / 2));
            numberList.Add("TB__D_2", 25.6609 + ((numberList["TB__D"] - 120) / 2));
            numberList.Add("TB__D_3", 5 + ((numberList["TB__D"] - 120) / 2));
            numberList.Add("TB__M", numberList["TB__Z"]);

            KompasModel.Compile(numberList);
        }

        private void CB_ListId_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var test = (sender as ComboBox).SelectedIndex;
            var test2 = (sender as ComboBox).Text;
            //MessageBox.Show(test2, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);


                var item = (CoreBarrel)CB_CoreBarrelListId.SelectedItem;
                if (item == null) return;

                var itemList = new List<double> { item.B, item.H, item.D, item.Z };
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
