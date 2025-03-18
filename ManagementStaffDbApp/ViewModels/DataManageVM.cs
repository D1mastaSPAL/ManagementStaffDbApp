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
    #region NotifyPropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    private void NotifyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion

    #region EditButtonAddDepartments
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
    #endregion

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
    //Свойства для отдела, позиции и сотрудников
    public string DepartmentName { get; set; }

    public string PositionName { get; set; }

    public decimal PositionSalary { get; set; }

    public int PositionMaxNumber { get; set; }

    public Department PositionDepartment { get; set; }

    public string UserName { get; set; }

    public string UserSurName { get; set; }

    public string UserPhone { get; set; }

    public Position UserPosition { get; set; }

    // Команды для отдела, позиции и сотрудников
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
                    UpdateAllDataView();
                    Message(resultCmdDep); // Показать сообщение о результате
                    SetNullValuesToProperties();
                    wnd.Close(); // Закрываем окно
                }
            }
            );
        }
    }

    private RelayCommand addNewPosition;
    public RelayCommand AddNewPosition
    {
        get
        {
            return addNewDepartment ?? new RelayCommand(obj =>
            {
                Window wnd = (Window)obj; // Получаем окно, которое передается в команду
                string resultCmdPos = "";
                // Проверка, что название отдела не пустое или состоящее только из пробелов
                if (PositionName == null || PositionName.Replace(" ", "").Length == 0)
                {
                    SetBlockControl(wnd, "NameBlock"); // Подсвечиваем поле, если название отдела пустое
                }
                if (PositionSalary == 0)
                {
                    SetBlockControl(wnd, "SalaryBlock");
                }
                if (PositionMaxNumber == 0)
                {
                    SetBlockControl(wnd, "MaxNumberBlock");
                }
                if (PositionDepartment == null)
                {
                    MessageBox.Show("Укажите отдел");
                }
                else
                {
                    resultCmdPos = DataWorker.CreatePosition(PositionName, PositionSalary, PositionMaxNumber, PositionDepartment); // Создаем позицию
                    UpdateAllDataView();
                    Message(resultCmdPos); // Показать сообщение о результате
                    SetNullValuesToProperties();
                    wnd.Close(); // Закрываем окно
                }
            }
            );
        }
    }

    private RelayCommand addNewUser;
    public RelayCommand AddNewUser
    {
        get
        {
            return addNewUser ?? new RelayCommand(obj =>
            {
                Window wnd = (Window)obj; // Получаем окно, которое передается в команду
                string resultCmdUser = "";
                // Проверка, что название отдела не пустое или состоящее только из пробелов
                if (UserName == null || UserName.Replace(" ", "").Length == 0)
                {
                    SetBlockControl(wnd, "NameBlock"); // Подсвечиваем поле, если название отдела пустое
                }
                if (UserSurName == null || UserSurName.Replace(" ", "").Length == 0)
                {
                    SetBlockControl(wnd, "SurNameBlock");
                }
                if (UserPhone == null || UserPhone.Replace(" ", "").Length == 0)
                {
                    SetBlockControl(wnd, "PhoneBlock");
                }
                if (UserPosition == null)
                {
                    MessageBox.Show("Укажите позицию");
                }
                else
                {
                    resultCmdUser = DataWorker.CreateUser(UserName, UserSurName, UserPhone, UserPosition); // Создаем сотрудника
                    UpdateAllDataView();
                    Message(resultCmdUser); // Показать сообщение о результате
                    SetNullValuesToProperties();
                    wnd.Close(); // Закрываем окно
                }
            }
            );
        }
    }

    #endregion

    #region CommandsToUpdate
    //
    private void SetNullValuesToProperties()
    {
        DepartmentName = null;
        PositionName = null;
        PositionSalary = 0;
        PositionMaxNumber = 0;
        PositionDepartment = null;
        UserName = null;
        UserSurName = null;
        UserPhone = null;
        UserPosition = null;
    }

    private void UpdateAllDataView()
    {
        UpdateAllDepartmentsView();
        UpdateAllPositionsView();
        UpdateAllUsersView();
    }

    private void UpdateAllDepartmentsView()
    {
        AllDepartments = DataWorker.GetAllDepartments();
        MainWindow.AllDepartmentsView.ItemsSource = null;
        MainWindow.AllDepartmentsView.Items.Clear();
        MainWindow.AllDepartmentsView.ItemsSource = AllDepartments;
        MainWindow.AllDepartmentsView.Items.Refresh();
    }

    private void UpdateAllPositionsView()
    {
        AllDepartments = DataWorker.GetAllDepartments();
        MainWindow.AllPositionsView.ItemsSource = null;
        MainWindow.AllPositionsView.Items.Clear();
        MainWindow.AllPositionsView.ItemsSource = AllDepartments;
        MainWindow.AllPositionsView.Items.Refresh();
    }

    private void UpdateAllUsersView()
    {
        AllDepartments = DataWorker.GetAllDepartments();
        MainWindow.AllUsersView.ItemsSource = null;
        MainWindow.AllUsersView.Items.Clear();
        MainWindow.AllUsersView.ItemsSource = AllDepartments;
        MainWindow.AllUsersView.Items.Refresh();
    }

    #endregion

    public TabItem SelectedTabItem { get; set; }

    public User SelectedUser { get; set; }

    public Position SelectedPosition { get; set; }

    public Department SelectedDepartment { get; set; }

    #region CommandsDelete

    private RelayCommand deleteItem;

    public RelayCommand DeleteItem
    {
        get
        {

            return deleteItem ?? new RelayCommand(obj =>
            {
                string resultStr = "Ничего не выбрано!";

                //если сотрудник
                if(SelectedTabItem.Name == "UsersTab" && SelectedUser != null)
                {
                    resultStr = DataWorker.DeleteUser(SelectedUser);
                    UpdateAllDataView();
                }
                // если позиция
                if (SelectedTabItem.Name == "PositionsTab" && SelectedPosition != null)
                {
                    resultStr = DataWorker.DeletePosition(SelectedPosition);
                    UpdateAllDataView();
                }
                //если отдел
                if (SelectedTabItem.Name == "DepartmentTab" && SelectedDepartment != null)
                {
                    resultStr = DataWorker.DeleteDepartment(SelectedDepartment);
                    UpdateAllDataView();
                }
                //обновление
                SetNullValuesToProperties();
                Message(resultStr);
            });
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