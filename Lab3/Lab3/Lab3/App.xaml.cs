using System.Windows;

namespace Lab3
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            WindowManager.Register(typeof(PersonViewModel), "add", typeof(AddPersonWindow));
            WindowManager.Register(typeof(PersonViewModel), "change", typeof(ChangePersonData));
        }
    }
}
