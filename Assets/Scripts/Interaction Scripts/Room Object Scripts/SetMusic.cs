using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetMusic : MonoBehaviour, IRoomObjectTrigger
{
    public AudioClip startMuic;
    public AudioClip endMuic;

    public void RoomEnter(Room room) => SoundManager.SetMusic(startMuic);

    public void RoomExit(Room room) => SoundManager.SetMusic(endMuic);
}
