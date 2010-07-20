// ========================================================================
// File:     ListServerMethodsCommand.cs
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
using DokuWikiClientConsoleApplication.Commands;
using CH.Froorider.DokuwikiClient.Contracts;
using CH.Froorider.DokuwikiClient.Communication.Messages;

namespace CH.Froorider.DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// Calls the system rpc method "system.listServerMethods".
	/// </summary>
	public class ListServerMethodsCommand : Command
	{
		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="ListServerMethodsCommand"/> class.
		/// </summary>
		/// <param name="wikiProvider">The wiki provider which gives us access to the remote host.</param>
		public ListServerMethodsCommand(IDokuWikiProvider wikiProvider)
			: base(wikiProvider)
		{
			this.Name = CommandName.ListServerMethods;
		}

		#endregion

		/// <summary>
		/// Executes this instance.
		/// </summary>
		public override void Execute()
		{
			Console.WriteLine("Listing server methods.");
			try
			{
				string[] serverMethodNames = this.communicationProxy.ListServerMethods();
				foreach (String serverMethodName in serverMethodNames)
				{
					Console.WriteLine("Method name: {0}", serverMethodName);
				}
			}
			catch (CommunicationException ce)
			{
				Console.WriteLine("Exception caught: " + ce.Message);
			}
		}
	}
}
