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
        private Grid boardGrid;
        private Feladvany feladvany;
        private CheckBox[,] checkBoxMuszaj;
        private int kiralynoSzama = 0;
        private TextBlock uzenet;

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

        private void createButton_Click(object sender, RoutedEventArgs e)
        {
            int rows = (int)rowCB.SelectedItem;
            int cols = (int)colCB.SelectedItem;

            if (boardGrid != null)
            {
                // AI
                ((Grid)this.Content).Children.Remove(boardGrid);
            }

            feladvany = new Feladvany(rows, cols);
            
            checkBoxMuszaj = new CheckBox[rows, cols];
            kiralynoSzama = 0;
            boardGrid = new Grid();
            boardGrid.Margin = new Thickness(10, 120, 10, 10);

            for (int i = 0; i < rows; i++)
            {
                // AI
                boardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20) });
            }
            for (int i = 0; i < cols; i++)
            {
                boardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(20) });
            }
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    CheckBox cb = new CheckBox();
                    cb.IsChecked = false;
                    cb.HorizontalAlignment = HorizontalAlignment.Center;
                    cb.VerticalAlignment = VerticalAlignment.Center;
                    cb.Checked += CheckBox_Checked;
                    cb.Unchecked += CheckBox_Unchecked;
                    
                    // AI
                    Grid.SetRow(cb, i);
                    Grid.SetColumn(cb, j);
                    checkBoxMuszaj[i, j] = cb;
                    boardGrid.Children.Add(cb);
                }
            }   
            // AI
            ((Grid)this.Content).Children.Add(boardGrid);
        }

        // AI segitett
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox clickedCheckBox = (CheckBox)sender;
            
            int row = Grid.GetRow(clickedCheckBox);
            int col = Grid.GetColumn(clickedCheckBox);
            
            feladvany.Tabla[row, col] = 1;
            kiralynoSzama++;
            checkBoxFrissites();
            ellenorizzMegoldottE();
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox clickedCheckBox = (CheckBox)sender;
            int row = Grid.GetRow(clickedCheckBox);
            int col = Grid.GetColumn(clickedCheckBox);
            feladvany.Tabla[row, col] = 0;
            kiralynoSzama--;
            uzenet.Text = "Minden sorban helyezzen el egy királynőt!";
            checkBoxFrissites();
        }

        private void checkBoxFrissites()
        {
            // AI
            int rows = checkBoxMuszaj.GetLength(0);
            int cols = checkBoxMuszaj.GetLength(1);
            
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    CheckBox cb = checkBoxMuszaj[i, j];
                    
                    if (cb.IsChecked == true)
                    {
                        cb.IsEnabled = true;
                    }
                    else
                    {
                        bool joMezo = feladvany.EzJoMezo(i, j);
                        cb.IsEnabled = joMezo;
                    }
                }
            }
        }

        private void ellenorizzMegoldottE()
        {
            int szuksegeslevel = checkBoxMuszaj.GetLength(0);
                
            if (kiralynoSzama == szuksegeslevel)
            {
                uzenetTB.Text = "Feladvány megoldva!";
                
                int rows = checkBoxMuszaj.GetLength(0);
                int cols = checkBoxMuszaj.GetLength(1);
                
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols; j++)
                    {
                        checkBoxMuszaj[i, j].IsEnabled = false;
                    }
                }
            }
        }
    }
}