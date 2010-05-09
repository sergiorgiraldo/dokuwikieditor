//========================================================================
//File:     XmlRpcFaultCodes.cs
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

namespace CH.Froorider.DokuwikiClient.Communication.Messages
{
	/// <summary>
	/// Declares all Xml Rpc Fault codes which are specified in the "Specification for fault code interoperability" version 20010516.
	/// </summary>
	public enum XmlRpcFaultCodes : int
	{
		/// <summary>
		/// 
		/// </summary>
		ParseErrorNotWellFormed = -32700,

		/// <summary>
		/// 
		/// </summary>
		ParseErrorInvalidCharacter = -32701,

		/// <summary>
		/// 
		/// </summary>
		ServerErrorInvalidXmlRpc = -32600,

		/// <summary>
		/// 
		/// </summary>
		ServerErrorRequestedMethodNotFound = -32601,

		/// <summary>
		/// 
		/// </summary>
		ServerErrorInvalidMethodParameters = -32602,

		/// <summary>
		/// 
		/// </summary>
		ServerErrorInternalXmlRpcError = -32603,

		/// <summary>
		/// 
		/// </summary>
		ApplicationError = -32500,

		/// <summary>
		/// 
		/// </summary>
		SystemError = -32400,

		/// <summary>
		/// 
		/// </summary>
		TransportError = -32300,

		/// <summary>
		/// Default value.
		/// </summary>
		Undefined = 0
	}
}
