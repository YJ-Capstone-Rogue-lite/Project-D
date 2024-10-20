using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject meteorsPrefab;

    private void Start()
    {
        StartCoroutine(CreateMeteorWithDelay());
    }
    private IEnumerator CreateMeteorWithDelay()
    {
        yield return new WaitForSeconds(2f); // 2초 대기

        Vector3 spawnPosition = new Vector3(transform.position.x, transform.position.y + 2.2f, transform.position.z);

        GameObject meteor = Instantiate(meteorsPrefab, spawnPosition, transform.rotation);

        yield return new WaitForSeconds(0.8f);

        Destroy(meteor);
        Destroy(gameObject);
    }
    
}
