using System.Collections.Generic;
// ========================================================================
// File:     DokuWikiFormatters.cs
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

using System.Linq;
using DokuwikiClient.Formatting.Renderers;
using WikiPlex.Formatting;

namespace DokuwikiClient.Formatting
{
	/// <summary>
	/// Class which contains or links all formatters needed to render DokuWiki files correctly to HTML.
	/// </summary>
	internal static class DokuWikiFormatters
	{
		/// <summary>
		/// Gets the doku wiki formatters.
		/// </summary>
		/// <returns>A MacroFormatter containing all specific DokuWiki renderers.</returns>
		public static MacroFormatter GetDokuWikiFormatters()
		{
			var siteRenderers = new IRenderer[] { new DokuWikiHeadingsRenderer(), new DokuWikiCodeRenderer() };
			IEnumerable<IRenderer> allRenderers = WikiPlex.Renderers.All.Union(siteRenderers);
			return new MacroFormatter(allRenderers);
		}
	}
}
