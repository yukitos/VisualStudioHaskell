using Microsoft.Build.Execution;
using Microsoft.VisualStudioTools.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.VisualStudioHaskell.Project
{
    public interface IHaskellProject
    {
        string GetProperty(string name);

        void SetProperty(string name, string value);

        string GetWorkingDirectory();

        string GetStartupFile();

        string ProjectDirectory
        {
            get;
        }

        string ProjectName
        {
            get;
        }

        //IHaskellInterpreterFactory GetInterpreterFactory();

        bool Publish(PublishProjectOptions options);

        string GetUnevaluatedProperty(string name);

        //VsProjectAnalyze GetProjectAnalyzer();

        //event EventHandler ProjectAnalyzerChanged;

        IAsyncCommand FindCommand(string canonicalName);

        ProjectInstance GetMSBuildProjectInstance();

        string ProjectHome { get; }

        string ProjectFile { get; }

        IServiceProvider Site { get; }

        void AddActionOnClose(object key, Action<object> action);

        //event EventHandler<AnalyzerChangingEventArgs> ProjectAnalyzerChanging;

        //IHaskellInterpreterFactory GetInterpreterFactoryOrThrow();
    }
}
