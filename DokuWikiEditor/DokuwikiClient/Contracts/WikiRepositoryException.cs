// ========================================================================
// File:     WikiRepositiryException.cs
// 
// Author:   $Author$
// Date:     $LastChangedDate$
// Revision: $Revision$
// ========================================================================
// Copyright [2010] [$Author$]
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

namespace CH.Froorider.DokuwikiClient.Contracts
{
	/// <summary>
	/// Base class for all exceptins which can be thrown by an <see cref="IWikiRepository"/>.
	/// </summary>
	public class WikiRepositoryException : Exception
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="WikiRepositoryException"/> class.
		/// </summary>
		public WikiRepositoryException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WikiRepositoryException"/> class.
		/// </summary>
		/// <param name="message">The message which should be transported using this exception.</param>
		public WikiRepositoryException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WikiRepositoryException"/> class.
		/// </summary>
		/// <param name="message">The message which should be transported using this exception.</param>
		/// <param name="innerException">The inner exception describing details about the occurred failure.</param>
		public WikiRepositoryException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		#endregion
	}
}
