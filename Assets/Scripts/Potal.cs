using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Potal : MonoBehaviour
{
    [SerializeField] private GameObject comment;
    private bool isPlayerColliding = false;

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
            SceneManager.LoadScene("Ingame");
        }
    }
}
