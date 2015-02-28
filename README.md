ExtendIt [![teamcity build status][teamcity-status]][teamcity-build]
========

A simple tool that enables namespacing of extension methods.

Usage
-----

This library provides you with an implementation for the extension point pattern. An extension point acts as a replacement context for your extension methods. Instead of extending a type directly, like string or IEnumerable, you will usually want to extend a specific extension point. This technique lets you regroup similar extension methods together under a single semantic banner as if they would have been declared under the same scope.

This is very useful when the amount of extension methods you use on a type starts to pile up under a long list of very similar and yet unrelated names. Here is an example of what this ambiguity could look like.

```csharp
using Serialization;
using Utilities;
using Validations;

var s = "some value";

s.HasValue();
s.IsNullOrEmpty();
s.FromJson();
```

With just three methods, we can already see where this is going. Two of them looks somewhat similar and the third deals with a completely different task. Even though the extension method themselves are declared within different namespaces, it is impossible to know their exact purpose just by looking at their usage. To make use of those namespaces, you would have to call those methods using the static syntax which would defeat the purpose of them being extensions in the first place.

This sample provides two ambiguous methods, HasValue and IsNullOrEmpty, to better illustrate this issue. Can you tell which one is a simple wrapper around string.IsNullOrEmpty and which one is actually a code contract that will throw an exception if not respected? In fact, you have a 50% chance of getting this guess right.

Here is the same example but, this time, using extension points.

```csharp
using Serialization;
using Utilities;
using Validations;

var s = "some value";

s.HasValue();
s.Validation().IsNullOrEmpty();
s.Serialization().FromJson();
```

We can now clearly tell the difference between the different goals of all three extension methods. This will also cleanup Visual Studio's Intellisense suggestions by only displaying the major groups instead of all of the disparate methods. There is also no obligation to put all extension methods under extension points or no limit on the amount of chained extension points you can use.

Declaration
-----------

To create an extension point, you simply have to inherit the ExtensionPointBase class by providing it a scope (type) in which it need to be available. Then, you have to declare an extension method to access this extension point.

```csharp
// This extension point will only be available on strings.
class StringExtensionPoint : ExtensionPointBase<string>
{
    public StringExtensionPoint(string value)
        : base(value) {}
}

static class StringExtensions
{
    // Expose the extension point.
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

As you can see, the extension point will provide you with a context when you writing an extension method. When using the default extension point class, this context is composed of two things:
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

This is an implementation of a pattern from [nVentive's Umbrella framework available on CodePlex](https://umbrella.codeplex.com/). This repository is mostly for those of us who do not want to pull in an entire framework just to use this awesome pattern.

[teamcity-status]: http://teamcity.furrybuilder.com/app/rest/builds/buildType:(id:FurryBuilder_ExtendIt_Dev)/statusIcon
[teamcity-build]:  http://teamcity.furrybuilder.com/viewType.html?buildTypeId=FurryBuilder_ExtendIt_Dev
