using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Timer : MonoBehaviour
{
    Text text;
    float countTime = 0;
    int displaySecond = 0;
    int displayHour = 0;
    bool plus = false;

    private int LimitHour = 5;

    //他のScriptで変更はしない
    //使用したScriptはEnemyManager,EnemyStatus,EnemyLevel
    public static bool TimeOut = false;

    //時間切れ時のSE
    public AudioClip sound1;
    [SerializeField] AudioSource audioSource;

    //時間切れ時のEffect
    [SerializeField] Volume _volume;
    private Vignette _vignette;
    private FilmGrain _film;
    private LiftGammaGain _gamma;

    private bool once = true;

    private void Start()
    {
        text = GetComponent<Text>();

        _volume.profile.TryGet<Vignette>(out _vignette);
        _volume.profile.TryGet<FilmGrain>(out _film);
        _volume.profile.TryGet<LiftGammaGain>(out _gamma);

        TimeOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(text.color.a);
        // countTimeに、ゲームが開始してからの秒数を格納
        countTime += Time.deltaTime * 1;
        displaySecond = (int)countTime - (displayHour * 60);
        if (displaySecond >= 60)
        {
            displayHour++;
            displaySecond = 0;
            _gamma.gain.value = _gamma.gain.value - new Vector4(0, 0, 0, 0.1f);
        }
        if (displayHour < LimitHour)
        {
            text.text = (24 - LimitHour + displayHour).ToString() + ":" + displaySecond.ToString("00");
        }
        else
        {
            TimeOut = true;
            if (once == true)
            {
                once = false;
                audioSource.PlayOneShot(sound1);
                _vignette.intensity.value = 0.6f;
            }
            text.text = (displayHour - LimitHour).ToString("00") + ":" + displaySecond.ToString("00");

            if (text.color.a < 0.1f)
            {
                plus = true;
            }
            if (text.color.a > 0.8f)
            {
                plus = false;
            }
            if (plus == false)
            {
                text.color = text.color - new Color(0, 0, 0, 0.01f);

            }
            else
            {
                text.color = text.color + new Color(0, 0, 0, 0.01f);
            }
        }
    }
}
