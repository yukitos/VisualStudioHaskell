using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace Company.VisualStudioHaskell
{
    [Export(typeof(ITaggerProvider)), ContentType(CoreConstants.ContentType)]
    [TagType(typeof(IOutliningRegionTag))]
    class TestTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag
        {
            return null;
        }
    }

    /*class KeywordTagger : ITagger<ClassificationTag>
    {
    }*/
}
