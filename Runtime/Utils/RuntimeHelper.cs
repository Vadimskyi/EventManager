using System;
using System.Reflection;

namespace VadimskyiLab.Utils
{
    /// <summary>
    /// Convert type or string to hashCode;
    /// Useful for caching inside zero-alloc arrays;
    /// </summary>
    public static class RuntimeHelper
    {
        /// <summary>
        /// Gets a 32-bits hashcode from a type computed for the <see cref="System.Type.AssemblyQualifiedName"/>
        /// </summary>
        /// <typeparam name="T">The type to compute the hash from</typeparam>
        /// <returns>The 32-bit hashcode.</returns>
        public static int GetHashCode32<T>()
        {
            return HashCode32<T>.Value;
        }

        /// <summary>
        /// Gets a 32-bits hashcode from a type computed for the <see cref="System.Type.AssemblyQualifiedName"/>
        /// This method cannot be used from a burst job.
        /// </summary>
        /// <param name="type">The type to compute the hash from</param>
        /// <returns>The 32-bit hashcode.</returns>
        public static int GetHashCode32(Type type)
        {
            return HashStringWithFNV1A32(type.AssemblyQualifiedName);
        }

        public static int GetHashCode32(MethodInfo type)
        {
            return HashStringWithFNV1A32(type.Name);
        }

        public static int GetHashCode32(string str)
        {
            return HashStringWithFNV1A32(str);
        }

        /// <summary>
        /// Gets a 64-bits hashcode from a type computed for the <see cref="System.Type.AssemblyQualifiedName"/>
        /// </summary>
        /// <typeparam name="T">The type to compute the hash from</typeparam>
        /// <returns>The 64-bit hashcode.</returns>
        public static long GetHashCode64<T>()
        {
            return HashCode64<T>.Value;
        }

        /// <summary>
        /// Gets a 64-bits hashcode from a type computed for the <see cref="System.Type.AssemblyQualifiedName"/>.
        /// This method cannot be used from a burst job.
        /// </summary>
        /// <param name="type">Type to calculate a hash for</param>
        /// <returns>The 64-bit hashcode.</returns>
        public static long GetHashCode64(Type type)
        {
            return HashStringWithFNV1A64(type.AssemblyQualifiedName);
        }

        // method internal as it is used by the compiler directly
        internal static int HashStringWithFNV1A32(string text)
        {
            // Using http://www.isthe.com/chongo/tech/comp/fnv/index.html#FNV-1a
            // with basis and prime:
            const uint offsetBasis = 2166136261;
            const uint prime = 16777619;

            uint result = offsetBasis;
            foreach (var c in text)
            {
                result = prime * (result ^ (byte)(c & 255));
                result = prime * (result ^ (byte)(c >> 8));
            }
            return (int)result;
        }

        // method internal as it is used by the compiler directly
        internal static long HashStringWithFNV1A64(string text)
        {
            // Using http://www.isthe.com/chongo/tech/comp/fnv/index.html#FNV-1a
            // with basis and prime:
            const ulong offsetBasis = 14695981039346656037;
            const ulong prime = 1099511628211;

            ulong result = offsetBasis;
            foreach (var c in text)
            {
                result = prime * (result ^ (byte)(c & 255));
                result = prime * (result ^ (byte)(c >> 8));
            }
            return (long)result;
        }

        private struct HashCode32<T>
        {
            public static readonly int Value = HashStringWithFNV1A32(typeof(T).AssemblyQualifiedName);
        }

        private struct HashCode64<T>
        {
            public static readonly long Value = HashStringWithFNV1A64(typeof(T).AssemblyQualifiedName);
        }
    }
}