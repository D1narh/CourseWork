﻿using System;
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

        public static DataModels.MaimfEntities Entity = new DataModels.MaimfEntities();

    }
}
