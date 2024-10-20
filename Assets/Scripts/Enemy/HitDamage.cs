using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitDamage : MonoBehaviour
{
    public GameObject hitbox;

    public void Onhitbox()
    {

        hitbox.SetActive(true);
        Debug.Log("맞음");
    }
}
