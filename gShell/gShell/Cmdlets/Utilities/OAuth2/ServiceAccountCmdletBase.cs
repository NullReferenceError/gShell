﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using gShell.dotNet.Utilities.OAuth2;

namespace gShell.Cmdlets.Utilities.OAuth2
{
    public abstract class ServiceAccountCmdletBase : AuthenticatedCmdletBase
    {
        /// <summary>Gets or sets the email account the gShell Service Account should impersonate.</summary>
        protected static string gShellServiceAccount { get; set; }

        #region Properties

        /// <summary>
        /// <para type="description">The email account to be targeted by the service account.</para>
        /// </summary>
        [Alias("ServiceAccountTarget")]
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string TargetUserEmail { get; set; }

        #endregion

        protected override void BeginProcessing()
        {
            if (!string.IsNullOrWhiteSpace(TargetUserEmail))
            {
                gShellServiceAccount = GetFullEmailAddress(TargetUserEmail, Domain);

                if (!OAuth2Base.infoConsumer.ServiceAccountExists(Domain))
                {
                    WriteWarning(String.Format("No service account was found for domain {0}. Please set a service"+
                        " account with Set-GShellServiceAccount, or see https://github.com/squid808/gShell/wiki/Service-Accounts"+
                        " for more information.", Domain));
                }
            }

            //Overwrite this section to work with the service account.
            var secrets = CheckForClientSecrets();
            if (secrets != null)
            {
                IEnumerable<string> scopes = EnsureScopesExist(Domain);
                Domain = mainBase.BuildService(Authenticate(scopes, secrets, Domain), gShellServiceAccount).domain;

                GWriteProgress = new gWriteProgress(WriteProgress);
            }
            else
            {
                WriteError(new ErrorRecord(null, (new Exception(
                    "Client Secrets must be set before running cmdlets. Run 'Get-Help "
                    + "Set-gShellClientSecrets -online' for more information."))));
            }
        }

        /// <summary>The gShell base implementation of the PowerShell EndProcessing method.</summary>
        /// <remarks>We need to reset the service account after every Cmdlet call to prevent the next
        /// Cmdlet from inheriting it as well.</remarks>
        protected override void EndProcessing()
        {
            gShellServiceAccount = string.Empty;
        }

        /// <summary>The gShell base implementation of the PowerShell StopProcessing method.</summary>
        /// <remarks>We need to reset the service account after every Cmdlet call to prevent the next
        /// Cmdlet from inheriting it as well.</remarks>
        protected override void StopProcessing()
        {
            gShellServiceAccount = string.Empty;
        }

    }
}
