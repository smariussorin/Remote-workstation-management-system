﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.HardwareStatic.Model;
using SystemMonitor.HardwareStatic.Model.Components;
using SystemMonitor.HardwareStatic.Model.CustomProperties;
using SystemMonitor.HardwareStatic.Model.CustomProperties.Enum;

[assembly: InternalsVisibleTo("SystemMonitorLibUnitTest")]
namespace SystemMonitor.HardwareStatic.WMI
{
    internal class WMIDataExtractor : IWMIDataExtractor
    {
        public Processor ExtractDataProcessor(ManagementObject managementObjectWin32_Processor, List<ManagementObject> managementObjectWin32_CacheMemory)
        {
            Processor processor = new Processor();
            processor.AddressWidth = new UnitValue(Unit.BIT, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_ADDRESS_WIDTH]?.ToString() ?? string.Empty);

            if (managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_ARCHITECTURE] != null)
            {
                processor.Architecture = ((ArchitectureEnum)((ushort)managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_ARCHITECTURE])).ToString();
            }
            else
            {
                processor.Architecture = string.Empty;
            }

            processor.Caption = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_CAPTION]?.ToString() ?? string.Empty;
            processor.DataWidth = new UnitValue(Unit.BIT, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_DATA_WIDTH]?.ToString() ?? string.Empty);
            processor.Description = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_DESCRIPTION]?.ToString() ?? string.Empty;
            processor.BusSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_BUS_SPEED]?.ToString() ?? string.Empty);

            foreach (var cache in managementObjectWin32_CacheMemory)
            {
                processor.Cache.Add(new CpuCacheMemory(
                                            new UnitValue(Unit.MHZ, cache[ConstStringHardwareStatic.PROCESSOR_CACHE_SPEED]?.ToString() ?? string.Empty),
                                            new UnitValue(Unit.KB, cache[ConstStringHardwareStatic.PROCESSOR_CACHE_SIZE]?.ToString() ?? string.Empty),
                                            ((CacheLevelEnum)((ushort)cache[ConstStringHardwareStatic.PROCESSOR_CACHE_LEVEL])).ToString()));
            }

            processor.Manufacturer = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_MANUFACTURER]?.ToString() ?? string.Empty;
            processor.MaxClockSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_MAX_CLOCK_SPEED]?.ToString() ?? string.Empty);
            processor.Name = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_NAME]?.ToString() ?? string.Empty;
            processor.NumberOfCores = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_NUMBER_OF_CORES]?.ToString() ?? string.Empty;
            processor.NumberOfLogicalProcessors = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_NUMBER_OF_LOGICAL_PROCESSORS]?.ToString() ?? string.Empty;
            processor.ProcessorID = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_ID]?.ToString() ?? string.Empty;
            //processor.SerialNumber = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_SERIAL_NUMBER]?.ToString() ?? string.Empty;
            processor.SocketDesignation = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_SOCKET_DESIGNATION]?.ToString() ?? string.Empty;
            processor.Stepping = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_STEPPING]?.ToString() ?? string.Empty;
            //processor.ThreadCount = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_THREAD_COUNT]?.ToString() ?? string.Empty;
            processor.UniqueId = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_UNIQUE_ID]?.ToString() ?? string.Empty;
            return processor;
        }
    }
}
