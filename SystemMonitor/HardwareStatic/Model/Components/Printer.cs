﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.HardwareStatic.Model.Components.Abstract;
using SystemMonitor.HardwareStatic.WMI;

namespace SystemMonitor.HardwareStatic.Model.Components
{
    public class Printer : HardwareComponent
    {
        public string AveragePagesPerMinute { get; private set; }

        public string Default { get; private set; }

        public string DeviceID { get; private set; }

        public string PortName { get; private set; }

        public string PrintProcessor { get; private set; }

        public override HardwareComponent ExtractData(ManagementObject managementObject)
        {
            Printer printer = new Printer();
            printer.AveragePagesPerMinute = managementObject[ConstStringHardwareStatic.PRINTER_AVG_PAGES_PER_MINUTE]?.ToString() ?? string.Empty;
            printer.Caption = managementObject[ConstStringHardwareStatic.HARDWARE_COMPONENT_CAPTION]?.ToString() ?? string.Empty;
            printer.Default = managementObject[ConstStringHardwareStatic.PRINTER_DEFAULT]?.ToString() ?? string.Empty;
            printer.Description = managementObject[ConstStringHardwareStatic.HARDWARE_COMPONENT_DESCRIPTION]?.ToString() ?? string.Empty;
            printer.DeviceID = managementObject[ConstStringHardwareStatic.PRINTER_DEVICE_ID]?.ToString() ?? string.Empty;
            printer.Name = managementObject[ConstStringHardwareStatic.HARDWARE_COMPONENT_NAME]?.ToString() ?? string.Empty;
            printer.PortName = managementObject[ConstStringHardwareStatic.PRINTER_PORT_NAME]?.ToString() ?? string.Empty;
            printer.PrintProcessor = managementObject[ConstStringHardwareStatic.PRINTER_PRINT_PROCESSOR]?.ToString() ?? string.Empty;
            printer.Status = managementObject[ConstStringHardwareStatic.HARDWARE_COMPONENT_STATUS]?.ToString() ?? string.Empty;

            return printer;
        }

        public override List<ManagementObject> GetManagementObjectsForHardwareComponent(IWMIClient wMIClient)
        {
            return wMIClient.RetriveListOfObjectsByExecutingWMIQuery(ConstStringHardwareStatic.WMI_NAMESPACE_ROOT_CIMV2, ConstStringHardwareStatic.WMI_QUERY_PRINTER);
        }
    }
}