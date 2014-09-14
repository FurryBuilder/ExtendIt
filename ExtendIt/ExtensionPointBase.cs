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