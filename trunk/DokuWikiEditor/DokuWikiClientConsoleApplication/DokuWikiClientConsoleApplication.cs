using System;
using System.Text;
using CH.Froorider.DokuwikiClient.Communication;
using CH.Froorider.DokuwikiClient.Communication.Messages;
using CH.Froorider.DokuwikiClient.Contracts;
using CH.Froorider.DokuwikiClient.Persistence;
using CH.Froorider.DokuwikiClient.Tools;
using CH.Froorider.DokuWikiClientConsoleApplication.Commands;
using DokuwikiClient.Communication.XmlRpcMessages;
using DokuWikiClientConsoleApplication.Commands;
using log4net;
using log4net.Config;

namespace CH.Froorider.DokuWikiClientConsoleApplication
{
	/// <summary>
	/// Console application to show how to use the library.
	/// </summary>
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

			StringBuilder menuString = new StringBuilder();
			menuString.AppendLine("Menu: ");

			foreach (CommandName enumValue in Enum.GetValues(typeof(CommandName)))
			{
				menuString.AppendLine(((int)enumValue) + " := " + enumValue.DescriptionOf());
			}
			menuString.AppendLine();

			do
			{
				Console.Write(menuString.ToString());
				string input = Console.ReadLine();
				CommandName commandValue = (CommandName)Enum.Parse(typeof(CommandName), input);

				if (commandValue == CommandName.ExitApplication)
				{
					exitLoop = true;
				}
				else
				{
					try
					{
						CommandFactory.CreateCommand(commandValue, communicationClient, dokuWikiClient).Execute();
					}
					catch (NotImplementedException nie)
					{
						Console.WriteLine(nie.Message);
					}
				}

				Console.WriteLine("----------------------------------------------------");
			}
			while (!exitLoop);
		}

		#endregion Methods
	}
}
