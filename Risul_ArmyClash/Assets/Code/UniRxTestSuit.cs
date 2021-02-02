using System;
using System.Collections;
using System.Threading;
using TMPro;
using UniRx;
using UniRx.Async;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UniRxTestSuit : MonoBehaviour
{
    public Button ResetButton;
    public Button ClickButton;
    public TextMeshProUGUI LifeText;
    
    private IDisposable cancel;
    private IDisposable webcancel;

    void Start()
    {
        var enemy = new Enemy(1000);
        
        ClickButton.OnClickAsObservable().Subscribe(async _ =>
        {
            enemy.ReduceLife();
            await Waiting();
        });
        

        enemy.ResetCommand.BindTo(ResetButton);

        enemy.Life.Subscribe(v => LifeText.text = v.ToString());
    }

    public async UniTask Waiting()
    {
        Debug.Log($"starting to wait");
        await UniTask.Delay(1000);
        Debug.Log($"done waiting");
    }
    
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cancel = Observable.FromCoroutine(AsyncA)
                .SelectMany(AsyncB).Subscribe();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            webcancel = GetWWW("http://www.google.com").Subscribe(x=> Debug.Log($"{x}"));
        }
    }

    public IObservable<string> GetWWW(string url)
    {
        IEnumerator Coroutine(IObserver<string> observer, CancellationToken token) => GetWWWCore(url, observer, token);

        IObservable<string> cr = Observable.FromCoroutine<string>(Coroutine);

        return cr;
    }

    public IEnumerator GetWWWCore(string url, IObserver<string> observer, CancellationToken cancelToken)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        while (www.isDone && !cancelToken.IsCancellationRequested)
        {
            yield return null;
        }
        
        if(cancelToken.IsCancellationRequested) yield break;

        if (www.error != null)
        {
            observer.OnError(new Exception(www.error));
        }
        else
        {
            observer.OnNext(www.downloadHandler.text);
            observer.OnCompleted();
        }
        
    }

    void OnDestroy()
    {
        cancel?.Dispose();
        webcancel?.Dispose();
    }
    
    IEnumerator AsyncA()
    {
        Debug.Log("a start");
        yield return new WaitForSeconds(1);
        Debug.Log("a end");
    }

    IEnumerator AsyncB()
    {
        Debug.Log("b start");
        yield return new WaitForEndOfFrame();
        Debug.Log("b end");
    }
}
