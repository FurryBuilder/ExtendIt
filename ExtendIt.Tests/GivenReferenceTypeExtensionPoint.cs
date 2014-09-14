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
