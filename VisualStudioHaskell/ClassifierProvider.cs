using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;

namespace Company.VisualStudioHaskell
{
    [Export(typeof(IClassifierProvider)), ContentType(CoreConstants.ContentType)]
    class ClassifierProvider : IClassifierProvider
    {
        private IContentType _contentType;
        internal readonly IServiceProvider _serviceProvider;

        [ImportingConstructor]
        public ClassifierProvider(IContentTypeRegistryService registryService, [Import(typeof(SVsServiceProvider))]IServiceProvider serviceProvider)
        {
            _contentType = registryService.GetContentType(CoreConstants.ContentType);
            _serviceProvider = serviceProvider;
        }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            // not implemented
            return null;
        }

        //[Import]
        //public IClassificationTypeRegistryService _classificationRegistry = null;
        [Import]
        internal IClassificationTypeRegistryService ClassificationRegistry = null;

        [Export]
        [Name(HaskellPredefinedClassificationTypeNames.Identifier)]
        [BaseDefinition(PredefinedClassificationTypeNames.Identifier)]
        internal static ClassificationTypeDefinition IdentifierClassificationDefinition = null;
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = HaskellPredefinedClassificationTypeNames.Identifier)]
    [Name(HaskellPredefinedClassificationTypeNames.Identifier)]
    [UserVisible(true)]
    [Order(After = LanguagePriority.NaturalLanguage, Before = LanguagePriority.FormalLanguage)]
    internal sealed class IdentifierFormat : ClassificationFormatDefinition
    {
        public IdentifierFormat()
        {
            DisplayName = ProjectResources.GetString(ProjectResources.IdentifierClassificationType);
            ForegroundColor = Colors.Blue;
        }
    }
}
