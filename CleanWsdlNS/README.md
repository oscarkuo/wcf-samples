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

3. Add or modify the `<services>` section under `<system.serviceModel>` section in the web.config to specify namespace on data binding

	```xml
<system.serviceModel>
  <services>
    <service name="CleanWsdlNS.Services.HelloServiceImpl">
      <endpoint address="HelloService.svc" bindingNamespace="http://oscarkuo.com/v1/hello" 
                binding="basicHttpBinding" contract="CleanWsdlNS.Services.IHelloService" />
    </service>
  </services>
</system.serviceModel>
	```

Finally, for data contracts you can either specify their namespace inidividually

```cs
[DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
public class HelloResponse
```
```cs
[DataContract(Namespace = "http://oscarkuo.com/v1/hello")]
public class HelloRequest
```

Or you can specify the data contract namespace globally by entering the following line into `AssemblyInfo.cs`

```cs
[assembly: ContractNamespace("http://oscarkuo.com/v1/hello", ClrNamespace = "CleanWsdlNS.ValueObjects")]
```

Note that the `ClrNamespace` will need to match the namespace of your data contracts.