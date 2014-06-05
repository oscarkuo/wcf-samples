# Log Message with Behaviour
This sample demostrates how to create a WCF behaviour to log request and response messages for both JSON and SOAP endpoints/contracts. The log entries will be written to the file `%TMP%\LogMessageWithBehaviour.txt`.

## Implement IDispatchMessageInspector
An implmentation of `IDispatchMessageInspector` can be plugged into WCF message processing pipeline. This requires you to implement AfterReceiveRequest and BeforeSendReply methods in your class, both of which are fairly straight forward.

The only tricky thing to remember is that the `request` parameter of type `System.ServiceModel.Channels` provided to your method by WCF is a reference copy and it can only be read _once_. Therefore, you need to create copies of it as illustrated in the code sample:


```
CopyAndLogMessage(logState, request.CreateBufferedCopy(int.MaxValue));
```

and then

```cs
private static Message CopyAndLogMessage(LogState state, MessageBuffer buffer) 
{
  var message = buffer.CreateMessage();
  ...
  return buffer.CreateMessage();
}
```

It is worth noting how I manage the `Log` object instance by using the _correlation state_ feature. 
Firstly I created the `Log` instance in `AfterReceivedRequest` and returned it.

```cs
public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
{
  ...
  var log = new Log(path, request.Headers.To);
  ...
  return log;
}
```

WCF then passes the same Log instance to `BeforeSendReply` for me.

```cs
public void BeforeSendReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
{
  var log = (Log)correlationState;
  reply = CopyAndLogMessage(log, reply.CreateBufferedCopy(int.MaxValue));
  log.Flush();
}
```

## Implement the BehaviorExtensionElement and IEndpointBehavior
To inject the `IDispatchMessageInspector`, I choose to do it via the `web.config` file. This means you'll need to create a behaviour extension class that inherits `BehaviorExtensionElement` and implements `IEndpointBehavior`. At a minimum you'll need to implment following properties/methods:

```cs
public override Type BehaviorType
{
  get { return typeof(MessageLoggingBehaviour); }
}
```

```cs
protected override object CreateBehavior()
 
  return new MessageLoggingBehaviour();
}
```
and finally

```cs
public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
{
  endpointDispatcher.DispatchRuntime.MessageInspectors.Add(new MessageLoggingInspector());
}
```

The `ApplyDispatchBehaviour` adds an instance of the `MessageLoggingInspector` class to the list of message inspectors to _each_ endpoint. In this example it is invoked twice when the service starts up because we configured it for both JSON and SOAP endpoints (see `web.config` section below).

## The Web.config
Firstly the behaviour extension class need to declared as an element.

```xml
<system.serviceModel>
  <extensions>
    <behaviorExtensions>
      <add name="messageLoggingInsepctor" type="LogMessageWithBehaviour.Services.MessageLoggingBehaviour, LogMessageWithBehaviour.Services, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"/>
    </behaviorExtensions>
  </extensions>    
  ...
```

Secondly, it needs to be added to behaviours that applies to endpoints.
Note below we have two `<behaviour>` sections and our `<messageLoggingInspector>` is appeared in each of them. The first one is the default and the second one is use for the JSON endpoint.

```xml
  ...
  <endpointBehaviors>
    <behavior> <!-- the default behaviour -->
      <messageLoggingInsepctor />
    </behavior>
    <behavior name="web">
      <webHttp/>
      <messageLoggingInsepctor />
    </behavior>
  </endpointBehaviors>
</behaviors>
```

Finally, endpoints are applied to SOAP and JSON endpoints as declared in section below.

```xml
<services>
  <service name="LogMessageWithBehaviour.Services.HelloServiceImpl">
    <endpoint binding="webHttpBinding"
              address="json"
              behaviorConfiguration="web"
              contract="LogMessageWithBehaviour.Services.IHelloServiceJson">
    </endpoint>
    <endpoint binding="basicHttpBinding"
              bindingNamespace="http://oscarkuo.com/v1/hello"
              contract="LogMessageWithBehaviour.Services.IHelloService" />
  </service>
</services>
```

## The Log class
The `Log` class is pretty basic. It buffers up `Write` requests in a `List<string>` and writes all entries out when `Flush` is called in `MessageLoggingInspector.BeforeSendReply`. The only thing that's worth noting is how I strips out new lines with `XmlWriterSettings`.

```cs
var settings = new XmlWriterSettings();
settings.OmitXmlDeclaration = true;
settings.Indent = true;
settings.NewLineHandling = NewLineHandling.None; //strips new line characters
settings.Indent = false;
```



