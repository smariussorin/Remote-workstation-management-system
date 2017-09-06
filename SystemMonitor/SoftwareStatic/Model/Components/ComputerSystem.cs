﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.HardwareStatic;
using SystemMonitor.Shared.WMI;
using SystemMonitor.SoftwareStatic.Model.Components.Abstract;
using SystemMonitor.SoftwareStatic.SoftwareStaticProvider;

namespace SystemMonitor.SoftwareStatic.Model.Components
{
    public class ComputerSystem : SoftwareStaticComponent, IWMISoftwareStaticComponent
    {
        public string Name { get; set; }

        public SoftwareStaticComponent ExtractData(ManagementObject managementObject)
        {
            throw new NotImplementedException();
        }

        public List<ManagementObject> GetManagementObjectsForSoftwareComponent(IWMIClient wmiClient)
        {
            return wmiClient.RetriveListOfObjectsByExecutingWMIQuery(ConstString.WMI_NAMESPACE_ROOT_CIMV2, ConstString.WMI_QUERY_OS);
        }

        public override List<SoftwareStaticComponent> GetStaticDataForSoftwareComponent(ISoftwareStaticProvider softwareStaticProvider)
        {
            //List<SoftwareStaticComponent> currentUserList = new List<SoftwareStaticComponent>();

            //ComputerSystem currentUser = new ComputerSystem();
            //var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            //currentUser.Name = System.Security.Principal.WindowsIdentity.GetCurrent().;
            ////string userName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

            //currentUserList.Add(currentUser);

            //return currentUserList;

            return softwareStaticProvider.GetSoftwareStaticDataFromWMI(new OS());
        }
    }
}
