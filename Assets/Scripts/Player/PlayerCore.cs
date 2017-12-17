using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerCore : Core, IDamageApplicate
{

    //player id
    private PlayerID _playerid;
    //readonly player id
    public PlayerID playerid { get { return _playerid; } }

    //ＨＰ情報
    [SerializeField]
    private int _HP;
    public int HP { get { return _HP; } }

    [SerializeField]
    private PlayerParameters DefaultPlayerParameter = new PlayerParameters();
    private ReactiveProperty<PlayerParameters> _currentPlayerParameter;
    public IReadOnlyReactiveProperty<PlayerParameters> CurrentPlayerParam { get { return _currentPlayerParameter; } }

    //これがサブジェクト。これにもろもろ登録。
    private Subject<DamageInfo> _damageSubject = new Subject<DamageInfo>();
    //サブジェクトをいじられないようにgetだけできるようにしてる
    public IObservable<DamageInfo> OnDamaged { get { return _damageSubject; } }

    //これがサブジェクト。これにもろもろ登録。
    private Subject<DeadReason> _deadSubject = new Subject<DeadReason>();
    //サブジェクトをいじられないようにgetだけできるようにしてる
    public IObservable<DeadReason> OnDied{ get { return _deadSubject; } }

    //ゲームステートを確認するためのインターフェース
    private IGameStateProvider gameStateProvider;
    public IReadOnlyReactiveProperty<GameState> CurrentGameState
    {
        get { return gameStateProvider.CurrentGameState; }
    }


    /// <summary>
    /// reset player parameter Default.
    /// </summary>
    public void ResetPlayerParamerter()
    {
        _currentPlayerParameter.Value = DefaultPlayerParameter;
    }

    /// <summary>
    /// Setting player parameter
    /// </summary>
    /// <param name="in_param"></param>
    public void SetPlayerParamerter(PlayerParameters in_param )
    {
        _currentPlayerParameter.Value = in_param;
    }


    void Awake()
    {
        _currentPlayerParameter = new ReactiveProperty<PlayerParameters>(DefaultPlayerParameter);

        //ダメージをうけたらＨＰを減らす
        OnDamaged
            .Subscribe(x => _HP = _HP - x.Value)
            .AddTo(gameObject);

        //ＨＰが0になったら自分自身を破棄する
        OnDamaged
            .Where(x => _HP < 0)
            .Subscribe(x => {
                _deadSubject.OnNext(new DeadReason(this._playerid, null));

                })
            .AddTo(gameObject);

        //死にましたときの命令
        OnDied
            .Subscribe(x => {
                Destroy(this.gameObject);

            })
            .AddTo(gameObject);


    }
    
    /// <summary>
    /// 自分に対してダメージを与える処理
    /// </summary>
    public void ApplyDamage(DamageInfo in_damage)
    {
        _damageSubject.OnNext(in_damage);
    }


    /// <summary>
    /// 初期化
    /// </summary>
    /// <param name="in_id">プレイヤーＩＤ</param>
    /// <param name="in_pos">リスポーン座標</param>
    public void Initialize(PlayerID in_id, Vector3 in_pos, PlayerProvider in_provider)
    {
        _playerid = in_id;

        _onInitializeAsyncSubject.OnNext((int)in_id);
        _onInitializeAsyncSubject.OnCompleted();
    }
}
