using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Company.VisualStudioHaskell.Parsing
{
    struct Token
    {
        // TODO: implement this

        public enum Kind
        {
            KeywordAs = 0,
            KeywordCase,
            KeywordOf,
            KeywordData,
            KeywordFamily,
            KeywordInstance,
            KeywordDefault,
            KeywordDeriving,
            KeywordDo,
            KeywordForall,
            KeywordForeign,
            KeywordHiding,
            KeywordIf,
            KeywordThen,
            KeywordElse,
            KeywordImport,
            KeywordInfix,
            KeywordInfixl,
            KeywordInfixr,
            KeywordLet,
            KeywordIn,
            KeywordMdo,
            KeywordModule,
            KeywordNewtype,
            KeywordProc,
            KeywordQualified,
            KeywordRec,
            KeywordType,
            KeywordWhere,
            FirstKeyword = KeywordAs,
            LastKeyword = KeywordWhere,
        }
    }

    class Tokenizer
    {
        // TODO: implement this

        #region Keywords

        private static string[] _keywords = {
            "as",
            "case",
            "of",
            "data",
            "family",
            "instance",
            "default",
            "deriving",
            "do",
            "forall",
            "foreign",
            "hiding",
            "if",
            "then",
            "else",
            "import",
            "infix",
            "infixl",
            "infixr",
            "let",
            "in",
            "mdo",
            "module",
            "newtype",
            "proc",
            "qualified",
            "rec",
            "type",
            "where",
        };

        #endregion
    }
}
