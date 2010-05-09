// ========================================================================
// File:     IDokuWikiClient.cs
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DokuwikiClient.Domain.Entities;

namespace CH.Froorider.DokuwikiClient.Contracts
{
    /// <summary>
    /// Defines the methods a customer of this library can consume.
    /// </summary>
    public interface IDokuWikiClient
    {
        /// <summary>
		/// Loads the wiki accounts.
		/// </summary>
		/// <returns></returns>
        List<WikiAccount> LoadWikiAccounts();

        /// <summary>
		/// Saves the wiki account.
		/// </summary>
		/// <param name="accountToSave">The account to save.</param>
        void SaveWikiAccount(WikiAccount accountToSave);
    }
}
