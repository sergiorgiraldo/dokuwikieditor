using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DokuwikiClient.Domain.Entities;
using CH.Froorider.DokuwikiClient.Persistence;
using CH.Froorider.DokuwikiClient.Contracts;
using CH.Froorider.Codeheap.Persistence;
using System.IO;
using CH.Froorider.Codeheap.Domain;

namespace DokuWikiClientTests
{
	[TestClass]
	public class FileRepositoryTests
	{
		#region set-up and tear-down

		[ClassCleanup]
		public static void TearDown()
		{
			DirectoryInfo directory = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "//DokuWikiStore//");
			directory.Delete(true);
		}

		#endregion

		[TestMethod]
		public void Constructor_RepositoryIsCreated_CreationWithoutErrors()
		{
			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);

			try
			{
				foreach (string identifier in repository.GetIdentifiers())
				{
					Console.WriteLine("Loaded business object with identifier: {0}" + identifier);
				}
			}
			catch (WikiRepositoryException wre)
			{
				Console.WriteLine(wre.Message);
			}
		}

		#region store tests

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
		public void Store_StoreAWikiObjectUsingItsOwnIdenitfier_ShouldBeDoneWithoutErrors()
		{
			WikiAccount myOwnAccount = new WikiAccount();
			myOwnAccount.LoginName = "jfdsah";
			myOwnAccount.Password = "jlkafhsgh";
			myOwnAccount.WikiUrl = new Uri("ftp://ftp.google.com/");

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string accountId = "MyOwnAccount";
			repository.Store<WikiAccount>(myOwnAccount, accountId);

			WikiAccount reloadedAccount = repository.Load<WikiAccount>(accountId);
			Assert.IsNotNull(reloadedAccount);
			Assert.AreEqual(myOwnAccount, reloadedAccount);
		}

		#endregion

		#region GetIdentifiers tests

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
		[ExpectedException(typeof(WikiRepositoryException))]
		public void GetIdenitfiers_NoIdentifiersAtAll_WikiRepositoryExceptionIsThrown()
		{
			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);

			//Possible that there are already wiki objects loaded, because of the other tests.
			//If not the exception is already thrown here.
			foreach (string identifier in repository.GetIdentifiers())
			{
				repository.Delete(identifier);
			}

			//Now it should definitly throw the exception
			repository.GetIdentifiers();
		}

		#endregion

		#region Load tests

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
		public void Load_LoadAManualPersistedWikiObject_IsLoadedAndAddedInRepository()
		{
			WikiAccount manualStoredAccount = new WikiAccount();
			manualStoredAccount.AccountName = "doodleidu";
			manualStoredAccount.LoginName = "Dödeldidu";
			manualStoredAccount.Password = "Very very secret password";
			manualStoredAccount.WikiUrl = new Uri("http://www.doodle.ch/tinyurl/987436219874");

			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			string targetIdenitfier = manualStoredAccount.Serialize(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "//DokuWikiStore//", ".wiki");

			WikiAccount loadedAccount = repository.Load<WikiAccount>(targetIdenitfier);
			Assert.IsNotNull(loadedAccount);
			Assert.AreEqual(manualStoredAccount, loadedAccount);

			string addedIdentifier = repository.GetIdentifiers().First(x => x.Equals(targetIdenitfier));
			Assert.AreEqual(addedIdentifier, targetIdenitfier);
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

		#endregion

		#region Delete tests

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

		[TestMethod]
		[ExpectedException(typeof(WikiRepositoryException))]
		public void Delete_TryToDeleteAWikiObjectWhichDoesNotExists_AWikiRepositoryExceptionShouldBeThrown()
		{
			IWikiRepository repository = WikiRepositoryFactory.CreateRepository(WikiRepositoryType.FileRepository);
			repository.Delete("foo_bar_li");
		}

		#endregion
	}
}
