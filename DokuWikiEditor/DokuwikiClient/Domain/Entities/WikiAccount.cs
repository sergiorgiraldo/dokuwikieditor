// ========================================================================
// File:     WikiAccount.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CH.Froorider.Codeheap.Domain;
using System.Xml.Serialization;

namespace DokuwikiClient.Domain.Entities
{
	/// <summary>
	/// A wiki acccount contains information about the remote wiki, like Accountname, Uri, login etc.
	/// </summary>
	[Serializable]
	[XmlRoot(ElementName="WikiAccount",Namespace="www.froorider.ch")]
	public class WikiAccount : BusinessObject
	{
		#region fields

		private Uri wikiUri;
		private string wikiUrlRaw;

		#endregion

		#region Properites

		/// <summary>
		/// Gets or sets the name of the account.
		/// </summary>
		/// <value>The name of the account.</value>
		public string AccountName { get; set; }
		
		/// <summary>
		/// Gets or sets the name of the login.
		/// </summary>
		/// <value>The name of the login.</value>
		public string LoginName { get; set; }
		
		/// <summary>
		/// Gets or sets the password.
		/// </summary>
		/// <value>The password.</value>
		public string Password { get; set; }

		/// <summary>
		/// Gets the wiki URL as a string.
		/// </summary>
		/// <value>The wiki URL as a string.</value>
		public string WikiUrlRaw
		{
			get
			{
				return this.wikiUrlRaw;
			}

			set
			{
				this.wikiUrlRaw = value;
			}
		}

		/// <summary>
		/// Gets or sets the wiki URL.
		/// </summary>
		/// <value>The wiki URL.</value>
		[XmlIgnore]
		public Uri WikiUrl
		{
			get
			{
				return this.wikiUri;
			}

			set
			{
				this.wikiUri = value;
				this.WikiUrlRaw = this.wikiUri.ToString();
			}
		}
		
		#endregion

		#region public methods



		#endregion
	}
}
