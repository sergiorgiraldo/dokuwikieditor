// ========================================================================
// File:     WikiRepositoryFactory.cs
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
using CH.Froorider.DokuwikiClient.Contracts;

namespace CH.Froorider.DokuwikiClient.Persistence
{
	/// <summary>
	/// Creates instances of <see cref="IWikiRepository"/> according to the given <see cref="WikiRepositoryType"/>.
	/// </summary>
	public class WikiRepositoryFactory
	{
		/// <summary>
		/// Creates the <see cref="IWikiRepository"/> instance to a given <see cref="WikiRepositoryType"/>.
		/// </summary>
		/// <param name="typeToCreate">The type of <see cref="IWikiRepository"/> to create.</param>
		/// <returns>An instance of <see cref="IWikiRepository"/>.</returns>
		/// <exception cref="ArgumentException">Is thrown when <paramref name="typeToCreate"/> is not a valid <see cref="WikiRepositoryType"/> value.</exception>
		/// <exception cref="NotSupportedException">Is thrown when no <see cref="IWikiRepository"/> instance can be created to the passed <paramref name="typeToCreate"/>.</exception>
		public static IWikiRepository CreateRepository(WikiRepositoryType typeToCreate)
		{
			if (!Enum.IsDefined(typeof(WikiRepositoryType), typeToCreate))
			{
				throw new ArgumentException("Passed WikiRepositoryType value is not defined in the enumeration.", "typeToCreate");
			}

			switch (typeToCreate)
			{
				case WikiRepositoryType.FileRepository:
					return new FileRepository();
				case WikiRepositoryType.Undefined:
				default:
					throw new NotSupportedException("Not supported repository type: " + typeToCreate.ToString());
			}
		}
	}
}
