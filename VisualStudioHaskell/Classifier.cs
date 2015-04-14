using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Company.VisualStudioHaskell
{
    class Classifier : IClassifier
    {
        private ClassifierProvider _provider;

        public Classifier(ClassifierProvider provider)
        {
            _provider = provider;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            var list = new List<ClassificationSpan>();

            list.Add(new ClassificationSpan(span, _provider.Identifier));

            return list;
        }
    }
}
