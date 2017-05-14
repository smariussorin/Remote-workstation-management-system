﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.HardwareStatic.Model.Components;
using SystemMonitor.HardwareStatic.Model.CustomProperties;
using SystemMonitor.HardwareStatic.Model.CustomProperties.Enum;

[assembly: InternalsVisibleTo("SystemMonitorLibUnitTest")]
namespace SystemMonitor.HardwareStatic.WMI
{
    internal class WMIDataExtractor : IWMIDataExtractor
    {
        public Processor ExtractDataProcessor(ManagementObject managementObjectWin32_Processor, ManagementObject managementObjectWin32_CacheMemory)
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
            processor.L1CacheSize = new UnitValue(Unit.KB, managementObjectWin32_CacheMemory[ConstStringHardwareStatic.PROCESSOR_L1_CACHE_SIZE]?.ToString() ?? string.Empty);
            processor.L1CacheSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_CacheMemory[ConstStringHardwareStatic.PROCESSOR_L1_CACHE_SPEED]?.ToString() ?? string.Empty);
            processor.L2CacheSize = new UnitValue(Unit.KB, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_L2_CACHE_SIZE]?.ToString() ?? string.Empty);
            processor.L2CacheSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_L2_CACHE_SPEED]?.ToString() ?? string.Empty);
            processor.L3CacheSize = new UnitValue(Unit.KB, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_L3_CACHE_SIZE]?.ToString() ?? string.Empty);
            processor.L3CacheSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_L3_CACHE_SPEED]?.ToString() ?? string.Empty);
            processor.Manufacturer = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_MANUFACTURER]?.ToString() ?? string.Empty;
            processor.MaxClockSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_MAX_CLOCK_SPEED]?.ToString() ?? string.Empty);
            processor.Name = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_NAME]?.ToString() ?? string.Empty;
            processor.NumberOfCores = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_NUMBER_OF_CORES]?.ToString() ?? string.Empty;
            processor.NumberOfLogicalProcessors = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_NUMBER_OF_LOGICAL_PROCESSORS]?.ToString() ?? string.Empty;
            processor.ProcessorID = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_ID]?.ToString() ?? string.Empty;
            processor.SocketDesignation = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_SOCKET_DESIGNATION]?.ToString() ?? string.Empty;
            processor.Stepping = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_STEPPING]?.ToString() ?? string.Empty;
            processor.ThreadCount = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_THREAD_COUNT]?.ToString() ?? string.Empty;
            processor.UniqueId = managementObjectWin32_Processor[ConstStringHardwareStatic.PROCESSOR_UNIQUE_ID]?.ToString() ?? string.Empty;

            return processor;
        }

        public Memory ExtractDataMemory(ManagementObject managementObjectWin32_PhysicalMemory)
        {
            Memory memory = new Memory();
            memory.BankLabel = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_BANK_LABEL]?.ToString() ?? string.Empty;
            memory.Capacity = new UnitValue(Unit.B, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_CAPACITY]?.ToString() ?? string.Empty);
            memory.Caption = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_CAPTION]?.ToString() ?? string.Empty;
            memory.ConfiguredClockSpeed = new UnitValue(Unit.MHZ, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_CONFIGURED_CLOCK_SPEED]?.ToString() ?? string.Empty);
            memory.ConfiguredVoltage = new UnitValue(Unit.MV, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_CONFIGURED_VOLTAGE]?.ToString() ?? string.Empty);
            memory.DataWidth = new UnitValue(Unit.BIT, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_DATA_WIDTH]?.ToString() ?? string.Empty);
            memory.Description = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_DESCRIPTION]?.ToString() ?? string.Empty;
            memory.DeviceLocator = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_DEVICE_LOCATOR]?.ToString() ?? string.Empty;
            memory.Manufacturer = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_MANUFACTURER]?.ToString() ?? string.Empty;
            memory.MaxVoltage = new UnitValue(Unit.MV, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_MAX_VOLTAGE]?.ToString() ?? string.Empty);

            if (managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_MEMORY_TYPE] != null)
            {
                memory.MemoryType = ((MemoryTypeEnum)((ushort)managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_MEMORY_TYPE])).ToString();
            }
            else
            {
                memory.MemoryType = string.Empty;
            }

            memory.MinVoltage = new UnitValue(Unit.MV, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_MIN_VOLTAGE]?.ToString() ?? string.Empty);
            memory.Name = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_NAME]?.ToString() ?? string.Empty;
            memory.PartNumber = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_PART_NUMBER]?.ToString() ?? string.Empty;
            memory.SerialNumber = managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_SERIAL_NUMBER]?.ToString() ?? string.Empty;
            memory.TotalWidth = new UnitValue(Unit.BIT, managementObjectWin32_PhysicalMemory[ConstStringHardwareStatic.MEMORY_TOTAL_WIDTH]?.ToString() ?? string.Empty);

            return memory;
        }
    }
}
