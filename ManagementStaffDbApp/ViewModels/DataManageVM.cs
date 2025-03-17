using ManagementStaffDbApp.Model;
using ManagementStaffDbApp.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ManagementStaffDbApp.ViewModel;

public class DataManageVM : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    //#region INotifyPropertyChanged_Implementation
    //public event PropertyChangedEventHandler? PropertyChanged;
    //private void OnPropertyChanged(string propertyName)
    //{
    //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //}
    //#endregion

    #region ImplementationCommand
    // Реализация команд

    private List<Department> allDepartments = DataWorker.GetAllDepartments();
    public List<Department> AllDepartments
    {
        get { return allDepartments; }
        set
        {
            allDepartments = value;
            NotifyPropertyChanged("AllDepartments");
        }
    }

    private List<Position> allPosition = DataWorker.GetAllPosition();
    public List<Position> AllPosition
    {
        get { return AllPosition; }
        set
        {
            AllPosition = value;
            NotifyPropertyChanged("AllPositions");
        }
    }

    private List<User> allUsers = DataWorker.GetAllUser();
    public List<User> AllUsers
    {
        get { return AllUsers; }
        set
        {
            AllUsers = value;
            NotifyPropertyChanged("AllUsers");
        }
    }
    #endregion

    #region MethodsFromOpenWindow
    //Методы открытия окон
    private void OpenAddDepartmentWindow()
    {
        AddNewDepartmentWindow newDepartmentWindow = new AddNewDepartmentWindow();
        SetCenterPositionAndOpen(newDepartmentWindow);
    }

    private void OpenAddPositionWindow()
    {
        AddNewPositionWindow newPositionWindow = new AddNewPositionWindow();
        SetCenterPositionAndOpen(newPositionWindow);
    }

    private void OpenAddUserWindow()
    {
        AddNewUserWindow newUserWindow = new AddNewUserWindow();
        SetCenterPositionAndOpen(newUserWindow);
    }

    private void SetCenterPositionAndOpen(Window window)
    {
        window.Owner = Application.Current.MainWindow;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
    }
    #endregion


}