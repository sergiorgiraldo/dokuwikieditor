using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DokuwikiClient.Domain.Entities;

namespace DokuWikiClientTests
{
	/// <summary>
	/// Summary description for WikipageTests
	/// </summary>
	[TestClass]
	public class WikipageTests
	{
		[TestMethod]
		public void Constructor_CreationWithValidInput_CreationIsDoneWithoutErrors()
		{
			WikiAccount account = new WikiAccount();
			account.AccountName = "Test Account";
			Wikipage pageToTest = new Wikipage(account);

			Assert.IsNotNull(pageToTest);
			Assert.IsTrue(pageToTest.IsAssociatedWithAnAccount());
			Assert.AreEqual(pageToTest.GetAssociatedAccountName(), account.AccountName);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentNullException))]
		public void Constructor_CreationWithInvalidInput_ArgumentNullExceptionIsThrown()
		{
			Wikipage pageToTest = new Wikipage(null);
		}
	}
}
