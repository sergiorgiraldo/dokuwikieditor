// ========================================================================
// File:     CommandFactory.cs
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
using CH.Froorider.DokuwikiClient.Contracts;
using DokuWikiClientConsoleApplication.Commands;

namespace CH.Froorider.DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// Creates the commands according to the command name.
	/// </summary>
	public class CommandFactory
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CommandFactory"/> class.
		/// </summary>
		/// <remarks>Is private to prevent outsiders of creating instances of this class.</remarks>
		private CommandFactory()
		{
		}

		/// <summary>
		/// Creates an <see cref="ICommand"/> according to the specified <see cref="CommandName"/>.
		/// </summary>
		/// <param name="commandName">One of the values of <see cref="CommandName"/>.</param>
		/// <param name="communicationProxy">The communication proxy as an instance of <see cref="IDokuWikiProvider"/>.</param>
		/// <param name="wikiClient">The "local" wiki client which has access to the persited data.</param>
		/// <returns>
		/// The created instance to the given named command.
		/// </returns>
		/// <exception cref="ArgumentNullException">Is thrown when <paramref name="communicationProxy"/> is a <see langword="null"/> reference.</exception>
		/// <exception cref="ArgumentNullException"> Is thrown when
		///		<para><paramref name="communicationProxy"/> is a <see langword="null"/> reference</para>
		///		<para>- or -</para>
		///		<para><paramref name="wikiClient"/> is a <see langword="null"/> reference.</para>
		/// </exception>
		/// <exception cref="ArgumentException">Is thrown when <paramref name="commandName"/> is not defined in <see cref="CommandName"/>.</exception>
		public static ICommand CreateCommand(CommandName commandName, IDokuWikiProvider communicationProxy, IDokuWikiClient wikiClient)
		{
			if (communicationProxy == null)
			{
				throw new ArgumentNullException("communicationProxy");
			}

			if (wikiClient == null)
			{
				throw new ArgumentNullException("wikiClient");
			}

			if (!Enum.IsDefined(typeof(CommandName), commandName))
			{
				throw new ArgumentException("Unkown command name", "commandName");
			}

			switch (commandName)
			{
				case CommandName.GetWikiPage:
					return new GetWikiPageCommand(communicationProxy);
				case CommandName.GetAllPages:
					return new GetAllPageItemsCommand(communicationProxy);
				case CommandName.GetWikiPageAsHtml:
					return new GetWikiPageAsHtmlCommand(communicationProxy);
				case CommandName.LoadMethodHelp:
					return new LoadMethodHelpCommand(communicationProxy);
				case CommandName.LoadMethodSignatures:
					return new LoadMethodSignaturesCommand(communicationProxy);
				case CommandName.ListStoredWikiAccounts:
					return new ListStoredWikiAccountsCommand(wikiClient, communicationProxy);
				case CommandName.ExitApplication:
				default:
					throw new NotImplementedException("No command available for this command name.");
			}
		}
	}
}
