// ========================================================================
// File:     IWikiRepository.cs
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
using CH.Froorider.Codeheap.Domain;

namespace CH.Froorider.DokuwikiClient.Contracts
{
	/// <summary>
	/// An IWikiRepository administrates <see cref="BusinessObject"/>s in a persistente store
	/// on the local file system. Usually wiki entities are maintained using this type of repository.
	/// But it is also possible to handle entities which simply derive from <see cref="BusinessObject"/>.
	/// </summary>
	public interface IWikiRepository
	{
		/// <summary>
		/// Deletes the <see cref="BusinessObject"/> with the given <paramref name="id"/> out of the repository.
		/// </summary>
		/// <param name="id">The identifier of the <see cref="BusinessObject"/> to delete.</param>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="id"/>
		/// could not be deleted.
		/// </exception>
		void Delete(string id);

		/// <summary>
		/// Loads the <see cref="BusinessObject"/> with the given <paramref name="id"/> out of the repository.
		/// </summary>
		/// <typeparam name="T">The specific subtype of <see cref="BusinessObject"/> to load.</typeparam>
		/// <param name="id">The identifier of the <see cref="BusinessObject"/> to delete.</param>
		/// <returns>The loaded <see cref="BusinessObject"/> of type <typeparamref name="T"/>.</returns>
		/// <exception cref="System.ArgumentNullException">Is thrown when <paramref name="id"/> is a <see langword="null"/> reference.</exception>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="id"/>
		/// could not be load.</exception>
		T Load<T>(string id) where T : BusinessObject;

		/// <summary>
		/// Stores the passed <see cref="BusinessObject"/> in the repositiory.
		/// </summary>
		/// <typeparam name="T">The specific subtype of <see cref="BusinessObject"/> to store.</typeparam>
		/// <param name="businessObjectToStore">The <see cref="BusinessObject"/> to store.</param>
		/// <returns>The associated id as an string to the passed <see cref="BusinessObject"/>.</returns>
		/// <exception cref="System.ArgumentNullException">Is thrown when <paramref name="businessObjectToStore"/> is a <see langword="null"/> reference.</exception>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="businessObjectToStore"/>
		/// could not be stored.</exception>
		string Store<T>(T businessObjectToStore) where T : BusinessObject;

		/// <summary>
		/// Stores the passed <see cref="BusinessObject"/> in the repositiory.
		/// </summary>
		/// <typeparam name="T">The specific subtype of <see cref="BusinessObject"/> to store.</typeparam>
		/// <param name="businessObjectToStore">The <see cref="BusinessObject"/> to store.</param>
		/// <param name="id">The identifier of the <see cref="BusinessObject"/> to store.</param>
		/// <exception cref="ArgumentNullException"> Is thrown when
		///		<para><paramref name="businessObjectToStore"/> is a <see langword="null"/> reference</para>
		///		<para>- or -</para>
		///		<para><paramref name="id"/> is a <see langword="null"/> reference.</para>
		/// </exception>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="businessObjectToStore"/>
		/// or <paramref name="id"/> could not be stored.</exception>
		void Store<T>(T businessObjectToStore, string id) where T : BusinessObject;

		/// <summary>
		/// Gets the identifiers this repository knows as an IEnumerable collection.
		/// </summary>
		/// <returns>An instance of IEnumerable which can be used to identify <see cref="BusinessObject"/>s.</returns>
		/// <exception cref="WikiRepositoryException">Is thrown when there are no identifiers at all.</exception>
		IEnumerable<string> GetIdentifiers();
	}
}
