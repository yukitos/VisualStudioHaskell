using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Settings;

namespace Company.VisualStudioHaskell.Options
{
    class OptionsService : IOptionsService
    {
        private const string _optionsKey = "Options";
        private readonly WritableSettingsStore _settingsStore;

        public OptionsService(IServiceProvider serviceProvider)
        {
            var settingsManager = VisualStudioHaskellPackage.GetSettings(serviceProvider);
            _settingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);
        }

        public void SaveString(string name, string category, string value)
        {
            throw new NotImplementedException();
        }

        public string LoadString(string name, string category)
        {
            throw new NotImplementedException();
        }
    }
}
