using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : RoomObjectTrigger
{
    public AudioClip startMuic;
    public AudioClip endMuic;

    public override void OnRoomEnter(Room room) => SoundManager.SetMusic(startMuic);

    public override void OnRoomExit(Room room) => SoundManager.SetMusic(endMuic);
}
