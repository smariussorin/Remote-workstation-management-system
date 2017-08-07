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
    public class SmartFailurePredictThresholds : HardwareStaticComponent
    {
        public byte[] VendorSpecific { get; set; }

        public string InstanceName { get; set; }

        public override HardwareStaticComponent ExtractData(ManagementObject managementObject)
        {
            SmartFailurePredictThresholds smartFailurePredictThresholds = new SmartFailurePredictThresholds();
            smartFailurePredictThresholds.Caption = string.Empty;
            smartFailurePredictThresholds.Description = string.Empty;
            smartFailurePredictThresholds.InstanceName = managementObject[ConstStringHardwareStatic.SMART_INSTANCE_NAME]?.ToString() ?? string.Empty;
            smartFailurePredictThresholds.Name = string.Empty;
            smartFailurePredictThresholds.VendorSpecific = (byte[])managementObject[ConstStringHardwareStatic.SMART_VENDOR_SPECIFIC] ?? new byte[0];
            smartFailurePredictThresholds.Status = string.Empty;
            return smartFailurePredictThresholds;
        }

        public override List<ManagementObject> GetManagementObjectsForHardwareComponent(IWMIClient wMIClient)
        {
            return wMIClient.RetriveListOfObjectsByExecutingWMIQuery(ConstStringHardwareStatic.WMI_NAMESPACE_ROOT_WMI, ConstStringHardwareStatic.WMI_QUERY_SMART_THRESHOLDS);
        }
    }
}