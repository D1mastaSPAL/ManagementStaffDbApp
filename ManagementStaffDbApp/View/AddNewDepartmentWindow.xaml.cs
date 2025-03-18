using ManagementStaffDbApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementStaffDbApp.View;

/// <summary>
/// Логика взаимодействия для AddNewDepartmentWindow.xaml
/// </summary>
public partial class AddNewDepartmentWindow : Window
{
    public AddNewDepartmentWindow()
    {
        InitializeComponent();
        DataContext = new DataManageVM();
    }
}
