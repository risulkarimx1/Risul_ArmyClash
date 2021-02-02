using UniRx;
using UnityEngine;

public class Enemy 
{
    public ReactiveProperty<int> Life { get; private set; }
    public IReadOnlyReactiveProperty<bool> IsDead { get; private set; }

    public ReactiveCommand ResetCommand { get; private set; }

    public Enemy(int initLife)
    {
        Life = new ReactiveProperty<int>(initLife);
        IReadOnlyReactiveProperty<bool> x = Life.Select(l => l <= 0).ToReactiveProperty();
        ResetCommand = new ReactiveCommand();
        ResetCommand.Subscribe(_ =>
        {
            Debug.Log($"reset command pressed");
        });
    }

    public void ReduceLife()
    {
        Life.Value -= 100;
    }
}
