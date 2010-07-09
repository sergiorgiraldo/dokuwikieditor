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
		#region fields

		private string accountName = String.Empty;

		#endregion

		#region Properties

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

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Wikipage"/> class.
		/// </summary>
		/// <remarks>This internal constructor is needed in order to provide serialization.</remarks>
		internal Wikipage()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Wikipage"/> class.
		/// </summary>
		/// <param name="account">The account this page is associated with.</param>
		/// <exception cref="ArgumentNullException">Is thrown when <paramref name="account"/> is a <see langword="null"/> reference.</exception>
		public Wikipage(WikiAccount account)
			: base()
		{
			if (account == null)
			{
				throw new ArgumentNullException("account");
			}

			this.accountName = account.AccountName;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Determines whether this page is associated with an account.
		/// </summary>
		/// <returns>
		/// 	<see langword="true"/> if this page is associated with an account; otherwise, <see langword="false"/>.
		/// </returns>
		public bool IsAssociatedWithAnAccount()
		{
			if (String.IsNullOrEmpty(this.accountName))
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		/// <summary>
		/// Gets the name of the associated account.
		/// </summary>
		/// <returns>A string containing the name of the assoicated account. Can be an empty string.</returns>
		public string GetAssociatedAccountName()
		{
			return this.accountName;
		}

		#endregion
	}
}
