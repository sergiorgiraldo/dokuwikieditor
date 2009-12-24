// ========================================================================
// File:     IDokuWikiClient.cs
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

using CookComputing.XmlRpc;
using DokuwikiClient.Communication.XmlRpcMessages;

namespace DokuwikiClient.Communication
{
	/// <summary>
	/// Definition of the remote procedure calls of the DokuWikiServer. 
	/// </summary>
	public interface IDokuWikiClient : IXmlRpcProxy
	{
		#region Introspection API

		/// <summary>
		/// Gets the capabilities the Xml Rpc server offers.
		/// </summary>
		/// <returns>An instance of <see cref="Capability"/> which tells you which Xml Rpc spcifications in which versions are implemented.</returns>
		[XmlRpcMethod("system.getCapabilities")]
		Capability GetCapabilities();

		#endregion

		#region DokuWiki specific remote methods

		/// <summary>
		/// Lists all pages within a given namespace.
		/// </summary>
		/// <param name="nameSpace">The namespace which should be searched.</param>
		/// <param name="options">The options for the php method searchAllPages().</param>
		/// <returns>
		/// A string of the page item names in this namespace.
		/// </returns>
		/// <remarks>The options are passed directly to the PHP method searchAllPages().</remarks>
		[XmlRpcMethod("dokuwiki.getPagelist")]
		string[] GetPageList(string nameSpace, string[] options);

		/// <summary>
		/// Gets the doku wiki version.
		/// </summary>
		/// <returns>A string containing the version number of the remote dokuwiki.</returns>
		[XmlRpcMethod("dokuwiki.getVersion")]
		string GetDokuWikiVersion();

		/// <summary>
		/// Gets the current time at the remote wiki server as Unix timestamp. 
		/// </summary>
		/// <returns>An integer value indicating the server time.</returns>
		[XmlRpcMethod("dokuwiki.getTime")]
		int GetTime();

		/// <summary>
		/// Gets the XML RPC API version.
		/// </summary>
		/// <remarks>
		/// Returns the XML RPC interface version of the remote Wiki. This is DokuWiki implementation specific and independent 
		/// of the supported standard API version returned by wiki.getRPCVersionSupported().
		/// </remarks>
		/// <returns>An integer representing the version number.</returns>
		[XmlRpcMethod("dokuwiki.getXMLRPCAPIVersion")]
		int GetXmlRpcApiVersion();

		/// <summary>
		/// Logins the specified user.
		/// </summary>
		/// <param name="user">The username as a string.</param>
		/// <param name="password">The password of the user as a string.</param>
		/// <returns>An integer describing the status of the login - request.</returns>
		[XmlRpcMethod("dokuwiki.login")]
		int Login(string user, string password);

		/// <summary>
		/// Locks or unlocks a bunch of wiki pages.
		/// </summary>
		/// <param name="pagesToLockOrUnlock">A List of two lists of page ids.</param>
		/// <returns>Array with 4 lists of pageids.</returns>
		[XmlRpcMethod("dokuwiki.setLocks")]
		string[] SetLocks(string[] pagesToLockOrUnlock);

		#endregion

		#region common remote methods

        /// <summary>
        /// Gets the supported Xml Rpc version.
        /// </summary>
        /// <returns>2 with the supported RPC API Version.</returns>
        [XmlRpcMethod("wiki.getRPCVersionSupported")]
        string GetRpcVersionSupported();

        /// <summary>
        /// Gets a wikipage identified by it's name as raw wiki text.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>The raw Wiki text for a page.</returns>
        [XmlRpcMethod("wiki.getPage")]
        string GetPage(string pageName);

        /// <summary>
        /// Gets a wikipage on a certain revison status.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="timestamp">The timestamp when the page was revised.</param>
        /// <returns>The raw Wiki text for a specific revision of a Wiki page.</returns>
        [XmlRpcMethod("wiki.getPageVersion")]
        string GetPageVersion(string pageName, int timestamp);

        /// <summary>
        /// Gets the page versions.
        /// </summary>
        /// <remarks>
        /// The number of pages in the result is controlled via the recent configuration setting. 
        /// The offset can be used to list earlier versions in the history. 
        /// </remarks>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The available versions of a Wiki page.</returns>
        [XmlRpcMethod("wiki.getPageVersions")]
        object[] GetPageVersions(string pageName, int offset);

        /// <summary>
        /// Gets the page info.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>Information about a Wiki page.</returns>
        [XmlRpcMethod("wiki.getPageInfo")]
        object[] GetPageInfo(string pageName);

        /// <summary>
        /// Gets the page info version.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns>Information about a specific version of a Wiki page. </returns>
        [XmlRpcMethod("wiki.getPageInfoVersion")]
        object[] GetPageInfoVersion(string pageName, int timestamp);

        /// <summary>
        /// Gets a certain wikipage pre-rendered as HTML.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>The rendered XHTML body of a Wiki page.</returns>
        [XmlRpcMethod("wiki.getPageHTML")]
        string GetPageHtml(string pageName);

        /// <summary>
        /// Gets a certain wikipage at a certain point of time rendered as HTML.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="timestamp">The timestamp of the revision.</param>
        /// <returns>The rendered HTML of a specific version of a Wiki page.</returns>
        [XmlRpcMethod("wiki.getPageHTMLVersion")]
        string GetPageHtmlVersion(string pageName, int timestamp);

        /// <summary>
        /// Saves a wikipage at remote server.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <param name="rawWikiText">The raw wiki text.</param>
        /// <param name="putParameters">The put parameters.</param>
        /// <returns>True if the page could be saved. False otherwise.</returns>
        [XmlRpcMethod("wiki.putPage")]
        bool PutPage(string pageName, string rawWikiText, PutParameters[] putParameters);

        /// <summary>
        /// Lists all the links contained in a wikipage.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>A list of all links contained in a Wiki page.</returns>
        [XmlRpcMethod("wiki.listLinks")]
        object[] ListLinks(string pageName);

        /// <summary>
        /// Gets all pages of the remote wiki in one big array.
        /// </summary>
        /// <returns>A list of all Wiki pages in the remote Wiki as a <see cref="PageItem"/> array.</returns>
        [XmlRpcMethod("wiki.getAllPages")]
        PageItem[] GetAllPages();

        /// <summary>
        /// Gets all backlinks of a certain wikipage.
        /// </summary>
        /// <param name="pageName">Name of the page.</param>
        /// <returns>A list of backlinks of a Wiki page.</returns>
        [XmlRpcMethod("wiki.getBackLinks")]
        object[] GetBackLinks(string pageName);

        /// <summary>
        /// Gets the recent changes made at the remote wiki since a given timestamp.
        /// </summary>
        /// <param name="timestamp">The timestamp.</param>
        /// <returns>A list of recent changes since given timestamp.</returns>
        [XmlRpcMethod("wiki.getRecentChanges")]
        object[] GetRecentChanges(int timestamp);

        /// <summary>
        /// Gets all attachments (media files) of a specific namespace.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="attachmentOptions">The attachment options.</param>
        /// <returns>A list of media files in a given namespace.</returns>
        [XmlRpcMethod("wiki.getAttachments")]
        object[] GetAttachments(string nameSpace, object[] attachmentOptions);

        /// <summary>
        /// Gets a specific media file.
        /// </summary>
        /// <param name="mediaFileId">The media file id.</param>
        /// <returns>The binary data of a media file encoded in Base64.</returns>
        [XmlRpcMethod("wiki.getAttachment")]
        object GetAttachment(string mediaFileId);

        /// <summary>
        /// Gets the information about an attachment.
        /// </summary>
        /// <param name="mediaFileId">The media file id.</param>
        /// <returns>Information about a media file.</returns>
        [XmlRpcMethod("wiki.getAttachmentInfo")]
        object[] GetAttachmentInfo(string mediaFileId);

        /// <summary>
        /// Saves an attachment at the remote side.
        /// </summary>
        /// <param name="mediaFileId">The media file id.</param>
        /// <param name="mediaFileData">The media file data encoded as Base64.</param>
        /// <param name="attachmentOptions">The attachment options.</param>
        [XmlRpcMethod("wiki.putAttachment")]
        void PutAttachment(string mediaFileId, object mediaFileData, object attachmentOptions);

        /// <summary>
        /// Deletes a file. Fails if the file is still referenced from any page in the wiki.
        /// </summary>
        /// <param name="mediaFileId">The media file id.</param>
        [XmlRpcMethod("wiki.deleteAttachment")]
        void DeleteAttachment(string mediaFileId);
		#endregion
	}
}
