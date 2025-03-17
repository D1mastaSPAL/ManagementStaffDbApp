using ManagementStaffDbApp.Model.Data;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagementStaffDbApp.Model;

//класс для упраления базой данных
public static class DataWorker
{
    // Получить все отделы
    public static List<Department> GetAllDepartments()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var resDepList = db.Departments.ToList();
            return resDepList;
        }
    }

    // Получить все позиции
    public static List<Position> GetAllPosition()
    {
        using (ApplicationContext db = new ApplicationContext())
        {
            var resPosList = db.Positions.ToList();
            return resPosList;
        }
    }

    // Получить всех сотрудников
    public static List<User> GetAllUser()
    {
        using(ApplicationContext db = new ApplicationContext())
        {
            var resUsList = db.Users.ToList();
            return resUsList;
        }
    }

    #region Create
    //Создать отдел 
    public static string CreateDepartment(string name)
    {
        string resultCreateDepartment = "Отдел Departments - Уже существует!";
        using (ApplicationContext db = new ApplicationContext())
        {
            bool checkIsExist = db.Departments.Any(_ => _.Name == name);
            if (!checkIsExist)
            {
                Department newDepartment = new Department { Name = name };
                db.Departments.Add(newDepartment);
                db.SaveChanges();
                resultCreateDepartment = "Отдел Departments - создан!";
            }
            return resultCreateDepartment;
        }
    }

    //Создать позицию
    public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
    {
        string resultCreatePosition = "Позиция - уже существует";
        using (ApplicationContext db = new ApplicationContext())
        {
            bool checkIsExist = db.Positions.Any(_ => _.Name == name && _.Salary == salary);
            if (!checkIsExist)
            {
                Position newPosition = new Position { Name = name, Salary = salary, MaxNumber = maxNumber, DepartmentId = department.Id };
                db.Positions.Add(newPosition);
                db.SaveChanges();
                resultCreatePosition = "Позиция - создана!";
            }
            return resultCreatePosition;
        }
    }

    //Создать сотрудника
    public static string CreateUser(string name, string surName, string phone, Position position)
    {
        string resultCreateUsers = "Сотрудник - существует!";
        using (ApplicationContext db = new ApplicationContext())
        {
            bool checkIsExist = db.Users.Any(_ => _.Name == name && _.Surname == surName &&_.Position == position);
            if (!checkIsExist)
            {
                User newUser = new User { Name = name, Surname = surName, Phone = phone, PositionId = position.Id };
                db.Users.Add(newUser);
                db.SaveChanges();
                resultCreateUsers = "Сотрудник - создан!";
            }
            return resultCreateUsers;
        }
    }
    #endregion

    #region Delete
    //Удаление отдела
    public static string DeleteDepartment(Department department)
    {
        string resultDelDep = "Отдел - не существует!";
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Departments.Remove(department);
            db.SaveChanges();
            resultDelDep = $"Отдел: {department.Name} - удален!";
        }
        return resultDelDep;
    }

    //Удаление позиции
    public static string DeletePosition(Position position)
    {
        string resultDelPos = "Позиции - не существует!";
        using (ApplicationContext db = new ApplicationContext())
        {
            db.Positions.Remove(position);
            db.SaveChanges();
            resultDelPos = $"Позиция: {position.Name} - удалена";
        }
        return resultDelPos;
    }

    //Удаление сотрудника
    public static string DeleteUser(User user)
    {
        string resultDelUs = "Сотрудника - не существует!";
        using(ApplicationContext db = new ApplicationContext())
        {
            db.Users.Remove(user);
            db.SaveChanges();
            resultDelUs = $"Сотрудник: {user.Name} - уволен";
        }
        return resultDelUs;
    }
    #endregion

    #region Edit
    //редактирование отдела
    public static string EditDepartment(Department oldDepartment, string editDepName)
    {
        string resultEditDep = "Отдел - не существует!";
        using(ApplicationContext db = new ApplicationContext())
        {
            Department department = db.Departments.FirstOrDefault(_ => _.Id == oldDepartment.Id);
            department.Name = editDepName;
            db.SaveChanges();
            resultEditDep = $"Отдел: {department.Name} - изменен";
        }
        return resultEditDep;
    }

    //редактирование позиции
    public static string EditPosition(Position oldPosition, string editPosName, int editMaxNumber, decimal editSalary, Department editDepartment)
    {
        string resultEditPos = "Позиции - не существует!";
        using (ApplicationContext db = new ApplicationContext())
        {
            Position position = db.Positions.FirstOrDefault(_ => _.Id == oldPosition.Id);
            if (position != null)
            {
                position.Name = editPosName;
                position.Salary = editSalary;
                position.MaxNumber = editMaxNumber;
                position.DepartmentId = editDepartment.Id;
                db.SaveChanges();
                resultEditPos = $"Позиция: {position.Name} - изменена!";
            }
        }
        return resultEditPos;
    }

    //редактирование сотрудника
    public static string EditUser(User oldUser, string editUsName, string editUsSurname, string editPhone, Position editPosition)
    {
        string resultEditUs = "Сотрудник - не существует!";
        using (ApplicationContext db = new ApplicationContext())
        {
            User user = db.Users.FirstOrDefault(_ => _.Id == oldUser.Id);
            if (user != null)
            {
                user.Name = editUsName;
                user.Surname = editUsSurname;
                user.Phone = editPhone;
                user.PositionId = editPosition.Id;
                db.SaveChanges();
                resultEditUs = $"Сотрудник: {user.Name} - изменен!";
            }
        }
        return resultEditUs;
    }
    #endregion
}
