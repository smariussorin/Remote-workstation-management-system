﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SystemManagament.Monitor.HardwareStatic.Model.CustomProperties;
using SystemManagament.Monitor.SoftwareDynamic.Model.Components.Interface;
using SystemManagament.Monitor.SoftwareDynamic.Provider;

namespace SystemManagament.Monitor.SoftwareDynamic.Model.Components
{
    [DataContract]
    public class WindowsLog : ISoftwareDynamicComponent<WindowsLog>
    {
        [DataMember]
        public EventLogEntryCollection Entries { get; internal set; }

        [DataMember]
        public string LogName { get; internal set; }

        [DataMember]
        public string LogDisplayName { get; internal set; }

        [DataMember]
        public UnitValue MaximumKilobytes { get; internal set; }

        [DataMember]
        public UnitValue MinimumRetentionDays { get; internal set; }

        public List<WindowsLog> GetDynamicDataForSoftwareComponent(ISoftwareDynamicProvider softwareDynamicProvider)
        {
            return softwareDynamicProvider.GetWindowsLogs();
        }
    }
}
