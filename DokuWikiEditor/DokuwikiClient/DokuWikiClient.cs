#region Header

// ========================================================================
// File:     DokuWikiClient.cs
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
	using System;
	using System.Collections.Generic;
	using System.Net;
	using DokuwikiClient.Communication;
	using DokuwikiClient.Communication.Messages;
	using DokuwikiClient.Domain.Entities;
	using DokuwikiClient.Persistence;
	using log4net;

	/// <summary>
	/// Root class for all applications using the library. Offers the access on the core functionality, objects, etc.
	/// </summary>
	public class DokuWikiClient
	{
		#region Fields

		private ILog logger = LogManager.GetLogger(typeof(DokuWikiClient).Name);
		private XmlRpcClient client;
		private FileManager fileManager = new FileManager();

		#endregion Fields

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="DokuWikiClient"/> class.
		/// </summary>
		/// <param name="account">The account to use for the communication etc.</param>
		public void InitializeDokuWikiClient(WikiAccount account)
		{
			if (account == null || account.WikiUrlRaw == null)
			{
				throw new ArgumentNullException("account", "The account object or the WikiUrl is null!");
			}

			client = new XmlRpcClient(account.WikiUrl);
			this.ConnectToWiki();
		}

		/// <summary>
		/// Loads the wiki accounts.
		/// </summary>
		/// <returns></returns>
		public List<WikiAccount> LoadWikiAccounts()
		{
			return this.fileManager.LoadObjects<WikiAccount>(typeof(WikiAccount).Name);
		}

		/// <summary>
		/// Saves the wiki account.
		/// </summary>
		/// <param name="accountToSave">The account to save.</param>
		public void SaveWikiAccount(WikiAccount accountToSave)
		{
			this.fileManager.Save<WikiAccount>(accountToSave);
		}

		/// <summary>
		/// Establishes the connection to the wiki.
		/// </summary>
		/// <returns>True, if the connection to the wikiserver could be established. False if not.</returns>
		private bool ConnectToWiki()
		{
			try
			{
				this.client.ListServerMethods();
			}
			catch (ArgumentException)
			{
				return false;
			}
			catch (WebException)
			{
				return false;
			}
			catch (CommunicationException)
			{
				return false;
			}

			return true;
		}

		#endregion Methods
	}
}