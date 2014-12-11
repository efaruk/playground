UnlockedStateProvider.Redis [aka Unlocked]
===================

Unlocked designed to support only MVC web applications and uses [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis), which is a high performance general purpose redis client for .NET. 
It works like a charm with only few lines of configuration.

Features
--

- Designed for advanced usage, you can use both standard session and Unlocked at the same time.
- Cookie based client tracking.
- Supports custom cookie. (You just need set your cookie with value which is unique imponderable id for client tracking).
- Async/Sync set, slide, delete
- Session like object trough IUnlockedStore.Items `Session["key"] is equal to IUnlockedStore["key"]`
- Shared redis connection with `Lazy<T>`
- Support custom Items using IUnlockedStore.Get/Set (When you have objects that you don't want to put them in main collection (Dictionary))

Installation
--

color{REDUnlocked can be installed via the nuget UI (as [UnlockedStateProvider.Redis](https://www.nuget.org/packages/UnlockedStateProvider.Redis/)), or via the nuget package manager console:

	PM> Install-Package UnlockedStateProvider.Redis

Configuration
--

AppSettings
-

	<add key="Unlocked:Host" value="localhost:6379,127.0.0.1:6379" />
	<!-- <add key="Unlocked:CookieName" value="UnlockedCookie" /> --> <!-- Custom Cookie -->
	<add key="Unlocked:SessionTimeout" value="20" /> <!-- As Minutes -->
	<add key="Unlocked:Database" value="3" /> <!-- For REDIS Database Id -->
	<add key="Unlocked:OperationTimeout" value="30" /> <!-- As Seconds -->
	<!--<add key="Unlocked:ConnectionTimeout" value="30" /> --><!-- As Seconds -->

On Every Controller
-
Use our action filter attribute;

	[RedisUnlockedStateUsage(Usage = UnlockedStateUsage.ReadWrite)]

For using with standard ASP.Net session, you should be started user session before using unlocked. Because unlocked use cookie to identify and track user. ASP.Net session use same method for standard session.

	UnlockedExtensions.StartSessionIfNew();

If you want to use only Unlocked without any locking first you need to disable standard session state using; 

	<sessionState mode="Off" />

and set a custom Cookie with unique value or just use our helper

	UnlockedExtensions.StartSessionIfNewWithCustomCookie("UnlockedCookie");


Real World Example / Demo Application
--

Demo application will guide you, just clone the repository and press F5. 

You need a REDIS server as configured, you can download from nuget for x64 system [redis-64](https://www.nuget.org/packages/Redis-64/), for x86 system [redis32](https://www.nuget.org/packages/Redis-32/) from MSOpenTech


Questions and Contributions
---

If you think you have found a bug or have an idea, please [report an issue](https://github.com/StackExchange/StackExchange.Redis/issues?state=open), or if appropriate: submit a pull request. If you have a question, feel free to [contact me](https://github.com/efaruk).