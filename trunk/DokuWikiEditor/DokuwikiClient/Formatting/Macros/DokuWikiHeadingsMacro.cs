// ========================================================================
// File:     DokuWikiHeadingsMacro.cs
// 
// Author:   $Author$
// Date:     $LastChangedDate$
// Revision: $Revision$
// ========================================================================
// Copyright [2009] [$Author$]
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
// 
// http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ========================================================================

using System.Collections.Generic;
using WikiPlex;
using WikiPlex.Compilation;
using WikiPlex.Compilation.Macros;

namespace DokuwikiClient.Formatting.Macros
{
	/// <summary>
	/// Lexer to find the Headings in wikitext.
	/// </summary>
	internal class DokuWikiHeadingsMacro : IMacro
	{
		#region fields

		private readonly string rendererName = typeof(DokuWikiHeadingsMacro).ToString();

		#endregion

		#region properties

		/// <summary>
		/// Gets the id which identifies this macro uniquely.
		/// </summary>
		/// <value>A string which can be used to identify this macro.</value>
		public string Id
		{
			get { return this.rendererName; }
		}

		#endregion

		#region IMacro Members

		/// <summary>
		/// Gets the rules.
		/// </summary>
		/// <value>The rules.</value>
		public IList<MacroRule> Rules
		{
			get 
			{
				return new List<MacroRule>
                           {
                               new MacroRule(EscapeRegexPatterns.CurlyBraceEscape),
                               new MacroRule(@"(={6}\s)(.*)(\s={6})",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, DokuWikiScope.HeadingOne},
                                                     {3, ScopeName.Remove}
                                                 }
                                   ),
							   new MacroRule(@"(={5}\s)(.*)(\s={5})",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, DokuWikiScope.HeadingTwo},
                                                     {3, ScopeName.Remove}
                                                 }
                                   ),
							   new MacroRule(@"(={4}\s)(.*)(\s={4})",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, DokuWikiScope.HeadingThree},
                                                     {3, ScopeName.Remove}
                                                 }
                                   ),
							   new MacroRule(@"(={3}\s)(.*)(\s={3})",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, DokuWikiScope.HeadingFour},
                                                     {3, ScopeName.Remove}
                                                 }
                                   ),
							   new MacroRule(@"(={2}\s)(.*)(\s={2})",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2, DokuWikiScope.HeadingFive},
                                                     {3, ScopeName.Remove}
                                                 }
                                   )
                           };
			}
		}

		#endregion
	}
}
