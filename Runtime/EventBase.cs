/* Copyright (C) 2021 Vadimskyi - All Rights Reserved
 * Github - https://github.com/Vadimskyi
 * Website - https://www.vadimskyi.com/
 * You may use, distribute and modify this code under the
 * terms of the GPL-3.0 License.
 */
using System;

namespace VadimskyiLab.Events
{
    public abstract class EventBase<Te> where Te : IEvent
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

    public abstract class EventBase<Te, Ta> where Te : IEvent where Ta : IEventArg
    {
        public static void Subscribe(Action<Ta> callback)
        {
            EventManager.SubscribeTo<Te, Ta>(callback);
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
}