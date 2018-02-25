﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SystemManagament.Monitor.HardwareStatic;
using SystemManagament.Monitor.SoftwareStatic.Model.Components.Interface;
using SystemManagament.Monitor.SoftwareStatic.Provider;

namespace SystemManagament.Monitor.SoftwareStatic.Model.Components
{
    // https://support.microsoft.com/en-us/help/243330/well-known-security-identifiers-in-windows-operating-systems
    public class CurrentUser : ISoftwareStaticComponent<CurrentUser>
    {
        public string Name { get; internal set; }

        public string AuthenticationType { get; internal set; }

        public IEnumerable<Claim> Claims { get; internal set; }

        public IdentityReferenceCollection Groups { get; internal set; }

        public List<CurrentUser> GetStaticDataForSoftwareComponent(ISoftwareStaticProvider softwareStaticProvider)
        {
            CurrentUser currentUser = softwareStaticProvider.GetCurrentUser();

            List<CurrentUser> currentUserList = new List<CurrentUser>();
            currentUserList.Add(currentUser);

            return currentUserList;
        }
    }
}