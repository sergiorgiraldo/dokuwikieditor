// ========================================================================
// File:     IIntrospectionApiProvider.cs
//
// Author:   $Author: froorider@gmail.com $
// Date:     $LastChangedDate: 2009-12-24 12:59:21 +0100 (Do, 24 Dez 2009) $
// Revision: $Revision: 18 $
// ========================================================================
// Copyright [2010] [$Author: froorider@gmail.com $]
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
using DokuwikiClient.Communication.XmlRpcMessages;

namespace CH.Froorider.DokuwikiClient.Contracts
{
	/// <summary>
	/// Defines the methods offered by an Xml-Rpc Service providing the Introspection API.
	/// </summary>
	public interface IIntrospectionApiProvider
	{
		/// <summary>
		/// Gets the server capabilites.
		/// </summary>
		/// <returns>An instance of <see cref="Capability"/>.</returns>
		/// <exception cref="ArgumentException">Is thrown when the XmlRpc server is not enabled.</exception>
		/// <exception cref="CH.Froorider.DokuwikiClient.Communication.Messages.CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="System.Net.WebException">Is thrown when the HTTP connection had errors.</exception>
		[XmlRpcMethod("system.getCapabilities")]
		Capability LoadServerCapabilites();

		/// <summary>
		/// Returns a list of methods implemented by the server.
		/// </summary>
		/// <returns>An array of strings listing all remote method names.</returns>
		/// <exception cref="ArgumentException">Is thrown when the XmlRpc server is not enabled.</exception>
		/// <exception cref="CH.Froorider.DokuwikiClient.Communication.Messages.CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="System.Net.WebException">Is thrown when the HTTP connection had errors.</exception>
		[XmlRpcMethod("system.listServerMethods")]
		string[] ListServerMethods();

		/// <summary>
		/// Gives you a list of possible methods implemented at the server.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <returns>An array containing all method signatures this remote method call offers.</returns>
		/// <exception cref="ArgumentException">Is thrown when the <paramref name="methodName"/> is unkown at remote host.</exception>
		/// <exception cref="CH.Froorider.DokuwikiClient.Communication.Messages.CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="System.Net.WebException">Is thrown when the HTTP connection had errors.</exception>
		[XmlRpcMethod("system.methodSignature")]
		object[] LoadMethodSignatures(string methodName);

		/// <summary>
		/// Gives you a string describing the use of a certain method.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <returns>A description for the usage of this remote method.</returns>
		/// <exception cref="ArgumentException">Is thrown when the <paramref name="methodName"/> is unkown at remote host.</exception>
		/// <exception cref="CH.Froorider.DokuwikiClient.Communication.Messages.CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="System.Net.WebException">Is thrown when the HTTP connection had errors.</exception>
		[XmlRpcMethod("system.methodHelp")]
		string LoadMethodHelp(string methodName);
	}
}
