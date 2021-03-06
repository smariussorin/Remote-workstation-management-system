﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using SystemManagament.Monitor.HardwareStatic;
using SystemManagament.Monitor.SoftwareStatic.Model.Components.Interface;
using SystemManagament.Monitor.SoftwareStatic.Provider;
using SystemManagament.Shared.WMI;

namespace SystemManagament.Monitor.SoftwareStatic.Model.Components
{
    [DataContract]
    public class Bios : ISoftwareStaticComponent<Bios>, IWMISoftwareStaticComponent<Bios>
    {
        [DataMember]
        public string BIOSVersion { get; private set; }

        [DataMember]
        public string BuildNumber { get; private set; }

        [DataMember]
        public string Caption { get; private set; }

        [DataMember]
        public string Description { get; private set; }

        [DataMember]
        public string EmbeddedControllerMajorVersion { get; private set; }

        [DataMember]
        public string EmbeddedControllerMinorVersion { get; private set; }

        [DataMember]
        public string Manufacturer { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public DateTime ReleaseDate { get; private set; }

        [DataMember]
        public string SerialNumber { get; private set; }

        [DataMember]
        public string SMBIOSBIOSVersion { get; private set; }

        [DataMember]
        public string SMBIOSMajorVersion { get; private set; }

        [DataMember]
        public string SMBIOSMinorVersion { get; private set; }

        [DataMember]
        public string SMBIOSPresent { get; private set; }

        [DataMember]
        public string SystemBiosMajorVersion { get; private set; }

        [DataMember]
        public string SystemBiosMinorVersion { get; private set; }

        [DataMember]
        public string Version { get; private set; }

        public Bios ExtractData(ManagementObject managementObject)
        {
            Bios bios = new Bios();

            bios.BIOSVersion = managementObject[ConstString.BIOS_BIOS_VERSION].TryGetStringValue();
            bios.BuildNumber = managementObject[ConstString.BIOS_BUILD_NUMBER].TryGetStringValue();
            bios.Caption = managementObject[ConstString.COMPONENT_CAPTION].TryGetStringValue();
            bios.Description = managementObject[ConstString.COMPONENT_DESCRIPTION].TryGetStringValue();
            bios.EmbeddedControllerMajorVersion = managementObject[ConstString.BIOS_EMBEDDED_CONTROLLER_MAJOR_VERSION].TryGetStringValue();
            bios.EmbeddedControllerMinorVersion = managementObject[ConstString.BIOS_EMBEDDED_CONTROLLER_MINOR_VERSION].TryGetStringValue();
            bios.Manufacturer = managementObject[ConstString.BIOS_MANUFACTURER].TryGetStringValue();
            bios.Name = managementObject[ConstString.COMPONENT_NAME].TryGetStringValue();
            bios.ReleaseDate = managementObject[ConstString.BIOS_RELEASE_DATE] != null ? ManagementDateTimeConverter.ToDateTime(managementObject[ConstString.BIOS_RELEASE_DATE].ToString()) : default(DateTime);
            bios.SerialNumber = managementObject[ConstString.BIOS_SERIAL_NUMBER].TryGetStringValue();
            bios.SMBIOSMajorVersion = managementObject[ConstString.BIOS_SMBIOS_MAJOR_VERSION].TryGetStringValue();
            bios.SMBIOSMinorVersion = managementObject[ConstString.BIOS_SMBIOS_MINOR_VERSION].TryGetStringValue();
            bios.SMBIOSPresent = managementObject[ConstString.BIOS_SMBIOS_PRESENT].TryGetStringValue();
            bios.SMBIOSBIOSVersion = managementObject[ConstString.BIOS_SMBIOS_VERSION].TryGetStringValue();
            bios.SystemBiosMajorVersion = managementObject[ConstString.BIOS_SYSTEM_BIOS_MAJOR_VERSION].TryGetStringValue();
            bios.SystemBiosMinorVersion = managementObject[ConstString.BIOS_SYSTEM_BIOS_MINOR_VERSION].TryGetStringValue();
            bios.Version = managementObject[ConstString.BIOS_VERSION].TryGetStringValue();

            return bios;
        }

        public List<ManagementObject> GetManagementObjectsForSoftwareComponent(IWMIClient wmiClient)
        {
            return wmiClient.RetriveListOfObjectsByExecutingWMIQuery(ConstString.WMI_NAMESPACE_ROOT_CIMV2, ConstString.WMI_QUERY_BIOS);
        }

        public List<Bios> GetStaticDataForSoftwareComponent(ISoftwareStaticProvider softwareStaticProvider)
        {
            return softwareStaticProvider.GetSoftwareStaticDataFromWMI<Bios>();
        }
    }
}
