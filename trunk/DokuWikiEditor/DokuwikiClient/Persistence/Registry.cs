// ========================================================================
// File:     Registry.cs
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

namespace DokuwikiClient.Persistence
{
	/// <summary>
	/// Class contains information about the stored BusinessObjects and where to find them etc.
	/// </summary>
	[Serializable]
	[XmlInclude(typeof(BusinessObject))]
	[XmlRoot(ElementName="Registry",Namespace="www.froorider.ch")]
	public class Registry
	{
		#region Properties

		[XmlArray(ElementName="WikiObjects")]
		public List<string> wikiObjects { get; set; } 

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Registry"/> class.
		/// </summary>
		public Registry()
		{
			wikiObjects = new List<string>();
		}

		#endregion

		#region methods

		/// <summary>
		/// Adds the wiki object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="wikiObject">The wiki object.</param>
		public void AddWikiObject<T>(T wikiObject) where T : BusinessObject
		{
			string typeName = wikiObject.GetType().Name;
			string identifier = wikiObject.ObjectIdentifier;
			wikiObjects.Add(String.Format("{0};{1}",typeName,identifier));
		}

		/// <summary>
		/// Gets the identifiers.
		/// </summary>
		/// <param name="wikiObjectTypeName">Name of the wiki object type.</param>
		/// <returns></returns>
		public List<string> GetIdentifiers(string wikiObjectTypeName)
		{
			List<string> resultList = new List<string>();

			foreach (string wikiObject in this.wikiObjects)
			{
				if (wikiObject.Contains(wikiObjectTypeName))
				{
					int splitIndex = wikiObject.IndexOf(";");
					resultList.Add(wikiObject.Substring(splitIndex + 1));
				}
			}

			return resultList;
		}

		#endregion
	}
}
