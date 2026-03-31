using PvzLauncherRemake.Classes;
using PvzLauncherRemake.Utils.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace PvzLauncherRemake.Windows
{
    /// <summary>
    /// WindowOverlay.xaml 的交互逻辑
    /// </summary>
    public partial class WindowOverlay : Window
    {
        private DispatcherTimer? _timer;

        public WindowOverlay()
        {
            InitializeComponent();
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(1)
            };
            _timer.Tick += Timer_Tick;


            Loaded += ((s, e) => _timer.Start());
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            //如游戏退出或进程信息为空则关闭覆盖界面
            if (!GameManager.IsGameRuning || AppProcess.Process == null || AppProcess.Process.HasExited) 
            {
                _timer?.Stop();_timer = null;
                this.Close();
            }

            AppProcess.Process!.Refresh();


            var result = Win32APIHelper.GetWindowArea(AppProcess.Process!.MainWindowHandle);

            this.Left = result.Left;
            this.Top = result.Top;
            this.Width = result.Width;
            this.Height = result.Height;

        }
    }
}
