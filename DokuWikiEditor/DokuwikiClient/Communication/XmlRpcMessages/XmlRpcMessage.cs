// ========================================================================
// File:     XmlRpcMessage.cs
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
using System.Globalization;
using System.Reflection;
using System.Text;
using CookComputing.XmlRpc;

namespace DokuwikiClient.Communication.XmlRpcMessages
{
	/// <summary>
	/// Base class for all XmlRpc messages. Provides common functionality.
	/// </summary>
	public class XmlRpcMessage
	{
		#region public methods

		/// <summary>
		/// Returns a string representation of the class content.
		/// </summary>
		/// <remarks>
		/// This method is used for log purposes.
		/// <para>
		/// All readable properties are fetched and their values converted to a string in the following format: " Name: | Value: ". 
		/// </para>
		/// </remarks>
		/// <returns>String containing the name and value of all properites.</returns>
		public string Dump()
		{
			StringBuilder builder = new StringBuilder();
			Type type = this.GetType();
			builder.AppendFormat(CultureInfo.InvariantCulture, ">> Information of object: '{0}' \n", type.Name);
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo currentProperty in properties)
			{
				// Only use the property if it's not an set-only property
				if (currentProperty.CanRead)
				{
					// Do not use the ?? operator -> cause of .NET 1.1
					object propiValue = currentProperty.GetValue(this, null);
					if (propiValue == null)
					{
						propiValue = string.Empty;
					}

					XmlRpcMessage xmlRpcMessage = propiValue as XmlRpcMessage;
					if (xmlRpcMessage != null)
					{
						// Recursive call for object hierarchies
						builder.AppendFormat(
							CultureInfo.InvariantCulture, 
							"Name: '{0}' Value: '{1}' \n",
							currentProperty.Name, 
							xmlRpcMessage.Dump());
					}
					else
					{
						if (propiValue is XmlRpcStruct)
						{
							// Print the data property (contains multiple fields)
							XmlRpcStruct dataProperty = propiValue as XmlRpcStruct;
							foreach (string key in dataProperty.Keys)
							{
								builder.AppendFormat(
									CultureInfo.InvariantCulture, 
									"Name: '{0}' Value: '{1}' \n",
									key.ToString(), 
									dataProperty[key].ToString());
							}
						}
						else
						{
							// Print common properites of base class
							builder.AppendFormat(
								CultureInfo.InvariantCulture, 
								"Name: '{0}' Value: '{1}' \n",
								currentProperty.Name, 
								currentProperty.GetValue(this, null));
						}
					}

					builder.Append("|");
				}
			}

			// Remove the final "|" from the end of the string
			if (builder.Length > 0)
			{
				builder.Remove(builder.Length - 1, 1);
			}

			return builder.ToString();
		}

		#endregion
	}
}
