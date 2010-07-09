// ========================================================================
// File:     CommunicationException.cs
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

namespace CH.Froorider.DokuwikiClient.Communication.Messages
{
	/// <summary>
	/// This type of exception is a wrapper around the whole bunch of XmlRpc exceptions. Especially the CookComputing specific exceptions.
	/// </summary>
	[Serializable]
	public class CommunicationException : Exception
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="CommunicationException"/> class.
		/// </summary>
		public CommunicationException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommunicationException"/> class.
		/// </summary>
		/// <param name="message">The message embedded in the newly constructed <see cref="CommunicationException"/> instance.</param>
		public CommunicationException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CommunicationException"/> class.
		/// </summary>
		/// <param name="message">The message embedded in the newly constructed <see cref="CommunicationException"/> instance.</param>
		/// <param name="inner">The <see cref="Exception"/> embedded in the newly constructed <see cref="CommunicationException"/> instance.</param>
		public CommunicationException(string message, Exception inner)
			: base(message, inner)
		{
		}

		#endregion

		#region Serialization implementation

		/// <summary>
		/// When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo"/> with information about the exception.
		/// </summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo"/> that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext"/> that contains contextual information about the source or destination.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// The <paramref name="info"/> parameter is a null reference (Nothing in Visual Basic).
		/// </exception>
		/// <PermissionSet>
		/// 	<IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*"/>
		/// 	<IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter"/>
		/// </PermissionSet>
		public override void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
		}

		#endregion
	}
}
