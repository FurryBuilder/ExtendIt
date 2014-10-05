//////////////////////////////////////////////////////////////////////////////////
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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendIt.Tests
{
	[TestClass]
	public class GivenReferenceTypeExtensionPoint
	{
		private class StubExtendable { }

		private class StubInheritingExtendable : StubExtendable { }

		private class MockExtensionPoint : ExtensionPointBase<StubExtendable>
		{
			public MockExtensionPoint(StubExtendable value) : base(value) { }
			public MockExtensionPoint(Type type) : base(type) { }
		}

		[TestMethod]
		public void WhenExtendByValue_ThenValueIsSet()
		{
			var v = new StubExtendable();

			Assert.AreSame(v, new MockExtensionPoint(v).ExtendedValue);
		}

		[TestMethod]
		public void WhenExtendByValue_ThenGenericValueIsSameAsTyped()
		{
			var v = new StubExtendable();

			Assert.AreSame(new MockExtensionPoint(v).ExtendedValue, (new MockExtensionPoint(v) as IExtensionPoint).ExtendedValue);
		}

		[TestMethod]
		public void WhenExtendedByValue_ThenTypeIsSet()
		{
			var v = new StubExtendable();

			Assert.AreEqual(typeof(StubExtendable), new MockExtensionPoint(v).ExtendedType);
		}

		[TestMethod]
		public void WhenExtendedByNullValue_ThenTypeIsSet()
		{
			StubExtendable v = null;

			Assert.AreEqual(typeof(StubExtendable), new MockExtensionPoint(v).ExtendedType);
		}

		[TestMethod]
		public void WhenExtendedByInheritedValue_ThenTypeIsKept()
		{
			var v = new StubInheritingExtendable();

			Assert.AreEqual(v.GetType(), new MockExtensionPoint(v).ExtendedType);
		}

		[TestMethod]
		public void WhenExtendByType_ThenValueIsDefault()
		{
			var t = typeof(StubExtendable);

			Assert.AreEqual(default(StubExtendable), new MockExtensionPoint(t).ExtendedValue);
		}

		[TestMethod]
		public void WhenExtendByType_ThenTypeIsSet()
		{
			var t = typeof(StubExtendable);

			Assert.AreEqual(t, new MockExtensionPoint(t).ExtendedType);
		}

		[TestMethod]
		public void WhenExtendByInheritedType_ThenTypeIsKept()
		{
			var t = typeof(StubInheritingExtendable);

			Assert.AreEqual(t, new MockExtensionPoint(t).ExtendedType);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void WhenExtendedByWrongType_ThenThrow()
		{
			new MockExtensionPoint(typeof(object));
		}
	}
}
