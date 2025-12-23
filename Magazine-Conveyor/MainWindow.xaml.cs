using System.Windows;
using Magazine_Conveyor.ViewModel;

namespace Magazine_Conveyor
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