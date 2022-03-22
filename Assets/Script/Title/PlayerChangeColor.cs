using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChangeColor : MonoBehaviour
{
    [SerializeField] Material humanMaterial;
    [SerializeField] Material clearMaterial1;
    [SerializeField] Material clearMaterial2;
    [SerializeField] Material clearMaterial3;
    [SerializeField] Material clearMaterial4;
    [SerializeField] Material clearMaterial5;
    [SerializeField] Material clearMaterial6;
    [SerializeField] Material clearMaterial7;
    [SerializeField] Material clearMaterial8;
    [SerializeField] Material clearMaterial9;
    [SerializeField] Material clearMaterial10;
    [SerializeField] GameObject player_temp;
    // Start is called before the first frame update
    void Start()
    {
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial1);

        StartCoroutine(TransparentPlayer());
    }

    public IEnumerator TransparentPlayer()
    {
        yield return new WaitForSeconds(1.0f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial2);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial3);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial4);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial5);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial6);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial7);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial8);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial9);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(clearMaterial10);
        yield return new WaitForSeconds(0.3f);
        player_temp.GetComponent<Renderer>().material = new Material(humanMaterial);
        yield break;
    }
}
