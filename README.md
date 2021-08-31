# Event Manager for C# (Unity, .NET, .NET Core)
- Non-thread-safe event manager. Use only in single thread environment. Example: Unity-Engine thread.
- There are zero dependencies on Unity Engine components. Usage in any kind of c# project should be allowed.
- Extremely easy to use and faster than most usual approaches
- For concurrent multithreaded event-system check out this package: TODO
![Performance comparison graph](https://user-images.githubusercontent.com/1322279/131223932-38d6fbb5-f8c7-449a-9e71-bf8abbfd1bf8.png)
![Performance comparison graph GC](https://user-images.githubusercontent.com/1322279/131224733-cb2a0ba8-d462-4eaf-918f-a41dc4da4d49.png)
UnityOfficialSystem: https://learn.unity.com/tutorial/create-a-simple-messaging-system-with-events#5cf5960fedbc2a281acd21fa

## Table of Contents

- [Installation](#installation)
    - [Unity](#unity)
- [Quick Start](#quick-start)
- [Main reasoning behind this library](#main-reasoning)
- [Precaution](#precaution)
- [Author Info](#author-info)

## Installation

This library is distributed via Unity's built-in package manager. Required Unity 2018.3 or later.
Sinse Unity's package manager does not support git-url dependencies you should install them manually, if required.

### Unity

- Open Unity project
- Navigate to Window->Package Manager menu
- Top left dropdown -> "Add package from git Url"
- Paste https://github.com/Vadimskyi/EventManager.git

## Quick Start

Define the class for your event and derive it from `EventBase<ClassName>` abstarct base class.

```csharp
public class TestEvent : EventBase<TestEvent>
{
}
```

Optionally, you can also define argument data or pass it directly for the generic argument type.

```csharp
public class PlayerMovedEvent : EventBase<PlayerMovedEvent, PlayerMovedData>
{
}

public readonly struct PlayerMovedData
{
    public readonly int PlayerId;
    public readonly Vector3 Position;
    public PlayerMovedData(int id, Vector3 pos)
    {
        PlayerId = id;
        Position = pos;
    }
}
```

Subscribe to event:

```csharp
public class SomeEventListener : MonoBehaviour
{
    public void Start()
    {
        PlayerMovedEvent.Subscribe(OnPlayerMoved);
    }
    
    public void OnPlayerMoved(PlayerMovedData data) { ... }
    
    public void OnDestroyed()
    {
        PlayerMovedEvent.Unsubscribe(OnPlayerMoved);
    }
}
```

Invoke event:

```csharp
public class Player : MonoBehaviour
{
    private int _playerId;
    
    public void Update()
    {
        if(positionChanged)
        {
            PlayerMovedEvent.Invoke(new PlayerMovedData(_playerId, transform.localPosition));
        }
    }
}
```

## Main reasoning

- Allows communication between components with regards to separations of concerns principle.
- Light-weight and easy on GC. Allowing it to be used in tight loops and Unity Update cycles.
- Maintainable and scalable architecture allows this system to be further optimized by using only blittable types for events and/or by managing native pointers instead of events-array.

## Precaution

Internally, this event manager is operating as "global" manager. Meaning that every event subscription will be persistent through all application lifecycle, unless unsubscribed manually. This, in fact, makes unsubscription of anonymous delegates problematic.
More on this issue: https://stackoverflow.com/questions/25563518/why-cant-i-unsubscribe-from-an-event-using-a-lambda-expression/25564492#25564492
Bottom line is: try not to use anonymous delegates with this [Event Manager] if possible.

## Author Info

Vadim Zakrzhewskyi (a.k.a. Vadimskyi) a software developer from Ukraine.

~10 years of experience working with Unity3d, mostly freelance/outsource.

* Twitter: [https://twitter.com/vadimskyi](https://twitter.com/vadimskyi) (English/Russian)
