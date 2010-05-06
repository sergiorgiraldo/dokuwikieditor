using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DokuwikiClient.Domain.Entities;
using CH.Froorider.DokuwikiClient.Persistence;
using CH.Froorider.DokuwikiClient.Contracts;

namespace DokuWikiClientTests
{
	[TestClass]
	public class FileRepositoryTests
	{
		[TestMethod]
		public void Constructor_RepositoryIsCreated_CreationWithoutErrors()
		{
			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			foreach (string identifier in repository.GetIdentifiers())
			{
				Console.WriteLine("Loaded business object with identifier: {0}" + identifier);
			}
		}

		[TestMethod]
		public void Store_StoreAWikiPage_BusinessObjectShouldBeStoredWithoutErrors()
		{
			Wikipage page = new Wikipage();
			page.WikiPageContent = "ölkjölkfsajdölkjafsd";
			page.WikiPageName = "start.php";

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string identifier = repository.Store<Wikipage>(page);
			Assert.IsFalse(String.IsNullOrEmpty(identifier));
			Assert.AreEqual(identifier, page.ObjectIdentifier);
		}

		[TestMethod]
		public void GetIdentifiers_StandardProcedure_AListOfStringsIsReturned()
		{
			bool found = false;

			WikiAccount account = new WikiAccount();
			account.AccountName = "foobar";
			account.LoginName = "barfoo";
			account.Password = "A secret password";
			account.WikiUrl = new Uri("http://some.where.over/the/Rainbow");

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string targetIdentifier = repository.Store<WikiAccount>(account);

			foreach (string identifier in repository.GetIdentifiers())
			{
				Assert.IsFalse(String.IsNullOrEmpty(identifier));
				if (targetIdentifier == identifier)
				{
					found = true;
					break;
				}
			}

			Assert.IsTrue(found);
		}

		[TestMethod]
		public void Load_LoadAWikiAccount_IsLoadedWithoutErrors()
		{
			WikiAccount expectedAccount = new WikiAccount();
			expectedAccount.AccountName = "doodle";
			expectedAccount.LoginName = "Dödel";
			expectedAccount.Password = "Very secret password";
			expectedAccount.WikiUrl = new Uri("http://www.doodle.ch/tinyurl/349");

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string targetIdenitfier = repository.Store<WikiAccount>(expectedAccount);

			WikiAccount targetAccount = repository.Load<WikiAccount>(targetIdenitfier);
			Assert.AreEqual(targetAccount, expectedAccount);
			Assert.IsTrue(targetAccount.Equals(expectedAccount));
		}

		[TestMethod]
		public void Store_StoreAModifiedWikiAccount_IdentifierShouldStayTheSame()
		{
			Wikipage pageToStore = new Wikipage();
			pageToStore.WikiPageName = "first.php";
			pageToStore.WikiPageContent = "Lorem ipsum dolor sit amet";

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string firstIdentifier = repository.Store<Wikipage>(pageToStore);

			pageToStore.WikiPageContent = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr," +
				" sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.";
			string secondIdentifier = repository.Store<Wikipage>(pageToStore);

			Assert.AreEqual(firstIdentifier, secondIdentifier);
		}

		[TestMethod]
		public void Load_LoadAPersistedWikiPage_ShouldBeLoadedWithoutErrors()
		{
			Wikipage pageToStore = new Wikipage();
			pageToStore.WikiPageName = "first.php";
			pageToStore.WikiPageContent = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr ...";

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string pageIdentifier = repository.Store<Wikipage>(pageToStore);
			repository = null;
			repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);

			Wikipage loadedPage = repository.Load<Wikipage>(pageIdentifier);
			Assert.AreEqual(pageToStore, loadedPage);
		}

		[TestMethod]
		public void Delete_SaveAndThenDeleteAWikiPage_ShouldBeDoneWithoutErrors()
		{
			Wikipage pageToStore = new Wikipage();
			pageToStore.WikiPageName = "delete.php";
			pageToStore.WikiPageContent = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr sed diam nonumy eirmod tempor invidunt ut labore et dolore ...";

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string pageIdentifier = repository.Store<Wikipage>(pageToStore);
			repository.Delete(pageIdentifier);
		}
	}
}
