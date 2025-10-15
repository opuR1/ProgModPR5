using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Security.Cryptography;
using HashPassword;
using ProgModPR5.Models;

namespace ProgModPR5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true) 
            {
                Console.Clear();
                Console.WriteLine("=== Система регистрации ===");
                Console.WriteLine("Войти в аккаунт: L");
                Console.WriteLine("Создать аккаунт: C");
                Console.WriteLine("Выйти из программы: E");
                Console.Write("Выберите действие: ");

                string button = Console.ReadLine()?.ToUpper();

                switch (button)
                {
                    case "L":
                        Login();
                        break;
                    case "C":
                        CreateAccount();
                        break;
                    case "E":
                        Console.WriteLine("Выход из программы...");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Неверный выбор! Нажмите любую клавишу для продолжения");
                        Console.ReadKey();
                        break;
                }
            }
        }
        static void Login()
        {
            Console.Clear();
            Console.WriteLine("=== Вход в аккаунт ===");
            Console.WriteLine("Введите почту: ");
            string Email = Console.ReadLine();
            Console.WriteLine("Введите пароль: ");
            string password = Console.ReadLine();
            Console.WriteLine("Сотрудник:(Y/N)  ");
            string type = Console.ReadLine()?.ToUpper();
            bool empl = false;
            if (type == "Y")
            {
                empl = true;
            }
            AccountLogin(Email, password, empl);
            Console.WriteLine("Нажмите любую клавишу для возврата в меню");
            Console.ReadKey();
        }
        private static void AccountLogin(string Email, string password, bool empl)
        {
            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Логин и пароль не могут быть пустыми!");
                return;
            }  
            try
            {
                string hashedPassword = PassHasher.HashPassword(password);

                var context = Helper.GetContext();

                var user = context.Users.FirstOrDefault(u => u.Email == Email && u.Password == hashedPassword);
                object ProfileType = null;
                if (empl == true)
                {
                    ProfileType = context.Employees.FirstOrDefault(e => e.UserID == user.UserID && e.Email == user.Email);
                }
                else
                {
                    ProfileType = context.Clients.FirstOrDefault(c => c.UserID == user.UserID && c.Email == user.Email);
                }

                if (user != null)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    string userName = "";
                    if (empl == true)
                    {
                        var employee = ProfileType as Employees;
                        userName = employee.FirstName;
                    }
                    else
                    {
                        var client = ProfileType as Clients;
                        userName = client.FirstName;
                    }
                    Console.WriteLine($"Вход выполнен успешно! Добро пожаловать, {userName}");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine("Неверный email или пароль!");
                    return;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при входе: {ex.Message}");
            }

        }
        static void CreateAccount()
        {
            Console.Clear();
            Console.WriteLine("=== Создание аккаунта ===");
            Console.WriteLine("Нажмите любую клавишу для возврата в меню");
            Console.ReadKey();
        }
    }
}
