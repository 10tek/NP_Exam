using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace Exam
{
    public class Menu
    {
        private const string MENU_RETURN = "Нажмите ENTER что бы вернуться в главное меню...";
        private RequestService<User> requestService = new RequestService<User>();

        public void ShowMenu()
        {
            WriteLine("1 - Get");
            WriteLine("2 - Add");
            WriteLine("3 - Update");
            WriteLine("4 - Remove");
            WriteLine("0 - Exit");
            Write("Выберите пункт: ");
        }

        public async Task Action()
        {
            ShowMenu();
            int menuNumber;
            while (!int.TryParse(ReadLine(), out menuNumber))
            {
                Write("Некорректный ввод! Попробуйте еще раз: ");
            }
            switch (menuNumber)
            {
                case 0: return;
                case 1: //await Get(); break;
                case 2: await Add(); break;
                case 3: await Update(); break;
                case 4: await Remove(); break;
            }
        }

        public async Task Get()
        {
            Clear();
            var s = await requestService.GetAsync();

            var header = s.Substring(s.IndexOf("[header]"), s.LastIndexOf("[/header]"));
            var headerText = header.Substring(header.IndexOf("h"), header.IndexOf("[/h]"));
            var data = s.Substring(s.IndexOf("[data]"), s.LastIndexOf("[/data]"));
            var userList = new List<User>();
            for(var i = 0; i < data.Length; i++)
            {
                var textD = s.Substring(s.IndexOf("[d]"), s.IndexOf("[/d]"));
                var tmpLogin = textD.Substring(0, textD.IndexOf("|"));
                var tmpName = textD.Substring(0, textD.IndexOf("[/d]"));
                userList.Add(new User
                {
                    Login = tmpLogin,
                    Name = tmpName
                });
            }
            WriteLine("------------------------------");
            WriteLine($"|{headerText}");
            SetCursorPosition(30,1);
            Write("|");
            SetCursorPosition(0, 2);
            WriteLine("------------------------------");
            WriteLine("|Логин        |           Имя|");
            WriteLine("------------------------------");
            var cursorY = 5;
            foreach(var user in userList)
            {
                Write($"|{user.Login} ");
                SetCursorPosition(15, cursorY);
                Write("|");
                Write($"{user.Name}");
                SetCursorPosition(30, cursorY++);
                WriteLine("|");
            }
            WriteLine("------------------------------");
        }

        private async Task Add()
        {
            Clear();
            Write(" Введите ваше имя: ");
            var name = ReadLine();
            Write("Введите ваш логин: ");
            var login = ReadLine();
            WriteLine(await requestService.AddAsync(name, login));
            WriteLine(MENU_RETURN);
            ReadLine();
        }

        private async Task Update()
        {
            Clear();
            Write("Введите ваш логин: ");
            var login = ReadLine();
            Write("Введите новое имя: ");
            var name = ReadLine();
            WriteLine(await requestService.UpdateAsync(name, login));
            WriteLine(MENU_RETURN);
            ReadLine();
        }

        private async Task Remove()
        {
            Clear();
            Write("Введите логин для удаления: ");
            var login = ReadLine();
            WriteLine(await requestService.RemoveAsync(login));
            WriteLine(MENU_RETURN);
            ReadLine();
        }

    }
}