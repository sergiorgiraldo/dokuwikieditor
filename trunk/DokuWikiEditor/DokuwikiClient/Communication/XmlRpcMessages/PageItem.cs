// ========================================================================
// File:     PageItem.cs
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
using CookComputing.XmlRpc;

namespace DokuwikiClient.Communication.XmlRpcMessages
{
	/// <summary>
	/// PageItem contains information about the identificator, size, permissions and last modified date. 
	/// </summary>
	public class PageItem : XmlRpcMessage
	{
		#region Properites

		/// <summary>
		/// Gets or sets the identificator of the wikipage.
		/// </summary>
		/// <value>An alphanumeric sequence (pagename).</value>
		[XmlRpcMember("id")]
		public string Identificator { get; set; }

		/// <summary>
		/// Gets or sets the permissions on this wikipage.
		/// </summary>
		/// <value>The permissions.</value>
		[XmlRpcMember("perms")]
		public int Permissions { get; set; }

		/// <summary>
		/// Gets or sets the size of the wikipage in bytes.
		/// </summary>
		/// <value>The size.</value>
		[XmlRpcMember("size")]
		public int Size { get; set; }

		/// <summary>
		/// Gets or sets the last modified date of the wikipage.
		/// </summary>
		/// <value>The last modified.</value>
		[XmlRpcMember("lastModified")]
		public DateTime LastModified { get; set; }

		#endregion
	}
}
