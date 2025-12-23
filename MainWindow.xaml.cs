using Magazine_WPF.ViewModel;
using System.Windows;

namespace Magazine_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// Pure View - minimal code-behind, all logic in ViewModel via Commands and Data Binding
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // Set DataContext to ViewModel (not the Model)            
            this.DataContext = new MagazineViewModel();
        }
    }
}