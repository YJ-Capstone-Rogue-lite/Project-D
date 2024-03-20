using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item_Manager : ScriptableObject
{
    public enum ItemType
    {
        Weapon,
        Accessory,
        Potion  
    }


    public ItemType itemType;

    [System.Serializable]
    public struct STAT
    {
        public string name;
        public int value;
    }
    
    public List<STAT> stats = new List<STAT>();

}

