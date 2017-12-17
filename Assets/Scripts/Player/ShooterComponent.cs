using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class ShooterComponent : BaseComponent {

    [SerializeField]
    private GameObject BulletPrefab;

    protected override void OnInitialize()
    {
        InputEventProvider.ShootButtonActivate
            .Where(x=> x&& true)
            .Subscribe(_ =>
            {
                var Prefab = BulletPrefab;
                var go = Instantiate(Prefab);
                go.transform.position = gameObject.transform.position;

            }).AddTo(gameObject);
    }

}
