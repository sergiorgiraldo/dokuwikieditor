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

using System;
using System.Collections.Generic;
using System.Net;
using CH.Froorider.DokuwikiClient.Communication.Messages;
using CH.Froorider.DokuwikiClient.Contracts;
using CH.Froorider.DokuwikiClient.Persistence;
using DokuwikiClient.Communication;
using DokuwikiClient.Domain.Entities;
using log4net;

namespace CH.Froorider.DokuwikiClient
{
	/// <summary>
	/// Root class for all applications using the library. Offers the access on the core functionality, objects, etc.
	/// </summary>
	internal class DokuWikiClient : IDokuWikiClient
	{
		#region Fields

		private ILog logger = LogManager.GetLogger(typeof(DokuWikiClient).Name);
		private XmlRpcClient client;
        private IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);

		#endregion Fields

		#region public Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="DokuWikiClient"/> class.
		/// </summary>
		/// <param name="account">The account to use for the communication etc.</param>
		internal void InitializeDokuWikiClient(WikiAccount account)
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
            List<WikiAccount> accounts = new List<WikiAccount>();

            List<string> identifiers;
            try
            {
                identifiers = (List<string>)repository.GetIdentifiers();
            }
            catch (WikiRepositoryException)
            {
                identifiers = new List<string>();
            }

            foreach(string identifier in identifiers)
            {
                WikiAccount loadedAccount = repository.Load<WikiAccount>(identifier);
                if (loadedAccount != null)
                {
                    accounts.Add(loadedAccount);
                }
            }

            return accounts;
		}

		/// <summary>
		/// Saves the wiki account.
		/// </summary>
		/// <param name="accountToSave">The account to save.</param>
		public void SaveWikiAccount(WikiAccount accountToSave)
		{
            repository.Store<WikiAccount>(accountToSave);
        }

        #endregion

        #region private Methods

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