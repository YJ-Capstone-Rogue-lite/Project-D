using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour, Item
{
    public int              number      { get; set; }
    public string           name        { get; set; }
    public Sprite           img         { get; set; }
    public string           info        { get; set; }
    public Item.RatingType  ratingType  { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
