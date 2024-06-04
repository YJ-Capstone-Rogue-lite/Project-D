using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPC_Char : MonoBehaviour
{
    [SerializeField] private GameObject TextBox;
    [SerializeField] private TextMeshPro npctext;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TextBox.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TextBox.SetActive(false);
        }
    }
}
