// ========================================================================
// File:     Command.cs
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

using CH.Froorider.DokuwikiClient.Contracts;
using DokuWikiClientConsoleApplication.Commands;

namespace CH.Froorider.DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// Abstract definition for all commands.
	/// </summary>
	public abstract class Command : ICommand
	{
		#region Fields

		protected IDokuWikiProvider communicationProxy;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="Command"/> class.
		/// </summary>
		/// <param name="provider">The provider which encapsulates the communication with the remote host.</param>
		protected Command(IDokuWikiProvider provider)
		{
			this.communicationProxy = provider;
		}

		#endregion

		#region ICommand Members

		/// <summary>
		/// Gets or sets the name of the command.
		/// </summary>
		/// <value>One of the values of <see cref="CommandName"/>.</value>
		public CommandName Name { get; protected set; }

		public abstract void Execute();

		#endregion
	}
}
