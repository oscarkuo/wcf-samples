# Clean WSDL Namespace Sample
This solution demostrates how to remove tempurl.org from SOAP services in WCF

1. Specify namespace on service contract
```cs
[ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
public interface IHelloService
```
2. Specify namespace on the service implementation class with the ServiceBehavior attribute
```cs
[ServiceBehavior(Namespace = "http://oscarkuo.com/v1/hello")]
public class HelloServiceImpl : IHelloService
```
3. Specify namespace on data contracts
```cs
[DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
public class HelloResponse
```
```cs
[DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
public class HelloRequest
```
4. Add or modify the <services> section in the web.config to specify namespace on data binding
```xml
<services>
    <service name="CleanWsdlNS.Services.HelloServiceImpl">
    <endpoint address="HelloService.svc" bindingNamespace="http://oscarkuo.com/v1/hello" binding="basicHttpBinding" contract="CleanWsdlNS.Services.IHelloService" />
    </service>
</services>
```