﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.Logger;

namespace SystemMonitor.Shared.Win32API
{
    public class Win32APIClient : IWin32APIClient
    {

        public Win32APIClient(INLogger logger)
        {
            this.Logger = logger;
        }

        public INLogger Logger { get; private set; }

        public string GetProcessUser(Process process)
        {
            IntPtr processHandle = IntPtr.Zero;
            try
            {
                OpenProcessToken(process.Handle, 8, out processHandle);
                WindowsIdentity windowsIdentity = new WindowsIdentity(processHandle);
                return windowsIdentity.Name;
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex.Message);
                return string.Empty;
            }
            finally
            {
                if (processHandle != IntPtr.Zero)
                {
                    CloseHandle(processHandle);
                }
            }
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr ProcessHandle, uint DesiredAccess, out IntPtr TokenHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);
    }
}
