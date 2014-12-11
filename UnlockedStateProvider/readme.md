Unlocked State Provider
===================

Unlocked state provider uses [StackExchange.Redis](https://github.com/StackExchange/StackExchange.Redis), which is a high performance general purpose redis client for .NET and designed to support only MVC web applications. 
It works like a charm with only few lines of configuration.

Features
--

- Designed for advenced use, you can use both standart session and Unlocked at the same time.
- Supports custom cookie. (You just need set your cookie with value which is unique imponderable id for client tracking)
- Cookie based client tracking.
- Async/Sync set, slide, delete
- Session like object trough IUnlockedStore.Items (Session["key"], IUnlockedStore["key"])
- Shared redis connection with Lazy<T>
- 
