using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleFall : MonoBehaviour
{
    [SerializeField] GameObject TitleG;
    [SerializeField] GameObject Titleh;
    [SerializeField] GameObject Titleo;
    [SerializeField] GameObject Titles;
    [SerializeField] GameObject Titlet;
    [SerializeField] GameObject TitleP;
    [SerializeField] GameObject Titleu;
    [SerializeField] GameObject Titlen;
    [SerializeField] GameObject Titlec;
    [SerializeField] GameObject Titleh2;
    // Start is called before the first frame update
    void Awake()
    {
        TitleG.SetActive(false);
        Titleh.SetActive(false);
        Titleo.SetActive(false);
        Titles.SetActive(false);
        Titlet.SetActive(false);
        TitleP.SetActive(false);
        Titleu.SetActive(false);
        Titlen.SetActive(false);
        Titlec.SetActive(false);
        Titleh2.SetActive(false);
        StartCoroutine("FallTitle");
    }

    private IEnumerator FallTitle()
    {
        TitleG.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titleh.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titleo.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titles.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titlet.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.2f);
        TitleP.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titleu.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titlen.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titlec.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Titleh2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        yield break;
    }
}
