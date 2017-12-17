using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletBase : MonoBehaviour {
    public IAttackerInfo AttackerInfo { get; set; }
    protected float DamageValue { get; set; }
}
