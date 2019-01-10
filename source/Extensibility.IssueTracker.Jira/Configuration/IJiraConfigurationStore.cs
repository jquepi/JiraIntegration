﻿using System;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Configuration;

namespace Octopus.Server.Extensibility.IssueTracker.Jira.Configuration
{
    public interface IJiraConfigurationStore : IExtensionConfigurationStore<JiraConfiguration>
    {
        string GetBaseUrl();
        void SetBaseUrl(string baseUrl);
        string GetPassword();
        void SetPassword(string password);
        string GetConnectAppUrl();
        void SetConnectAppUrl(string url);
    }
}
