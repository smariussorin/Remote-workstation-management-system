﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using SystemMonitor.DataBuilder;
using SystemMonitor.HardwareDynamic.Builder;
using SystemMonitor.HardwareDynamic.Model;
using SystemMonitor.HardwareDynamic.OHMProvider;
using SystemMonitor.HardwareStatic.Analyzer;
using SystemMonitor.HardwareStatic.Builder;
using SystemMonitor.HardwareStatic.Model;
using SystemMonitor.NLogger;
using SystemMonitor.Shared.Win32API;
using SystemMonitor.Shared.WMI;
using SystemMonitor.SoftwareDynamic.Builder;
using SystemMonitor.SoftwareDynamic.Model;
using SystemMonitor.SoftwareDynamic.Provider;
using SystemMonitor.SoftwareStatic.Builder;
using SystemMonitor.SoftwareStatic.Model;
using SystemMonitor.SoftwareStatic.Provider;

namespace HardwareMonitor
{
    public class WorkstationMonitor
    {
        public WorkstationMonitor()
        {
            this.InitializeIoCContainer();
        }

        private ISystemMonitorDataBuilder SystemMonitorDataBuilder { get; set; }

        public HardwareStaticData GetHardwareStaticData()
        {
            return this.SystemMonitorDataBuilder.GetHardwareStaticData();
        }

        public HardwareDynamicData GetHardwareDynamicData()
        {
            return this.SystemMonitorDataBuilder.GetHardwareDynamicData();
        }

        public SoftwareStaticData GetSoftwareStaticData()
        {
            return this.SystemMonitorDataBuilder.GetSoftwareStaticData();
        }

        public SoftwareDynamicData GetSoftwareDynamicData()
        {
            return this.SystemMonitorDataBuilder.GetSoftwareDynamicData();
        }

        private void InitializeIoCContainer()
        {
            var container = new WindsorContainer();
            container.Register(Component.For<INLogger>().ImplementedBy<NLogger>().LifeStyle.Singleton);
            container.Register(Component.For<ISystemMonitorDataBuilder>().ImplementedBy<SystemMonitorDataBuilder>().LifeStyle.Singleton);
            container.Register(Component.For<IHardwareStaticBuilder>().ImplementedBy<HardwareStaticBuilder>().LifeStyle.Singleton);
            container.Register(Component.For<IHardwareStaticAnalyzer>().ImplementedBy<HardwareStaticAnalyzer>().LifeStyle.Singleton);
            container.Register(Component.For<IWMIClient>().ImplementedBy<WMIClient>().LifeStyle.Singleton);
            container.Register(Component.For<IOHMProvider>().ImplementedBy<OHMProvider>().LifeStyle.Singleton);
            container.Register(Component.For<IHardwareDynamicBuilder>().ImplementedBy<HardwareDynamicBuilder>().LifeStyle.Singleton);
            container.Register(Component.For<ISoftwareStaticProvider>().ImplementedBy<SoftwareStaticProvider>().LifeStyle.Singleton);
            container.Register(Component.For<ISoftwareStaticBuilder>().ImplementedBy<SoftwareStaticBuilder>().LifeStyle.Singleton);
            container.Register(Component.For<ISoftwareDynamicProvider>().ImplementedBy<SoftwareDynamicProvider>().LifeStyle.Singleton);
            container.Register(Component.For<ISoftwareDynamicBuilder>().ImplementedBy<SoftwareDynamicBuilder>().LifeStyle.Singleton);
            container.Register(Component.For<IWin32APIClient>().ImplementedBy<Win32APIClient>().LifeStyle.Singleton);

            this.SystemMonitorDataBuilder = container.Resolve<ISystemMonitorDataBuilder>();
        }
    }
}
