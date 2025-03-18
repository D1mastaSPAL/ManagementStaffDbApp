using ManagementStaffDbApp.Command;
using ManagementStaffDbApp.Model;
using ManagementStaffDbApp.View;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ManagementStaffDbApp.ViewModel;

public class DataManageVM : INotifyPropertyChanged
{

    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetBlockControl(Window wnd, string blockName)
    {
        Control block = (Control)wnd.FindName(blockName);
        block.BorderBrush = Brushes.Red;
    }

    private void Message(string message)
    {
        MessageView messageView = new MessageView(message);
        SetCenterPositionAndOpen(messageView);
    }

    #region ImplementationCommand
    // Реализация команд

    private List<Department> allDepartments = DataWorker.GetAllDepartments();
    public List<Department> AllDepartments
    {
        get { return allDepartments; }
        set
        {
            allDepartments = value;
            NotifyPropertyChanged(nameof(Department));
        }
    }

    private List<Position> allPositions = DataWorker.GetAllPosition();
    public List<Position> AllPositions
    {
        get { return allPositions; }
        set
        {
            allPositions = value;
            NotifyPropertyChanged(nameof(Position));
        }
    }

    private List<User> allUsers = DataWorker.GetAllUser();
    public List<User> AllUsers
    {
        get { return allUsers; }
        set
        {
            allUsers = value;
            NotifyPropertyChanged(nameof(User));
        }
    }
    #endregion

    #region CommandsToAdd
    //
    public string DepartmentName { get; set; }

    private RelayCommand addNewDepartment;
    public RelayCommand AddNewDepartment
    {
        get
        {
            return addNewDepartment ?? new RelayCommand(obj =>
            {
                Window wnd = (Window)obj; // Получаем окно, которое передается в команду
                string resultCmdDep = "";
                // Проверка, что название отдела не пустое или состоящее только из пробелов
                if (DepartmentName == null || DepartmentName.Replace(" ", "").Length == 0)
                {
                    SetBlockControl(wnd, "NameBlock"); // Подсвечиваем поле, если название отдела пустое
                }
                else
                {
                    resultCmdDep = DataWorker.CreateDepartment(DepartmentName); // Создаем отдел
                    Message(resultCmdDep); // Показать сообщение о результате
                    wnd.Close(); // Закрываем окно
                }
            }
            );
        }
    }
    #endregion


    #region CommandsToOpenWindows
    //
    private RelayCommand openRegistryDepWindow;
    public RelayCommand OpenRegistryDepWindow
    {
        get
        {
            return openRegistryDepWindow ?? new RelayCommand(
                obj => OpenAddDepartmentWindow(),
                obj => true // Можно добавить сюда логику проверки, например, если окно уже открыто.
            );
        }
    }

    private RelayCommand openRegistryPosWindow;
    public RelayCommand OpenRegistryPosWindow
    {
        get
        {
            return openRegistryPosWindow ?? new RelayCommand(
                obj => OpenAddPositionWindow(),
                obj => true // Можно добавить сюда логику проверки, например, если окно уже открыто.
            );
        }
    }

    private RelayCommand openRegistryUserWindow;
    public RelayCommand OpenRegistryUserWindow
    {
        get
        {
            return openRegistryUserWindow ?? new RelayCommand(
                obj => OpenAddUserWindow(),
                obj => true // Можно добавить сюда логику проверки, например, если окно уже открыто.
            );
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
    #endregion

    #region MethodsFromEditWindow
    //Методы изменения открытых окон
    private void OpenEditDepartmentWindow()
    {
        EditDepartmentWindow editDepartmentWindow = new EditDepartmentWindow();
        SetCenterPositionAndOpen(editDepartmentWindow);
    }

    private void OpenEditPositionWindow()
    {
        EditPositionWindow editPositionWindow = new EditPositionWindow();
        SetCenterPositionAndOpen(editPositionWindow);
    }

    private void OpenEditUserWindow()
    {
        EditUserWindow editUserWindow = new EditUserWindow();
        SetCenterPositionAndOpen(editUserWindow);
    }
    #endregion

    private void SetCenterPositionAndOpen(Window window)
    {
        window.Owner = Application.Current.MainWindow;
        window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
        window.ShowDialog();
    }
}