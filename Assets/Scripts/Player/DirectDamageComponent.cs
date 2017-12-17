using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectDamageComponent : BaseComponent {
    [SerializeField]
    private int damagevalue;
    protected override void OnInitialize()
    {


    }
    void OnTriggerEnter(Collider other)
    {
        var damageApplicable = other.gameObject.GetComponent<IDamageApplicate>();

        if (damageApplicable == null) return;
        //ダメージ構造体を新規作成。情報を組み込んでダメージアプライ関数に渡す。
        var damage = new DamageInfo
        {
            Attacker = new SNonPlayerAttacker(),
            Value = damagevalue,
            Direction = 0,
            Type = AttackType.EnemyAttack
        };
        damageApplicable.ApplyDamage(damage);
    }
}
