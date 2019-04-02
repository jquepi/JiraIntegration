﻿using System.Collections.Generic;
using Octopus.Server.Extensibility.Extensions.Infrastructure.Configuration;
using Octopus.Server.Extensibility.HostServices.Configuration;
using Octopus.Server.Extensibility.HostServices.Licensing;
using Octopus.Server.Extensibility.HostServices.Mapping;

namespace Octopus.Server.Extensibility.IssueTracker.Jira.Configuration
{
    public class JiraConfigurationSettings : ExtensionConfigurationSettings<JiraConfiguration, JiraConfigurationResource, IJiraConfigurationStore>, IJiraConfigurationSettings
    {
        private readonly IInstallationIdProvider installationIdProvider;
        private readonly IServerConfigurationStore serverConfigurationStore;

        public JiraConfigurationSettings(IJiraConfigurationStore configurationDocumentStore, 
            IInstallationIdProvider installationIdProvider,
            IServerConfigurationStore serverConfigurationStore) : base(configurationDocumentStore)
        {
            this.installationIdProvider = installationIdProvider;
            this.serverConfigurationStore = serverConfigurationStore;
        }

        public override string Id => JiraConfigurationStore.SingletonId;

        public override string ConfigurationSetName => "Jira Issue Tracker";

        public override string Description => "Jira Issue Tracker settings";

        public override IEnumerable<IConfigurationValue> GetConfigurationValues()
        {
            var isEnabled = ConfigurationDocumentStore.GetIsEnabled();

            yield return new ConfigurationValue<bool>("Octopus.IssueTracker.JiraIssueTracker", isEnabled, isEnabled, "Is Enabled");
            yield return new ConfigurationValue<string>("Octopus.IssueTracker.JiraBaseUrl", ConfigurationDocumentStore.GetBaseUrl(), isEnabled && !string.IsNullOrWhiteSpace(ConfigurationDocumentStore.GetBaseUrl()), "Jira Base Url");
            yield return new ConfigurationValue<string>("Octopus.IssueTracker.JiraConnectAppPassword", ConfigurationDocumentStore.GetPassword(), isEnabled && !string.IsNullOrWhiteSpace(ConfigurationDocumentStore.GetPassword()), "Jira Connect App Password");
        }

        public override void BuildMappings(IResourceMappingsBuilder builder)
        {
            builder.Map<JiraConfigurationResource, JiraConfiguration>()
                .EnrichResource((model, resource) =>
                {
                    resource.OctopusInstallationId = installationIdProvider.GetInstallationId().ToString();
                    resource.OctopusServerUrl = serverConfigurationStore.GetServerUri();
                });
        }
    }
}