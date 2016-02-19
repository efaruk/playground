I don't recommend to use this http module, use this extension method instead of this module:

```cs
public static string UserRealHostAddress(this HttpRequest request, string headerName = "X-Forwarded-For", string seperator = ",", int clientIpIndex = 0)
{
    if (request == null) throw new ArgumentNullException("request");
    var rc = request.UserHostAddress;
    if (request.Headers.AllKeys.Contains(headerName))
    {
        var header = request.Headers[headerName];
        var ha = header.Split(new[] {seperator}, StringSplitOptions.RemoveEmptyEntries);
        rc = ha[clientIpIndex];
    }
    return rc;
}
```
