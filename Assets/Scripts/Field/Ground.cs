using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Ground:MonoBehaviour{

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("aaaaaaaaaaaaaa");
        var damageApplicable = other.gameObject.GetComponent<IDamageApplicate>();

        if (damageApplicable == null) return;
        //ダメージ構造体を新規作成。情報を組み込んでダメージアプライ関数に渡す。即死
        var damage = new DamageInfo
        {
            Attacker = new SNonPlayerAttacker(),
            Value = 9999999,
            Direction = 0,
            Type = AttackType.Ground
        };
        damageApplicable.ApplyDamage(damage);
    }
}
