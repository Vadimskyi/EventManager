/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using VadimskyiLab.Utils;

namespace VadimskyiLab.Events
{
    /// <summary>
    /// Non-thread-safe event manager
    /// Use only in single thread environment. Preferably Unity thread.
    /// </summary>
    internal static class EventManager
    {
        private static IReadOnlyDictionary<int, List<Delegate>> _eventBag;

        static EventManager()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(GetTypesWithInterface);

            var writeBag = new Dictionary<int, List<Delegate>>();

            foreach (var type in types)
            {
                var typeIndex = TypeManager.GetTypeIndex(type);
                writeBag.Add(typeIndex, new List<Delegate>(5));
            }

            _eventBag = writeBag;
        }

        public static void DispatchEvent<Te>()
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            //check in case there was a dynamic type created in runtime
            if (!_eventBag.ContainsKey(typeIndex)) return;

            var list = _eventBag[typeIndex];
            var l = list.Count;
            for (int i = 0; i < l; i++)
            {
                if (i >= list.Count) break;
                ((Action)list[i]).Invoke();
            }
        }

        public static void SubscribeTo<Te>(Delegate callback) 
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            if (!_eventBag.ContainsKey(typeIndex)) return;

            _eventBag[typeIndex].Add(callback);
        }

        public static void UnsubscribeFrom<Te>(Delegate callback)
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();
            if (!_eventBag.ContainsKey(typeIndex)) return;
            _eventBag[typeIndex].Remove(callback);
        }

        private static IEnumerable<Type> GetTypesWithInterface(Assembly asm)
        {
            var it = typeof(IEvent);
            return asm.GetLoadableTypes().Where(w => it.IsAssignableFrom(w) && !w.IsInterface);
        }

        public static void DispatchEvent<Te, Ta>(Ta data)
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            //check in case there was a dynamic type created in runtime
            if (!_eventBag.ContainsKey(typeIndex)) return;

            var list = _eventBag[typeIndex];
            var l = list.Count;
            for (int i = 0; i < l; i++)
            {
                if (i >= list.Count) break;
                ((Action<Ta>)list[i]).Invoke(data);
            }
        }

        public static void DispatchEvent<Te, Ta0, Ta1>(Ta0 data0, Ta1 data1)
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            //check in case there was a dynamic type created in runtime
            if (!_eventBag.ContainsKey(typeIndex)) return;

            var list = _eventBag[typeIndex];
            var l = list.Count;
            for (int i = 0; i < l; i++)
            {
                if (i >= list.Count) break;
                ((Action<Ta0, Ta1>)list[i]).Invoke(data0, data1);
            }
        }
    }
}
