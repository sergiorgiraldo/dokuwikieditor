// ========================================================================
// File:     Wikipage.cs
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
using CH.Froorider.Codeheap.Domain;


namespace DokuwikiClient.Domain.Entities
{
	/// <summary>
	/// This is the object representation of a wikipage. The page contains information about the creator, version history
	/// and (logically) the rawWikiPage as a simple string.
	/// </summary>
	[Serializable]
	public class Wikipage : BusinessObject
	{
		#region properties

		/// <summary>
		/// Gets or sets the content of the wiki page.
		/// </summary>
		/// <value>The content of the wiki page.</value>
		public string WikiPageContent { get; set; }

		/// <summary>
		/// Gets or sets the name of the wiki page.
		/// </summary>
		/// <value>The name of the wiki page.</value>
		public string WikiPageName { get; set; }

		#endregion
	}
}
