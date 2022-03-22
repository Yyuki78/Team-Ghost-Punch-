using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderEffect : MonoBehaviour
{
    [SerializeField]
    GameObject Thunder;

    public AudioClip sound1;
    AudioSource audioSource;

    private float Interval;

    public bool canSee = false;

    //タイトルでは起動しない
    public bool IsTutrial = false;

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if(!IsTutrial) StartCoroutine("showThunder");
    }

    private IEnumerator showThunder()
    {
        while (true)
        {
            Interval = Random.Range(10f, 20f);
            yield return new WaitForSeconds(Interval);
            audioSource.PlayOneShot(sound1);
            Thunder.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Thunder.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            Thunder.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            Thunder.SetActive(false);
            canSee = true;
            yield return new WaitForSeconds(3f);
            canSee = false;
        }
    }
}
