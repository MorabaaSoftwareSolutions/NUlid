﻿using System;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace NUlid;

/// <summary>
/// Converts a <see cref="string"/> or <see cref="T:byte[]"/> type to a <see cref="Ulid"/> type, and vice versa.
/// </summary>
public sealed class UlidTypeConverter : TypeConverter
{
    /// <summary>
    /// Returns whether this converter can convert an object of the given type to the type of this converter.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
    /// <param name="sourceType">A <see cref="Type"/> that represents the type that you want to convert from.</param>
    /// <returns>
    /// true if sourceType is a <see cref="string"/> type or a <see cref="T:byte[]"/> type can be assigned from
    /// sourceType; otherwise, false.
    /// </returns>
    /// <exception cref="ArgumentNullException">The sourceType parameter is null.</exception>
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        => sourceType == null
            ? throw new ArgumentNullException(nameof(sourceType))
            : sourceType == typeof(byte[])
            || sourceType == typeof(string)
            || base.CanConvertFrom(context, sourceType);

    /// <summary>
    /// Returns whether this converter can convert the object to the specified type, using the specified context.
    /// </summary>
    /// <param name="context"> An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
    /// <param name="destinationType">A <see cref="Type"/> that represents the type that you want to convert to.</param>
    /// <returns>
    /// true if destinationType is of type <see cref="InstanceDescriptor"/>, <see cref="string"/>, or 
    /// <see cref="T:byte[]"/>; otherwise, false.
    /// </returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        => destinationType == typeof(byte[])
            || destinationType == typeof(string)
            || base.CanConvertTo(context, destinationType);

    /// <summary>
    /// Converts the given object to the type of this converter, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
    /// <param name="culture">The <see cref="CultureInfo"/> to use as the current culture.</param>
    /// <param name="value">The <see cref="object"/> to convert.</param>
    /// <returns>An <see cref="object"/> that represents the converted value.</returns>
    /// <exception cref="NotSupportedException">The conversion cannot be performed.</exception>
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) => value is byte[] asByteArray
            ? new Ulid(asByteArray)
            : value is string asString
            ? Ulid.TryParse(asString, out var ulid) ? ulid : throw new NotSupportedException($"Invalid Ulid representation: \"{asString}\"")
            : base.ConvertFrom(context, culture, value);

    /// <summary>
    /// Converts a given value object to the specified type, using the specified context and culture information.
    /// </summary>
    /// <param name="context">An <see cref="ITypeDescriptorContext"/> that provides a format context.</param>
    /// <param name="culture">A <see cref="CultureInfo"/>. If <see langword="null"/> is passed, the current culture is assumed.</param>
    /// <param name="value">The <see cref="object"/> to convert.</param>
    /// <param name="destinationType">The <see cref="Type"/> to convert the value parameter to.</param>
    /// <returns>An <see cref="object"/> that represents the converted value.</returns>
    /// <exception cref="ArgumentNullException">The destinationType parameter is <see langword="null"/>.</exception>
    /// <exception cref="NotSupportedException">The conversion cannot be performed.</exception>
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) => destinationType == typeof(byte[])
            ? ((Ulid)value).ToByteArray()
            : destinationType == typeof(string) ? ((Ulid)value).ToString() : base.ConvertTo(context, culture, value, destinationType);
}
