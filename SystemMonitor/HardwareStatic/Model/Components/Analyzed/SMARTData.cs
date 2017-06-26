﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using SystemMonitor.HardwareStatic.Model.Components.Abstract;
using SystemMonitor.HardwareStatic.Model.CustomProperties;
using SystemMonitor.HardwareStatic.Model.CustomProperties.Attributes;
using SystemMonitor.HardwareStatic.Model.CustomProperties.Enums;
using SystemMonitor.HardwareStatic.WMI;

namespace SystemMonitor.HardwareStatic.Model.Components.Analyzed
{
    public class SMARTData
    {
        public string InstanceName { get; set; }

        public string PredicFailure { get; set; }

        public Dictionary<int, SmartDataAttribute> Attributes { get; set; } = new Dictionary<int, SmartDataAttribute>()
        {
                { 0x00, new SmartDataAttribute("(00)Invalid") },
                { 0x01, new SmartDataAttribute("(01)Raw read error rate", RawIdealEnum.LOW) },
                { 0x02, new SmartDataAttribute("(02)Throughput performance", RawIdealEnum.HIGH) },
                { 0x03, new SmartDataAttribute("(03)Spin-up time", RawIdealEnum.LOW) },
                { 0x04, new SmartDataAttribute("(04)Start/Stop count") },
                { 0x05, new SmartDataAttribute("(05)Reallocated sector count", RawIdealEnum.LOW, true) },
                { 0x06, new SmartDataAttribute("(06)Read channel margin") },
                { 0x07, new SmartDataAttribute("(07)Seek error rate") },
                { 0x08, new SmartDataAttribute("(08)Seek time performance", RawIdealEnum.HIGH) },
                { 0x09, new SmartDataAttribute("(09)Power-on hours count") },
                { 0x0A, new SmartDataAttribute("(0A)Spin-up retry count", RawIdealEnum.LOW, true) },
                { 0x0B, new SmartDataAttribute("(0B)Recalibration Retries or Calibration Retry Count", RawIdealEnum.LOW) },
                { 0x0C, new SmartDataAttribute("(0C)Power cycle count") },
                { 0x0D, new SmartDataAttribute("(0D)Soft read error rate", RawIdealEnum.LOW) },
                { 0x16, new SmartDataAttribute("(16)Current Helium Level") },
                { 0xAA, new SmartDataAttribute("(AA)Available Reserved Space") },
                { 0xAB, new SmartDataAttribute("(AB)SSD Program Fail Count") },
                { 0xAC, new SmartDataAttribute("(AC)SSD Erase Fail Count") },
                { 0xAD, new SmartDataAttribute("(AD)SSD Wear Leveling Count") },
                { 0xAE, new SmartDataAttribute("(AE)Unexpected power loss count") },
                { 0xAF, new SmartDataAttribute("(AF)Power Loss Protection Failure") },
                { 0xB0, new SmartDataAttribute("(B0)Erase Fail Count") },
                { 0xB1, new SmartDataAttribute("(B1)Wear Range Delta") },
                { 0xB3, new SmartDataAttribute("(B3)Used Reserved Block Count Total") },
                { 0xB4, new SmartDataAttribute("(B4)Unused Reserved Block Count Total") },
                { 0xB5, new SmartDataAttribute("(B5)Program Fail Count Total or Non-4K Aligned Access Count", RawIdealEnum.LOW) },
                { 0xB6, new SmartDataAttribute("(B6)Erase Fail Count") },
                { 0xB7, new SmartDataAttribute("(B7)SATA Downshift Error Count or Runtime Bad Block", RawIdealEnum.LOW) },
                { 0xB8, new SmartDataAttribute("(B8)End-to-End error / IOEDC", RawIdealEnum.LOW, true) },
                { 0xB9, new SmartDataAttribute("(B9)Head Stability") },
                { 0xBA, new SmartDataAttribute("(BA)Induced Op-Vibration Detection") },
                { 0xBB, new SmartDataAttribute("(BB)Reported Uncorrectable Errors", RawIdealEnum.LOW, true) },
                { 0xBC, new SmartDataAttribute("(BC)Command Timeout", RawIdealEnum.LOW, true) },
                { 0xBD, new SmartDataAttribute("(BD)High Fly Writes", RawIdealEnum.LOW) },
                { 0xBE, new SmartDataAttribute("(BE)Temperature Difference or Airflow Temperature") },
                { 0xBF, new SmartDataAttribute("(BF)G-sense error rate", RawIdealEnum.LOW) },
                { 0xC0, new SmartDataAttribute("(C0)Power-off Retract Count, Emergency Retract Cycle Count (Fujitsu) or Unsafe Shutdown Count", RawIdealEnum.LOW) },
                { 0xC1, new SmartDataAttribute("(C1)Load Cycle Count or Load/Unload Cycle Count (Fujitsu)", RawIdealEnum.LOW) },
                { 0xC2, new SmartDataAttribute("(C2)Temperature or Temperature Celsius", RawIdealEnum.LOW) },
                { 0xC3, new SmartDataAttribute("(C3)Hardware ECC recovered") },
                { 0xC4, new SmartDataAttribute("(C4)Reallocation Event Count", RawIdealEnum.LOW, true) },
                { 0xC5, new SmartDataAttribute("(C5)Current pending sector count", RawIdealEnum.LOW, true) },
                { 0xC6, new SmartDataAttribute("(C6)(Offline) Uncorrectable Sector Count", RawIdealEnum.LOW, true) },
                { 0xC7, new SmartDataAttribute("(C7)UltraDMA CRC Error Count", RawIdealEnum.LOW) },
                { 0xC8, new SmartDataAttribute("(C8)Write error rate", RawIdealEnum.LOW) },
                { 0xC9, new SmartDataAttribute("(C9)Soft Read Error Rate or TA Counter Detected", RawIdealEnum.LOW, true) },
                { 0xCA, new SmartDataAttribute("(CA)Data Address Mark errors or TA Counter Increased", RawIdealEnum.LOW) },
                { 0xCB, new SmartDataAttribute("(CB)Run out cancel", RawIdealEnum.LOW) },
                { 0xCC, new SmartDataAttribute("(CC)Soft ECC correction", RawIdealEnum.LOW) },
                { 0xCD, new SmartDataAttribute("(CD)Thermal asperity rate (TAR)", RawIdealEnum.LOW) },
                { 0xCE, new SmartDataAttribute("(CE)Flying height") },
                { 0xCF, new SmartDataAttribute("(CF)Spin high current", RawIdealEnum.LOW) },
                { 0xD0, new SmartDataAttribute("(D0)Spin buzz") },
                { 0xD1, new SmartDataAttribute("(D1)Offline seek performance") },
                { 0xD2, new SmartDataAttribute("(D2)Vibration During Write") },
                { 0xD3, new SmartDataAttribute("(D3)Vibration During Write") },
                { 0xD4, new SmartDataAttribute("(D4)Shock During Write") },
                { 0xDC, new SmartDataAttribute("(DC)Disk shift", RawIdealEnum.LOW) },
                { 0xDD, new SmartDataAttribute("(DD)G-sense error rate", RawIdealEnum.LOW) },
                { 0xDE, new SmartDataAttribute("(DE)Loaded hours") },
                { 0xDF, new SmartDataAttribute("(DF)Load/unload retry count") },
                { 0xE0, new SmartDataAttribute("(E0)Load friction", RawIdealEnum.LOW) },
                { 0xE1, new SmartDataAttribute("(E1)Load/Unload cycle count", RawIdealEnum.LOW) },
                { 0xE2, new SmartDataAttribute("(E2)Load-in time") },
                { 0xE3, new SmartDataAttribute("(E3)Torque amplification count", RawIdealEnum.LOW) },
                { 0xE4, new SmartDataAttribute("(E4)Power-off retract count", RawIdealEnum.LOW) },
                { 0xE6, new SmartDataAttribute("(E6)GMR Head Amplitude (magnetic HDDs), Drive Life Protection Status (SSDs)") },
                { 0xE7, new SmartDataAttribute("(E7)Life Left (SSDs) or Temperature") },
                { 0xE8, new SmartDataAttribute("(E8)Endurance Remaining or Available Reserved Space") },
                { 0xE9, new SmartDataAttribute("(E9)Media Wearout Indicator (SSDs) or Power-On Hours") },
                { 0xEA, new SmartDataAttribute("(EA)Average erase count AND Maximum Erase Count") },
                { 0xEB, new SmartDataAttribute("(EB)Good Block Count AND System(Free) Block Count") },
                { 0xF0, new SmartDataAttribute("(F0)Head Flying Hours or 'Transfer Error Rate' (Fujitsu)") },
                { 0xF1, new SmartDataAttribute("(F1)Total LBAs Written") },
                { 0xF2, new SmartDataAttribute("(F2)Total LBAs Read") },
                { 0xF3, new SmartDataAttribute("(F3)Total LBAs Written Expanded") },
                { 0xF4, new SmartDataAttribute("(F4)Total LBAs Read Expanded") },
                { 0xF9, new SmartDataAttribute("(F9)NAND Writes (1GiB)") },
                { 0xFA, new SmartDataAttribute("(FA)Read Error Retry Rate", RawIdealEnum.LOW) },
                { 0xFB, new SmartDataAttribute("(FB)Minimum Spares Remaining") },
                { 0xFC, new SmartDataAttribute("(FC)Newly Added Bad Flash Block") },
                { 0xFE, new SmartDataAttribute("(FE)Free Fall Protection", RawIdealEnum.LOW) },
            };
    }
}
