﻿using System;
using Octopus.Server.Extensibility.IssueTracker.Extensions;
using Octopus.Server.Extensibility.IssueTracker.Jira.Configuration;

namespace Octopus.Server.Extensibility.IssueTracker.Jira
{
    public class JiraIssueTracker : IIssueTracker
    {
        readonly IJiraConfigurationStore configurationStore;

        public JiraIssueTracker(IJiraConfigurationStore configurationStore)
        {
            this.configurationStore = configurationStore;
        }

        public string IssueTrackerName => "Jira";
        public bool IsEnabled => configurationStore.GetIsEnabled();
    }
}
