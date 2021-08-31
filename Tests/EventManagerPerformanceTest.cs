using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.PerformanceTesting;
using UnityEngine;
using UnityEngine.Events;
using VadimskyiLab.Events;

namespace VadimskyiLab.Tests
{
    public class EventManagerPerformanceTest
    {
        Action action = null;
        event Action eventAction = null;
        event EventHandler eventHandler;
        private UnityEvent unityEvent = new UnityEvent();

        [Test]
        [Performance]
        public void EventManager_Subscribe()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        TestEvent.Subscribe(MethodSubscriber);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemAction_Subscribe()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        action += MethodSubscriber;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemEventAction_Subscribe()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        eventAction += MethodSubscriber;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemEventHandler_Subscribe()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        eventHandler += EventHandlerMethodSubscriber;
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void UnityEvent_Subscribe()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        unityEvent.AddListener(MethodSubscriber);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void UnityEventManager_Subscribe()
        {
            Measure.Method(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        UnityEventManager.StartListening("testEvent", MethodSubscriber);
                    }
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void EventManager_Unsubscribe()
        {
            for (int i = 0; i < 1000; i++)
            {
                TestEvent.Subscribe(MethodSubscriber);
            }
            Measure.Method(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    TestEvent.Unsubscribe(MethodSubscriber);
                }
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemAction_Unsubscribe()
        {
            for (int i = 0; i < 1000; i++)
            {
                action += MethodSubscriber;
            }
            Measure.Method(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    action -= MethodSubscriber;
                }
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemEventAction_Unsubscribe()
        {
            for (int i = 0; i < 1000; i++)
            {
                eventAction += MethodSubscriber;
            }
            Measure.Method(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    eventAction -= MethodSubscriber;
                }
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemEventHandler_Unsubscribe()
        {
            for (int i = 0; i < 1000; i++)
            {
                eventHandler += EventHandlerMethodSubscriber;
            }
            Measure.Method(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    eventHandler -= EventHandlerMethodSubscriber;
                }
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void UnityEvent_Unsubscribe()
        {
            for (int i = 0; i < 1000; i++)
            {
                unityEvent.AddListener(MethodSubscriber);
            }
            Measure.Method(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    unityEvent.RemoveListener(MethodSubscriber);
                }
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void UnityEventManager_Unsubscribe()
        {
            for (int i = 0; i < 1000; i++)
            {
                UnityEventManager.StartListening("testEvent", MethodSubscriber);
            }
            Measure.Method(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    UnityEventManager.StopListening("testEvent", MethodSubscriber);
                }
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }
        [Test]
        [Performance]
        public void EventManager_Invoke()
        {
            for (int i = 0; i < 10000; i++)
            {
                TestEvent.Subscribe(MethodSubscriber);
            }
            Measure.Method(() =>
                {
                    TestEvent.Invoke();
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemAction_Invoke()
        {
            for (int i = 0; i < 10000; i++)
            {
                action += MethodSubscriber;
            }
            Measure.Method(() =>
            {
                action.Invoke();
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemEventAction_Invoke()
        {
            for (int i = 0; i < 10000; i++)
            {
                eventAction += MethodSubscriber;
            }
            Measure.Method(() =>
            {
                eventAction.Invoke();
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void SystemEventHandler_Invoke()
        {
            for (int i = 0; i < 10000; i++)
            {
                eventHandler += EventHandlerMethodSubscriber;
            }
            Measure.Method(() =>
            {
                eventHandler.Invoke(this, null);
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void UnityEvent_Invoke()
        {
            for (int i = 0; i < 10000; i++)
            {
                unityEvent.AddListener(MethodSubscriber);
            }
            Measure.Method(() =>
            {
                unityEvent.Invoke();
            })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        [Test]
        [Performance]
        public void UnityEventManager_Invoke()
        {
            for (int i = 0; i < 10000; i++)
            {
                UnityEventManager.StartListening("testEvent", MethodSubscriber);
            }
            Measure.Method(() =>
                {
                    UnityEventManager.TriggerEvent("testEvent");
                })
                .WarmupCount(10)
                .MeasurementCount(10)
                .IterationsPerMeasurement(1)
                .GC()
                .Run();
        }

        private int _sum;
        [Test]
        public void EventManager_ConsistencyTest()
        {
            _sum = 0;
            TestDataEvent.Subscribe(MethodDataSubscriber);

            int iterations = 1000;
            var list = new List<TestData>(iterations);
            for (int i = 1; i <= iterations; i++)
            {
                list.Add(new TestData(i));
            }

            for (int i = 0; i < list.Count; i++)
            {
                TestDataEvent.Invoke(list[i]);
            }
            Assert.AreEqual(iterations / 2 * (iterations + 1), _sum);
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void MethodSubscriber()
        {

        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void MethodDataSubscriber(TestData data)
        {
            _sum += data.Index;
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private void EventHandlerMethodSubscriber(object sender, EventArgs e)
        {

        }
    }

    public class TestEvent : EventBase<TestEvent>
    {
    }

    public class TestDataEvent : EventBase<TestDataEvent, TestData>
    {

    }

    public readonly struct TestData
    {
        public readonly int Index;

        public TestData(int index)
        {
            Index = index;
        }
    }

    /// <summary>
    /// Unity Event Manager from official tutorial:
    /// https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa
    /// </summary>
    public class UnityEventManager : MonoBehaviour
    {

        private Dictionary<string, UnityEvent> eventDictionary;

        private static UnityEventManager eventManager;

        public static UnityEventManager instance
        {
            get
            {
                if (!eventManager)
                {
                    eventManager = FindObjectOfType(typeof(UnityEventManager)) as UnityEventManager;

                    if (!eventManager)
                    {
                        eventManager = new GameObject("m", typeof(UnityEventManager)).GetComponent<UnityEventManager>();
                    }

                    eventManager.Init();
                }

                return eventManager;
            }
        }

        void Init()
        {
            if (eventDictionary == null)
            {
                eventDictionary = new Dictionary<string, UnityEvent>();
            }
        }

        public static void StartListening(string eventName, UnityAction listener)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.AddListener(listener);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                instance.eventDictionary.Add(eventName, thisEvent);
            }
        }

        public static void StopListening(string eventName, UnityAction listener)
        {
            if (eventManager == null) return;
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.RemoveListener(listener);
            }
        }

        public static void TriggerEvent(string eventName)
        {
            UnityEvent thisEvent = null;
            if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        public void ClearStateForTests()
        {

        }
    }
}