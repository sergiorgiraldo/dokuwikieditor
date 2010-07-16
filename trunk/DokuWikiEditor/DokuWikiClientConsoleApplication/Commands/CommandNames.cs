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

namespace DokuWikiClientConsoleApplication.Commands
{
	/// <summary>
	/// List's all the known commands which are used in the console application.
	/// </summary>
	public enum CommandName
	{
		ExitApplication = 0,

		GetWikiPage = 1,

		GetAllPages = 3,

		GetWikiPageAsHtml = 4,

		ListStoredWikiAccounts = 5,

		LoadMethodHelp = 6,

		LoadMethodSignatures = 7
	}
}
