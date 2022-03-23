using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class ShowHP : MonoBehaviour
{
    //HP10個を受け取ってPlayerのHP表示をするストレス発散用脳死プログラム
    [SerializeField] GameObject HP1;
    [SerializeField] GameObject HP2;
    [SerializeField] GameObject HP3;
    [SerializeField] GameObject HP4;
    [SerializeField] GameObject HP5;
    [SerializeField] GameObject HP6;
    [SerializeField] GameObject HP7;
    [SerializeField] GameObject HP8;
    [SerializeField] GameObject HP9;
    [SerializeField] GameObject HP10;

    //赤い画面
    [SerializeField] Image hitImg;

    [SerializeField] Volume _volume;
    private Vignette _vignette;
    private FilmGrain _film;

    private bool once = true;
    private bool once2 = true;

    [SerializeField] GameObject Player;
    Player _player;

    // Start is called before the first frame update
    void Start()
    {
        HP1.SetActive(true);
        HP2.SetActive(true);
        HP3.SetActive(true);
        HP4.SetActive(true);
        HP5.SetActive(true);
        HP6.SetActive(true);
        HP7.SetActive(true);
        HP8.SetActive(true);
        HP9.SetActive(true);
        HP10.SetActive(true);

        hitImg.color = Color.clear;

        _volume.profile.TryGet<Vignette>(out _vignette);
        _volume.profile.TryGet<FilmGrain>(out _film);

        _player = Player.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator coroutine = HitEffect();
        switch (_player._life)
        {
            case 9:
                HP10.SetActive(false);
                if (once2 == true)
                {
                    once = true;
                    once2 = false;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 8:
                HP9.SetActive(false);
                if (once == true)
                {
                    once = false;
                    once2 = true;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 7:
                HP8.SetActive(false);
                if (once2 == true)
                {
                    once = true;
                    once2 = false;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 6:
                HP7.SetActive(false);
                if (once == true)
                {
                    once = false;
                    once2 = true;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 5:
                HP6.SetActive(false);
                if (once2 == true)
                {
                    once = true;
                    once2 = false;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 4:
                HP5.SetActive(false);
                if (once == true)
                {
                    once = false;
                    once2 = true;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 3:
                HP4.SetActive(false);
                if (once2 == true)
                {
                    once = true;
                    once2 = false;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 2:
                HP3.SetActive(false);
                if (once == true)
                {
                    once = false;
                    once2 = true;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 1:
                HP2.SetActive(false);
                if (once2 == true)
                {
                    once = true;
                    once2 = false;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            case 0:
                HP1.SetActive(false);
                if (once == true)
                {
                    once = false;
                    once2 = true;
                    StopCoroutine(coroutine);
                    coroutine = HitEffect();
                    StartCoroutine(coroutine);
                }
                break;
            default:
                break;
        }
    }

    private IEnumerator HitEffect()
    {
        hitImg.color = new Color(0.2f, 0, 0, 0.3f);
        _film.intensity.value = 0.75f;
        _vignette.intensity.value = 0.6f;

        for(int i = 0; i < 30; i++)
        {
            hitImg.color = hitImg.color - new Color(0f, 0, 0, 0.01f);
            _vignette.intensity.value = _vignette.intensity.value - 0.01f;
            yield return new WaitForSeconds(0.1f);
        }
        _film.intensity.value = 0f;
        _vignette.intensity.value = 0.4f;
        hitImg.color = new Color(0.2f, 0, 0, 0);
        yield break;
    }
}
