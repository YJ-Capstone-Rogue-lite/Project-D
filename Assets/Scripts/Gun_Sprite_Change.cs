using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun_Sprite_Change : MonoBehaviour
{
    public Sprite weapon_Change_sprite;

    public Weapon weapon_img;

    private void Start()
    {
        changeSprite();
    }
    public void changeSprite()
    {
        weapon_Change_sprite = weapon_img.img;
        Debug.Log("이미지 변경 :" + weapon_Change_sprite);
    }
}
