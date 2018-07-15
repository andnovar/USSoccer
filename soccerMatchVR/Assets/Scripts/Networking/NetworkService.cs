using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using CookComputing.XmlRpc;
using System.Net;
using System.IO;
using System.Threading;

using SimpleJSON;
using controller.Utilities;
using UnityEngine.UI;
using System.Net.Sockets;
using System.Text;

public class NetworkService : Singleton<NetworkService>{

    //seng
    [SerializeField]
    int[] parameters;
    public void SetParameters(int[] parameters) { this.parameters = parameters;}

    private bool isRunning = true;
    private Thread serverThread;

    public delegate void ClickAction(string message);
    public event ClickAction OnMessageReceived;

    public String server_port;
    public String server_xml_rpc_url;
    public String client_port;
    public String client_xml_rpc_url;

    JSONObject controller_object = new JSONObject();
    int index = 0;

    private String message = "";
    HttpListener listener = null;
    ISumAndDiff proxy;

    public string gameobjectName = null;

    delegate void SetTextCallback(String text);

    public String ServerPort {
        get { return this.server_port; }
        set { this.server_port = value; }
    }

    public String ServerURL {
        get { return this.server_xml_rpc_url; }
        set { this.server_xml_rpc_url = value; }
    }

    public String ClientPort {
        get { return this.client_port; }
        set { this.client_port = value; }
    }

    public String ClientURL {
        get { return this.client_xml_rpc_url; }
        set { this.client_xml_rpc_url = value; }
    }

    public String Message
    {
        get { return this.message; }
        set
        {
            this.message = value;
            if (OnMessageReceived != null)
                OnMessageReceived(value);
        }
    }

    // Use this for initialization
    void Start () {

        StartServer();
        
    }

    private void startThread()
    {
        try
        {
            //Create the XML-RPC Server connection
            listener = new HttpListener();
            listener.Prefixes.Add("http://" + server_xml_rpc_url + ":" + server_port + "/");
            listener.Start();

            //listen for requests until told to stop
            while (isRunning)
            {
                HttpListenerContext context = listener.GetContext();
                ListenerService svc = new ControllerListenerService(this);
                svc.ProcessRequest(context);
            }

        }
        catch (Exception e)
        {
            if (isRunning)
            {
                Debug.Log("Caught exception while running listener:\n" + e);
            }
        }
        finally
        {
            //cleanup
            if (listener != null && listener.IsListening)
            {
                try
                {
                    listener.Stop();
                    listener = null;
                }
                catch (Exception e)
                {
                    Debug.Log("Caught exception while running listener:\n" + e);
                }
            }
        }
    }

    /// <summary>
    /// Setup this class and any threads/connections needed
    /// </summary>
    private void setup()
    {
        /*if (serverThread == null || !serverThread.IsAlive) { serverThread = new Thread(startThread); }

        if (!serverThread.IsAlive)
        {
            serverThread.Start();
        }*/

        /// Client RPC setup
        proxy = (ISumAndDiff)XmlRpcProxyGen.Create(typeof(ISumAndDiff));
        proxy.Url = "http://" + client_xml_rpc_url + ":" + client_port + "/";
        proxy.NonStandard = XmlRpcNonStandard.AllowInvalidHTTPContent;
        proxy.Timeout = 5000;
        Debug.Log("Connect");
    }

    public void StartServer() {
        setup();
    }

    public void StopServer() {
        stop();
    }

    /// <summary>
    /// Establish a new incoming connection from client
    /// </summary>
    /// <param name="data"></param>
    public void Connect()
    {
        // Do something to show connection
        //JSONObject testconnection = new JSONObject();
        //testconnection["test"] = "1";
        //this.Message = testconnection.ToString();
        //status.image.color = Color.green;
        //status.gameObject.GetComponentInChildren<Text>().text = "Connected";
    }

    /// <summary>
    /// Drops and Close a connection from client
    /// </summary>
    /// <param name="data"></param>
    public void Disconnect()
    {
        // Do something to show disconnected

    }

    /// <summary>
    /// Stop any threads/connections created by this class
    /// </summary>
    public void stop()
    {
        isRunning = false;

        if (listener != null)
        {
            listener.Stop();
        }
    }

    



#region CLIENT_CALLS

    /// <summary>
    /// Send data to HoloLens app
    /// </summary>
    /// <param name="data">Data to send</param>
    public void SendTestRequest()
    {
        try
        {
            //string other = proxy.Test(90, 50, 76, 88, 81, 78, 82);
            //this.Message = proxy.Test(90, 50, 76, 88, 81, 78, 82);
            this.Message = proxy.Test(parameters[3], parameters[4], parameters[0], parameters[5], parameters[2], 50, parameters[1]);
            //TextAsset t = Resources.Load<TextAsset>("data");
            //this.Message = t.text;
        }
        catch (Exception e)
        {
            Debug.Log("Caught exception while trying to notify a message:\n" + e);
        }
        // This constructor arbitrarily assigns the local port number.
        
    }



#endregion

}

#region XML-RPC Server

/**************************************XML-RPC Server stuff ************************************************************************************/
/// <summary>
/// The derived class which contains the implementation of the methods required in the XML-RPC service supported
/// by this applications XML-RPC server.
/// </summary>
public class ControllerListenerService : ListenerService
{
    // Used to update the form's received message listbox
    private NetworkService gameobject;

    public ControllerListenerService(NetworkService form)
    {
        this.gameobject = form;
    }

    [XmlRpcMethod("network.Connect", Description = "Test Connection with the Client")]
    public bool Connect()
    {
        //do something, in this case show some text on the dialog
        gameobject.Connect();
        return true;
    }

    [XmlRpcMethod("network.Disconnect", Description = "Test Connection with the Client")]
    public bool Disconnect()
    {
        //do something, in this case show some text on the dialog
        gameobject.Disconnect();
        return true;
    }

    [XmlRpcMethod("network.Test", Description = "Invoking Test")]
    public bool Test(string msg)
    {
        //When Selection an asset
        Debug.Log("message received");
        return true;
    }

}

public enum Command { Players, Costs }

/// <summary>
/// Handles XML-RPC requests
/// </summary>
public abstract class ListenerService : XmlRpcHttpServerProtocol
{
    public virtual void ProcessRequest(HttpListenerContext RequestContext)
    {
        try
        {
            IHttpRequest req = new ListenerRequest(RequestContext.Request);
            IHttpResponse resp = new ListenerResponse(RequestContext.Response);
            HandleHttpRequest(req, resp);
            RequestContext.Response.OutputStream.Close();
        }
        catch (Exception ex)
        {
            // "Internal server error"
            RequestContext.Response.StatusCode = 500;
            RequestContext.Response.StatusDescription = ex.Message;
        }
    }
}

/// <summary>
/// Provides access to .Net HttpListenerRequest classes
/// </summary>
public class ListenerRequest : IHttpRequest
{
    public ListenerRequest(HttpListenerRequest request)
    {
        this.request = request;
    }

    public Stream InputStream
    {
        get { return request.InputStream; }
    }

    public string HttpMethod
    {
        get { return request.HttpMethod; }
    }

    private HttpListenerRequest request;
}

/// <summary>
/// Provides access to the .Net HttpListenerResponse classes
/// </summary>
public class ListenerResponse : IHttpResponse
{
    public ListenerResponse(HttpListenerResponse response)
    {
        this.response = response;
    }

    string IHttpResponse.ContentType
    {
        get { return response.ContentType; }
        set { response.ContentType = value; }
    }

    TextWriter IHttpResponse.Output
    {
        get { return new StreamWriter(response.OutputStream); }
    }

    Stream IHttpResponse.OutputStream
    {
        get { return response.OutputStream; }
    }

    int IHttpResponse.StatusCode
    {
        get { return response.StatusCode; }
        set { response.StatusCode = value; }
    }

    string IHttpResponse.StatusDescription
    {
        get { return response.StatusDescription; }
        set { response.StatusDescription = value; }
    }

    bool IHttpResponse.SendChunked
    {
        get { return true; }
        set { response.SendChunked = value; }
    }

    long IHttpResponse.ContentLength
    {
        set { response.ContentLength64 = value; }
    }

    private HttpListenerResponse response;
}

/*****************************************(end) XML-RPC Server stuff ********************************************************************************/
#endregion

#region XML-RPC Client

/***************************************** XML-RPC Client stuff ********************************************************************************/

/// <summary>
/// This defines the various methods on the GIFT XML-RPC server that can be called in this C# application
/// </summary>
public interface ISumAndDiff : IXmlRpcProxy
{

    /// <summary>
    /// This method is used to check a connection between two apps.
    /// </summary>
    [XmlRpcMethod("network.Connect")]
    bool Connect();

    /// <summary>
    /// This method is used to disconnect and app from another.
    /// </summary>
    [XmlRpcMethod("network.Disconnect")]
    bool Disconnect();

    /// <summary>
    /// Method to send the pointer position from the phone to the Hololens world.
    /// </summary>
    /// <param name="msg">Message to be sent to the server with the position.</param>
    /// <returns>True if the message received, False otherwise.</returns>
    [XmlRpcMethod("network.Test")]
    string Test(int fin, int mark, int acc, int sp, int drib, int pos, int bc);

}

/*****************************************(end) XML-RPC Client stuff ********************************************************************************/

#endregion
