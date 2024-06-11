using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : MonoBehaviour
{
    public  Transform    ownerTransform;
    private LayerMask    ignoreLayer;
    public  float        damage;
    private float        radius;
    public  EffectData?  effect;

    public void Set(Transform ownerTransform, LayerMask ignoreLayer, float damage, float radius, EffectData? effect = null)
    {
        this.ownerTransform = ownerTransform;
        this.damage = damage;
        var c2d = gameObject.GetComponent<CircleCollider2D>();
        c2d.radius = radius;
        c2d.excludeLayers = ignoreLayer;
        this.effect = effect;
    }
    public void Set(float damage, EffectData? effect = null)
    {
        this.damage = damage;
        this.effect = effect;
    }
}
