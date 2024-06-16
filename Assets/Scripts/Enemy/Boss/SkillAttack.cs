using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillAttack : MonoBehaviour
{

    [SerializeField] GameObject hitSkill;

    public void HitSkill()
    {
        Debug.Log("켜짐");
        hitSkill.SetActive(true);
    }
    public void HitSkillEnd()
    {
        Debug.Log("꺼짐");
        hitSkill.SetActive(false);
    }
}
