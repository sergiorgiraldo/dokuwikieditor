// ========================================================================
// File:     XmlRpcClient.cs
//
// Author:   $Author$
// Date:     $LastChangedDate$
// Revision: $Revision$
// ========================================================================
// Copyright [2009] [$Author$]
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// ========================================================================

using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Net;
using CH.Froorider.DokuwikiClient.Communication.Messages;
using CH.Froorider.DokuwikiClient.Contracts;
using CookComputing.XmlRpc;
using DokuwikiClient.Communication.XmlRpcMessages;
using log4net;

namespace DokuwikiClient.Communication
{
	/// <summary>
	/// Proxy class for the communication between the program and the XmlRpcServer.
	/// Wraps all the remote method calls in a common way.
	/// </summary>
	internal class XmlRpcClient : IDokuWikiProvider
	{
		#region fields

		private ILog logger = LogManager.GetLogger(typeof(XmlRpcClient));
		private IDokuWikiProxy clientProxy;

		#endregion

		#region Constructor

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlRpcClient"/> class.
		/// </summary>
		/// <param name="serverUri">The server URI.</param>
		/// <param name="userName">The username to use as login.</param>
		/// <param name="passWord">The password to use to authenticate.</param>
		/// <exception cref="ArgumentNullException"> Is thrown when
		///		<para><paramref name="userName"/> is a <see langword="null"/> reference</para>
		///		<para>- or -</para>			
		///		<para><paramref name="passWord"/> is a <see langword="null"/> reference.</para>
		/// </exception>
		public XmlRpcClient(Uri serverUri, string userName, string passWord)
			: this(serverUri)
		{
			if (String.IsNullOrEmpty(userName))
			{
				throw new ArgumentNullException("userName");
			}

			if (String.IsNullOrEmpty(passWord))
			{
				throw new ArgumentNullException("passWord");
			}

			this.clientProxy.Credentials = new NetworkCredential(userName, passWord);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="XmlRpcClient"/> class.
		/// </summary>
		/// <param name="serverUrl">The server URL.</param>
		/// <exception cref="ArgumentException">Is thrown when the passed server URL was not valid.</exception>
		public XmlRpcClient(Uri serverUrl)
		{
			try
			{
				this.clientProxy = XmlRpcProxyGen.Create<IDokuWikiProxy>();
				this.clientProxy.NonStandard = XmlRpcNonStandard.AllowNonStandardDateTime;
				this.clientProxy.Url = serverUrl.AbsoluteUri;
				Console.WriteLine("XmlRpc proxy to URL: " + serverUrl.AbsoluteUri + " generated.");

				// Network logging; Only used by developers in debug configuration when "LOG_NETWORK_TRAFFIC" is set.
				this.LogXmlTraffic();
			}
			catch (UriFormatException ufe)
			{
				Console.WriteLine(ufe);
				throw new ArgumentException("serverUrl", "Server URL is not valid. Cause: " + ufe.Message);
			}
		}

		#endregion

		#region Introspection API

		/// <summary>
		/// Gets the server capabilites.
		/// </summary>
		/// <returns>An instance of <see cref="Capability"/>.</returns>
		/// <exception cref="ArgumentException">Is thrown when the XmlRpc server is not enabled.</exception>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public Capability LoadServerCapabilites()
		{
			try
			{
				return this.clientProxy.LoadServerCapabilites();
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcIllFormedXmlException)
			{
				throw new ArgumentException("XmlRpc server not enabled.");
			}
			catch (XmlRpcFaultException xrpcfe)
			{
				if (xrpcfe.FaultCode == (int)XmlRpcFaultCodes.ServerErrorRequestedMethodNotFound)
				{
					throw new ArgumentException("Method not found.");
				}
				else
				{
					throw new ArgumentException(xrpcfe.Message);
				}
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Returns a list of methods implemented by the server.
		/// </summary>
		/// <returns>An array of strings listing all remote method names.</returns>
		/// <exception cref="ArgumentException">Is thrown when the XmlRpc server is not enabled.</exception>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public string[] ListServerMethods()
		{
			try
			{
				return this.clientProxy.SystemListMethods();
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcIllFormedXmlException)
			{
				throw new ArgumentException("XmlRpc server not enabled.");
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Gives you a list of possible methods implemented at the server.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <returns>An array containing all method signatures this remote method call offers.</returns>
		/// <exception cref="ArgumentException">Is thrown when the <paramref name="methodName"/> is unkown at remote host.</exception>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public object[] LoadMethodSignatures(string methodName)
		{
			try
			{
				return this.clientProxy.SystemMethodSignature(methodName);
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcFaultException xrfe)
			{
				logger.Warn(xrfe);
				throw new ArgumentException("Unknown remote method.", "methodName");
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Gives you a string describing the use of a certain method.
		/// </summary>
		/// <param name="methodName">Name of the method.</param>
		/// <returns>A description for the usage of this remote method.</returns>
		/// <exception cref="ArgumentException">Is thrown when the <paramref name="methodName"/> is unkown at remote host.</exception>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public string LoadMethodHelp(string methodName)
		{
			try
			{
				return this.clientProxy.SystemMethodHelp(methodName);
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcFaultException xrfe)
			{
				logger.Warn(xrfe);
				throw new ArgumentException("Unknown remote method.", "methodName");
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		#endregion

		#region IDokuWiki Members

		/// <summary>
		/// Gets a wikipage identified by it's name as raw wiki text.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <returns>The raw Wiki text for a page.</returns>
		/// <exception cref="ArgumentNullException">Is thrown when the passed argument is null.</exception>
		/// <exception cref="ArgumentException">Is thrown when the <paramref name="pageName"/> is unkown at remote host.</exception>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public string GetPage(string pageName)
		{
			string wikiText = String.Empty;

			if (String.IsNullOrEmpty(pageName))
			{
				throw new ArgumentNullException("pageName");
			}

			try
			{
				wikiText = this.clientProxy.GetPage(pageName);
				return wikiText;
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcFaultException xrfe)
			{
				logger.Warn(xrfe);
				throw new ArgumentException("Unknown wiki page.", "pageName");
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Gets all pages of the remote wiki.
		/// </summary>
		/// <returns>An array of <see cref="PageItem"/>s.</returns>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public PageItem[] GetAllPages()
		{
			try
			{
				return this.clientProxy.GetAllPages();
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Gets the a wiki page pre-formatted in HTML.
		/// </summary>
		/// <param name="pageName">Name of the wikpage.</param>
		/// <returns>XHTML represenation of the wiki page.</returns>
		/// <exception cref="ArgumentNullException">Is thrown when the passed argument is null.</exception>
		/// <exception cref="ArgumentException">Is thrown when the <paramref name="pageName"/> is unkown at remote host.</exception>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public string GetPageHtml(string pageName)
		{
			string wikiTextAsHtml = String.Empty;

			if (String.IsNullOrEmpty(pageName))
			{
				throw new ArgumentNullException("pageName");
			}

			try
			{
				wikiTextAsHtml = this.clientProxy.GetPageHtml(pageName);
				return wikiTextAsHtml;
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcFaultException xrfe)
			{
				logger.Warn(xrfe);
				throw new ArgumentException("Unknown wiki page.", "pageName");
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Puts / saves the modified wiki page at the remote wiki server.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <param name="rawWikiText">The raw wiki text.</param>
		/// <param name="putParameters">The put parameters.</param>
		/// <returns>True if all went good. False otherwise.</returns>
		/// <exception cref="CommunicationException">Is thrown when the Xml-Rpc mechanism had errors.</exception>
		/// <exception cref="WebException">Is thrown when the HTTP connection had errors.</exception>
		public bool PutPage(string pageName, string rawWikiText, PutParameters[] putParameters)
		{
			try
			{
				return this.clientProxy.PutPage(pageName, rawWikiText, putParameters);
			}
			catch (WebException we)
			{
				logger.Warn(we);
				throw;
			}
			catch (XmlRpcException xrpce)
			{
				logger.Warn(xrpce);
				throw new CommunicationException(xrpce.Message);
			}
		}

		/// <summary>
		/// Gets the page list.
		/// </summary>
		/// <param name="nameSpace">The name space.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public string[] GetPageList(string nameSpace, string[] options)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the doku wiki version.
		/// </summary>
		/// <returns></returns>
		public string GetDokuWikiVersion()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the time.
		/// </summary>
		/// <returns></returns>
		public int GetTime()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the XML RPC API version.
		/// </summary>
		/// <returns></returns>
		public int GetXmlRpcApiVersion()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Logins the specified user.
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="password">The password.</param>
		/// <returns></returns>
		public int Login(string user, string password)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Sets the locks.
		/// </summary>
		/// <param name="pagesToLockOrUnlock">The pages to lock or unlock.</param>
		/// <returns></returns>
		public string[] SetLocks(string[] pagesToLockOrUnlock)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the RPC version supported.
		/// </summary>
		/// <returns></returns>
		public string GetRpcVersionSupported()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the page version.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <param name="timestamp">The timestamp.</param>
		/// <returns></returns>
		public string GetPageVersion(string pageName, int timestamp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the page versions.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <param name="offset">The offset.</param>
		/// <returns></returns>
		public object[] GetPageVersions(string pageName, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the page info.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <returns></returns>
		public object[] GetPageInfo(string pageName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the page info version.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <param name="timestamp">The timestamp.</param>
		/// <returns></returns>
		public object[] GetPageInfoVersion(string pageName, int timestamp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the page HTML version.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <param name="timestamp">The timestamp.</param>
		/// <returns></returns>
		public string GetPageHtmlVersion(string pageName, int timestamp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Lists the links.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <returns></returns>
		public object[] ListLinks(string pageName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the back links.
		/// </summary>
		/// <param name="pageName">Name of the page.</param>
		/// <returns></returns>
		public object[] GetBackLinks(string pageName)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the recent changes.
		/// </summary>
		/// <param name="timestamp">The timestamp.</param>
		/// <returns></returns>
		public object[] GetRecentChanges(int timestamp)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the attachments.
		/// </summary>
		/// <param name="nameSpace">The name space.</param>
		/// <param name="attachmentOptions">The attachment options.</param>
		/// <returns></returns>
		public object[] GetAttachments(string nameSpace, object[] attachmentOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the attachment.
		/// </summary>
		/// <param name="mediaFileId">The media file id.</param>
		/// <returns></returns>
		public object GetAttachment(string mediaFileId)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Gets the attachment info.
		/// </summary>
		/// <param name="mediaFileId">The media file id.</param>
		/// <returns></returns>
		public object[] GetAttachmentInfo(string mediaFileId)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Puts the attachment.
		/// </summary>
		/// <param name="mediaFileId">The media file id.</param>
		/// <param name="mediaFileData">The media file data.</param>
		/// <param name="attachmentOptions">The attachment options.</param>
		public void PutAttachment(string mediaFileId, object mediaFileData, object attachmentOptions)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Deletes the attachment.
		/// </summary>
		/// <param name="mediaFileId">The media file id.</param>
		public void DeleteAttachment(string mediaFileId)
		{
			throw new NotImplementedException();
		}

		#endregion

		#region private methods

		/// <summary>
		/// Logs the XML traffic (The XML files raw as sent by the device) into a special folder.
		/// </summary>
		/// <remarks>Activate the "LOG_NETWORK_TRAFFIC" variable to enable this.</remarks>
		[Conditional("LOG_NETWORK_TRAFFIC")]
		private void LogXmlTraffic()
		{
			RequestResponseLogger dumper = new RequestResponseLogger
			{
				Directory = ConfigurationManager.AppSettings.Get("NETWORK_LOGGING_PATH")
			};

			if (!Directory.Exists(dumper.Directory))
			{
				Directory.CreateDirectory(dumper.Directory);
			}

			dumper.Attach(this.clientProxy);
		}

		#endregion
	}
}
