using System.Windows;

namespace WpfSimple
{
    /// <summary>
    /// Логика взаимодействия для ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        public ChildWindow()
        {
            InitializeComponent();
        }

        public string ViewModel { get; set; }
        public void ShowViewModel()
        {
            textBlock.Text = ViewModel;
        }
    }
}
