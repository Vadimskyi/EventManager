/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using System;

namespace VadimskyiLab.Events
{
    public interface IEvent { }

    public abstract class EventBase<Te> : IEvent
    {
        public static void Subscribe(Action callback)
        {
            EventManager.SubscribeTo<Te>(callback);
        }

        public static void Unsubscribe(Action callback)
        {
            EventManager.UnsubscribeFrom<Te>(callback);
        }

        public static void Invoke()
        {
            EventManager.DispatchEvent<Te>();
        }
    }

    public abstract class EventBase<Te, Ta> : IEvent
    {
        public static void Subscribe(Action<Ta> callback)
        {
            EventManager.SubscribeTo<Te>(callback);
        }

        public static void Unsubscribe(Action<Ta> callback)
        {
            EventManager.UnsubscribeFrom<Te>(callback);
        }

        public static void Invoke(Ta data)
        {
            EventManager.DispatchEvent<Te, Ta>(data);
        }
    }

    public abstract class EventBase<Te, Ta0, Ta1> : IEvent
    {
        public static void Subscribe(Action<Ta0, Ta1> callback)
        {
            EventManager.SubscribeTo<Te>(callback);
        }

        public static void Unsubscribe(Action<Ta0, Ta1> callback)
        {
            EventManager.UnsubscribeFrom<Te>(callback);
        }

        public static void Invoke(Ta0 data0, Ta1 data1)
        {
            EventManager.DispatchEvent<Te, Ta0, Ta1>(data0, data1);
        }
    }
}