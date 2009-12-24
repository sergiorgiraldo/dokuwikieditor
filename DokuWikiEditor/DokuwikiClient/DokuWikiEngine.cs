#region Header

// ========================================================================
// File:     DokuWikiEngine.cs
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

#endregion Header

namespace DokuwikiClient
{
    using DokuwikiClient.Formatting.Macros;

    using WikiPlex;

    /// <summary>
    /// The DokuWikiEngine can render raw wikitext into HTML.
    /// </summary>
    public class DokuWikiEngine
    {
        #region Fields

        private readonly string footer = "</body></html>";
        private readonly string header = "<html><head><link rel='stylesheet' type='text/css' href='./Ressources/design.css'></head><body>";

        private IWikiEngine engine = new WikiEngine();

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DokuWikiEngine"/> class.
        /// </summary>
        public DokuWikiEngine()
        {
            Macros.Register<DokuWikiHeadingsMacro>();
            Macros.Register<DokuWikiCodeMacro>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Renders the specified raw wiki text.
        /// </summary>
        /// <param name="rawWikiText">The raw wiki text.</param>
        /// <returns>The rendererd raw wiki text as an HTML string.</returns>
        public string Render(string rawWikiText)
        {
            string htmlPage = header;
            htmlPage += engine.Render(rawWikiText, DokuwikiClient.Formatting.DokuWikiFormatters.GetDokuWikiFormatters());
            htmlPage += footer;
            return htmlPage;
        }

        #endregion Methods
    }
}