using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public interface IGameStateProvider
{

    IReadOnlyReactiveProperty<GameState> CurrentGameState { get; }
}
