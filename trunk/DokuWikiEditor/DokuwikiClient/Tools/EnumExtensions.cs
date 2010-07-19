// ========================================================================
// File:     EnumExtensions.cs
// 
// Author:   $Author$
// Date:     $LastChangedDate$
// Revision: $Revision$
// ========================================================================
// Copyright [2010] [$Author$]
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
using System.Reflection;
using System.ComponentModel;

namespace CH.Froorider.DokuwikiClient.Tools
{
	/// <summary>
	/// Extends the functionality of enumeration types.
	/// </summary>
	public static class EnumExtensions
	{
		/// <summary>
		/// Loads the description string, if the enum value is tagged with the <see cref="DescriptionAttribute"/>.
		/// </summary>
		/// <param name="value">The enum value to inspect.</param>
		/// <returns>A string containing the description of this enum value.</returns>
		public static string DescriptionOf(this Enum value)
		{
			FieldInfo field = value.GetType().GetField(value.ToString());
			DescriptionAttribute[] attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
			if (attributes.Length > 0)
			{
				return attributes[0].Description;
			}
			else
			{
				return value.ToString();
			}
		}

		/// <summary>
		/// Loads the enum value to a given description string.
		/// </summary>
		/// <param name="value">The description string.</param>
		/// <param name="enumType">Type of the enum to parse.</param>
		/// <returns>The enum constant associated with the given description string.</returns>
		/// <exception cref="ArgumentException">Is thrown when the string cannot be found in the given enum type.</exception>
		public static object EnumValueOf(this string value, Type enumType)
		{
			string[] names = Enum.GetNames(enumType);
			foreach (string name in names)
			{
				if (DescriptionOf((Enum)Enum.Parse(enumType, name)).Equals(value))
				{
					return Enum.Parse(enumType, name);
				}
			}

			throw new ArgumentException("The string is not a description or value of the specified enum.", "value");
		}
	}
}
