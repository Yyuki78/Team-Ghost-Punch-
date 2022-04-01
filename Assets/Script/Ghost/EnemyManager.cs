using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// Enemyの残り数に応じた難易度調整
    /// </summary>
    private enum LevelEnum
    {
        Level1, // 最初
        Level2, // 途中
        Level3, // 最後
    }

    // EnemyのLevel
    private LevelEnum _level = LevelEnum.Level1; 

    /// <summary>
    /// Level1かどうか
    /// </summary>
    public bool IsLevel1 => LevelEnum.Level1 == _level;
    /// <summary>
    /// Level2かどうか
    /// </summary>
    public bool IsLevel2 => LevelEnum.Level2 == _level;
    /// <summary>
    /// Level3かどうか
    /// </summary>
    public bool IsLevel3 => LevelEnum.Level3 == _level;
    //それぞれのEnemyを受け取る
    [SerializeField] GameObject Enemy1;
    private EnemyStatus _status1;
    bool once1 = false;
    [SerializeField] GameObject Enemy2;
    private EnemyStatus _status2;
    bool once2 = false;
    [SerializeField] GameObject Enemy3;
    private EnemyStatus _status3;
    bool once3 = false;
    [SerializeField] GameObject Enemy4;
    private EnemyStatus _status4;
    bool once4 = false;
    [SerializeField] GameObject Enemy5;
    private EnemyStatus _status5;
    bool once5 = false;
    [SerializeField] GameObject Enemy6;
    private EnemyStatus _status6;
    bool once6 = false;

    private int StrongEvent = 0;//Enemyの強化段階を切り替える用の変数
    private int DeathGhost = 0;//死んだ敵の数

    [SerializeField] Volume _volume;
    private LiftGammaGain _gamma;
    private int changeTime = 250;
    private bool change = false;

    //GameLogicで使う
    public bool IsRunAnyone = false;
    public bool IsDeadGhost3 => StrongEvent >= 3;

    //GameOverMessageで使う
    public bool IsDeadGhostNone => DeathGhost == 0;
    public bool IsDeadGhostFive => DeathGhost == 5;

    //GameClearで使う
    public bool IsDeadAllEnemy => DeathGhost == 6;

    // Start is called before the first frame update
    void Awake()
    {
        _status1 = Enemy1.GetComponent<EnemyStatus>();
        _status2 = Enemy2.GetComponent<EnemyStatus>();
        _status3 = Enemy3.GetComponent<EnemyStatus>();
        _status4 = Enemy4.GetComponent<EnemyStatus>();
        _status5 = Enemy5.GetComponent<EnemyStatus>();
        _status6 = Enemy6.GetComponent<EnemyStatus>();
        _volume.profile.TryGet<LiftGammaGain>(out _gamma);
        _gamma.gamma.value = _gamma.gamma.value + new Vector4(0, 0, 0, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        //Enemyが倒れた時に強化用の変数を+1する
        if (_status1.Life == 0 && once1 == false)
        {
            once1 = true;
            StrongEvent++;
            DeathGhost++;
        }
        if (_status2.Life == 0 && once2 == false)
        {
            once2 = true;
            StrongEvent++;
            DeathGhost++;
        }
        if (_status3.Life == 0 && once3 == false)
        {
            once3 = true;
            StrongEvent++;
            DeathGhost++;
        }
        if (_status4.Life == 0 && once4 == false)
        {
            once4 = true;
            StrongEvent++;
            DeathGhost++;
        }
        if (_status5.Life == 0 && once5 == false)
        {
            once5 = true;
            StrongEvent++;
            DeathGhost++;
        }
        if (_status6.Life == 0 && once6 == false)
        {
            once6 = true;
            StrongEvent++;
            DeathGhost++;
        }
        //時間切れ時は最大レベルに強制変更
        if (Timer.TimeOut == true)
        {
            StrongEvent = 6;
        }

        //StrongEventの数に応じてEnemyを強化する変数を送る
        if (StrongEvent >= 0 && StrongEvent <= 1)
        {
            //初めの状態、Enemyの3分の1を倒すまで続く
            //ユーレイは基本的に部屋に居て、自分からは余り動かない
            //追いかける速度は1.5
            //検知範囲は4
            _level = LevelEnum.Level1;
        }
        else if (StrongEvent == 2 || StrongEvent == 3)
        {
            //Enemyの3分の1を倒すことでこの段階になる
            //Enemyの3分の2を倒すまで続く
            //ユーレイは部屋の中をゆっくり歩きまわる
            //追いかける速度は人より少し遅いくらい-- > 2 ?
            //検知範囲は5
            _level = LevelEnum.Level2;
        }
        else
        {
            //Enemyの3分の2を倒すことでこの段階になる
            //ユーレイは部屋の中を普通に歩き回る
            //追いかける速度は人よりほんの僅かに遅い程度--> 2.5 ?
            //検知範囲は6
            _level = LevelEnum.Level3;
        }

        if (GameOver.gameover)
        {

        }
        else
        {
            if (changeTime >= 250)
            {
                change = false;
            }
            if (changeTime <= 0)
            {
                change = true;
            }
            //Enemyが一体でもHumanを追いかけている時、Effectがかかる
            if (change == false)
            {
                changeTime--;
                _gamma.gamma.value = _gamma.gamma.value - new Vector4(0, 0, 0, 0.0005f);
                if (_status1.IsRunState || _status2.IsRunState || _status3.IsRunState || _status4.IsRunState || _status5.IsRunState || _status6.IsRunState)
                {
                    changeTime -= 2;
                    _gamma.gamma.value = _gamma.gamma.value - new Vector4(0, 0, 0, 0.001f);
                }
            }
            else
            {
                changeTime++;
                _gamma.gamma.value = _gamma.gamma.value + new Vector4(0, 0, 0, 0.0005f);
                if (_status1.IsRunState || _status2.IsRunState || _status3.IsRunState || _status4.IsRunState || _status5.IsRunState || _status6.IsRunState)
                {
                    changeTime += 2;
                    _gamma.gamma.value = _gamma.gamma.value + new Vector4(0, 0, 0, 0.001f);
                }
            }
        }

        if (_status1.IsChangeBGM || _status2.IsChangeBGM || _status3.IsChangeBGM || _status4.IsChangeBGM || _status5.IsChangeBGM || _status6.IsChangeBGM)
        {
            IsRunAnyone = true;
        }
        else
        {
            IsRunAnyone = false;
        }
    }
}
