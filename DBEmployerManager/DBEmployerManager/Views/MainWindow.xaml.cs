using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DBEmployerManager.ViewModels;
using DBEmployerManager.Views;

namespace DBEmployerManager
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly Regex regex = new Regex("[^0-9.-]+");

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
            EV = new EmployerView();
            UV = new UnitView();
            OV = new OrderView();
            PV = new ProductView();
        }


        string CS
        {
            get { return (string)GetValue(CSProperty); }
            set { SetValue(CSProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CS.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CSProperty =
            DependencyProperty.Register("CS", typeof(string), typeof(MainWindow), new PropertyMetadata());



        EmployerView EV
        {
            get { return (EmployerView)GetValue(EVProperty); }
            set { SetValue(EVProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EV.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EVProperty =
            DependencyProperty.Register("EV", typeof(EmployerView), typeof(MainWindow), new PropertyMetadata());



        UnitView UV
        {
            get { return (UnitView)GetValue(UVProperty); }
            set { SetValue(UVProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UV.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UVProperty =
            DependencyProperty.Register("UV", typeof(UnitView), typeof(MainWindow), new PropertyMetadata());



        OrderView OV
        {
            get { return (OrderView)GetValue(OVProperty); }
            set { SetValue(OVProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OV.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OVProperty =
            DependencyProperty.Register("OV", typeof(OrderView), typeof(MainWindow), new PropertyMetadata());


        ProductView PV
        {
            get { return (ProductView)GetValue(PVProperty); }
            set { SetValue(PVProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PV.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PVProperty =
            DependencyProperty.Register("PV", typeof(ProductView), typeof(MainWindow), new PropertyMetadata());






        private void AddEmployer_Click(object sender, RoutedEventArgs e)
        {
            EV.Surname = TextSurname.Text;
            EV.Name = TextName.Text;
            EV.Patronymic = TextPatronymic.Text;
            EV.Born = CalendarBorn.SelectedDate != null ? CalendarBorn.SelectedDate.Value : new DateTime();
            EV.Gender = ComboGender.SelectedItem != null ? ComboGender.SelectedItem.ToString() : "";
            EV.Unit = ComboUnit.SelectedItem;
        }

        private void UpdateEmployer_Click(object sender, RoutedEventArgs e)
        {
            EV.Surname = TextSurname.Text;
            EV.Name = TextName.Text;
            EV.Patronymic = TextPatronymic.Text;
            EV.Born = CalendarBorn.SelectedDate != null ? CalendarBorn.SelectedDate.Value : new DateTime();
            EV.Gender = ComboGender.SelectedItem != null ? ComboGender.SelectedItem.ToString() : "";
            EV.Unit = ComboUnit.SelectedItem;
        }



        private void AddUnit_Click(object sender, RoutedEventArgs e)
        {
            UV.Name = TextUnitName.Text;
            UV.Employer = ComboHead.SelectedItem;
        }

        private void UpdateUnit_Click(object sender, RoutedEventArgs e)
        {
            UV.Name = TextUnitName.Text;
            UV.Employer = ComboHead.SelectedItem;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            OV.Contractor = TextOrderContractor.Text;
            OV.Date = CalendarOrderDate.SelectedDate != null ? CalendarOrderDate.SelectedDate.Value : DateTime.Now;
        }

        private void UpdateOrder_Click(object sender, RoutedEventArgs e)
        {
            OV.Contractor = TextOrderContractor.Text;
            OV.Date = CalendarOrderDate.SelectedDate != null ? CalendarOrderDate.SelectedDate.Value : DateTime.Now;
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            PV.Name = TextProductName.Text;
            PV.Count = Int32.Parse(TextProductCount.Text);
            PV.Price = Decimal.Parse(TextProductPrice.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        }

        private void UpdateProduct_Click(object sender, RoutedEventArgs e)
        {
            PV.Name = TextProductName.Text;
            PV.Count = Int32.Parse(TextProductCount.Text);
            PV.Price = Decimal.Parse(TextProductPrice.Text, NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        }

        private void ComboHead_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DataGrid_EmployerSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void DataGrid_OrderSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void TextProductCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void TextProductPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private static bool IsTextAllowed(string text)
        {
            return !regex.IsMatch(text);
        }
    }
}
