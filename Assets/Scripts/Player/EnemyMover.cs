using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class EnemyMover : BaseComponent
{
    [SerializeField]
    private Vector3 Offset;

    protected override void OnInitialize()
    {
        this.UpdateAsObservable()
                   .Subscribe(_ => this.transform.position += Offset);

    }
}
