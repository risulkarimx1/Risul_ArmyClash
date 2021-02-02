using System;
using Moq;
using NUnit.Framework;
using UniRx;
using UnityEngine;
using Zenject;

[TestFixture]
public class MessageServiceManagerTest : ZenjectUnitTestFixture
{
    [Inject] private MessageServiceManager _testObject;
    private Mock<IMessageAPI> _mockApi;
    private Mock<IMessageClient> _mockClient;

    [Test]
    public void Test()
    {
        Assert.IsTrue(true);
    }

    [SetUp]
    public void Initialize()
    {
        _mockApi = new Mock<IMessageAPI>();
        _mockClient = new Mock<IMessageClient>();

        Container.Bind<IMessageAPI>().FromInstance(_mockApi.Object);
        Container.Bind<IMessageClient>().FromInstance(_mockClient.Object);

        Container.Bind<MessageServiceManager>().AsSingle();

        Container.Inject(this);
    }

    /*
     * we want to test that,
     * when MessageServiceManager.Connect is called
     * it calls IMessageApi.Connect() method inside
     * _mockApi.Verify(m => m.Connect()); verifies when
     * _testObject.Connect() is called, _api.Connect() is called
     * simultaneously 
     */

    [Test]
    public void Calling_connect_calls_Connect_On_API()
    {
        _testObject.Connect(string.Empty);
        _mockApi.Verify(m => m.Connect());
    }

    

    /*
     * Let’s test that when we call Connect()
     * on our MessageServiceManager we call Connect
     * on our IMessageClient with the correct arguments.
     */

    [Test]
    public void Calling_Connect_Calls_Connect_On_Client_With_API_and_Channel_args()
    {
        var channel = "test";
        _testObject.Connect(channel);
        _mockClient.Verify(m=>m.Connect(_mockApi.Object, channel));
    }
    
    /*
     * we want to be sure that
     * api.Connect() gets called before
     * client.Connect()
     */

    [Test]
    public void Calling_Connect_Calls_apiConnect_then_clientConnect()
    {
        var channel = "test";
        var apiConnectCalled = false;
        var clietConnectCalled = false;

        // apiConnectCalled will be true when api.Connect method will be called
        _mockApi.Setup(m => m.Connect())
            .Callback(() => apiConnectCalled = true);

        // if for some reason, api.connect is not called before client.connect
        // the line callOrderCorrec = apiConnectedCalled will be making
        // callOrderCorrect value to false
        _mockClient.Setup(m => m.Connect(It.IsAny<IMessageAPI>(), It.IsAny<string>()))
            .Callback(() => clietConnectCalled = apiConnectCalled? true: false);

        
        _testObject.Connect(channel);
        Assert.True(apiConnectCalled && clietConnectCalled);
    }
    
    /*
     * the scenario is
     * when when IMessageClient fails to connect,
     * it fires an event OnConnectionError.
     * if MessageServiceManager sees this error (by subscribing the event)
     * it will throw an exception
     *
     * we can use Raise to make the event happen in mock
     */

    [Test]
    public void Exception_thrown_when_client_FailedToConnect_event_Fired()
    {
        Assert.Throws<Exception>(() =>
        {
            _mockClient.Raise(m=>m.OnConnectionFailedEvent += null, new EventArgs());
        });
    }

    /*
     *We’re going to generate a stream of messages
     * that are coming from an event in the Client
     *
     */

    [Test]
    public void New_Message_Emits_To_Message_Stream()
    {
        var newMessage = false;
        _testObject.MessageReceieveObservable.Subscribe(_ =>
        {
            newMessage = true;
        });
        
        _mockClient.Raise(m=>m.OnMessageReceivedEvent += null, new OnMessageReceievedArgs());
        Assert.IsTrue(newMessage);
    }
}
