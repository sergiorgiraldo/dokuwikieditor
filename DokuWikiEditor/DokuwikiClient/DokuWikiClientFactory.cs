// ========================================================================
// File:     DokuWikiClientFactory.cs
// 
// Author:   $Author: froorider@gmail.com $
// Date:     $LastChangedDate: 2010-05-07 18:39:21 +0200 (Fri, 07 May 2010) $
// Revision: $Revision: 25 $
// ========================================================================
// Copyright [2010] [$Author: froorider@gmail.com $]
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
using CH.Froorider.DokuwikiClient.Contracts;
using DokuwikiClient.Domain.Entities;

namespace CH.Froorider.DokuwikiClient.Persistence
{
    /// <summary>
    /// Creates instances of <see cref="IDokuWikiClient"/> according to the given <see cref="WikiAccount"/>.
    /// </summary>
    public class DokuWikiClientFactory
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="DokuWikiClientFactory"/> class.
        /// </summary>
        /// <remarks>Is private to ensure nobody can create an instance of this class accidentally.</remarks>
        private DokuWikiClientFactory()
        {
        }

        /// <summary>
        /// Creates the doku wiki client.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns></returns>
        public static IDokuWikiClient CreateDokuWikiClient(WikiAccount account)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account");
            }

            DokuWikiClient client = new DokuWikiClient();
            client.InitializeDokuWikiClient(account);
            return client;
        }
    }
}