# SOAP and JSON Endpoints
To demostrate the RESTful services support in WCF. This sample is based on the FilelessActivation sample (https://github.com/oscarkuo/wcf-samples/tree/master/FilelessActivation) so check it out if some `web.config` settings doesn't make sense to you.

## JSON Service Contract
Create a new service contract with `WebInvoke` attribute on top of the usual `OperationContract`.

  ```cs
  [ServiceContract(Namespace = "http://oscarkuo.com/v1/hello")]
  public interface IHelloServiceJson
  {
      [WebInvoke(Method = "GET",
                 RequestFormat = WebMessageFormat.Json,
                 ResponseFormat = WebMessageFormat.Json,
                 UriTemplate = "SayHelloTo/{name}?correlationId={correlationId}")]
      [OperationContract(Name = "SayHelloJson")]
      HelloResponse SayHello(string name, string correlationId);
  }
  ```

## Web.config changes
1. Add the `<endpointBehaviors>` section under `<behaviors>` and careate a _web_ behaviour that uses the `<webHttp>` behaviour extension.

  ```xml
    ...
    <endpointBehaviors>
      <behavior name="web">
        <webHttp/>
      </behavior>
    </endpointBehaviors>
  </behaviors>
  ```
  
2. Under the `<service>` section, add a new endpoint with `binding="webHttpBinding"` and `behaviorConfiguration="web"`.

  ```xml
  <service name="SoapAndJsonEndpoints.Services.HelloServiceImpl">
  <endpoint binding="webHttpBinding"
            address="json"
            behaviorConfiguration="web"
            contract="SoapAndJsonEndpoints.Services.IHelloServiceJson">
  ```
