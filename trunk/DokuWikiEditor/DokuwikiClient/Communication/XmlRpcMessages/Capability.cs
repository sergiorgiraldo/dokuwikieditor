// ========================================================================
// File:     Capability.cs
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
	/// A capability defines which XmlRpc Specifications are implemented.
	/// This information can be used by a client to determine which services can be called.
	/// </summary>
	public class Capability : XmlRpcMessage
	{
		#region Properties

		/// <summary>
		/// Gets or sets the implemented xml rpc specification.
		/// </summary>
		/// <value>A <see cref="SpecificationInfo"/> class containing the infos about the implemented version of the specification.</value>
		[XmlRpcMember("xmlrpc")]
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public SpecificationInfo XmlRpcSpecification { get; set; }

		/// <summary>
		/// Gets or sets the implemented Fault Codes specification.
		/// </summary>
		/// <value>A <see cref="SpecificationInfo"/> class containing the infos about the implemented version of the specification.</value>
		[XmlRpcMember("faults_interop")]
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public SpecificationInfo FaultCodesSpecification { get; set; }

		/// <summary>
		/// Gets or sets the implemented Xml Rpc Multicall specification.
		/// </summary>
		/// <value>A <see cref="SpecificationInfo"/> class containing the infos about the implemented version of the specification.</value>
		[XmlRpcMember("system.multicall")]
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public SpecificationInfo MultiCallSpecification { get; set; }
		
		/// <summary>
		/// Gets or sets the implemented Introspection API specification.
		/// </summary>
		/// <value>A <see cref="SpecificationInfo"/> class containing the infos about the implemented version of the specification.</value>
		[XmlRpcMember("introspection")]
		[XmlRpcMissingMapping(MappingAction.Ignore)]
		public SpecificationInfo IntrospectionSpecification { get; set; }

		#endregion
	}
}
