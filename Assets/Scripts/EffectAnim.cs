using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectAnim : MonoBehaviour
{
    [SerializeField] private GameObject gun_Effect;
    [SerializeField] private Animator gun_anim;
    private Animator effect_anim;
    private void Start()
    {
        gun_anim = GetComponent<Animator>();
        effect_anim = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        gun_anim?.Rebind();
        gun_anim?.Update(0);
    }
    private void OnDisable()
    {
        effect_anim?.Rebind();
        effect_anim?.Update(0);
        Gun_effectOff();
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
