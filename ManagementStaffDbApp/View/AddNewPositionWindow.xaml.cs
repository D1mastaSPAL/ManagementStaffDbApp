using ManagementStaffDbApp.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;

namespace ManagementStaffDbApp.View;

/// <summary>
/// Логика взаимодействия для AddNewPositionWindow.xaml
/// </summary>
public partial class AddNewPositionWindow : Window
{
    public AddNewPositionWindow()
    {
        InitializeComponent();
        DataContext = new DataManageVM();
    }

    private void PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
    {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
    }
}
