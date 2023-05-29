using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;

namespace MaimApp
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static HttpClient HttpClient = new HttpClient();
        public static readonly string AppDir;

        static App()
        {
            var splittedPath = System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase.Split('/');
            AppDir = string.Join(@"\", splittedPath.Skip(2).Take(splittedPath.Length - 4).ToArray()) + "\\";
        }
    }
}
