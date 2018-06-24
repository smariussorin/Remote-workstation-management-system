﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.VisualStudio.Language.Intellisense;
using SystemManagament.Client.WPF.Extensions;
using SystemManagament.Client.WPF.Factories;
using SystemManagament.Client.WPF.Validator;
using SystemManagament.Client.WPF.ViewModel.Commands.Abstract;
using SystemManagament.Client.WPF.ViewModel.Helpers;
using SystemManagament.Client.WPF.ViewModel.Wcf;
using SystemManagament.Client.WPF.WorkstationMonitorService;

namespace SystemManagament.Client.WPF.ViewModel
{
    public class WorkStationViewModel : ViewModelBase
    {
        private readonly IWcfClient wcfClient;
        private readonly ICommandFactory commandFactory;
        private readonly IUintValidator uintValidator;

        private WpfObservableRangeCollection<WindowsProcess> windowsProcess = new WpfObservableRangeCollection<WindowsProcess>();
        private WpfObservableRangeCollection<WindowsService> windowsService = new WpfObservableRangeCollection<WindowsService>();
        private WpfObservableRangeCollection<WindowsLog> windowsLog = new WpfObservableRangeCollection<WindowsLog>();

        private WpfObservableRangeCollection<HardwareDynamicData> hardwareDynamic = new WpfObservableRangeCollection<HardwareDynamicData>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorPower = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorTemp = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorLoad = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorClock = new WpfObservableRangeCollection<DynamicLineChartViewModel>();

        private WpfObservableRangeCollection<DynamicPieChartViewModel> dynamicChartViewModelDiskLoad = new WpfObservableRangeCollection<DynamicPieChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelDiskTemp = new WpfObservableRangeCollection<DynamicLineChartViewModel>();

        private WpfObservableRangeCollection<DynamicPieChartViewModel> dynamicChartViewModelMemoryData = new WpfObservableRangeCollection<DynamicPieChartViewModel>();

        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPULoad = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUTemp = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUClock = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicPieChartViewModel> dynamicChartViewModelGPUData = new WpfObservableRangeCollection<DynamicPieChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUVoltage = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUFan = new WpfObservableRangeCollection<DynamicLineChartViewModel>();

        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelMainBoardTemp = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelMainBoardFan = new WpfObservableRangeCollection<DynamicLineChartViewModel>();
        private WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelMainBoardVoltage = new WpfObservableRangeCollection<DynamicLineChartViewModel>();

        private WpfObservableRangeCollection<ProcessorStatic> processorStatic = new WpfObservableRangeCollection<ProcessorStatic>();
        private WpfObservableRangeCollection<ProcessorCache> processorCache = new WpfObservableRangeCollection<ProcessorCache>();
        private WpfObservableRangeCollection<Memory> memory = new WpfObservableRangeCollection<Memory>();
        private WpfObservableRangeCollection<BaseBoard> baseBoard = new WpfObservableRangeCollection<BaseBoard>();
        private WpfObservableRangeCollection<VideoController> videoController = new WpfObservableRangeCollection<VideoController>();
        private WpfObservableRangeCollection<NetworkAdapter> networkAdapter = new WpfObservableRangeCollection<NetworkAdapter>();
        private WpfObservableRangeCollection<PnPEntity> pnPEntity = new WpfObservableRangeCollection<PnPEntity>();
        private WpfObservableRangeCollection<CDROMDrive> cDROMDrive = new WpfObservableRangeCollection<CDROMDrive>();
        private WpfObservableRangeCollection<Fan> fan = new WpfObservableRangeCollection<Fan>();
        private WpfObservableRangeCollection<Battery> battery = new WpfObservableRangeCollection<Battery>();
        private WpfObservableRangeCollection<Printer> printer = new WpfObservableRangeCollection<Printer>();
        private WpfObservableRangeCollection<Storage> storage = new WpfObservableRangeCollection<Storage>();

        private WpfObservableRangeCollection<CurrentUser> currentUser = new WpfObservableRangeCollection<CurrentUser>();
        private WpfObservableRangeCollection<ClaimDuplicate> currentUserClaims = new WpfObservableRangeCollection<ClaimDuplicate>();
        private WpfObservableRangeCollection<GroupDuplicate> currentUserGroups = new WpfObservableRangeCollection<GroupDuplicate>();
        private WpfObservableRangeCollection<OS> operatingSystem = new WpfObservableRangeCollection<OS>();
        private WpfObservableRangeCollection<Bios> bios = new WpfObservableRangeCollection<Bios>();
        private WpfObservableRangeCollection<InstalledProgram> installedProgram = new WpfObservableRangeCollection<InstalledProgram>();
        private WpfObservableRangeCollection<MicrosoftWindowsUpdate> microsoftWindowsUpdate = new WpfObservableRangeCollection<MicrosoftWindowsUpdate>();
        private WpfObservableRangeCollection<StartupCommand> startupCommand = new WpfObservableRangeCollection<StartupCommand>();
        private WpfObservableRangeCollection<LocalUser> localUser = new WpfObservableRangeCollection<LocalUser>();

        private UIntParameter turnMachineOffTimeoutInSeconds = new UIntParameter(60);
        private UIntParameter restartMachineTimeoutInSeconds = new UIntParameter(60);

        public WorkStationViewModel(IWcfClient wcfClient, ICommandFactory commandFactory, IUintValidator uintValidator)
        {
            this.wcfClient = wcfClient;
            this.commandFactory = commandFactory;
            this.uintValidator = uintValidator;
            this.InitializeCommands();
        }

        public IAsyncCommand LoadWindowsProcessDynamicDataCommand { get; private set; }

        public IAsyncCommand LoadWindowsServiceDynamicDataCommand { get; private set; }

        public IAsyncCommand LoadWindowsLogDynamicDataCommand { get; private set; }

        public IAsyncCommand LoadHardwareDynamicDataCommand { get; private set; }

        public IAsyncCommand LoadHardwareStaticDataCommand { get; private set; }

        public IAsyncCommand LoadSoftwareStaticDataCommand { get; private set; }

        public IAsyncCommand TurnMachineOffCommand { get; private set; }

        public IAsyncCommand ForceTurnMachineOffCommand { get; private set; }

        public IAsyncCommand RestartMachineCommand { get; private set; }

        public IAsyncCommand ForceRestartMachineCommand { get; private set; }

        public ICommand ClearDataCommand { get; private set; }

        public WpfObservableRangeCollection<WindowsProcess> WindowsProcess
        {
            get
            {
                return this.windowsProcess;
            }

            private set
            {
                this.Set(() => this.WindowsProcess, ref this.windowsProcess, value);
            }
        }

        public WpfObservableRangeCollection<WindowsService> WindowsService
        {
            get
            {
                return this.windowsService;
            }

            private set
            {
                this.Set(() => this.WindowsService, ref this.windowsService, value);
            }
        }

        public WpfObservableRangeCollection<WindowsLog> WindowsLog
        {
            get
            {
                return this.windowsLog;
            }

            private set
            {
                this.Set(() => this.WindowsLog, ref this.windowsLog, value);
            }
        }

        public WpfObservableRangeCollection<HardwareDynamicData> HardwareDynamic
        {
            get
            {
                return this.hardwareDynamic;
            }

            private set
            {
                this.Set(() => this.HardwareDynamic, ref this.hardwareDynamic, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelProcessorClock
        {
            get
            {
                return this.dynamicChartViewModelProcessorClock;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelProcessorClock, ref this.dynamicChartViewModelProcessorClock, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelProcessorPower
        {
            get
            {
                return this.dynamicChartViewModelProcessorPower;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelProcessorPower, ref this.dynamicChartViewModelProcessorPower, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelProcessorLoad
        {
            get
            {
                return this.dynamicChartViewModelProcessorLoad;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelProcessorLoad, ref this.dynamicChartViewModelProcessorLoad, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelProcessorTemp
        {
            get
            {
                return this.dynamicChartViewModelProcessorTemp;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelProcessorTemp, ref this.dynamicChartViewModelProcessorTemp, value);
            }
        }

        public WpfObservableRangeCollection<DynamicPieChartViewModel> DynamicChartViewModelDiskLoad
        {
            get
            {
                return this.dynamicChartViewModelDiskLoad;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelDiskLoad, ref this.dynamicChartViewModelDiskLoad, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelDiskTemp
        {
            get
            {
                return this.dynamicChartViewModelDiskTemp;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelDiskTemp, ref this.dynamicChartViewModelDiskTemp, value);
            }
        }

        public WpfObservableRangeCollection<DynamicPieChartViewModel> DynamicChartViewModelMemoryData
        {
            get
            {
                return this.dynamicChartViewModelMemoryData;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelMemoryData, ref this.dynamicChartViewModelMemoryData, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelGPULoad
        {
            get
            {
                return this.dynamicChartViewModelGPULoad;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelGPULoad, ref this.dynamicChartViewModelGPULoad, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelGPUTemp
        {
            get
            {
                return this.dynamicChartViewModelGPUTemp;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelGPUTemp, ref this.dynamicChartViewModelGPUTemp, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelGPUClock
        {
            get
            {
                return this.dynamicChartViewModelGPUClock;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelGPUClock, ref this.dynamicChartViewModelGPUClock, value);
            }
        }

        public WpfObservableRangeCollection<DynamicPieChartViewModel> DynamicChartViewModelGPUData
        {
            get
            {
                return this.dynamicChartViewModelGPUData;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelGPUData, ref this.dynamicChartViewModelGPUData, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelGPUVoltage
        {
            get
            {
                return this.dynamicChartViewModelGPUVoltage;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelGPUVoltage, ref this.dynamicChartViewModelGPUVoltage, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelGPUFan
        {
            get
            {
                return this.dynamicChartViewModelGPUFan;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelGPUFan, ref this.dynamicChartViewModelGPUFan, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelMainBoardTemp
        {
            get
            {
                return this.dynamicChartViewModelMainBoardTemp;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelMainBoardTemp, ref this.dynamicChartViewModelMainBoardTemp, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelMainBoardFan
        {
            get
            {
                return this.dynamicChartViewModelMainBoardFan;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelMainBoardFan, ref this.dynamicChartViewModelMainBoardFan, value);
            }
        }

        public WpfObservableRangeCollection<DynamicLineChartViewModel> DynamicChartViewModelMainBoardVoltage
        {
            get
            {
                return this.dynamicChartViewModelMainBoardVoltage;
            }

            private set
            {
                this.Set(() => this.DynamicChartViewModelMainBoardVoltage, ref this.dynamicChartViewModelMainBoardVoltage, value);
            }
        }

        public WpfObservableRangeCollection<ProcessorStatic> ProcessorStatic
        {
            get
            {
                return this.processorStatic;
            }

            private set
            {
                this.Set(() => this.ProcessorStatic, ref this.processorStatic, value);
            }
        }

        public WpfObservableRangeCollection<ProcessorCache> ProcessorCache
        {
            get
            {
                return this.processorCache;
            }

            private set
            {
                this.Set(() => this.ProcessorCache, ref this.processorCache, value);
            }
        }

        public WpfObservableRangeCollection<Memory> Memory
        {
            get
            {
                return this.memory;
            }

            private set
            {
                this.Set(() => this.Memory, ref this.memory, value);
            }
        }

        public WpfObservableRangeCollection<BaseBoard> BaseBoard
        {
            get
            {
                return this.baseBoard;
            }

            private set
            {
                this.Set(() => this.BaseBoard, ref this.baseBoard, value);
            }
        }

        public WpfObservableRangeCollection<VideoController> VideoController
        {
            get
            {
                return this.videoController;
            }

            private set
            {
                this.Set(() => this.VideoController, ref this.videoController, value);
            }
        }

        public WpfObservableRangeCollection<NetworkAdapter> NetworkAdapter
        {
            get
            {
                return this.networkAdapter;
            }

            private set
            {
                this.Set(() => this.NetworkAdapter, ref this.networkAdapter, value);
            }
        }

        public WpfObservableRangeCollection<PnPEntity> PnPEntity
        {
            get
            {
                return this.pnPEntity;
            }

            private set
            {
                this.Set(() => this.PnPEntity, ref this.pnPEntity, value);
            }
        }

        public WpfObservableRangeCollection<CDROMDrive> CDROMDrive
        {
            get
            {
                return this.cDROMDrive;
            }

            private set
            {
                this.Set(() => this.CDROMDrive, ref this.cDROMDrive, value);
            }
        }

        public WpfObservableRangeCollection<Fan> Fan
        {
            get
            {
                return this.fan;
            }

            private set
            {
                this.Set(() => this.Fan, ref this.fan, value);
            }
        }

        public WpfObservableRangeCollection<Printer> Printer
        {
            get
            {
                return this.printer;
            }

            private set
            {
                this.Set(() => this.Printer, ref this.printer, value);
            }
        }

        public WpfObservableRangeCollection<Battery> Battery
        {
            get
            {
                return this.battery;
            }

            private set
            {
                this.Set(() => this.Battery, ref this.battery, value);
            }
        }

        public WpfObservableRangeCollection<Storage> Storage
        {
            get
            {
                return this.storage;
            }

            private set
            {
                this.Set(() => this.Storage, ref this.storage, value);
            }
        }

        public WpfObservableRangeCollection<CurrentUser> CurrentUser
        {
            get
            {
                return this.currentUser;
            }

            private set
            {
                this.Set(() => this.CurrentUser, ref this.currentUser, value);
            }
        }

        public WpfObservableRangeCollection<ClaimDuplicate> CurrentUserClaims
        {
            get
            {
                return this.currentUserClaims;
            }

            private set
            {
                this.Set(() => this.CurrentUserClaims, ref this.currentUserClaims, value);
            }
        }

        public WpfObservableRangeCollection<GroupDuplicate> CurrentUserGroups
        {
            get
            {
                return this.currentUserGroups;
            }

            private set
            {
                this.Set(() => this.CurrentUserGroups, ref this.currentUserGroups, value);
            }
        }

        public WpfObservableRangeCollection<OS> OperatingSystem
        {
            get
            {
                return this.operatingSystem;
            }

            private set
            {
                this.Set(() => this.OperatingSystem, ref this.operatingSystem, value);
            }
        }

        public WpfObservableRangeCollection<Bios> Bios
        {
            get
            {
                return this.bios;
            }

            private set
            {
                this.Set(() => this.Bios, ref this.bios, value);
            }
        }

        public WpfObservableRangeCollection<InstalledProgram> InstalledProgram
        {
            get
            {
                return this.installedProgram;
            }

            private set
            {
                this.Set(() => this.InstalledProgram, ref this.installedProgram, value);
            }
        }

        public WpfObservableRangeCollection<MicrosoftWindowsUpdate> MicrosoftWindowsUpdate
        {
            get
            {
                return this.microsoftWindowsUpdate;
            }

            private set
            {
                this.Set(() => this.MicrosoftWindowsUpdate, ref this.microsoftWindowsUpdate, value);
            }
        }

        public WpfObservableRangeCollection<StartupCommand> StartupCommand
        {
            get
            {
                return this.startupCommand;
            }

            private set
            {
                this.Set(() => this.StartupCommand, ref this.startupCommand, value);
            }
        }

        public WpfObservableRangeCollection<LocalUser> LocalUser
        {
            get
            {
                return this.localUser;
            }

            private set
            {
                this.Set(() => this.LocalUser, ref this.localUser, value);
            }
        }

        public UIntParameter TurnMachineOffTimeoutInSeconds
        {
            get
            {
                return this.turnMachineOffTimeoutInSeconds;
            }

            set
            {
                if (this.uintValidator.Validate(value, CultureInfo.CurrentCulture).IsValid)
                {
                    this.Set(() => this.TurnMachineOffTimeoutInSeconds, ref this.turnMachineOffTimeoutInSeconds, value);
                }
            }
        }

        public UIntParameter RestartMachineTimeoutInSeconds
        {
            get
            {
                return this.restartMachineTimeoutInSeconds;
            }

            set
            {
                if (this.uintValidator.Validate(value, CultureInfo.CurrentCulture).IsValid)
                {
                    this.Set(() => this.RestartMachineTimeoutInSeconds, ref this.restartMachineTimeoutInSeconds, value);
                }
            }
        }

        public UIntParameter ForceMachineTurnOffTimeout { get; } = new UIntParameter(10);

        public UIntParameter ForceMachineRestartTimeout { get; } = new UIntParameter(10);

        private void InitializeCommands()
        {
            this.ClearDataCommand = this.commandFactory.CreateClearDataCommand(this.ClearData);
            this.LoadWindowsProcessDynamicDataCommand = this.commandFactory.CreateWindowsProcessDynamicDataCommand(this.WindowsProcess);
            this.LoadWindowsServiceDynamicDataCommand = this.commandFactory.CreateWindowsServiceDynamicDataCommand(this.WindowsService);
            this.LoadWindowsLogDynamicDataCommand = this.commandFactory.CreateWindowsLogDynamicDataCommand(this.WindowsLog);
            this.LoadHardwareDynamicDataCommand = this.commandFactory.CreateHardwareDynamicDataCommand(
                this.HardwareDynamic,
                this.DynamicChartViewModelProcessorClock,
                this.DynamicChartViewModelProcessorPower,
                this.DynamicChartViewModelProcessorTemp,
                this.DynamicChartViewModelProcessorLoad,
                this.DynamicChartViewModelDiskLoad,
                this.DynamicChartViewModelDiskTemp,
                this.DynamicChartViewModelMemoryData,
                this.DynamicChartViewModelGPULoad,
                this.DynamicChartViewModelGPUTemp,
                this.DynamicChartViewModelGPUClock,
                this.DynamicChartViewModelGPUData,
                this.DynamicChartViewModelGPUVoltage,
                this.DynamicChartViewModelGPUFan,
                this.DynamicChartViewModelMainBoardTemp,
                this.DynamicChartViewModelMainBoardFan,
                this.DynamicChartViewModelMainBoardVoltage);

            this.LoadHardwareStaticDataCommand = this.commandFactory.CreateHardwareStaticDataCommand(
                this.ProcessorStatic,
                this.ProcessorCache,
                this.Memory,
                this.BaseBoard,
                this.VideoController,
                this.NetworkAdapter,
                this.PnPEntity,
                this.CDROMDrive,
                this.Fan,
                this.Printer,
                this.Battery,
                this.Storage);

            this.LoadSoftwareStaticDataCommand = this.commandFactory.CreateSoftwareStaticDataCommand(
                this.CurrentUser,
                this.CurrentUserClaims,
                this.CurrentUserGroups,
                this.OperatingSystem,
                this.Bios,
                this.InstalledProgram,
                this.MicrosoftWindowsUpdate,
                this.StartupCommand,
                this.LocalUser);

            this.TurnMachineOffCommand = this.commandFactory.CreateTurnMachineOffCommand(this.TurnMachineOffTimeoutInSeconds);
            this.RestartMachineCommand = this.commandFactory.CreateRestartMachineOffCommand(this.RestartMachineTimeoutInSeconds);
            this.ForceTurnMachineOffCommand = this.commandFactory.CreateTurnMachineOffCommand(this.ForceMachineTurnOffTimeout);
            this.ForceRestartMachineCommand = this.commandFactory.CreateRestartMachineOffCommand(this.ForceMachineRestartTimeout);
        }

        private void ClearData()
        {
            //this.windowsProcess.ClearAllItems();
            //this.memoryItems.ClearAllItems();
        }
    }
}
