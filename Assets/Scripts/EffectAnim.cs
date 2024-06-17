using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnim : MonoBehaviour
{
    [SerializeField] private GameObject gun_Effect;
    [SerializeField] private Animator gun_anim;
    private void Start()
    {
        gun_anim = GetComponent<Animator>();
        
    }
    public void Gun_effectOn()
    {
        gun_Effect.SetActive(true);
    }
    public void Gun_effectOff()
    {
        gun_Effect.SetActive(false);
    }
}
