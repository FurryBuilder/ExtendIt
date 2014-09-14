﻿//////////////////////////////////////////////////////////////////////////////////
//
// The MIT License (MIT)
//
// Copyright (c) 2014 Furry Builder
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
//
//////////////////////////////////////////////////////////////////////////////////

using System;
using ExtendIt.Contracts;
using JetBrains;

namespace ExtendIt
{
	/// <summary>
	/// Base class uppon which specific extension points inherits.
	/// 
	/// Encapsulate the scope of an extension point.
	/// </summary>
	/// <typeparam name="T">Extension's scope. This can also be object or forwarded using generics.</typeparam>
	public class ExtensionPointBase<T> : IExtensionPoint
	{
		private readonly T _value;
		private readonly Type _type;

		protected ExtensionPointBase([CanBeNull] T value)
		{
			_value = value;
		}

		protected ExtensionPointBase([NotNull] Type type)
		{
			if (!typeof(T).IsAssignableFrom(type))
			{
				throw new ArgumentException("type");
			}

			_type = type;
		}

		public T ExtendedValue
		{
			get { return _value; }
		}

		object IExtensionPoint.ExtendedValue
		{
			get { return _value; }
		}

		public Type ExtendedType
		{
			get { return _type ?? (_value == null ? typeof(T) : _value.GetType()); }
		}
	}
}