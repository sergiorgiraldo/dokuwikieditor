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
using System.Linq;
using System.Text;
using CH.Froorider.DokuwikiClient.Contracts;
using CH.Froorider.Codeheap.Domain;
using CH.Froorider.Codeheap.Persistence;
using System.IO;
using log4net;

namespace CH.Froorider.DokuwikiClient.Persistence
{
	/// <summary>
	/// Implementation based on local file system of an <see cref="IWikiRepository"/>.
	/// </summary>
	internal class FileRepository : IWikiRepository
	{
		#region Fields

		private static readonly string repositoryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "//DokuWikiEditor//";
		private static ILog logger = LogManager.GetLogger(typeof(FileRepository));
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
				if (!Directory.Exists(FileRepository.repositoryPath))
				{
					Directory.CreateDirectory(FileRepository.repositoryPath);
				}
			}
			catch (Exception e)
			{
				FileRepository.logger.Error("Repository directory could not be created. Cause: " + e.Message);
			}
		}

		#endregion

		#region IWikiRepository Members

		public void Delete(string id)
		{
			throw new NotImplementedException();
		}

		public T Load<T>(string id) where T : CH.Froorider.Codeheap.Domain.BusinessObject
		{
			throw new NotImplementedException();
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
		public string Store<T>(T businessObjectToStore) where T : CH.Froorider.Codeheap.Domain.BusinessObject
		{
			if (businessObjectToStore == null)
			{
				throw new ArgumentNullException("businessObjectToStore", "The buisness object to store must be set.");
			}

			return businessObjectToStore.Serialize();
		}

		public void Store<T>(T businessObjectToStore, string id) where T : CH.Froorider.Codeheap.Domain.BusinessObject
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the identifiers this repository knows as an <see cref="IEnumerable"/> collection.
		/// </summary>
		/// <returns>
		/// An instance of <see cref="IEnumerable"/> which can be used to identify <see cref="BusinessObject"/>s.
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
