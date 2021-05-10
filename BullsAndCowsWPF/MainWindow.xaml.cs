using System.Collections.Generic;
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

namespace BullsAndCowsWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BullCowsModel model;
        public MainWindow()
        {
            InitializeComponent();
            model =new BullCowsModel();
            DataContext = model;
            icKeys.ItemsSource = "1234567890";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var dig = btn.Content.ToString();
            btn.IsEnabled = false;
            model.PressKey(dig);
        }

        private void Button_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var btn = sender as Button;
            var dig = btn.Content.ToString();
            if (!btn.IsEnabled)
            {
                model.RemovKey(dig);
                btn.IsEnabled = true;
            }
        }
    }
}
