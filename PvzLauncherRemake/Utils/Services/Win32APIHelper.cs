using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

namespace PvzLauncherRemake.Utils.Services
{
    public static class Win32APIHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);


        /// <summary>
        /// 根据Process设置窗口标题
        /// </summary>
        /// <param name="process">目标Process</param>
        /// <param name="newTitle">新标题</param>
        /// <returns>成功返回True，失败返回False</returns>
        /// <exception cref="ArgumentException">进程为空或已退出。</exception>
        /// <exception cref="InvalidOperationException">无法获取进程的主窗口句柄。进程可能无可见窗口或尚未完全启动。</exception>
        public static bool SetWindowTitle(Process process, string newTitle)
        {
            if (process == null || process.HasExited)
                throw new ArgumentException("进程为空或已退出。");

            process.WaitForInputIdle(5000);
            process.Refresh();

            IntPtr hWnd = process.MainWindowHandle;
            if (hWnd == IntPtr.Zero)
                throw new InvalidOperationException("无法获取进程的主窗口句柄。进程可能无可见窗口或尚未完全启动。");

            return SetWindowText(hWnd, newTitle);
        }
    }
}
