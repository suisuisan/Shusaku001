using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class EnemyCore : Core, IDamageApplicate
{
    [SerializeField]
    private Vector2 _fieldsize;
    public Vector2 fieldsize { get { return _fieldsize; } }

    //ＨＰ情報
    [SerializeField]
    private int _HP;
    public int HP { get { return _HP; } }

    //id
    private int _id;
    //readonly id
    public int id { get { return _id; } }


    private PlayerParameters DefaultPlayerParameter = new PlayerParameters();

    //これがサブジェクト。これにもろもろ登録。
    private Subject<DamageInfo> _damageSubject = new Subject<DamageInfo>();
    //サブジェクトをいじられないようにgetだけできるようにしてる
    public IObservable<DamageInfo> OnDamaged { get { return _damageSubject; } }

    //これが死因サブジェクト。これにもろもろ登録。
    private Subject<DeadReason> _deadSubject = new Subject<DeadReason>();
    //サブジェクトをいじられないようにgetだけできるようにしてる
    public IObservable<DeadReason> OnDied { get { return _deadSubject; } }

    void Start()
    {
        //ダメージをうけたらＨＰを減らす
        OnDamaged
            .Subscribe(x => _HP = _HP - x.Value)
            .AddTo(gameObject);

        //ＨＰが0になったら自分自身を破棄する
        OnDamaged
            .Where(x => _HP<0)
            .Subscribe(x => Destroy(this.gameObject))
            .AddTo(gameObject);

        //死んだ時の処理とイベント
        //OnDied
        //いまはとくになし
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
    public void Initialize(int in_id, Vector3 in_pos, EnemyProvider in_provider)
    {
        _id = in_id;

        //わからん
        _onInitializeAsyncSubject.OnNext(in_id);
        _onInitializeAsyncSubject.OnCompleted();

    }
}
