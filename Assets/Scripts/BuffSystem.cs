using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class BuffSystem : MonoBehaviour
{
    public Character character;

    private void Update()
    {
        if(character == null)
        {
            character = FindObjectOfType<Character>();
        }
    }
    public int ATK_UPgrade()
    {
        int needcoin = 1;
        if (character.damageUpStack >= 0)
        {
            needcoin = needcoin << character.damageUpStack;
        }
        return needcoin;
    }

    public int HP_UPgrade()
    {
        int needcoin = 1;
        if (character.max_hp_UPStack >= 0)
        {
            needcoin = needcoin << character.max_hp_UPStack;
        }
        return needcoin;
    }

    public int MoveMent_UPgrade()
    {
        int needcoin = 1;
        if (character.movement_SpeedUpStack >= 0)
        {
            needcoin = needcoin << character.movement_SpeedUpStack;
        }
        return needcoin;
    }
}
