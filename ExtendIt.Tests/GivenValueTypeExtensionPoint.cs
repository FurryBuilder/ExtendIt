using System;
using ExtendIt.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExtendIt.Tests
{
	[TestClass]
	public class GivenValueTypeExtensionPoint
	{
		private class MockExtensionPoint : ExtensionPointBase<int>
		{
			public MockExtensionPoint(int value) : base(value) { }
			public MockExtensionPoint(Type type) : base(type) { }
		}

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

			Assert.AreEqual(typeof(int), new MockExtensionPoint(i).ExtendedType);
		}

		[TestMethod]
		public void WhenExtendByType_ThenValueIsDefault()
		{
			var t = typeof(int);

			Assert.AreEqual(default(int), new MockExtensionPoint(t).ExtendedValue);
		}

		[TestMethod]
		public void WhenExtendByType_ThenTypeIsSet()
		{
			var t = typeof(int);

			Assert.AreEqual(t, new MockExtensionPoint(t).ExtendedType);
		}

		[TestMethod]
		[ExpectedException(typeof(ArgumentException))]
		public void WhenExtendedByWrongType_ThenThrow()
		{
			new MockExtensionPoint(typeof(double));
		}
	}
}
