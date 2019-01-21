using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        MainViewModel _mainViewModel = new MainViewModel();

        public MainWindow()
        {

            InitializeComponent();

            DataContext = _mainViewModel;
        }
    }
}
