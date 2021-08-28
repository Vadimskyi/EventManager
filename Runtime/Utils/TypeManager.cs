/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using System;

namespace VadimskyiLab.Utils
{
    /// <summary>
    /// TODO: create optimized variant
    /// TODO: replace array with RefStructArray
    /// TODO: assign custom incremented index to each type
    /// TODO: lookup array of types by custom index
    /// TODO: profit???
    /// </summary>
    public static class TypeManager
    {
        public const int NULL_TYPE_INDEX = -1;
        private static TypeData[] _types;

        private static volatile int _typesCount;

        static TypeManager()
        {
            _types = new TypeData[1000];
            _typesCount = 0;
        }

        public static int GetTypeIndex<T>()
        {
            return GetTypeIndex(typeof(T));
        }

        public static Type GetIndexedType(int typeIndex)
        {
            return FindIndexedType(typeIndex);
        }

        public static int GetTypeIndex(Type type)
        {
            return FindTypeIndex(type);
        }

        private static Type FindIndexedType(int typeIndex)
        {
            for (int i = 0; i < _typesCount - 1; i++)
            {
                ref var t = ref _types[i];
                if (t.Hash == typeIndex) return t.Type;
            }

            throw new NotImplementedException($"Type with index {typeIndex} was not added to {nameof(TypeManager)} prior to retrieval!");
        }

        private static int FindTypeIndex(Type type)
        {
            if (type == null)
                return -1;

            for (int i = 0; i < _typesCount - 1; i++)
            {
                ref var t = ref _types[i];
                if (t.Type == type) return t.Hash;
            }

            return RegisterComponentType(type);
        }

        private static int RegisterComponentType(Type type)
        {
            
            int hash = RuntimeHelper.GetHashCode32(type);
            _types[_typesCount++] = new TypeData { Hash = hash, Type = type };
            return hash;
        }
    }

    public struct TypeData
    {
        public int Hash;
        public Type Type;
    }
}