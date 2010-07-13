using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using log4net.Config;
using CH.Froorider.DokuwikiClient.Contracts;
using CH.Froorider.DokuwikiClient.Communication;
using DokuwikiClient.Communication.XmlRpcMessages;
using CH.Froorider.DokuwikiClient.Persistence;
using DokuwikiClient.Domain.Entities;
using CH.Froorider.DokuwikiClient.Communication.Messages;

namespace CH.Froorider.DokuWikiClientConsoleApplication
{
	public class DokuWikiClientConsoleApplication
	{
		#region Fields

		private static bool exitLoop = false;
		private static ILog logger = LogManager.GetLogger(typeof(DokuWikiClientConsoleApplication));

		#endregion Fields

		#region Methods

		/// <summary>
		/// Called when an [unhandled exception] occures.
		/// </summary>
		/// <param name="sender">The sender of the event.</param>
		/// <param name="args">The <see cref="System.UnhandledExceptionEventArgs"/> instance containing the event data.</param>
		public static void OnUnhandledException(object sender, UnhandledExceptionEventArgs args)
		{
			Exception exc = (Exception)args.ExceptionObject;
			logger.Fatal("Unhandled exception caught.", exc);
			Console.WriteLine("Press enter to exit.");
			Console.ReadLine();
			Environment.Exit(-1);
		}

		/// <summary>
		/// Mains the specified args.
		/// </summary>
		/// <param name="args">The args.</param>
		public static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += DokuWikiClientConsoleApplication.OnUnhandledException;
			XmlConfigurator.Configure();
			IDokuWikiProvider communicationClient;
			IDokuWikiClient dokuWikiClient = DokuWikiClientFactory.CreateDokuWikiClient();

			Uri uriToWiki;
			string password = string.Empty;
			string username = string.Empty;

			Console.WriteLine("Connecting to wiki.");
			if (args.Length > 0 && !String.IsNullOrEmpty(args[0]))
			{
				uriToWiki = new Uri(args[0]);
				if (args.Length == 3 && !String.IsNullOrEmpty(args[1]) && !String.IsNullOrEmpty(args[2]))
				{
					password = args[2];
					username = args[1];
					communicationClient = XmlRpcProxyFactory.CreateSecureCommunicationProxy(uriToWiki, username, password);
				}
				else
				{
					communicationClient = XmlRpcProxyFactory.CreateCommunicationProxy(uriToWiki);
				}
			}
			else
			{
				Console.WriteLine("Usage: DokuWikiClientConsoleApplication <URI of wiki> [username] [password]");
				Console.ReadLine();
				return;
			}

			try
			{
				Console.WriteLine("----------------------------------------------------");
				Console.WriteLine("Listing server capabilites.");

				Capability serverCapability = communicationClient.LoadServerCapabilites();
				if (serverCapability.XmlRpcSpecification != null)
				{
					Console.WriteLine("Xml-Rpc Version: {0}", serverCapability.XmlRpcSpecification.SpecificationVersion);
				}
				if (serverCapability.IntrospectionSpecification != null)
				{
					Console.WriteLine("Introspection Version: {0}", serverCapability.IntrospectionSpecification.SpecificationVersion);
				}
				if (serverCapability.MultiCallSpecification != null)
				{
					Console.WriteLine("Multicall Version: {0}", serverCapability.MultiCallSpecification.SpecificationVersion);
				}
				if (serverCapability.FaultCodesSpecification != null)
				{
					Console.WriteLine("Fault codes Version: {0}", serverCapability.FaultCodesSpecification.SpecificationVersion);
				}

				Console.WriteLine("----------------------------------------------------");

				Console.WriteLine("Listing server methods.");
				foreach (String serverMethodName in communicationClient.ListServerMethods())
				{
					Console.WriteLine("Method name: {0}", serverMethodName);
				}
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine(ae.Message);
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
				Environment.Exit(0);
			}
			catch (CommunicationException ce)
			{
				Console.WriteLine("Communication error. Cause: {0}", ce.Message);
			}

			Console.WriteLine("----------------------------------------------------");

			do
			{
				Console.Write(" 0 := Exit application\n 1 := Get Wikipage \n 2 := GetAllPages \n 3 := Get WikiPage as HTML \n");
				Console.Write(" 4 := List all stored wiki accounts. \n");
				string input = Console.ReadLine();
				if (input.Equals("0"))
				{
					exitLoop = true;
				}
				else if (input.Equals("1"))
				{
					Console.WriteLine("Enter pagename of wikipage.");
					string pageName = Console.ReadLine();
					Console.WriteLine("Wikitext of page: \n" + communicationClient.GetPage(pageName));
				}
				else if (input.Equals("2"))
				{
					Console.WriteLine("Getting all page items.");
					PageItem[] pages = communicationClient.GetAllPages();
					foreach (PageItem pageItem in pages)
					{
						Console.WriteLine("ID: " + pageItem.Identificator);
						Console.WriteLine("LastModified: " + pageItem.LastModified);
						Console.WriteLine("Permissions: " + pageItem.Permissions);
						Console.WriteLine("Size: " + pageItem.Size);
						Console.WriteLine();
					}
				}
				else if (input.Equals("3"))
				{
					Console.WriteLine("Enter pagename of wikipage.");
					string pageName = Console.ReadLine();
					Console.WriteLine("Page as HTML: \n" + communicationClient.GetPageHtml(pageName));
				}
				else if (input.Equals("4"))
				{
					List<WikiAccount> accountList = dokuWikiClient.LoadWikiAccounts();
					foreach (WikiAccount account in accountList)
					{
						Console.WriteLine("Loaded account : {0}" + account.AccountName);
					}
				}
				else
				{
					Console.WriteLine("Unknown command.");

				}
				Console.WriteLine("----------------------------------------------------");
			}
			while (!exitLoop);
		}

		#endregion Methods
	}
}
