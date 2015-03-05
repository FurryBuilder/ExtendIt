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
    public class GivenValueTypeExtensionPoint
    {
        [TestMethod]
        public void WhenExtendByValue_ThenValueIsSet()
        {
            var i = 3;

            Assert.AreEqual(i, new MockExtensionPoint(i).ExtendedValue);
        }

        [TestMethod]
        public void WhenExtendByValue_ThenGenericValueIsEqualAsTyped()
        {
            var i = 3;

            Assert.AreEqual(new MockExtensionPoint(i).ExtendedValue, (new MockExtensionPoint(i) as IExtensionPoint).ExtendedValue);
        }

        [TestMethod]
        public void WhenExtendedByValue_ThenTypeIsSet()
        {
            var i = 3;

            Assert.AreEqual(i.GetType(), new MockExtensionPoint(i).ExtendedType);
        }

        [TestMethod]
        public void WhenExtendedByDefaultValue_ThenTypeIsSet()
        {
            var i = 0;

            Assert.AreEqual(typeof (int), new MockExtensionPoint(i).ExtendedType);
        }

        [TestMethod]
        public void WhenExtendByType_ThenValueIsDefault()
        {
            var t = typeof (int);

            Assert.AreEqual(default(int), new MockExtensionPoint(t).ExtendedValue);
        }

        [TestMethod]
        public void WhenExtendByType_ThenTypeIsSet()
        {
            var t = typeof (int);

            Assert.AreEqual(t, new MockExtensionPoint(t).ExtendedType);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void WhenExtendedByWrongType_ThenThrow()
        {
            new MockExtensionPoint(typeof (double));
        }

        private class MockExtensionPoint : ExtensionPointBase<int>
        {
            public MockExtensionPoint(int value) : base(value)
            {
            }

            public MockExtensionPoint(Type type) : base(type)
            {
            }
        }
    }
}