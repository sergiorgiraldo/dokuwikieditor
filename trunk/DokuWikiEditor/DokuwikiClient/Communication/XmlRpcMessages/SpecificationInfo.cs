//========================================================================
//File:     SpecificationInfo.cs
//
//Author:   $Author$
//Date:     $LastChangedDate$
//Revision: $Revision$
//========================================================================
//Copyright [2009] [$Author$]
//
//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//You may obtain a copy of the License at
//
//http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
//========================================================================

using CookComputing.XmlRpc;

namespace DokuwikiClient.Communication.XmlRpcMessages
{
	/// <summary>
	/// A specification info contains information about the implemented version number of the spec and where
	/// to find a reference file for it.
	/// </summary>
	public class SpecificationInfo : XmlRpcMessage
	{
		#region Properties

		/// <summary>
		/// Gets or sets the specification reference.
		/// </summary>
		/// <value>A link to the specification document.</value>
		[XmlRpcMember("specUrl")]
		public string SpecificationReference { get; set; }

		/// <summary>
		/// Gets or sets the specification version.
		/// </summary>
		/// <value>The implemented specification version of this service.</value>
		[XmlRpcMember("specVersion")]
		public int SpecificationVersion { get; set; }

		#endregion
	}
}
