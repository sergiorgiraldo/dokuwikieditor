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
		public void Store_StoreAWikiAccount_BusinessObjectShouldBeStoredWithoutErrors()
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
	}
}
