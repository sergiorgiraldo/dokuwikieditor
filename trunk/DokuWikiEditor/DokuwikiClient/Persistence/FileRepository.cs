// ========================================================================
// File:     FileRepository.cs
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
using System.IO;
using System.Linq;
using CH.Froorider.Codeheap.Domain;
using CH.Froorider.Codeheap.Persistence;
using CH.Froorider.DokuwikiClient.Contracts;
using log4net;

namespace CH.Froorider.DokuwikiClient.Persistence
{
	/// <summary>
	/// Implementation based on local file system of an <see cref="IWikiRepository"/>.
	/// </summary>
	internal class FileRepository : IWikiRepository
	{
		#region Fields

		private readonly string repositoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "//DokuWikiStore//";
        private readonly string fileExtension = ".wiki";
		private ILog logger = LogManager.GetLogger(typeof(FileRepository));
		private Dictionary<string, BusinessObject> documents = new Dictionary<string, BusinessObject>();

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="FileRepository"/> class.
		/// </summary>
		internal FileRepository()
		{
			try
			{
				DirectoryInfo directory;

				if (!Directory.Exists(this.repositoryPath))
				{
					directory = Directory.CreateDirectory(this.repositoryPath);
				}
				else
				{
					directory = new DirectoryInfo(this.repositoryPath);
				}
			}
			catch (Exception e)
			{
				this.logger.Error("Repository directory could not be created. Cause: " + e.Message);
			}
		}

		#endregion

		#region IWikiRepository Members

		/// <summary>
		/// Deletes the <see cref="BusinessObject"/> with the given <paramref name="id"/> out of the repository.
		/// </summary>
		/// <param name="id">The identifier of the <see cref="BusinessObject"/> to delete.</param>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="id"/>
		/// could not be deleted.
		/// </exception>
		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Loads the <see cref="BusinessObject"/> with the given <paramref name="id"/> out of the repository.
		/// </summary>
		/// <typeparam name="T">The specific subtype of <see cref="BusinessObject"/> to load.</typeparam>
		/// <param name="id">The identifier of the <see cref="BusinessObject"/> to delete.</param>
		/// <returns>
		/// The loaded <see cref="BusinessObject"/> of type <typeparamref name="T"/>.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">Is thrown when <paramref name="id"/> is a <see langword="null"/> reference.</exception>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="id"/>
		/// could not be load.</exception>
		public T Load<T>(string id) where T : BusinessObject
		{
			if (this.documents.ContainsKey(id))
			{
				return this.documents[id] as T;
			}
			else
			{
                T loadedWikiObject = PersistenceManager.DeserializeObject<T>(id,this.repositoryPath,this.fileExtension);
                this.documents.Add(id, loadedWikiObject);
                return loadedWikiObject;
			}
		}

		/// <summary>
		/// Stores the passed <see cref="BusinessObject"/> in the repositiory.
		/// </summary>
		/// <typeparam name="T">The specific subtype of <see cref="BusinessObject"/> to store.</typeparam>
		/// <param name="businessObjectToStore">The <see cref="BusinessObject"/> to store.</param>
		/// <returns>
		/// The associated id as an string to the passed <see cref="BusinessObject"/>.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">Is thrown when <paramref name="businessObjectToStore"/> is a <see langword="null"/> reference.</exception>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="businessObjectToStore"/>
		/// could not be stored.</exception>
		public string Store<T>(T businessObjectToStore) where T : BusinessObject
		{
			if (businessObjectToStore == null)
			{
				throw new ArgumentNullException("businessObjectToStore");
			}

			string businessObjectIdentifier = businessObjectToStore.ObjectIdentifier;
			try
			{
				if (String.IsNullOrEmpty(businessObjectIdentifier))
				{
					businessObjectIdentifier = businessObjectToStore.Serialize(this.repositoryPath,this.fileExtension);
				}
				else
				{
					businessObjectToStore.Serialize(this.repositoryPath,this.fileExtension);
				}

				if (documents.ContainsKey(businessObjectIdentifier))
				{
					documents[businessObjectIdentifier] = businessObjectToStore;
				}
				else
				{
					documents.Add(businessObjectIdentifier, businessObjectToStore);
				}
			}
			catch (InvalidOperationException ioe)
			{
				throw new WikiRepositoryException("The given business object could not be serialized.", ioe);
			}

			return businessObjectIdentifier;
		}

		/// <summary>
		/// Stores the passed <see cref="BusinessObject"/> in the repositiory.
		/// </summary>
		/// <typeparam name="T">The specific subtype of <see cref="BusinessObject"/> to store.</typeparam>
		/// <param name="businessObjectToStore">The <see cref="BusinessObject"/> to store.</param>
		/// <param name="id">The identifier of the <see cref="BusinessObject"/> to store.</param>
		/// <exception cref="ArgumentNullException"> Is thrown when
		/// <para><paramref name="businessObjectToStore"/> is a <see langword="null"/> reference</para>
		/// 	<para>- or -</para>
		/// 	<para><paramref name="id"/> is a <see langword="null"/> reference.</para>
		/// </exception>
		/// <exception cref="WikiRepositoryException">Is thrown when the desired business object referenced by <see paramref="businessObjectToStore"/>
		/// or <paramref name="id"/> could not be stored.</exception>
		public void Store<T>(T businessObjectToStore, string id) where T : BusinessObject
		{
			if (businessObjectToStore == null)
			{
				throw new ArgumentNullException("businessObjectToStore");
			}

			if (String.IsNullOrEmpty(id))
			{
				throw new ArgumentNullException("id");
			}

			try
			{
				businessObjectToStore.Serialize(this.repositoryPath + id, this.fileExtension);
				if (documents.ContainsKey(id))
				{
					documents[id] = businessObjectToStore;
				}
				else
				{
					documents.Add(id, businessObjectToStore);
				}
			}
			catch (InvalidOperationException ioe)
			{
				throw new WikiRepositoryException("The given business object could not be serialized.", ioe);
			}
		}

		/// <summary>
		/// Gets the identifiers this repository knows as anIEnumerable collection.
		/// </summary>
		/// <returns>
		/// An instance of IEnumerable which can be used to identify <see cref="BusinessObject"/>s.
		/// </returns>
		/// <exception cref="WikiRepositoryException">Is thrown when there are no identifiers at all.</exception>
		public IEnumerable<string> GetIdentifiers()
		{
			if (this.documents.Count == 0)
			{
				throw new WikiRepositoryException("No identifiers at all.");
			}
			else
			{
				return this.documents.Keys.ToList<string>();
			}
		}

		#endregion
	}
}
