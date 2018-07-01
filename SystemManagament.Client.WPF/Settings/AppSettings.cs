﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystemManagament.Client.WPF.Settings
{
    public class AppSettings : ApplicationSettingsBase
    {
        [UserScopedSetting]
        [SettingsSerializeAs(SettingsSerializeAs.Binary)]
        public Dictionary<string, WorkstationSettings> WorkstationsParameters
        {
            get
            {
                return (Dictionary<string, WorkstationSettings>)this["WorkstationsParameters"];
            }

            set
            {
                this["WorkstationsParameters"] = value;
            }
        }

    }
}
