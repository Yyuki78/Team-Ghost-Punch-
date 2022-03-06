using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateDust : MonoBehaviour
{
    [SerializeField]
    GameObject DustPrefab;
    [SerializeField]
    GameObject TissuePrefab;

    private float Distance = 18.0f;
    // Start is called before the first frame update
    void Awake()
    {
        StartGenerate();
        InvokeRepeating("UpdateMakePrefab", 0f, 10f);
    }

    private void StartGenerate()
    {
        for(int i = 0; i < 50; i++)
        {
            Instantiate(DustPrefab, new Vector3(Random.Range(-Distance, Distance), 1f, Random.Range(-Distance, Distance)), Quaternion.identity);
            Instantiate(TissuePrefab, new Vector3(Random.Range(-Distance, Distance), 1f, Random.Range(-Distance, Distance)), Quaternion.identity);
        }
        for (int i = 0; i < 50; i++)
        {
            Instantiate(DustPrefab, new Vector3(Random.Range(-Distance, Distance), 1f, Random.Range(-Distance, Distance)), Quaternion.identity);
        }
    }

    private void UpdateMakePrefab()
    {
        for (int i = 0; i < 10; i++)
        {
            Instantiate(DustPrefab, new Vector3(Random.Range(-Distance, Distance), 1f, Random.Range(-Distance, Distance)), Quaternion.identity);
            Instantiate(TissuePrefab, new Vector3(Random.Range(-Distance, Distance), 1f, Random.Range(-Distance, Distance)), Quaternion.identity);
        }
    }     
}
