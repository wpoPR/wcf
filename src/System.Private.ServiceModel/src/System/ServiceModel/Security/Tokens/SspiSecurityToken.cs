// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.IdentityModel.Tokens;
using System.Collections.ObjectModel;
using System.Security.Principal;
using System.Net;

namespace System.ServiceModel.Security.Tokens
{
    public class SspiSecurityToken : SecurityToken
    {
        private string _id;
        private TokenImpersonationLevel _impersonationLevel;
        private bool _allowNtlm;
        private NetworkCredential _networkCredential;
        private bool _extractGroupsForWindowsAccounts;
        private bool _allowUnauthenticatedCallers = SspiSecurityTokenProvider.DefaultAllowUnauthenticatedCallers;
        private DateTime _effectiveTime;
        private DateTime _expirationTime;

        public SspiSecurityToken(TokenImpersonationLevel impersonationLevel, bool allowNtlm, NetworkCredential networkCredential)
        {
            _impersonationLevel = impersonationLevel;
            _allowNtlm = allowNtlm;
            _networkCredential = SecurityUtils.GetNetworkCredentialsCopy(networkCredential);
            _effectiveTime = DateTime.UtcNow;
            _expirationTime = _effectiveTime.AddHours(10);
        }

        public SspiSecurityToken(NetworkCredential networkCredential, bool extractGroupsForWindowsAccounts, bool allowUnauthenticatedCallers)
        {
            _networkCredential = SecurityUtils.GetNetworkCredentialsCopy(networkCredential);
            _extractGroupsForWindowsAccounts = extractGroupsForWindowsAccounts;
            _allowUnauthenticatedCallers = allowUnauthenticatedCallers;
            _effectiveTime = DateTime.UtcNow;
            _expirationTime = _effectiveTime.AddHours(10);
        }

        public override string Id
        {
            get
            {
                if (_id == null)
                    _id = SecurityUniqueId.Create().Value;
                return _id;
            }
        }

        public override DateTime ValidFrom
        {
            get { return _effectiveTime; }
        }

        public override DateTime ValidTo
        {
            get { return _expirationTime; }
        }

        public bool AllowUnauthenticatedCallers
        {
            get
            {
                return _allowUnauthenticatedCallers;
            }
        }

        public TokenImpersonationLevel ImpersonationLevel
        {
            get
            {
                return _impersonationLevel;
            }
        }

        public bool AllowNtlm
        {
            get
            {
                return _allowNtlm;
            }
        }

        public NetworkCredential NetworkCredential
        {
            get
            {
                return _networkCredential;
            }
        }

        public bool ExtractGroupsForWindowsAccounts
        {
            get
            {
                return _extractGroupsForWindowsAccounts;
            }
        }

        public override ReadOnlyCollection<SecurityKey> SecurityKeys
        {
            get
            {
                return EmptyReadOnlyCollection<SecurityKey>.Instance;
            }
        }
    }
}
