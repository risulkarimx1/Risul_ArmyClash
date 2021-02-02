using System;
using UniRx;
using UnityEngine.EventSystems;
using Zenject;

public class MessageServiceManager
{
    [Inject] private IMessageClient _client;
    [Inject] private IMessageAPI _api;
    
    private Subject<OnMessageReceievedArgs> _messageReceivedSubject = new Subject<OnMessageReceievedArgs>();

    public IObservable<OnMessageReceievedArgs> MessageReceieveObservable => _messageReceivedSubject;

    [Inject]
    public void Init()
    {
        _client.OnConnectionFailedEvent += OnClientConnectionFailed;
        _client.OnMessageReceivedEvent += ClientOnMessageReceivedEvent;
    }

    private void ClientOnMessageReceivedEvent(object sender, OnMessageReceievedArgs e)
    {
        _messageReceivedSubject.OnNext(e);
    }

    private void OnClientConnectionFailed(object sender, EventArgs e)
    {
        throw new Exception("Client failed to connect");
    }

    public void Connect(string channel)
    {
        _api.Connect();
        _client.Connect(_api, channel);
    }
}

public interface IMessageAPI
{
    void Connect();
}

public interface IMessageClient
{
    void Connect(IMessageAPI mockApiObject, string channel);
    event EventHandler OnConnectionFailedEvent;
    event EventHandler<OnMessageReceievedArgs> OnMessageReceivedEvent;
}

public class OnMessageReceievedArgs
{
}