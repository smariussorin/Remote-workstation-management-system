﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using GalaSoft.MvvmLight.Messaging;
using LiveCharts;
using LiveCharts.Wpf;
using Microsoft.VisualStudio.Language.Intellisense;
using SystemManagament.Client.WPF.Comparer;
using SystemManagament.Client.WPF.Extensions;
using SystemManagament.Client.WPF.ViewModel.Helpers;
using SystemManagament.Client.WPF.ViewModel.Messages;
using SystemManagament.Client.WPF.WorkstationMonitorService;

namespace SystemManagament.Client.WPF.ViewModel.Wcf
{
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1118:ParameterMustNotSpanMultipleLines", Justification = "Reviewed.")]
    public class WcfClient : IWcfClient
    {
        private IDynamicDataHelper dynamicDataHelper;
        private string connectionError = "Can't connect to remote machine.";
        private string endpointNotFoundError = "Can't connect to remote machine. The remote endpoint could not be found. The endpoint may not be found or reachable because the remote endpoint is down, the remote endpoint is unreachable, or because the remote network is unreachable. ";
        private string timeoutError = "Cant't connect remote machine. Timeout was reached.";

        public WcfClient(IDynamicDataHelper dynamicDataHelper)
        {
            this.dynamicDataHelper = dynamicDataHelper;
        }

        public string UriAddress { get; set; }

        public string MachineIdentifier { get; set; }

        public async Task<HardwareStaticData> ReadHardwareStaticDataAsync()
        {
            HardwareStaticData result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.ReadHardwareStaticDataAsync();
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            return result;
        }

        public async Task<HardwareDynamicData> ReadHardwareDynamicDataAsync(
            WpfObservableRangeCollection<HardwareDynamicData> hardwareDynamicObservableCollection,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorClock,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorPower,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorTemp,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelProcessorLoad,
            WpfObservableRangeCollection<DynamicPieChartViewModel> dynamicChartViewModelDiskLoad,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelDiskTemp,
            WpfObservableRangeCollection<DynamicPieChartViewModel> dynamicChartViewModelMemoryData,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPULoad,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUTemp,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUClock,
            WpfObservableRangeCollection<DynamicPieChartViewModel> dynamicChartViewModelGPUData,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUVoltage,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelGPUFan,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelMainBoardTemp,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelMainBoardFan,
            WpfObservableRangeCollection<DynamicLineChartViewModel> dynamicChartViewModelMainBoardVoltage,
            CancellationToken cancellationToken)
        {
            HardwareDynamicData result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.ReadHardwareDynamicDataAsync();
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            if (result != null && !cancellationToken.IsCancellationRequested)
            {
                foreach (var processor in result.Processor)
                {
                    foreach (var load in processor.Load)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelProcessorLoad, load, processor.Name);
                    }

                    foreach (var clock in processor.Clock)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelProcessorClock, clock, processor.Name);
                    }

                    foreach (var power in processor.Power)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelProcessorPower, power, processor.Name);
                    }

                    foreach (var temperature in processor.Temperature)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelProcessorTemp, temperature, processor.Name);
                    }
                }

                foreach (var disk in result.Disk)
                {
                    foreach (var load in disk.Load)
                    {
                        this.dynamicDataHelper.DrawDynamicPieChartForHardwareSensor(dynamicChartViewModelDiskLoad, load, disk.Name);
                    }

                    foreach (var temperature in disk.Temperature)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelDiskTemp, temperature, disk.Name);
                    }
                }

                foreach (var memory in result.Memory)
                {
                    foreach (var data in memory.Data)
                    {
                        this.dynamicDataHelper.DrawDynamicPieChartForHardwareSensor(dynamicChartViewModelMemoryData, data, memory.Name);
                    }
                }

                foreach (var gpu in result.VideoController)
                {
                    foreach (var load in gpu.Load)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelGPULoad, load, gpu.Name);
                    }

                    foreach (var temperature in gpu.Temperature)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelGPUTemp, temperature, gpu.Name);
                    }

                    foreach (var clock in gpu.Clock)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelGPUClock, clock, gpu.Name);
                    }

                    foreach (var data in gpu.Data)
                    {
                        this.dynamicDataHelper.DrawDynamicPieChartForHardwareSensor(dynamicChartViewModelGPUData, data, gpu.Name);
                    }

                    foreach (var voltage in gpu.Voltage)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelGPUVoltage, voltage, gpu.Name);
                    }

                    foreach (var fan in gpu.Fan)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelGPUFan, fan, gpu.Name);
                    }
                }

                foreach (var mainBoard in result.MainBoard)
                {
                    foreach (var temperature in mainBoard.Temperature)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelMainBoardTemp, temperature, mainBoard.Name);
                    }

                    foreach (var fan in mainBoard.Fan)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelMainBoardFan, fan, mainBoard.Name);
                    }

                    foreach (var voltage in mainBoard.Voltage)
                    {
                        this.dynamicDataHelper.DrawDynamicLineChartForHardwareSensor(dynamicChartViewModelMainBoardVoltage, voltage, mainBoard.Name);
                    }
                }
            }

            return result;
        }

        public async Task<SoftwareStaticData> ReadSoftwareStaticDataAsync()
        {
            SoftwareStaticData result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.ReadSoftwareStaticDataAsync();
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            return result;
        }

        public async Task<WindowsProcess[]> ReadWindowsProcessDynamicDataAsync(
            WpfObservableRangeCollection<WindowsProcess> windowsProcessDynamicObservableCollection,
            CancellationToken cancellationToken)
        {
            WindowsProcess[] result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.ReadWindowsProcessDynamicDataAsync();
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            if (result != null && !cancellationToken.IsCancellationRequested)
            {
                windowsProcessDynamicObservableCollection.ReplaceRange(result);
            }

            return result;
        }

        public async Task<WindowsService[]> ReadWindowsServiceDynamicDataAsync(
            WpfObservableRangeCollection<WindowsService> windowsServiceDynamicObservableCollection,
            CancellationToken cancellationToken)
        {
            WindowsService[] result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.ReadWindowsServiceDynamicDataAsync();
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            if (result != null && !cancellationToken.IsCancellationRequested)
            {
                windowsServiceDynamicObservableCollection.ReplaceRange(result, new WindowsServiceComparer());
            }

            return result;
        }

        public async Task<WindowsLog[]> ReadWindowsLogDynamicDataAsync()
        {
            WindowsLog[] result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.ReadWindowsLogDynamicDataAsync();
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            return result;
        }

        public async Task<OperationStatus> TurnMachineOffAsync(uint timeoutInSeconds)
        {
            OperationStatus result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.TurnMachineOffAsync(timeoutInSeconds);
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            return result;
        }

        public async Task<OperationStatus> RestartMachineAsync(uint timeoutInSeconds)
        {
            OperationStatus result = null;

            var client = this.GetNewWorkstationMonitorServiceClient();

            try
            {
                result = await client.RestartMachineAsync(timeoutInSeconds);
                client.Close();
            }
            catch (EndpointNotFoundException ex)
            {
                this.SendErrorMessageEndpointNotFound();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (TimeoutException ex)
            {
                this.SendErrorMessageTimeout();
                this.SendCancelCommandMessage();
                client.Abort();
            }
            catch (CommunicationException ex)
            {
                this.SendErrorMessage(ex.Message);
                this.SendCancelCommandMessage();
                client.Abort();
            }

            return result;
        }

        private WorkstationMonitorServiceClient GetNewWorkstationMonitorServiceClient()
        {
            NetTcpBinding netTcpBinding = new NetTcpBinding();
            netTcpBinding.CloseTimeout = new TimeSpan(0, 10, 0);
            netTcpBinding.OpenTimeout = new TimeSpan(0, 10, 0);
            netTcpBinding.SendTimeout = new TimeSpan(0, 10, 0);
            netTcpBinding.MaxBufferPoolSize = 2147483647;
            netTcpBinding.MaxBufferSize = 2147483647;
            netTcpBinding.MaxReceivedMessageSize = 2147483647;

            EndpointAddress endpointAddress = new EndpointAddress(this.UriAddress);

            return new WorkstationMonitorServiceClient(netTcpBinding, endpointAddress);
        }

        private void SendErrorMessageTimeout()
        {
            Messenger.Default.Send(new ErrorMessage()
            {
                Message = this.timeoutError
            });
        }

        private void SendErrorMessageEndpointNotFound()
        {
            Messenger.Default.Send(new ErrorMessage()
            {
                Message = this.endpointNotFoundError
            });
        }

        private void SendErrorMessage(string message)
        {
            Messenger.Default.Send(new ErrorMessage()
            {
                Message = this.connectionError + message
            });
        }

        private void SendCancelCommandMessage()
        {
            Messenger.Default.Send(new CancelCommandMessage()
            {
                RecipientIdentifier = this.MachineIdentifier,
            });
        }
    }
}
