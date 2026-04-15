using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SakkfeladvanyGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            for (int i = 3; i <= 10; i++)
            {
                rowCB.Items.Add(i);
                colCB.Items.Add(i);
            }
            rowCB.SelectedItem = 8;
            colCB.SelectedItem = 8;
        }
    }
}