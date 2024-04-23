using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChar : Character
{

    // 데미지 데이터
    [SerializeField]
    protected DamageData damageData;


    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        base.OnTriggerEnter2D(collider);
    }
    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
    }
}
