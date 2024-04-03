using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData : MonoBehaviour
{
    public  Transform    ownerTransform;
    private LayerMask    ignoreLayer;
    public  float        damage;
    private float        radius;
    public  Effect       effect;

    public void Set(Transform ownerTransform, LayerMask ignoreLayer, float damage, float radius, Effect effect)
    {
        this.ownerTransform = ownerTransform;
        this.damage = damage;
        var c2d = gameObject.GetComponent<CircleCollider2D>();
        c2d.radius = radius;
        c2d.excludeLayers = ignoreLayer;
    }
}
