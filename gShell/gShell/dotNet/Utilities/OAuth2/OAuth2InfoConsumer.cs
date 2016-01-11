﻿using System;
using System.IO;
using System.Collections.Generic;

using Google.Apis.Auth.OAuth2;
using gShell.dotNet.Utilities.OAuth2.DataStores;
using gShell.dotNet.Utilities.Settings;

namespace gShell.dotNet.Utilities.OAuth2
{
    /// <summary>
    /// Maintains a copy of the OAuth2 Info in memory and acts as a mediator for saving and loading information to and 
    /// from the OAuth2 Info.
    /// </summary>
    public class OAuth2InfoConsumer
    {
        #region Properties

        public gShellSettings settings { get; set; }

        public static string dataStoreLocation
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(
                    Environment.SpecialFolder.LocalApplicationData), @"gShell\");
            }
        }

        /// <summary>The data store responsible for saving and loading the OAuth2 information.</summary>
        private IOAuth2DataStore dataStore { get { return _dataStore; } }
        private readonly IOAuth2DataStore _dataStore;

        /// <summary>An in-memory copy of the stored OAuth2 information.</summary>
        private OAuth2Info info;

        #endregion

        #region Constructors

        public OAuth2InfoConsumer()
        {
            settings = gShellSettingsLoader.Load();

            if (settings != null && settings.SerializeType == gShellSettings.SerializeTypes.Json)
            {
                _dataStore = new OAuth2JsonDataStore(dataStoreLocation);
            }
            else
            {
                _dataStore = new OAuth2BinDataStore(dataStoreLocation);
            }

            info = dataStore.LoadInfo();
            if (info == null)
            {
                info = new OAuth2Info();
                dataStore.SaveInfo(info);
            }
        }

        public OAuth2InfoConsumer(IOAuth2DataStore DataStore)
        {
            _dataStore = DataStore;
            info = dataStore.LoadInfo();
            if (info == null)
            {
                info = new OAuth2Info();
                dataStore.SaveInfo(info);
            }
        }

        #endregion

        #region Domains

        /// <summary>Returns the Domain if it exists.</summary>
        public OAuth2Domain GetDomain(string Domain)
        {
            if (DomainExists(Domain))
            {
                return info.domains[Domain];
            }
            else
            {
                return null;
            }
        }

        /// <summary>Return all Domains.</summary>
        public IEnumerable<OAuth2Domain> GetAllDomains()
        {
            return info.domains.Values;
        }

        /// <summary>Creates and/or overwrites the domain provided.</summary>
        public void SetDomain(OAuth2Domain Domain)
        {
            if (Domain != null && !string.IsNullOrWhiteSpace(Domain.domain))
            {
                info.domains[Domain.domain] = Domain;
            }

            dataStore.SaveInfo(info);
        }

        /// <summary>Return false if the domain string is blank, if the info or the info.domains are null.</summary>
        public bool DomainExists(string Domain)
        {
            return info.domains.ContainsKey(Domain);
        }

        /// <summary>Removes the given domain.</summary>
        public void RemoveDomain(string Domain)
        {
            info.domains.Remove(Domain);

            dataStore.SaveInfo(info);
        }

        /// <summary>Removes all domains.</summary>
        public void RemoveAllDomains()
        {
            info.domains = new Dictionary<string, OAuth2Domain>();

            info.defaultDomain = null;

            dataStore.SaveInfo(info);
        }

        #endregion

        #region DefaultDomain

        public string GetDefaultDomain()
        {
            return info.defaultDomain;
        }

        public void SetDefaultDomain(string Domain)
        {
            info.defaultDomain = Domain;
            dataStore.SaveInfo(info);
        }

        public bool DefaultDomainExists()
        {
            return !string.IsNullOrWhiteSpace(info.defaultDomain);
        }

        public void RemoveDefaultDomain()
        {
            info.defaultDomain = string.Empty;
            dataStore.SaveInfo(info);
        }

        #endregion

        #region Users

        /// <summary>Returns the DomainUser if both it and the domain exist, and uses the default user if user is blank.</summary>
        public OAuth2DomainUser GetUser(string Domain, string UserName)
        {
            if (UserExists(Domain, UserName))
            {
                return info.domains[Domain].users[UserName];
            }
            else
            {
                return null;
            }
        }

        public IEnumerable<OAuth2DomainUser> GetAllUsers(string Domain = null)
        {
            List<OAuth2DomainUser> users = new List<OAuth2DomainUser>();

            List<string> domains = new List<string>();

            if (Domain != null)
            {
                domains.Add(Domain);
            }
            else
            {
                foreach (var domain in info.domains.Keys)
                {
                    domains.Add(domain);
                }
            }

            foreach (var domain in domains)
            {
                foreach (string userName in info.domains[domain].users.Keys)
                {
                    users.Add(info.domains[domain].users[userName]);
                }
            }

            return users;
        }

        /// <summary>Summary.</summary>
        public void SetUser(string Domain, OAuth2DomainUser User)
        {
            if (User != null)
            {
                if (DomainExists(Domain))
                {
                    info.domains[Domain].users[User.userName] = User;
                    dataStore.SaveInfo(info);
                }
                else
                {
                    OAuth2Domain domain = new OAuth2Domain() { domain = Domain };
                    domain.defaultUser = User.userName;
                    domain.users.Add(User.userName, User);
                    SetDomain(domain);

                    if (!DefaultDomainExists())
                    {
                        SetDefaultDomain(Domain);
                    }
                }
            }
        }

        /// <summary>Summary.</summary>
        public bool UserExists(string Domain, string UserName)
        {
            OAuth2Domain domain = GetDomain(Domain);

            if (domain == null) return false;

            return domain.users.ContainsKey(UserName);
        }

        /// <summary>Summary.</summary>
        public void RemoveUser(string Domain, string UserName)
        {

            info.domains[Domain].users.Remove(UserName);

            if (info.domains[Domain].defaultUser == UserName)
            {
                info.domains[Domain].defaultUser = null;
            }

            dataStore.SaveInfo(info);
        }

        #endregion

        #region Token and Scope

        public OAuth2TokenInfo GetTokenInfo(string Domain, string UserName, string Api)
        {
            if (TokenAndScopesExist(Domain, UserName, Api))
            {
                return info.domains[Domain].users[UserName].tokenAndScopesByApi[Api];
            }
            else
            {
                return null;
            }
        }

        public void SetTokenAndScopes(string Domain, string UserName, string Api, string TokenString, Google.Apis.Auth.OAuth2.Responses.TokenResponse TokenResponse, List<string> Scopes)
        {
            if (!UserExists(Domain, UserName))
            {
                SetUser(Domain, new OAuth2DomainUser());
            }

            info.domains[Domain].users[UserName].tokenAndScopesByApi[Api] = new OAuth2TokenInfo(Scopes, TokenString, TokenResponse);

            dataStore.SaveInfo(info);
        }

        public bool TokenAndScopesExist(string Domain, string UserName, string Api)
        {
            if (DomainExists(Domain) && UserExists(Domain, UserName))
            {
                return (info.domains[Domain].users[UserName].tokenAndScopesByApi.ContainsKey(Api));
            }
            else
            {
                return false;
            }
        }

        public void RemoveTokenAndScopes(string Domain, string UserName, string Api)
        {
            if (TokenAndScopesExist(Domain, UserName, Api))
            {
                info.domains[Domain].users[UserName].tokenAndScopesByApi.Remove(Api);
            }

            dataStore.SaveInfo(info);
        }

        #endregion

        #region DefaultUser

        public string GetDefaultUser(string Domain)
        {
            if (DomainExists(Domain))
            {
                return info.domains[Domain].defaultUser;
            }

            return null;
        }

        public void SetDefaultUser(string Domain, string UserName)
        {
            info.domains[Domain].defaultUser = UserName;

            dataStore.SaveInfo(info);
        }

        public bool DefaultUserExists(string Domain)
        {
            if (DomainExists(Domain) && !string.IsNullOrWhiteSpace(info.domains[Domain].defaultUser))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveDefaultUser(string Domain)
        {
            if (DomainExists(Domain)) {
                info.domains[Domain].defaultUser = string.Empty;
            }
        }

        #endregion

        #region ClientSecrets

        /// <summary>Summary.</summary>
        public ClientSecrets GetClientSecrets(string Domain, string UserName)
        {
            if (UserExists(Domain, UserName))
            {
                return info.domains[Domain].users[UserName].clientSecrets;
            }
            else
            {
                return null;
            }
        }

        /// <summary>Sets the client secrets for the given user.</summary>
        public void SetClientSecrets(string Domain, string UserName, ClientSecrets Secrets)
        {
            if (!UserExists(Domain, UserName))
            {
                OAuth2DomainUser user = new OAuth2DomainUser();
                user.clientSecrets = Secrets;
                //create the domain too
                SetUser(Domain, user);
            }
            else
            {
                info.domains[Domain].users[UserName].clientSecrets = Secrets;

                dataStore.SaveInfo(info);
            }
        }

        /// <summary>Do the client secrets exist for the given user.</summary>
        public bool ClientSecretsExist(string Domain, string UserName)
        {
            if (UserExists(Domain, UserName))
            {
                return info.domains[Domain].users[UserName].clientSecrets != null;
            }
            else
            {
                return false;
            }
        }

        /// <summary>Removes the client secrets for the user.</summary>
        public void RemoveClientSecrets(string Domain, string UserName)
        {
            if (ClientSecretsExist(Domain, UserName))
            {
                info.domains[Domain].users[UserName].clientSecrets = null;
            }

            dataStore.SaveInfo(info);
        }

        #endregion

        #region DefaultClientSecrets

        public ClientSecrets GetDefaultClientSecrets()
        {
            return info.defaultClientSecrets;
        }

        public void SetDefaultClientSecrets(ClientSecrets Secrets)
        {
            if (Secrets != null)
            {
                info.defaultClientSecrets = Secrets;
            }

            dataStore.SaveInfo(info);
        }

        public bool DefaultClientSecretsExist()
        {
            return info.defaultClientSecrets != null;
        }

        public void RemoveDefaultClientSecrets()
        {
            info.defaultClientSecrets = null;

            dataStore.SaveInfo(info);
        }

        #endregion

        #region ServiceAccount

        /// <summary>Summary.</summary>
        public void GetServiceAccount()
        {
            throw new NotImplementedException();
        }

        /// <summary>Summary.</summary>
        public void SetServiceAccount()
        {
            throw new NotImplementedException();
        }

        /// <summary>Summary.</summary>
        public void CheckServiceAccount()
        {
            throw new NotImplementedException();
        }

        /// <summary>Summary.</summary>
        public void RemoveServiceAccount()
        {
            throw new NotImplementedException();
        }

        #endregion

    }
}
