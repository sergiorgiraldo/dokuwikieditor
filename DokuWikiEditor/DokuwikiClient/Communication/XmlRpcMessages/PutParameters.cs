// ========================================================================
// File:     PutParameters.cs
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

using CookComputing.XmlRpc;

namespace DokuwikiClient.Communication.XmlRpcMessages
{
	/// <summary>
	/// Contains versioning information about a wikipage. 
	/// </summary>
	public class PutParameters : XmlRpcMessage
	{
		#region Properites

		/// <summary>
		/// Gets or sets the change summary for the page.
		/// </summary>
		/// <value>An string describing the changes made to the wikipage.</value>
		[XmlRpcMember("sum")]
		public string ChangeSummary { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the wikipage changes are minor changes or not.
		/// </summary>
		/// <value><c>true</c> if the changes are minor; otherwise, <c>false</c>.</value>
		[XmlRpcMember("minor")]
		public bool IsMinor { get; set; }

		#endregion
	}
}
