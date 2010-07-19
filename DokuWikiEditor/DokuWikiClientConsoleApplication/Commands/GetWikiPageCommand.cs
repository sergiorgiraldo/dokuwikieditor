// ========================================================================
// File:     GetWikiPageCommand.cs
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
	/// Handles the logic for getting a wiki page from host.
	/// </summary>
	public class GetWikiPageCommand : Command
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="GetWikiPageCommand"/> class.
		/// </summary>
		/// <param name="wikiProvider">The wiki provider which gives us access to the remote host.</param>
		public GetWikiPageCommand(IDokuWikiProvider wikiProvider)
			: base(wikiProvider)
		{
			this.Name = CommandName.GetWikiPage;
		}

		#endregion

		/// <summary>
		/// Executes this instance.
		/// </summary>
		public override void Execute()
		{
			Console.WriteLine("Enter pagename of wikipage.");
			string pageName = Console.ReadLine();
			Console.WriteLine("Wikitext of page: \n" + this.communicationProxy.GetPage(pageName));
		}
	}
}
