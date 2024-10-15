using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : RoomObjectTrigger
{
    [SerializeField] private GameObject comment;
    [SerializeField] private string nextFloor;
    private bool isPlayerColliding = false;

    public override void OnRoomEnter(Room room)
    {
        gameObject.SetActive(false);
    }

    public override void OnRoomExit(Room room)
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            comment.SetActive(true);
            isPlayerColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            comment.SetActive(false);
            isPlayerColliding = false;
        }
    }

    private void Update()
    {
        if (isPlayerColliding && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("넘어간다~");
            SceneManager.LoadScene(nextFloor);
        }
    }
}
