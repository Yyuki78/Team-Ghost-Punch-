using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class GameClear : MonoBehaviour
{
    //ゲームクリア時にすることの管理
    //時間経過が一瞬だけスローに
    //BGMは停止(GameLogicScript)
    //ゲームクリア用音楽
    //視界が段々暗くなる
    //ダメージ音も残響的な感じが欲しい
    //暗転後、GameOver演出

    [SerializeField] public static bool gameclear = false;

    [SerializeField] GameObject Player;
    private Player _player;

    [SerializeField] GameObject EnemyManager;
    private EnemyManager _manager;

    [SerializeField] GameObject timer;
    private Timer _time;

    private int clearTime;

    [SerializeField] GameObject GameClearMessage;
    private GameClearMessage _message;

    AudioSource _source;
    [SerializeField] AudioClip GameClearSE;
    [SerializeField] AudioClip GameClearBGM;

    [SerializeField] Volume _volume;
    private ShadowsMidtonesHighlights _shadows;

    private bool once = true;

    //GameOverPanelObject
    [SerializeField] GameObject GameClearPanel;
    [SerializeField] GameObject GameClearText;
    [SerializeField] GameObject Message;
    [SerializeField] GameObject Button;

    // Start is called before the first frame update
    void Start()
    {
        _manager = EnemyManager.GetComponent<EnemyManager>();
        _player = Player.GetComponent<Player>();
        _time = timer.GetComponent<Timer>();
        _message = GameClearMessage.GetComponent<GameClearMessage>();
        _source = GetComponent<AudioSource>();
        _volume.profile.TryGet<ShadowsMidtonesHighlights>(out _shadows);
        gameclear = false;
        GameClearPanel.SetActive(false);
        GameClearText.SetActive(false);
        Message.SetActive(false);
        Button.SetActive(false);
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (_manager.IsDeadAllEnemy && _player._life > 0)
        {
            gameclear = true;
            if (once == true)
            {
                once = false;
                clearTime = _time.IsTimer;
                if (clearTime > 60 * 5)
                {
                    clearTime = 1;
                }
                else
                {
                    clearTime = 60 * 5 - clearTime;
                }
                Time.timeScale = 0.2f;
                _message.updateMessage(clearTime, _player._life);
                StartCoroutine(GameClearEffect());
            }
        }
    }

    private IEnumerator GameClearEffect()
    {
        _source.PlayOneShot(GameClearSE);
        for (int i = 0; i < 50; i++)
        {
            _shadows.shadows.value = _shadows.shadows.value + new Vector4(0, 0, 0, 0.1f);
            yield return new WaitForSeconds(0.008f);
        }
        GameClearText.SetActive(true);
        Time.timeScale = 1.0f;
        _source.PlayOneShot(GameClearBGM);
        yield return new WaitForSeconds(0.2f);
        
        yield return new WaitForSeconds(3.6f);
        GameClearPanel.SetActive(true);
        yield return new WaitForSeconds(0.8f);
        Message.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Button.SetActive(true);
        yield break;
    }
}
