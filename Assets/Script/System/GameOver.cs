using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    //ゲームオーバー時にすることの管理
    //時間経過がスローに
    //BGMは停止(GameLogicScript)
    //Playerは移動不可＋死亡時演出(PlayerScript)
    //右上のHPバーの画像変更
    //ゲームオーバー用音楽
    //視界が段々暗くなる
    //ダメージ音も残響的な感じが欲しい
    //暗転後、GameOver演出
    //
    [SerializeField] public static bool gameover = false;

    [SerializeField] GameObject Player;
    private Player _player;

    //PlayerのHPバーの画像
    [SerializeField] GameObject Image;
    private Image _image;
    [SerializeField] Sprite sprite;

    AudioSource _source;
    [SerializeField] AudioClip GameOverSE;
    [SerializeField] AudioClip GameOverBGM;

    private bool once = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        _image = Image.GetComponent<Image>();
        _source = GetComponent<AudioSource>();
        gameover = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_player._life <= 0)
        {
            gameover = true;
        }
        if (gameover == true)
        {
            Time.timeScale = 0.1f;
            if (once == true)
            {
                once = false;
                _image.sprite = sprite;
                StartCoroutine(GameOverCroutine());
            }
        }
    }

    private IEnumerator GameOverCroutine()
    {
        _source.PlayOneShot(GameOverSE);
        yield return new WaitForSeconds(0.5f);
        _source.PlayOneShot(GameOverBGM);
        yield break;
    }
}
