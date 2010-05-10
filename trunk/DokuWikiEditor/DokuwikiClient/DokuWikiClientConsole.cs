namespace DokuwikiClient
{
	using System;

	using DokuwikiClient.Communication;
	using DokuwikiClient.Communication.XmlRpcMessages;

	using log4net;
	using log4net.Config;
	using CH.Froorider.DokuwikiClient.Contracts;
	using CH.Froorider.DokuwikiClient.Communication;

	class DokuWikiClientConsole
	{
		#region Fields

		private static bool exitLoop = false;
		private static ILog logger = LogManager.GetLogger(typeof(DokuWikiClientConsole));

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

		static void Main(string[] args)
		{
			AppDomain.CurrentDomain.UnhandledException += DokuWikiClientConsole.OnUnhandledException;
			XmlConfigurator.Configure();
			IDokuWikiProvider client;

			Console.WriteLine("Connecting to wiki.");
			if (args.Length != 0 && !String.IsNullOrEmpty(args[0]))
			{
				client = XmlRpcProxyFactory.CreateCommunicationProxy(new Uri(args[0]));
			}
			else
			{
				client = XmlRpcProxyFactory.CreateSecureCommunicationProxy(new Uri("https://wiki.froorider.ch/lib/exe/xmlrpc.php"), "foobar", "barfoo");
			}

			try
			{
				Console.WriteLine("Listing server methods.");
				foreach (String serverMethodName in client.ListServerMethods())
				{
					Console.WriteLine("Method name: {0}", serverMethodName);
				}
				Console.WriteLine("----------------------------------------------------");

				Console.WriteLine("Listing server capabilites.");
				Capability serverCapability = client.LoadServerCapabilites();
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
			}
			catch (ArgumentException ae)
			{
				Console.WriteLine(ae.Message);
				Console.WriteLine("Press enter to exit.");
				Console.ReadLine();
				Environment.Exit(0);
			}

			Console.WriteLine("----------------------------------------------------");

			do
			{
				Console.Write(" 0 := Exit application\n 1 := Get Wikipage \n 2 := GetAllPages \n 3 := Get WikiPage as HTML \n");
				string input = Console.ReadLine();
				if (input.Equals("0"))
				{
					exitLoop = true;
				}
				else if (input.Equals("1"))
				{
					Console.WriteLine("Enter pagename of wikipage.");
					string pageName = Console.ReadLine();
					Console.WriteLine("Wikitext of page: \n" + client.GetPage(pageName));
				}
				else if (input.Equals("2"))
				{
					Console.WriteLine("Getting all page items.");
					PageItem[] pages = client.GetAllPages();
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
					Console.WriteLine("Page as HTML: \n" + client.GetPageHtml(pageName));
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