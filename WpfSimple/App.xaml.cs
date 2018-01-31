using System.Windows;

namespace WpfSimple
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_StartUp(object sender, StartupEventArgs e)
        {
            MainWindow wnd = new MainWindow();
            wnd.Title = "WpfSimple";
            wnd.Show();
        }
    }
}
