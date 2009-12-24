// ========================================================================
// File:     DokuWikiCodeRenderer.cs
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
using WikiPlex.Formatting;

namespace DokuwikiClient.Formatting.Renderers
{
	/// <summary>
	/// Formattes the text between two code - tags.
	/// </summary>
	internal class DokuWikiCodeRenderer : IRenderer
	{
		#region fields

		private readonly string rendererName = typeof(DokuWikiCodeRenderer).ToString();

        private readonly string headingOneFormat = "<div style='outline:4px solid invert; background-color:yellow; padding:4px; margin:0px;'>{0}</div>";

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
			if (scopeName == DokuWikiScope.CodeBlock)
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
			if (scopeName == DokuWikiScope.CodeBlock)
			{
                return String.Format(this.headingOneFormat, htmlEncode(input));
			}
			else
			{
				throw new NotImplementedException("Unknown scope name."); ;
			}
		}

		#endregion
	}
}
