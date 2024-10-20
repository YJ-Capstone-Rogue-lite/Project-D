using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Fireball : MonoBehaviour
{
    public GameObject fireballPrefab;
    public int fireballCount = 8;
    public float radius = 5f;
    public float fireballSpeed = 5f;
    public float spawnDelay = 0.5f;

    private List<GameObject> fireballs = new List<GameObject>();

    void Start()
    {
        StartCoroutine(SpawnFireballsSequentially());
    }

    IEnumerator SpawnFireballsSequentially()
    {
        float angleStep = 360f / fireballCount;
        float angle = 0f;

        for (int i = 0; i < fireballCount; i++)
        {
            float fireballDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float fireballDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector3 fireballPosition = new Vector3(fireballDirX, fireballDirY, 0) * radius + transform.position;

            GameObject fireball = Instantiate(fireballPrefab, fireballPosition, Quaternion.identity);
            fireballs.Add(fireball);

            angle -= angleStep;

            yield return new WaitForSeconds(spawnDelay);
        }

        FireAllBalls();
    }
    void FireAllBalls()
    {
        foreach (GameObject fireball in fireballs)
        {
            if (fireball != null)
            {
                Vector3 fireballDirection = (fireball.transform.position - transform.position).normalized;

                Rigidbody2D rb = fireball.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.velocity = fireballDirection * fireballSpeed;
                }
            }
        }
    }
}