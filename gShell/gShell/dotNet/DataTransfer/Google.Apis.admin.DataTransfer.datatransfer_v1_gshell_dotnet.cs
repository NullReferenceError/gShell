namespace gShell.Cmdlets.DataTransfer{

    using System;
    using System.Collections.Generic;
    using System.Management.Automation;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Services;
    using datatransfer_v1 = Google.Apis.admin.DataTransfer.datatransfer_v1;
    using Data = Google.Apis.admin.DataTransfer.datatransfer_v1.Data;

    using gShell.dotNet.Utilities;
    using gShell.dotNet.Utilities.OAuth2;
    using gDataTransfer = gShell.dotNet.DataTransfer;

    public abstract class DataTransferBase : OAuth2CmdletBase
    {

        #region Properties
        [Parameter(Mandatory = false)]
        [ValidateNotNullOrEmpty]
        public string Domain { get; set; }

        protected static gDataTransfer mainBase { get; set; }

        public Applications applications { get; set; }
        public Transfers transfers { get; set; }

        protected override string apiNameAndVersion { get { return mainBase.apiNameAndVersion; } }
        #endregion

        #region Constructors
        public DataTransferBase()
        {
            mainBase = new gDataTransfer();

            applications = new Applications();
            transfers = new Transfers();
        }
        #endregion

        #region PowerShell Methods
        protected override void BeginProcessing()
        {
            var secrets = CheckForClientSecrets();
            if (secrets != null)
            {
                IEnumerable<string> scopes = EnsureScopesExist(Domain);
                Domain = mainBase.BuildService(Authenticate(scopes, secrets, Domain)).domain;

                GWriteProgress = new gWriteProgress(WriteProgress);
            }
            else
            {
                WriteError(new ErrorRecord(null, (new Exception(
                    "Client Secrets must be set before running cmdlets. Run 'Get-Help "
                    + "Set-gShellClientSecrets -online' for more information."))));
            }
        }
        #endregion

        #region Authentication & Processing
        protected override AuthenticatedUserInfo Authenticate(IEnumerable<string> Scopes, ClientSecrets Secrets, string Domain = null)
        {
            return mainBase.Authenticate(apiNameAndVersion, Scopes, Secrets, Domain);
        }
        #endregion

        #region Wrapped Methods



        #region Applications

        public class Applications
        {




            public Google.Apis.admin.DataTransfer.datatransfer_v1.Data.Application Get (
            long

             applicationId)
            {

                return mainBase.applications.Get(
                applicationId);
            }


            public List<Google.Apis.admin.DataTransfer.datatransfer_v1.Data.ApplicationsListResponse> List(
            gDataTransfer.Applications.ApplicationsListProperties properties = null)
            {

                properties = (properties != null) ? properties : new gDataTransfer.Applications.ApplicationsListProperties();
                properties.startProgressBar = StartProgressBar;
                properties.updateProgressBar = UpdateProgressBar;

                return mainBase.applications.List(properties);
            }
        }

        #endregion



        #region Transfers

        public class Transfers
        {




            public Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfer Get (
            string

             dataTransferId)
            {

                return mainBase.transfers.Get(
                dataTransferId);
            }


            public Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfer Insert (
            Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfer body)
            {

                return mainBase.transfers.Insert(
                body);
            }


            public List<Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfersListResponse> List(
            gDataTransfer.Transfers.TransfersListProperties properties = null)
            {

                properties = (properties != null) ? properties : new gDataTransfer.Transfers.TransfersListProperties();
                properties.startProgressBar = StartProgressBar;
                properties.updateProgressBar = UpdateProgressBar;

                return mainBase.transfers.List(properties);
            }
        }

        #endregion

        #endregion

    }
}



namespace gShell.dotNet
{
    using System;
    using System.Collections.Generic;

    using gShell.dotNet;
    using gShell.dotNet.Utilities.OAuth2;

    using datatransfer_v1 = Google.Apis.admin.DataTransfer.datatransfer_v1;
    using Data = Google.Apis.admin.DataTransfer.datatransfer_v1.Data;

    public class DataTransfer : ServiceWrapper<datatransfer_v1.DataTransferService>
    {

        protected override bool worksWithGmail { get { return false; } }

        protected override datatransfer_v1.DataTransferService CreateNewService(string domain)
        {
            return new datatransfer_v1.DataTransferService(OAuth2Base.GetInitializer(domain));
        }

        public override string apiNameAndVersion { get { return "admin:datatransfer_v1"; } }

        public Applications applications{ get; set; }
        public Transfers transfers{ get; set; }

        public DataTransfer() //public Reports()
        {

            applications = new Applications();
            transfers = new Transfers();
        }




        public class Applications
        {



            public class ApplicationsListProperties
            {
                public     string     customerId = null; //test
                public int maxResults = 500;

                public Action<string, string> startProgressBar = null;
                public Action<int, int, string, string> updateProgressBar = null;
                public int totalResults = 0;
            }


            public Google.Apis.admin.DataTransfer.datatransfer_v1.Data.Application Get
            (long applicationId)
            {
                return GetService().Applications.Get(    applicationId).Execute();
            }

            public List<Google.Apis.admin.DataTransfer.datatransfer_v1.Data.ApplicationsListResponse> List(
                ApplicationsListProperties properties = null)
            {
                var results = new List<Google.Apis.admin.DataTransfer.datatransfer_v1.Data.ApplicationsListResponse>();

                datatransfer_v1.ApplicationsResource.ListRequest request = GetService().Applications.List(
            );

                if (properties != null)
                {
                    request.CustomerId = properties.customerId;
                    request.MaxResults = properties.maxResults;

                }

                if (null != properties.startProgressBar)
                {
                    properties.startProgressBar("Gathering Applications",
                        string.Format("-Collecting Applications 1 to {0}", request.MaxResults.ToString()));
                }

                Google.Apis.admin.DataTransfer.datatransfer_v1.Data.ApplicationsListResponse pagedResult = request.Execute();

                if (pagedResult != null)
                {
                    results.Add(pagedResult);

                    while (!string.IsNullOrWhiteSpace(pagedResult.NextPageToken) &&
                        pagedResult.NextPageToken != request.PageToken &&
                    (properties.totalResults == 0 || results.Count < properties.totalResults))
                    {
                        request.PageToken = pagedResult.NextPageToken;

                        if (null != properties.updateProgressBar)
                        {
                            properties.updateProgressBar(5, 10, "Gathering Applications",
                                    string.Format("-Collecting Applications {0} to {1}",
                                        (results.Count + 1).ToString(),
                                        (results.Count + request.MaxResults).ToString()));
                        }
                        pagedResult = request.Execute();
                        results.Add(pagedResult);
                    }

                    if (null != properties.updateProgressBar)
                    {
                        properties.updateProgressBar(1, 2, "Gathering Applications",
                                string.Format("-Returning {0} results.", results.Count.ToString()));
                    }
                }

                return results;
            }

        }


        public class Transfers
        {



            public class TransfersListProperties
            {
                public     string     customerId = null; //test
                public int maxResults = 500;
                public     string     newOwnerUserId = null; //test
                public     string     oldOwnerUserId = null; //test

                public     string     status = null; //test
                public Action<string, string> startProgressBar = null;
                public Action<int, int, string, string> updateProgressBar = null;
                public int totalResults = 0;
            }


            public Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfer Get
            (string dataTransferId)
            {
                return GetService().Transfers.Get(    dataTransferId).Execute();
            }

            public Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfer Insert
            (Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfer body)
            {
                return GetService().Transfers.Insert(    body).Execute();
            }

            public List<Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfersListResponse> List(
                TransfersListProperties properties = null)
            {
                var results = new List<Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfersListResponse>();

                datatransfer_v1.TransfersResource.ListRequest request = GetService().Transfers.List(
            );

                if (properties != null)
                {
                    request.CustomerId = properties.customerId;
                    request.MaxResults = properties.maxResults;
                    request.NewOwnerUserId = properties.newOwnerUserId;
                    request.OldOwnerUserId = properties.oldOwnerUserId;
                    request.Status = properties.status;

                }

                if (null != properties.startProgressBar)
                {
                    properties.startProgressBar("Gathering Transfers",
                        string.Format("-Collecting Transfers 1 to {0}", request.MaxResults.ToString()));
                }

                Google.Apis.admin.DataTransfer.datatransfer_v1.Data.DataTransfersListResponse pagedResult = request.Execute();

                if (pagedResult != null)
                {
                    results.Add(pagedResult);

                    while (!string.IsNullOrWhiteSpace(pagedResult.NextPageToken) &&
                        pagedResult.NextPageToken != request.PageToken &&
                    (properties.totalResults == 0 || results.Count < properties.totalResults))
                    {
                        request.PageToken = pagedResult.NextPageToken;

                        if (null != properties.updateProgressBar)
                        {
                            properties.updateProgressBar(5, 10, "Gathering Transfers",
                                    string.Format("-Collecting Transfers {0} to {1}",
                                        (results.Count + 1).ToString(),
                                        (results.Count + request.MaxResults).ToString()));
                        }
                        pagedResult = request.Execute();
                        results.Add(pagedResult);
                    }

                    if (null != properties.updateProgressBar)
                    {
                        properties.updateProgressBar(1, 2, "Gathering Transfers",
                                string.Format("-Returning {0} results.", results.Count.ToString()));
                    }
                }

                return results;
            }

        }

    }
}