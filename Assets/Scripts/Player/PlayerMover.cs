using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerMover : BaseComponent {

    [SerializeField]
    private float MoveSpeed;

    [SerializeField]
    private float LimitPosition;

    protected override void OnInitialize()
    {
        InputEventProvider.MoveVector
            .Subscribe(x =>
            {
                var movevalue = x.normalized.x * MoveSpeed;
                move(movevalue);
            });

    }

    /// <summary>
    /// 移動処理
    /// </summary>
    /// <param name="in_val"></param>
    private void move( float in_val)
    {
        Vector3 currentpos = transform.position;

        Vector3 npos = transform.position;

        //移動制限をかける
        if (currentpos.x > LimitPosition) {
            npos.x = LimitPosition;
            transform.position = npos;
        }
        if (-LimitPosition > currentpos.x) {
            npos.x = -LimitPosition;
            transform.position = npos;
        }

            gameObject.transform.position += new Vector3(in_val, 0, 0);
    }
   }
