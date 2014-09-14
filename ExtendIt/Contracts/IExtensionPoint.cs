using System;

namespace ExtendIt.Contracts
{
	/// <summary>
	/// Encapsulate a set of extension methods in a specific scope (namespace)
	/// represented by the implementer of this interface.
	/// </summary>
	public interface IExtensionPoint
	{
		object ExtendedValue { get; }

		Type ExtendedType { get; }
	}
}