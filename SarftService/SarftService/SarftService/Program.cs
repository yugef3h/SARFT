using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NRails;
using NRails.Service;

namespace SarftService
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            #region 目录引用

#pragma warning disable 0618

            AppDomain.CurrentDomain.AppendPrivatePath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "libs"));
            AppDomain.CurrentDomain.AppendPrivatePath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin"));
            AppDomain.CurrentDomain.AppendPrivatePath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bins"));
            AppDomain.CurrentDomain.AppendPrivatePath(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "lib"));

#pragma warning restore 0618

            #endregion

            MainThread();
        }

        static ServiceRunner runner;

        static void MainThread()
        {
            ServiceLoader main = new ServiceLoader();
            runner = new ServiceRunner(main.Run, main.Exit, false);
            main.Exiting += (sender, e) => runner.Exit();
            runner.Run();
        }
    }

}
