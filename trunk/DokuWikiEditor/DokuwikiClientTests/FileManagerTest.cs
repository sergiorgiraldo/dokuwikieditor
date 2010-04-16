namespace DokuWikiClientTests
{
	using System;
	using System.Collections.Generic;
	using DokuwikiClient.Domain.Entities;
	using DokuwikiClient.Persistence;
	using Microsoft.VisualStudio.TestTools.UnitTesting;

	/// <summary>
	///This is a test class for FileManagerTest and is intended
	///to contain all FileManagerTest Unit Tests
	///</summary>
	[TestClass]
	public class FileManagerTest
	{
		#region Fields

		private static WikiAccount commonAccount = new WikiAccount();

		#endregion Fields

		#region Methods

		[ClassInitialize]
		public static void InitializeTestEnvironment(TestContext context)
		{
			commonAccount.WikiUrl = new Uri("http://wiki.froorider.ch/lib/exe/xmlrpc.php");
			commonAccount.AccountName = "Froorider's wiki";
			commonAccount.LoginName = "foobar";
			commonAccount.Password = "barfoo";
		}

		[TestMethod]
		public void Register_SaveANewCommonBO_Successful()
		{
			FileManager manager = new FileManager();
			manager.Save<WikiAccount>(commonAccount);
			List<WikiAccount> account = manager.LoadObjects<WikiAccount>(typeof(WikiAccount).Name);
			Assert.IsTrue(account.Count != 0);

		}

		#endregion Methods
	}
}