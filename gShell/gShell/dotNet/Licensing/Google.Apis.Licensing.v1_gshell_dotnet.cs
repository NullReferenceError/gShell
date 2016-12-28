// gShell is licensed under the GNU GENERAL PUBLIC LICENSE, Version 3
//
// http://www.gnu.org/licenses/gpl-3.0.en.html
//
// Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on
// an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the License for the
// specific language governing permissions and limitations under the License.
//
// gShell is based upon https://github.com/google/google-api-dotnet-client, which is licensed under the Apache 2.0
// license: https://github.com/google/google-api-dotnet-client/blob/master/LICENSE
//
// gShell is reliant upon the Google Apis. Please see the specific API pages for specific licensing information.

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a fork of google-apis-code-generator:
//       https://github.com/squid808/apis-client-generator
//
//     How neat is that? Pretty neat.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using gShell.Cmdlets.Utilities.OAuth2;
using gShell.dotNet;

namespace gShell.Cmdlets.Licensing{

    using System;
    using System.Collections.Generic;
    using System.Management.Automation;

    using Google.Apis.Auth.OAuth2;
    using Google.Apis.Services;
    using v1 = Google.Apis.Licensing.v1;
    using Data = Google.Apis.Licensing.v1.Data;

    using gShell.dotNet.Utilities;
    using gShell.dotNet.Utilities.OAuth2;
    using gLicensing = gShell.dotNet.Licensing;

    /// <summary>
    /// A PowerShell-ready wrapper for the Licensing api, as well as the resources and methods therein.
    /// </summary>
    public abstract class LicensingBase : StandardParamsCmdletBase
    {

        #region Properties

        /// <summary>The gShell dotNet class wrapper base.</summary>
        protected static gLicensing mainBase { get; set; }


        /// <summary>An instance of the LicenseAssignments gShell dotNet resource.</summary>
        public LicenseAssignments licenseAssignments { get; set; }

        /// <summary>
        /// Required to be able to store and retrieve the mainBase from the ServiceWrapperDictionary
        /// </summary>
        protected override Type mainBaseType { get { return typeof(gLicensing); } }
        #endregion

        #region Constructors
        protected LicensingBase()
        {
            mainBase = new gLicensing();

            ServiceWrapperDictionary[mainBaseType] = mainBase;


            licenseAssignments = new LicenseAssignments();
        }
        #endregion

        #region Wrapped Methods



        #region LicenseAssignments

        /// <summary>A wrapper class for the LicenseAssignments resource.</summary>
        public class LicenseAssignments
        {




            /// <summary>Revoke License.</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param
            /// name="UserId">email id or unique Id of the user</param>
            public void Delete (string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                mainBase.licenseAssignments.Delete(ProductId, SkuId, UserId, StandardQueryParams);
            }



            /// <summary>Get license assignment of a particular product and sku for a user</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param
            /// name="UserId">email id or unique Id of the user</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Get (string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                return mainBase.licenseAssignments.Get(ProductId, SkuId, UserId, StandardQueryParams);
            }



            /// <summary>Assign License.</summary>
            /// <param name="LicenseAssignmentInsertBody">The body of the request.</param>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Insert (Google.Apis.Licensing.v1.Data.LicenseAssignmentInsert LicenseAssignmentInsertBody, string ProductId, string SkuId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                return mainBase.licenseAssignments.Insert(LicenseAssignmentInsertBody, ProductId, SkuId, StandardQueryParams);
            }



            /// <summary>List license assignments for given product of the customer.</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="CustomerId">CustomerId represents the customer
            /// for whom licenseassignments are queried</param>
            /// <param name="properties">The optional properties for this method.</param>

            public List<Google.Apis.Licensing.v1.Data.LicenseAssignmentList> ListForProduct(string ProductId, string CustomerId, gLicensing.LicenseAssignments.LicenseAssignmentsListForProductProperties properties= null, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                properties = properties ?? new gLicensing.LicenseAssignments.LicenseAssignmentsListForProductProperties();
                properties.StartProgressBar = StartProgressBar;
                properties.UpdateProgressBar = UpdateProgressBar;

                return mainBase.licenseAssignments.ListForProduct(ProductId, CustomerId, properties);
            }

            /// <summary>List license assignments for given product and sku of the customer.</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param
            /// name="CustomerId">CustomerId represents the customer for whom licenseassignments are queried</param>
            /// <param name="properties">The optional properties for this method.</param>

            public List<Google.Apis.Licensing.v1.Data.LicenseAssignmentList> ListForProductAndSku(string ProductId, string SkuId, string CustomerId, gLicensing.LicenseAssignments.LicenseAssignmentsListForProductAndSkuProperties properties= null, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                properties = properties ?? new gLicensing.LicenseAssignments.LicenseAssignmentsListForProductAndSkuProperties();
                properties.StartProgressBar = StartProgressBar;
                properties.UpdateProgressBar = UpdateProgressBar;

                return mainBase.licenseAssignments.ListForProductAndSku(ProductId, SkuId, CustomerId, properties);
            }

            /// <summary>Assign License. This method supports patch semantics.</summary>
            /// <param name="LicenseAssignmentBody">The body of the request.</param>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku for which license would be
            /// revoked</param>
            /// <param name="UserId">email id or unique Id of the user</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Patch (Google.Apis.Licensing.v1.Data.LicenseAssignment LicenseAssignmentBody, string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                return mainBase.licenseAssignments.Patch(LicenseAssignmentBody, ProductId, SkuId, UserId, StandardQueryParams);
            }



            /// <summary>Assign License.</summary>
            /// <param name="LicenseAssignmentBody">The body of the request.</param>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku for which license would be
            /// revoked</param>
            /// <param name="UserId">email id or unique Id of the user</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Update (Google.Apis.Licensing.v1.Data.LicenseAssignment LicenseAssignmentBody, string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {

                return mainBase.licenseAssignments.Update(LicenseAssignmentBody, ProductId, SkuId, UserId, StandardQueryParams);
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

    using v1 = Google.Apis.Licensing.v1;
    using Data = Google.Apis.Licensing.v1.Data;

    /// <summary>The dotNet gShell version of the licensing api.</summary>
    public class Licensing : ServiceWrapper<v1.LicensingService>, IServiceWrapper<Google.Apis.Services.IClientService>
    {

        protected override bool worksWithGmail { get { return true; } }

        /// <summary>Creates a new v1.Licensing service.</summary>
        /// <param name="domain">The domain to which this service will be authenticated.</param>
        /// <param name="authInfo">The authenticated AuthInfo for this user and domain.</param>
        /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>

        protected override v1.LicensingService CreateNewService(string domain, AuthenticatedUserInfo authInfo, string gShellServiceAccount = null)
        {
            return new v1.LicensingService(OAuth2Base.GetInitializer(domain, authInfo));
        }

        /// <summary>Returns the api name and version in {name}:{version} format.</summary>
        public override string apiNameAndVersion { get { return "licensing:v1"; } }


        /// <summary>Gets or sets the licenseAssignments resource class.</summary>
        public LicenseAssignments licenseAssignments{ get; set; }

        public Licensing()
        {

            licenseAssignments = new LicenseAssignments();
        }



        /// <summary>The "licenseAssignments" collection of methods.</summary>
        public class LicenseAssignments
        {

            /// <summary>Optional parameters for the LicenseAssignments ListForProduct method.</summary>
            public class LicenseAssignmentsListForProductProperties
            {
                /// <summary>Maximum number of campaigns to return at one time. Must be positive. Optional. Default value is 100.</summary>
                public int? MaxResults = 1000;

                /// <summary>A delegate that is used to start a progress bar.</summary>
                public Action<string, string> StartProgressBar = null;

                /// <summary>A delegate that is used to update a progress bar.</summary>
                public Action<int, int, string, string> UpdateProgressBar = null;

                /// <summary>A counter for the total number of results to pull when iterating through paged results.</summary>
                public int TotalResults = 0;
            }

            /// <summary>Optional parameters for the LicenseAssignments ListForProductAndSku method.</summary>
            public class LicenseAssignmentsListForProductAndSkuProperties
            {
                /// <summary>Maximum number of campaigns to return at one time. Must be positive. Optional. Default value is 100.</summary>
                public int? MaxResults = 1000;

                /// <summary>A delegate that is used to start a progress bar.</summary>
                public Action<string, string> StartProgressBar = null;

                /// <summary>A delegate that is used to update a progress bar.</summary>
                public Action<int, int, string, string> UpdateProgressBar = null;

                /// <summary>A counter for the total number of results to pull when iterating through paged results.</summary>
                public int TotalResults = 0;
            }


            /// <summary>Revoke License.</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param
            /// name="UserId">email id or unique Id of the user</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public void Delete (string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var request = GetService().LicenseAssignments.Delete(ProductId, SkuId, UserId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }



                request.Execute();
            }

            /// <summary>Get license assignment of a particular product and sku for a user</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param
            /// name="UserId">email id or unique Id of the user</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Get (string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var request = GetService().LicenseAssignments.Get(ProductId, SkuId, UserId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }



                return request.Execute();
            }

            /// <summary>Assign License.</summary>
            /// <param name="LicenseAssignmentInsertBody">The body of the request.</param>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Insert (Google.Apis.Licensing.v1.Data.LicenseAssignmentInsert LicenseAssignmentInsertBody, string ProductId, string SkuId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var request = GetService().LicenseAssignments.Insert(LicenseAssignmentInsertBody, ProductId, SkuId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }



                return request.Execute();
            }

            /// <summary>List license assignments for given product of the customer.</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="CustomerId">CustomerId represents the customer
            /// for whom licenseassignments are queried</param>
            /// <param name="properties">The optional properties for this method.</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public List<Google.Apis.Licensing.v1.Data.LicenseAssignmentList> ListForProduct(
                string ProductId, string CustomerId, LicenseAssignmentsListForProductProperties properties= null, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var results = new List<Google.Apis.Licensing.v1.Data.LicenseAssignmentList>();

                v1.LicenseAssignmentsResource.ListForProductRequest request = GetService().LicenseAssignments.ListForProduct(ProductId, CustomerId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }

                if (properties != null)
                {
                    request.MaxResults = properties.MaxResults;

                }

                if (null != properties.StartProgressBar)
                {
                    properties.StartProgressBar("Gathering LicenseAssignments",
                        string.Format("-Collecting LicenseAssignments page 1"));
                }

                Google.Apis.Licensing.v1.Data.LicenseAssignmentList pagedResult = request.Execute();

                if (pagedResult != null)
                {
                    results.Add(pagedResult);

                    while (!string.IsNullOrWhiteSpace(pagedResult.NextPageToken) &&
                        pagedResult.NextPageToken != request.PageToken &&
                    (properties.TotalResults == 0 || results.Count < properties.TotalResults))
                    {
                        request.PageToken = pagedResult.NextPageToken;

                        if (null != properties.UpdateProgressBar)
                        {
                            properties.UpdateProgressBar(5, 10, "Gathering LicenseAssignments",
                                    string.Format("-Collecting LicenseAssignments page {0}",
                                        (results.Count + 1).ToString()));
                        }
                        pagedResult = request.Execute();
                        results.Add(pagedResult);
                    }

                    if (null != properties.UpdateProgressBar)
                    {
                        properties.UpdateProgressBar(1, 2, "Gathering LicenseAssignments",
                                string.Format("-Returning {0} pages.", results.Count.ToString()));
                    }
                }

                return results;
            }

            /// <summary>List license assignments for given product and sku of the customer.</summary>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku</param>
            /// <param
            /// name="CustomerId">CustomerId represents the customer for whom licenseassignments are queried</param>
            /// <param name="properties">The optional properties for this method.</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public List<Google.Apis.Licensing.v1.Data.LicenseAssignmentList> ListForProductAndSku(
                string ProductId, string SkuId, string CustomerId, LicenseAssignmentsListForProductAndSkuProperties properties= null, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var results = new List<Google.Apis.Licensing.v1.Data.LicenseAssignmentList>();

                v1.LicenseAssignmentsResource.ListForProductAndSkuRequest request = GetService().LicenseAssignments.ListForProductAndSku(ProductId, SkuId, CustomerId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }

                if (properties != null)
                {
                    request.MaxResults = properties.MaxResults;

                }

                if (null != properties.StartProgressBar)
                {
                    properties.StartProgressBar("Gathering LicenseAssignments",
                        string.Format("-Collecting LicenseAssignments page 1"));
                }

                Google.Apis.Licensing.v1.Data.LicenseAssignmentList pagedResult = request.Execute();

                if (pagedResult != null)
                {
                    results.Add(pagedResult);

                    while (!string.IsNullOrWhiteSpace(pagedResult.NextPageToken) &&
                        pagedResult.NextPageToken != request.PageToken &&
                    (properties.TotalResults == 0 || results.Count < properties.TotalResults))
                    {
                        request.PageToken = pagedResult.NextPageToken;

                        if (null != properties.UpdateProgressBar)
                        {
                            properties.UpdateProgressBar(5, 10, "Gathering LicenseAssignments",
                                    string.Format("-Collecting LicenseAssignments page {0}",
                                        (results.Count + 1).ToString()));
                        }
                        pagedResult = request.Execute();
                        results.Add(pagedResult);
                    }

                    if (null != properties.UpdateProgressBar)
                    {
                        properties.UpdateProgressBar(1, 2, "Gathering LicenseAssignments",
                                string.Format("-Returning {0} pages.", results.Count.ToString()));
                    }
                }

                return results;
            }

            /// <summary>Assign License. This method supports patch semantics.</summary>
            /// <param name="LicenseAssignmentBody">The body of the request.</param>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku for which license would be
            /// revoked</param>
            /// <param name="UserId">email id or unique Id of the user</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Patch (Google.Apis.Licensing.v1.Data.LicenseAssignment LicenseAssignmentBody, string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var request = GetService().LicenseAssignments.Patch(LicenseAssignmentBody, ProductId, SkuId, UserId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }



                return request.Execute();
            }

            /// <summary>Assign License.</summary>
            /// <param name="LicenseAssignmentBody">The body of the request.</param>
            /// <param name="ProductId">Name for product</param>
            /// <param name="SkuId">Name for sku for which license would be
            /// revoked</param>
            /// <param name="UserId">email id or unique Id of the user</param>
            /// <param name="gShellServiceAccount">The optional email address the service account should impersonate.</param>
            public Google.Apis.Licensing.v1.Data.LicenseAssignment Update (Google.Apis.Licensing.v1.Data.LicenseAssignment LicenseAssignmentBody, string ProductId, string SkuId, string UserId, gShell.dotNet.Utilities.OAuth2.StandardQueryParameters StandardQueryParams = null)
            {
                var request = GetService().LicenseAssignments.Update(LicenseAssignmentBody, ProductId, SkuId, UserId);

                if (StandardQueryParams != null) {
                    request.Fields = StandardQueryParams.fields;
                    request.QuotaUser = StandardQueryParams.quotaUser;
                    request.UserIp = StandardQueryParams.userIp;
                }



                return request.Execute();
            }

        }

    }
}