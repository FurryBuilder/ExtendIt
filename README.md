ExtendIt
========

A simple tool that enables namespacing of extension methods.

Usage
-----

This library provides you with an implementation for the extension point pattern. An extension point acts as a replacement context for your extension methods. Instead of extending a type directly, like string or IEnumerable, you will usually want to extend a specific extension point. This technique lets you regroup similar extension methods together like if they where all regrouped under the same namespace.

This is very useful when the amount of extension methods you use on a type starts to pile up under a long list of very similar names. Here is an example of what this ambiguity could look like.

```csharp
using Serialization;
using Utilities;
using Validations;

aString.HasValue();
aString.IsNullOrEmpty();
aString.FromJson();
```

With just three methods, we can already see where this is going. Two of them looks somewhat similar and the third one deals with a completely different task than the firsts.

Even though the extension method themselves are declared within different namespaces, it is impossible to know their exact purpose just by looking at their usage. This sample provides two ambiguous methods, HasValue and IsNullOrEmpty, to better illustrate this issue. Can you tell which one is a simple wrapper around string.IsNullOrEmpty and which one is actually a contract that will throw an exception if not respected?

Here is the same example but, this time, using extension points.

```csharp
using Serialization;
using Utilities;
using Validations;

aString.HasValue();
aString.Validation().IsNullOrEmpty();
aString.Serialization().FromJson();
```

We can now clearly tell the difference between the different goals of all three extension methods. This will also cleanup Visual Studio's Intellisense suggestions by only displaying the major groups instead of all of the disparate methods. There is also no obligation to put all extension methods under extension points.

Declaration
-----------

To create an extension point, you simply have to inherit the ExtensionPointBase class by providing it a scope in which it will be available. Then, you have to declare an extension method to access this extension point.

```csharp
class StringExtensionPoint : ExtensionPointBase<string>
{
    public StringExtensionPoint(string value)
        : base(value) {}
}

static class StringExtensions
{
    public static StringExtensionPoint Utilities(this string value)
    {
        return new StringExtensionPoint(value);
    }
}
```

In this example, we created an extension point specific to strings that can be used through the Utilities() method. To add extension method under this extension point, simply extend the StringExtensionPoint class instead of string.

```csharp
static class StringExtensions
{
    public static bool HasValue(this StringExtensionPoint extensionPoint)
    {
        return !string.IsNullOrEmpty(extensionPoint.ExtendedValue);
    }
}
```


As you can see, the extension point will take care to providing you with a context when you write an extension method. When using the default extension point class, this context is composed of two things:
1.    The extended value. The value that would be passed via the this parameter in a classic extension method.
2.    The extended type. This provides the original type of the value, before getting casted to its interface or base class type if the extension point is generic.

For more generic extension points, you could extend the object type, or even forward the extended type through generics.

```csharp
class ValidationExtensionPoint<T> : ExtensionPointBase<T>
{
    public ValidationExtensionPoint<T>(T value)
        : base(value) {}
}

static class ValidationExtensions
{
    public static ValidationExtensionPoint<T> Validation(this T value)
    {
        return new ValidationExtensionPoint(value);
    }

    public static T IsNotNull(this ValidationExtensionPoint<T> extensionPoint)
    {
        if (extensionPoint.ExtendedValue == null)
        {
            throw new Exception();
        }

        return extensionPoint.ExtendedValue;
    }
}
```

Disclaimer
----------

This is an implementation of a pattern from nVentive's Umbrella framework available on CodePlex. This repository is mostly for those of us who do not want to pull in an entire framework just to use this awesome pattern.
