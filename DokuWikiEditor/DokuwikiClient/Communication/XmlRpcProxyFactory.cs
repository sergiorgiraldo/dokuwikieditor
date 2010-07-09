using System;
using CH.Froorider.DokuwikiClient.Contracts;
using DokuwikiClient.Communication;

namespace CH.Froorider.DokuwikiClient.Communication
{
	/// <summary>
	/// Creates instances of <see cref="IDokuWikiProvider"/>s. These providers gives you access on the servers functionality.
	/// </summary>
	public class XmlRpcProxyFactory
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="XmlRpcProxyFactory"/> class.
		/// </summary>
		/// <remarks>Is private to avoid the cration of instances of this factory.</remarks>
		private XmlRpcProxyFactory()
		{
		}

		/// <summary>
		/// Creates the communication proxy to a given server URI.
		/// </summary>
		/// <param name="uriToServer">The URI to server.</param>
		/// <returns>An instance of <see cref="IDokuWikiProvider"/> giving you access on the server's functionality.</returns>
		/// <exception cref="ArgumentNullException">Is thrown when <paramref name="uriToServer"/> is a <see langword="null"/> reference.</exception>
		public static IDokuWikiProvider CreateCommunicationProxy(Uri uriToServer)
		{
			if (uriToServer == null)
			{
				throw new ArgumentNullException("uriToServer");
			}

			return new XmlRpcClient(uriToServer);
		}

		/// <summary>
		/// Creates the secure communication proxy.
		/// </summary>
		/// <param name="uriToServer">The URI to server.</param>
		/// <param name="userName">Name of the user.</param>
		/// <param name="passWord">The pass word.</param>
		/// <returns>An instance of <see cref="IDokuWikiProvider"/> giving you access on the server's functionality.</returns>
		/// <exception cref="ArgumentNullException"> Is thrown when
		///		<para><paramref name="userName"/> is a <see langword="null"/> reference</para>
		///		<para>- or -</para>			
		///		<para><paramref name="passWord"/> is a <see langword="null"/> reference.</para>
		/// </exception>
		public static IDokuWikiProvider CreateSecureCommunicationProxy(Uri uriToServer, string userName, string passWord)
		{
			return new XmlRpcClient(uriToServer, userName, passWord);
		}
	}
}
