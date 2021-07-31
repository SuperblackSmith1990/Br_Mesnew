using System;
using System.Collections.Generic;
using System.Diagnostics;//导入，诊断
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Modules;

namespace WpfApp1.Examples
{
    /// <summary>
    /// Interaction logic for StartStopProcess.xaml
    /// </summary>
    public partial class StartStopProcess : Page
    {
        int fileIndex = 1;
        string fileName = "Notepad";
        List<ProcessEntity> list = new List<ProcessEntity>();

        public StartStopProcess()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            string arguement = Environment.CurrentDirectory + "\\myfile" + (fileIndex++) + ".txt";
            if (File.Exists(arguement) == false)
            {
                File.CreateText(arguement);
            }
            Process p = new Process();
            p.StartInfo.FileName = fileName;//启动的应用程序
            p.StartInfo.Arguments = arguement;//可以被该应用程序打开的文件地址
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
            p.Start();
            p.WaitForInputIdle();
            RefreshProcessInfo();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;
            Process[] myprocesses=Process.GetProcessesByName(fileName);
            foreach (var p in myprocesses)
            {
                using (p) {
                    p.CloseMainWindow();
                    Thread.Sleep(1000);
                    p.WaitForExit();
                }
            }
            fileIndex = 0;
            RefreshProcessInfo();
            this.Cursor = Cursors.Arrow;
        }
        private void RefreshProcessInfo()
        {
            datagrid1.ItemsSource = null;
            list.Clear();
            Process[] processes = Process.GetProcessesByName(fileName);
            foreach (var p in processes)
            {
                list.Add(new ProcessEntity()
                {
                    Id = p.Id,
                    ProcessName = p.ProcessName,
                    TotalMemory = string.Format("{0,10:0}", p.WorkingSet64 / 1024d),
                    StartTime = p.StartTime.ToString("yyyy-m-d HH:mm:ss"),
                    FileName = p.MainModule.FileName,
                });
            }
            datagrid1.ItemsSource = list;
        }
    }
}
