using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class PlayerBullet : BulletBase
{

    //主人公の弾の攻撃力
    [SerializeField]
    private float offensivePow;

    [SerializeField]
    private Vector3 movingspeed;

    [SerializeField]
    private SphereCollider m_Collider;

    void Start()
    {
        // 移動しつづける
        this.UpdateAsObservable()
            .Subscribe(x =>
            {
                transform.position += movingspeed;
            });
        //画面外に出たら死ぬ
        this.ObserveEveryValueChanged(x => x.transform.position.y)
            .Where(x => x > 17.5)
            .Subscribe(__ => Destroy(this.gameObject))
            .AddTo(this);

    }

    void Reset()
    {
        offensivePow = 1.0f;
        m_Collider = GetComponent<SphereCollider>();
    }

    void OnTriggerEnter(Collider other)
    {
        var DamageApplible = other.gameObject.GetComponent<IDamageApplicate>();

        //接触対象が敵(隕石)であればダメージ処理に
        var enemy = other.GetComponent<EnemyCore>();
        if (enemy != null)
        {

            //敵のApply Damageを呼び出す
            var damage = new DamageInfo()
            {
               Attacker = this.AttackerInfo,
               Value = 10,
               Direction = 0,
               Type = AttackType.Bullet
           };
           DamageApplible.ApplyDamage(damage);
        }


    }
}
