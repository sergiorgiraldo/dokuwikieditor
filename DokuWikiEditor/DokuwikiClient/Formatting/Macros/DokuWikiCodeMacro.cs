// ========================================================================
// File:     DokuWikiCodeMacro.cs
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
          
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WikiPlex.Compilation.Macros;
using WikiPlex.Compilation;
using WikiPlex;

namespace DokuwikiClient.Formatting.Macros
{
	/// <summary>
	/// Lexer to find the code - tags in wikitext.
	/// </summary>
	internal class DokuWikiCodeMacro : IMacro
	{
		#region fields

		private readonly string macroName = typeof(DokuWikiCodeMacro).ToString();

		#endregion

		#region properties

		/// <summary>
		/// Gets the id which identifies this macro uniquely.
		/// </summary>
		/// <value>A string which can be used to identify this macro.</value>
		public string Id
		{
			get { return this.macroName; }
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
                               new MacroRule(@"(?si)(\<code(.*?)\>)(.*?)(\<\/code(.*?)\>)",
                                             new Dictionary<int, string>
                                                 {
                                                     {1, ScopeName.Remove},
                                                     {2,ScopeName.Remove},
                                                     {3, DokuWikiScope.CodeBlock},
                                                     {4,ScopeName.Remove},
                                                     {5,ScopeName.Remove}
                                                 }
                                   )
                           };
			}
		}

		#endregion
	}
}
