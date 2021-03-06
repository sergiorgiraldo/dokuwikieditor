﻿// ========================================================================
// File:     ICommand.cs
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

using DokuWikiClientConsoleApplication.Commands;

namespace CH.Froorider.DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// Defines the methods a Command offers. A command contains the logic which is executed.
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		/// Gets or sets the name of the command.
		/// </summary>
		/// <value>One of the values of <see cref="CommandName"/>.</value>
		CommandName Name { get; }

		/// <summary>
		/// Triggers the instance to execute it's logic.
		/// </summary>
		void Execute();
	}
}
