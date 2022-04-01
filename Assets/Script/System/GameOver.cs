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

    [SerializeField] GameObject GameOverMessage;
    private GameOverMessage _message;

    //PlayerのHPバーの画像
    [SerializeField] GameObject Image;
    private Image _image;
    [SerializeField] Sprite sprite;

    AudioSource _source;
    [SerializeField] AudioClip GameOverSE;
    [SerializeField] AudioClip GameOverBGM;

    private bool once = true;

    //GameOverPanelObject
    [SerializeField] GameObject Message;
    [SerializeField] GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        _player = Player.GetComponent<Player>();
        _message = GameOverMessage.GetComponent<GameOverMessage>();
        _image = Image.GetComponent<Image>();
        _source = GetComponent<AudioSource>();
        gameover = false;
        Message.SetActive(false);
        Button.SetActive(false);
        Time.timeScale = 1.0f;
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
            if (once == true)
            {
                once = false;
                Time.timeScale = 0.1f;
                _image.sprite = sprite;
                StartCoroutine(GameOverCroutine());
            }
        }
    }

    private IEnumerator GameOverCroutine()
    {
        _source.PlayOneShot(GameOverSE);
        yield return new WaitForSeconds(0.4f);
        _source.PlayOneShot(GameOverBGM);
        yield return new WaitForSeconds(0.2f);
        Time.timeScale = 0.75f;
        yield return new WaitForSeconds(3.3f);
        Message.SetActive(true);
        _message.updateMessage();
        yield return new WaitForSeconds(0.5f);
        Button.SetActive(true);
        yield break;
    }
}
