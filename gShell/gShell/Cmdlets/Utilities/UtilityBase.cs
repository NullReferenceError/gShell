﻿using System.Collections.Generic;
using System.Management.Automation;
using gShell.Cmdlets.Utilities.OAuth2;
using Google.Apis.Auth.OAuth2;
using gShell.dotNet.Utilities.OAuth2;

namespace gShell.Cmdlets.Utilities
{
    public abstract class UtilityBase : OAuth2CmdletBase
    {
        protected override void BeginProcessing() { }

        protected override AuthenticatedUserInfo Authenticate(IEnumerable<string> Scopes, ClientSecrets Secrets, string Domain=null) { return null; }

        protected override string apiNameAndVersion { get { return "gShellUtils"; } }
    }
}
