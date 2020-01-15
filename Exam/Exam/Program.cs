using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Exam
{
    class Program
    {
        static void Main(string[] args)
        {
            var menu = new Menu();
            while (true)
            {
                menu.Action();
            }
        }

    }
}
