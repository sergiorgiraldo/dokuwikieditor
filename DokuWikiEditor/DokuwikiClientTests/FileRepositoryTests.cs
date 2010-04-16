using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DokuwikiClient.Domain.Entities;
using CH.Froorider.DokuwikiClient.Persistence;

namespace DokuWikiClientTests
{
	[TestClass]
	public class FileRepositoryTests
	{
		[TestMethod]
		public void Store_StoreAWikiPage_BusinessObjectShouldBeStoredWithoutErrors()
		{
			Wikipage page = new Wikipage();
			page.WikiPageContent = "ölkjölkfsajdölkjafsd";
			page.WikiPageName = "start.php";

			FileRepository repository = new FileRepository();
			string identifier = repository.Store<Wikipage>(page);
			Assert.IsFalse(String.IsNullOrEmpty(identifier));
			Assert.AreEqual(identifier, page.ObjectIdentifier);
		}

		[TestMethod]
		public void GetIdentifiers_StandardProcedure_AListOfStringsIsReturned()
		{
			WikiAccount account = new WikiAccount();
			account.AccountName = "foobar";
			account.LoginName = "barfoo";
			account.Password = "A secret password";
			account.WikiUrl = new Uri("http://some.where.over/the/Rainbow");

			FileRepository repository = new FileRepository();
			string targetIdentifier = repository.Store<WikiAccount>(account);

			foreach (string identifier in repository.GetIdentifiers())
			{
				Assert.IsFalse(String.IsNullOrEmpty(identifier));
				Assert.AreEqual(targetIdentifier, identifier);
			}
		}

		[TestMethod]
		public void Load_LoadAWikiAccount_IsLoadedWithoutErrors()
		{
			WikiAccount expectedAccount = new WikiAccount();
			expectedAccount.AccountName = "doodle";
			expectedAccount.LoginName = "Dödel";
			expectedAccount.Password = "Very secret password";
			expectedAccount.WikiUrl = new Uri("http://www.doodle.ch/tinyurl/349");

			FileRepository repository = new FileRepository();
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

			FileRepository repository = new FileRepository();
			string firstIdentifier = repository.Store<Wikipage>(pageToStore);

			pageToStore.WikiPageContent = "Lorem ipsum dolor sit amet, consetetur sadipscing elitr," +
				" sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.";
			string secondIdentifier = repository.Store<Wikipage>(pageToStore);

			Assert.AreEqual(firstIdentifier, secondIdentifier);
		}
	}
}
