﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using SystemManagament.Client.WPF.ViewModel.Messages;

namespace SystemManagament.Client.WPF.ViewModel
{
    public class PreferencesViewModel : ViewModelBase
    {
        private PreferencesWindow preferencesWindow;

        private uint delayBetweenCalls_WindowsProcess;
        private uint delayBetweenCalls_WindowsService;
        private uint delayBetweenCalls_HardwareDynamic;
        private bool dynamicHardwareLogs_Include;
        private string dynamicHardwareLogs_Path;

        public PreferencesViewModel()
        {
            Messenger.Default.Register<ShowPreferencesWindowMessage>(this, this.ShowPreferencesWindowMessageReceived);

            this.SendSavePreferencesMessageCommand = new RelayCommand(this.SendSavePreferencesMessage);
            this.CloseWindowCommand = new RelayCommand<Window>(this.CloseWindow);
        }

        public ICommand SendSavePreferencesMessageCommand { get; private set; }

        public RelayCommand<Window> CloseWindowCommand { get; private set; }

        public uint DelayBetweenCalls_WindowsProcess
        {
            get
            {
                return this.delayBetweenCalls_WindowsProcess;
            }

            set
            {
                this.Set(() => this.DelayBetweenCalls_WindowsProcess, ref this.delayBetweenCalls_WindowsProcess, value);
            }
        }

        public uint DelayBetweenCalls_WindowsService
        {
            get
            {
                return this.delayBetweenCalls_WindowsService;
            }

            set
            {
                this.Set(() => this.DelayBetweenCalls_WindowsService, ref this.delayBetweenCalls_WindowsService, value);
            }
        }

        public uint DelayBetweenCalls_HardwareDynamic
        {
            get
            {
                return this.delayBetweenCalls_HardwareDynamic;
            }

            set
            {
                this.Set(() => this.DelayBetweenCalls_HardwareDynamic, ref this.delayBetweenCalls_HardwareDynamic, value);
            }
        }

        public bool DynamicHardwareLogs_Include
        {
            get
            {
                return this.dynamicHardwareLogs_Include;
            }

            set
            {
                this.Set(() => this.DynamicHardwareLogs_Include, ref this.dynamicHardwareLogs_Include, value);
            }
        }

        public string DynamicHardwareLogs_Path
        {
            get
            {
                return this.dynamicHardwareLogs_Path;
            }

            set
            {
                this.Set(() => this.DynamicHardwareLogs_Path, ref this.dynamicHardwareLogs_Path, value);
            }
        }

        private void ShowPreferencesWindowMessageReceived(ShowPreferencesWindowMessage message)
        {
            this.DelayBetweenCalls_HardwareDynamic = message.UpdatePreferencesMessage.DelayBetweenCalls_HardwareDynamic;
            this.DelayBetweenCalls_WindowsProcess = message.UpdatePreferencesMessage.DelayBetweenCalls_WindowsProcess;
            this.DelayBetweenCalls_WindowsService = message.UpdatePreferencesMessage.DelayBetweenCalls_WindowsService;
            this.DynamicHardwareLogs_Include = message.UpdatePreferencesMessage.DynamicHardwareLogs_Include;
            this.DynamicHardwareLogs_Path = message.UpdatePreferencesMessage.DynamicHardwareLogs_Path;

            this.preferencesWindow = new PreferencesWindow();
            this.preferencesWindow.DataContext = this;
            this.preferencesWindow.ShowDialog();
            this.preferencesWindow.Focus();
        }

        private void SendSavePreferencesMessage()
        {
            Messenger.Default.Send(new UpdatePreferencesMessage()
            {
                DelayBetweenCalls_WindowsProcess = this.DelayBetweenCalls_WindowsProcess,
                DelayBetweenCalls_WindowsService = this.DelayBetweenCalls_WindowsService,
                DelayBetweenCalls_HardwareDynamic = this.DelayBetweenCalls_HardwareDynamic,
                DynamicHardwareLogs_Include = this.DynamicHardwareLogs_Include,
                DynamicHardwareLogs_Path = this.DynamicHardwareLogs_Path,
            });

            this.preferencesWindow.Close();
        }

        private void CloseWindow(Window window)
        {
            if (window != null)
            {
                window.Close();
            }
        }
    }
}
