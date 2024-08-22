using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ReizzzTracking.DAL.Primitives
{
    public abstract class Enumeration<TEnum> : IEquatable<Enumeration<TEnum>>
        where TEnum : Enumeration<TEnum>
    {
        private static readonly Dictionary<long, TEnum> Enumerations = CreateEnumerations();


        protected Enumeration(long value, string name)
        {
            Id = value;
            Name = name;
        }
        public long Id { get; protected init; }
        public string Name { get; protected init; } = string.Empty;
        public static TEnum? FromValue(int value)
        {
            return Enumerations.TryGetValue(value, out TEnum enumeration)
                ? enumeration
                : default;
        }
        public static TEnum? FromValue(string name)
        {
            return Enumerations.Values.SingleOrDefault(x => x.Name == name);
        }
        public static IReadOnlyCollection<TEnum> GetValues()
        {
            return Enumerations.Values.ToList();
        }
        public bool Equals(Enumeration<TEnum>? other)
        {
            if (other is null)
            {
                return false;
            }
            return GetType() == other.GetType() && Id == other.Id;
        }
        public override bool Equals(object? obj)
        {
            return obj is Enumeration<TEnum> other && Equals(other);
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        public override string ToString()
        {
            return Name;
        }
        private static Dictionary<long, TEnum> CreateEnumerations()
        {
            var enumerationType = typeof(TEnum);
            var fieldsForType = enumerationType
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fieldInfo => enumerationType.IsAssignableFrom(fieldInfo.FieldType))
                .Select(fieldInfo => (TEnum)fieldInfo.GetValue(default)!);
            return fieldsForType.ToDictionary(x => x.Id);
        }
    }
}
