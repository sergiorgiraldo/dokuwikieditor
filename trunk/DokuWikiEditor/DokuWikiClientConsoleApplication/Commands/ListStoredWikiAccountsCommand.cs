// ========================================================================
// File:     ListStoredWikiAccounts.cs
// 
// Author:   $Author$
// Date:     $LastChangedDate$
// Revision: $Revision$
// ========================================================================
// Copyright [2010] [$Author$]
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
using CH.Froorider.DokuwikiClient.Contracts;
using DokuWikiClientConsoleApplication.Commands;
using DokuwikiClient.Domain.Entities;

namespace CH.Froorider.DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// List's all permanently stored wiki accounts.
	/// </summary>
	public class ListStoredWikiAccountsCommand : Command
	{
		#region fields

		private IDokuWikiClient wikiClient;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="ListStoredWikiAccountsCommand"/> class.
		/// </summary>
		/// <param name="wikiProvider">The wiki provider which gives us access to the remote host.</param>
		public ListStoredWikiAccountsCommand(IDokuWikiClient wikiClient, IDokuWikiProvider wikiProvider)
			: base(wikiProvider)
		{
			this.Name = CommandName.ListStoredWikiAccounts;
			this.wikiClient = wikiClient;
		}

		#endregion

		/// <summary>
		/// Executes this instance.
		/// </summary>
		public override void Execute()
		{
			Console.WriteLine("Load all permanently stored wiki accounts.");
			Console.WriteLine();
			List<WikiAccount> accounts = wikiClient.LoadWikiAccounts();
			foreach (WikiAccount account in accounts)
			{
				Console.WriteLine("Account name: " + account.AccountName);
				Console.WriteLine("Login name: " + account.LoginName);
				Console.WriteLine("Password: " + account.Password);
				Console.WriteLine("Wiki url: " + account.WikiUrlRaw);
				Console.WriteLine("Is active: " + account.IsActive);
				Console.WriteLine("------------------------------------------");
			}
		}
	}
}
