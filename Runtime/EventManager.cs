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
    public static class EventManager
    {
        private static IReadOnlyDictionary<int, List<Delegate>> _bag;

        static EventManager()
        {
            CleanStateForEachTestCase();
        }

        public static void DispatchEvent<Te>() where Te : IEvent
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            //check in case there was a dynamic type created in runtime
            if (!_bag.ContainsKey(typeIndex)) return;

            var list = _bag[typeIndex];
            var l = list.Count;
            for (int i = 0; i < l; i++)
            {
                if (i >= list.Count) break;
                ((Action)list[i]).Invoke();
            }
        }

        public static void DispatchEvent<Te, Ta>(Ta data) where Te : IEvent where Ta : IEventArg
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            //check in case there was a dynamic type created in runtime
            if (!_bag.ContainsKey(typeIndex)) return;

            var list = _bag[typeIndex];
            var l = list.Count;
            for (int i = 0; i < l; i++)
            {
                if (i >= list.Count) break;
                ((Action<Ta>)list[i]).Invoke(data);
            }
        }

        public static void SubscribeTo<Te>(Action callback) where Te : IEvent
        {
            Subscribe<Te>(callback);
        }

        public static void SubscribeTo<Te, Ta>(Action<Ta> callback) where Te : IEvent where Ta : IEventArg
        {
            Subscribe<Te>(callback);
        }

        private static void Subscribe<Te>(Delegate callback) where Te : IEvent
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();

            if (!_bag.ContainsKey(typeIndex)) return;

            _bag[typeIndex].Add(callback);
        }

        public static void UnsubscribeFrom<Te>(Delegate callback) where Te : IEvent
        {
            var typeIndex = TypeManager.GetTypeIndex<Te>();
            if (!_bag.ContainsKey(typeIndex)) return;
            _bag[typeIndex].Remove(callback);
        }

        private static IEnumerable<Type> GetTypesWithInterface(Assembly asm)
        {
            var it = typeof(IEvent);
            return asm.GetLoadableTypes().Where(w => it.IsAssignableFrom(w) && !w.IsInterface);
        }

        /// <summary>
        /// DO NOT CALL THIS METHOD MANUALLY!
        /// Used for correct test results. As sometimes static class is cached in between tests.
        /// </summary>
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static void CleanStateForEachTestCase()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(GetTypesWithInterface);

            var writeBag = new Dictionary<int, List<Delegate>>();

            foreach (var type in types)
            {
                var typeIndex = TypeManager.GetTypeIndex(type);
                writeBag.Add(typeIndex, new List<Delegate>(5));
            }

            _bag = writeBag;
        }
    }
}