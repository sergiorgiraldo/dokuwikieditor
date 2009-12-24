using System;
// ========================================================================
// File:     DokuWikiHeadingsRenderer.cs
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

using WikiPlex.Formatting;

namespace DokuwikiClient.Formatting.Renderers
{
	/// <summary>
	/// Renders all the Heading tags (H1 - H5).
	/// </summary>
	internal class DokuWikiHeadingsRenderer : IRenderer
	{
		#region fields

		private readonly string rendererName = typeof(DokuWikiHeadingsRenderer).ToString();

		private readonly string headingOneFormat = "<h1>{0}</h1>";
		private readonly string headingTwoFormat = "<h2>{0}</h2>";
		private readonly string headingThreeFormat = "<h3>{0}</h3>";
		private readonly string headingFourFormat = "<h4>{0}</h4>";
		private readonly string headingFiveFormat = "<h5>{0}</h5>";

		#endregion

		#region Properties

		/// <summary>
		/// Gets the id.
		/// </summary>
		/// <value>The unique name of this renderer.</value>
		public string Id
		{
			get { return this.rendererName; }
		}

		#endregion

		#region IRenderer Members

		/// <summary>
		/// Determines whether this instance can expand the specified scope name.
		/// </summary>
		/// <param name="scopeName">Name of the scope.</param>
		/// <returns>
		/// 	<c>true</c> if this instance can expand the specified scope name; otherwise, <c>false</c>.
		/// </returns>
		public bool CanExpand(string scopeName)
		{
			if (scopeName == DokuWikiScope.HeadingOne ||
				scopeName == DokuWikiScope.HeadingTwo ||
				scopeName == DokuWikiScope.HeadingThree ||
				scopeName == DokuWikiScope.HeadingFour ||
				scopeName == DokuWikiScope.HeadingFive)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Expands the specified scope name.
		/// </summary>
		/// <param name="scopeName">Name of the scope.</param>
		/// <param name="input">The input.</param>
		/// <param name="htmlEncode">The HTML encode.</param>
		/// <param name="attributeEncode">The attribute encode.</param>
		/// <returns></returns>
		public string Expand(string scopeName, string input, Func<string, string> htmlEncode, Func<string, string> attributeEncode)
		{
			if (scopeName == DokuWikiScope.HeadingOne)
			{
				return String.Format(this.headingOneFormat, htmlEncode(input));
			}
			else if (scopeName == DokuWikiScope.HeadingTwo)
			{
				return String.Format(this.headingTwoFormat, htmlEncode(input));
			}
			else if (scopeName == DokuWikiScope.HeadingThree)
			{
				return String.Format(this.headingThreeFormat, htmlEncode(input));
			}
			else if (scopeName == DokuWikiScope.HeadingFour)
			{
				return String.Format(this.headingFourFormat, htmlEncode(input));
			}
			else if (scopeName == DokuWikiScope.HeadingFive)
			{
				return String.Format(this.headingFiveFormat, htmlEncode(input));
			}
			else
			{
				throw new NotImplementedException("Unknown scope name."); ;
			}
		}

		#endregion
	}
}
