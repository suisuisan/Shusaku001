using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

/// <summary>
/// ゲームステート（状態のenum）
/// </summary>
public enum GameState
{
    Initializing,
    Ready,
    Battle,
    Result,
    Finished
}

//これはなに
public class GameStateReactiveProperty : ReactiveProperty<GameState>
{
    public GameStateReactiveProperty()
    {
    }
    public GameStateReactiveProperty(GameState in_initialvalue)
        :base(in_initialvalue)
    {
    }
}

/// <summary>
/// ゲーム管理マネージャ
/// </summary>
public class MainGameManager : MonoBehaviour, IGameStateProvider
{
    //[RequireComponent(typeof(PlayerProvider))]
    //[RequireComponent(typeof(EnemyProvider))]

    //プレーヤープロバイダ
    private PlayerProvider playerProvider;

    //敵プロバイダ
    private EnemyProvider enemyProvider;

    //タイムマネージャー
    private GameTimeManager timeManager;

    private PlayerCore p_core;

    [SerializeField]
    private Vector3 playerpos;

    //Current Game State property
    private GameStateReactiveProperty CurrentState
        = new GameStateReactiveProperty(GameState.Initializing);
    public IReadOnlyReactiveProperty<GameState> CurrentGameState
    {
        get { return CurrentState; }
    }

    void Start()
    {
        timeManager = GetComponent<GameTimeManager>();
        playerProvider = GetComponent<PlayerProvider>();
        enemyProvider = GetComponent<EnemyProvider>();

        CurrentState.Subscribe(state =>
        {
            //state.Red();
            OnStateChanged(state);
        });
    }

    /// <summary>
    /// ステートチェンジ関数
    /// </summary>
    /// <param name="nextState">変更したいステート</param>
    void OnStateChanged(GameState nextState)
    {
         switch (nextState)
        {
            case GameState.Initializing:
                StartCoroutine(InitializeCoroutine());
                break;
            case GameState.Ready:
                StartCoroutine(ReadyCoroutine());
                break;
            case GameState.Battle:
                Battle();
                break;
            case GameState.Result:
                Result();
                break;
            case GameState.Finished:
                Finished();
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 初期化コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator InitializeCoroutine()
    {
        Debug.Log("--- INITIALIZE ---");
        //プレイヤー生成
        p_core = playerProvider.Create(PlayerID.Player1, playerpos, playerpos);

        CurrentState.Value = GameState.Ready;

        yield return null;
        
    }

    /// <summary>
    /// 準備コルーチン
    /// </summary>
    /// <returns></returns>
    private IEnumerator ReadyCoroutine()
    {
        Debug.Log("--- READY---");
        //Readyタイマを動かして0になったらバトルへ
        timeManager.ReadyTime
            .FirstOrDefault(x => x == 0)
            .Delay(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentState.Value = GameState.Battle)
            .AddTo(gameObject);
        yield return null;
        timeManager.StartGameReadyCountDown();
        
    }

    /// <summary>
    /// バトルステート
    /// </summary>
    private void Battle()
    {
        Debug.Log("--- BATTLE ---");

        //いまいちかきかたわからぬ～
        //敵の出現数が0になったら????とりあえずうごいた
        /*enemyProvider.enemyCount
            .FirstOrDefault(x => x == 0)
            .Delay(TimeSpan.FromSeconds(1))
            .Subscribe(_ => CurrentState.Value = GameState.Result)
            .AddTo(gameObject);*/

        enemyProvider.StartEnemyStartEnemySequence();

        //死んだらリザルト
        p_core.OnDied
            .Subscribe(x =>
            {
                CurrentState.Value = GameState.Result;
            })
            .AddTo(gameObject);

    }

    private void Finished()
    {

    }

    /// <summary>
    /// 結果表示
    /// </summary>
    private void Result()
    {
        Debug.Log("--- RESULT ---");
    }

}
