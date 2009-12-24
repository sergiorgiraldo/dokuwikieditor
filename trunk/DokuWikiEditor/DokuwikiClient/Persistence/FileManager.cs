// ========================================================================
// File:     FileManager.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using CH.Froorider.Codeheap.Domain;
using CH.Froorider.Codeheap.Persistence;
using log4net;
using DokuwikiClient.Communication.Messages;

namespace DokuwikiClient.Persistence
{
	/// <summary>
	/// Class which maintains the stored files / business objects.
	/// </summary>
	internal class FileManager
	{
		#region fields

		private static readonly string registryPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "//DokuWikiEditor//";

		private static readonly string registryFile = "Registry.dat";

		private static ILog logger = LogManager.GetLogger(typeof(FileManager));

		private Registry registry;

		#endregion

		#region properties

		#endregion

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="FileManager"/> class.
		/// </summary>
		/// <exception cref="DokuWikiClientException">Is thrown when the access on the file system did not worked.</exception>
		public FileManager()
		{
			if (!this.CreateDirectoryIfNotExisting())
			{
				throw new DokuWikiClientException("Could not create base directory. See log-files for details.");
			}

			if (!this.LoadRegistry())
			{
				throw new DokuWikiClientException("Could not load files and registry. See log-files for details.");
			}
		}

		#endregion

		#region public methods

		/// <summary>
		/// Saves the given business object permamently on the disk.
		/// </summary>
		/// <typeparam name="T">The concrete Type of the BusinessObject.</typeparam>
		/// <param name="objectToRegister">The object itself to save.</param>
		public void Save<T>(T objectToSave) where T : BusinessObject
		{
			List<string> identifiers = this.registry.GetIdentifiers(objectToSave.GetType().Name);
			try
			{
				objectToSave.Serialize(registryPath + "//WikiObjects//", ".dat");
			}
			catch (Exception e)
			{
				Console.WriteLine(e.Message);
			}
			if (identifiers.Count == 0 || !identifiers.Contains(objectToSave.ObjectIdentifier))
			{
				this.registry.AddWikiObject(objectToSave);
			}

			this.SaveRegistry();
		}

		/// <summary>
		/// Loads all wiki objects of a certain type.
		/// </summary>
		/// <typeparam name="T">The type of BO's to load.</typeparam>
		/// <param name="typeToLoad">The type to load.</param>
		/// <returns>A list containing all BO's of this type, which could be loaded.</returns>
		public List<T> LoadObjects<T>(string typeToLoadName) where T : BusinessObject
		{
			List<string> identifiers = this.registry.GetIdentifiers(typeToLoadName);
			List<T> loadedObjects = new List<T>();

			foreach (string identifier in identifiers)
			{
				loadedObjects.Add(PersistenceManager.DeserializeObject<T>(identifier, registryPath + "//WikiObjects//", ".dat"));
			}

			return loadedObjects;
		}

		#endregion

		#region private methods

		/// <summary>
		/// Creates the directory if not existing.
		/// </summary>
		/// <returns>True if the creation was successful. False if not.</returns>
		private bool CreateDirectoryIfNotExisting()
		{
			try
			{
				if (!Directory.Exists(registryPath))
				{
					Directory.CreateDirectory(registryPath);
				}

				if (!File.Exists(FileManager.registryPath + FileManager.registryFile))
				{
					using (FileStream stream = File.Create(FileManager.registryPath + FileManager.registryFile))
					{
						StreamWriter writer = new StreamWriter(stream);
						writer.WriteLine("<?xml version='1.0' encoding = 'utf-8' ?>");
						writer.WriteLine("<Registry xmlns='www.froorider.ch'>");
						writer.WriteLine("</Registry>");
						writer.Close();
						stream.Close();
					}
				}

				return true;
			}
			catch (Exception e)
			{
				logger.Error(e.Message);
				return false;
			}
		}

		/// <summary>
		/// Loads the registry.
		/// </summary>
		/// <returns>True if the loading was sucessful. False if not.</returns>
		private bool LoadRegistry()
		{
			try
			{
				using (XmlTextReader reader = new XmlTextReader(FileManager.registryPath + FileManager.registryFile))
				{
					XmlSerializer serializer = new XmlSerializer(typeof(Registry));
					registry = serializer.Deserialize(reader) as Registry;
					reader.Close();
				}
				return true;
			}
			catch (InvalidOperationException ioe)
			{
				logger.Error(ioe.Message);
				return false;
			}
		}

		/// <summary>
		/// Saves the registry.
		/// </summary>
		private void SaveRegistry()
		{
			XmlTextWriter writer = new XmlTextWriter(FileManager.registryPath + FileManager.registryFile, Encoding.UTF8);
			XmlSerializer serializer = new XmlSerializer(typeof(Registry));
			serializer.Serialize(writer, registry);
			writer.Close();
		}

		/// <summary>
		/// Saves the business object.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="objectToSave">The object to save.</param>
		private void SaveBusinessObject<T>(T objectToSave) where T : BusinessObject
		{
			using (XmlTextWriter writer = new XmlTextWriter(FileManager.registryPath + objectToSave.ObjectIdentifier + ".dat", Encoding.UTF8))
			{
				XmlSerializer serializer = new XmlSerializer(typeof(T));
				serializer.Serialize(writer, objectToSave);
				writer.Close();
			}
		}
		#endregion
	}
}
