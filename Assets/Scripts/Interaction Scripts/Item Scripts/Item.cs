using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item")]
public class Item : ScriptableObject, ItemData
{
    public int                     _number;
    public  int                     number      { get=>_number; set=>_number = value; }
    public string                  _name;
    public  string                  name        { get=>_name; set=>_name = value; }
    public Sprite                  _img;
    public  Sprite                  img         { get=>_img; set=>_img = value; }
    public string                  _info;
    public  string                  info        { get=>_info; set=>_info = value; }
    public ItemData.RatingType     _ratingType;
    public  ItemData.RatingType     ratingType  { get=>_ratingType; set=>_ratingType = value; }
}
public interface ItemData
{
    public enum RatingType { NONE, COMMON, RARE, UNIQUE, LEGENDARY }

    public int          number      { get; set; }
    public string       name        { get; set; }
    public Sprite       img         { get; set; }
    public string       info        { get; set; }
    public RatingType   ratingType  { get; set; }
}
