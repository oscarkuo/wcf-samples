# Fileless Activation
Since .NET 4.0, it is possible to deploy a WCF service without physically having the .svc file. This also allows you to dynamically specify the relative address.

1. Remove the .svc file if you have any in the project.
2. Create a plan C# class that implements the service contract interface (see HelloServiceImpl.cs)
	```cs
	[ServiceBehavior(Namespace = "http://oscarkuo.com/v1/hello")]
	public class HelloServiceImpl : IHelloService
	```
3. Expand the serviceHostingEnvironment with <serviceActivations>. Specify the desired relative URL with the "relativeAddress" attribute (e.g. relativeAddress="v1/HelloService.svc") and specify the service implementation class wit the "service" attribute (e.g. service="FilelessActivation.Services.HelloServiceImpl")
	```xml
	<serviceHostingEnvironment aspNetCompatibilityEnabled="true" multipleSiteBindingsEnabled="true">
	  <serviceActivations>
		<add relativeAddress="v1/HelloService.svc"
			 service="FilelessActivation.Services.HelloServiceImpl"
			 factory="System.ServiceModel.Activation.ServiceHostFactory" />
	  </serviceActivations>
	</serviceHostingEnvironment>
	```
4. Browse to the provided relative address to see the usual WCF service page (e.g. http://localhost:52495/v1/HelloService.svc)