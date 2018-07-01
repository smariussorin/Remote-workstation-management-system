﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemManagament.Client.WPF.Settings
{
    [Serializable]
    [SettingsSerializeAs(SettingsSerializeAs.Binary)]
    public class WorkstationSettings
    {
        public string Uri { get; set; }

        public uint ForceMachineTurnOffTimeout { get; set; }

        public uint ForceMachineRestartTimeout { get; set; }
    }
}
