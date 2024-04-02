using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DamageData
{
    public Transform    ownerTransform;
    public LayerMask    ignoreLayer;
    public float        damage;
    public EffectData   effectData;
}
