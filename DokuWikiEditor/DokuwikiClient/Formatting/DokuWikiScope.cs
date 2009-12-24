// ========================================================================
// File:     DokuWikiScope.cs
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


namespace DokuwikiClient.Formatting
{
	/// <summary>
	/// Defines the Scopes which can be parsed and formatted inside a rawwiki text file of Dokuwiki.
	/// </summary>
	public class DokuWikiScope
	{
		/// <summary>
		/// H1 - Title section. 
		/// </summary>
		public const string HeadingOne = "Heading One";

		/// <summary>
		/// H2 - Title section. 
		/// </summary>
		public const string HeadingTwo= "Heading Two";

		/// <summary>
		/// H3 - Title section. 
		/// </summary>
		public const string HeadingThree = "Heading Three";

		/// <summary>
		/// H4 - Title section. 
		/// </summary>
		public const string HeadingFour = "Heading Four";

		/// <summary>
		/// H5 - Title section. 
		/// </summary>
		public const string HeadingFive = "Heading Five";

		/// <summary>
		/// Text between code - tag's.
		/// </summary>
		public const string CodeBlock = "Code block";
	}
}
