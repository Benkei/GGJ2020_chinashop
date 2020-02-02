using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0649

public class SpawnController : MonoBehaviour
{

    [SerializeField] GameObject spawnPoints;
    [SerializeField] GameObject[] spawnPrefabs;
    GameObject plates;
  

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(plateFall(0.7f));

    }



    private IEnumerator plateFall(float timer)
    {
        while (true)
        {
            GameObject go = Instantiate(spawnPrefabs[0]);
            int count = spawnPoints.transform.childCount;
            Transform spawnPoint = spawnPoints.transform.GetChild(Random.Range(0, count));
            Debug.Log(count);
            go.transform.position = spawnPoint.position;

            yield return new WaitForSeconds(timer);
        }
    }



}
