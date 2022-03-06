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

    // Start is called before the first frame update
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine("showThunder");
    }

    private IEnumerator showThunder()
    {
        while (true)
        {
            Interval = Random.Range(5f, 15f);
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
