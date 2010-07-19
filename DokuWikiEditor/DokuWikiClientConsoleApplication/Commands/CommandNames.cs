// ========================================================================
// File:     CommandNames.cs
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

using System.ComponentModel;
namespace DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// List's all the known commands which are used in the console application.
	/// </summary>
	public enum CommandName
	{
		[Description("Exit application")]
		ExitApplication = 0,

		[Description("Get a single wikipage.")]
		GetWikiPage = 1,

		[Description("Get all wikipage descriptions.")]
		GetAllPages = 2,

		[Description("Get a single wikipage rendered as HTML - source.")]
		GetWikiPageAsHtml = 3,

		[Description("Get a list of all stored wiki accounts.")]
		ListStoredWikiAccounts = 4,

		[Description("Get help description for an xml rpc method.")]
		LoadMethodHelp = 5,

		[Description("Get all method signatures for an xml rpc method.")]
		LoadMethodSignatures = 6
	}
}
