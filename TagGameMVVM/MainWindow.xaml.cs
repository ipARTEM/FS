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

namespace TagGameMVVM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Model model = new Model();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = model;
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            model.Init();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var brd = (sender as Border);
            var fishka = brd.DataContext as Fishka;
            model.PressBy(fishka);

            //model.IncStep();
            //model.Fire(); // благодаря Set<T> теперь писать не надо
        }
    }
}
