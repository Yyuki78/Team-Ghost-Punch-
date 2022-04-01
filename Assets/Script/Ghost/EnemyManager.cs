using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class EnemyManager : MonoBehaviour
{
    /// <summary>
    /// Enemy�̎c�萔�ɉ�������Փx����
    /// </summary>
    private enum LevelEnum
    {
        Level1, // �ŏ�
        Level2, // �r��
        Level3, // �Ō�
    }

    // Enemy��Level
    private LevelEnum _level = LevelEnum.Level1; 

    /// <summary>
    /// Level1���ǂ���
    /// </summary>
    public bool IsLevel1 => LevelEnum.Level1 == _level;
    /// <summary>
    /// Level2���ǂ���
    /// </summary>
    public bool IsLevel2 => LevelEnum.Level2 == _level;
    /// <summary>
    /// Level3���ǂ���
    /// </summary>
    public bool IsLevel3 => LevelEnum.Level3 == _level;
    //���ꂼ���Enemy���󂯎��
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

    private int StrongEvent = 0;//Enemy�̋����i�K��؂�ւ���p�̕ϐ�
    private int DeathGhost = 0;//���񂾓G�̐�

    [SerializeField] Volume _volume;
    private LiftGammaGain _gamma;
    private int changeTime = 250;
    private bool change = false;

    //GameLogic�Ŏg��
    public bool IsRunAnyone = false;
    public bool IsDeadGhost3 => StrongEvent >= 3;

    //GameOverMessage�Ŏg��
    public bool IsDeadGhostNone => DeathGhost == 0;
    public bool IsDeadGhostFive => DeathGhost == 5;

    //GameClear�Ŏg��
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
        //Enemy���|�ꂽ���ɋ����p�̕ϐ���+1����
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
        //���Ԑ؂ꎞ�͍ő僌�x���ɋ����ύX
        if (Timer.TimeOut == true)
        {
            StrongEvent = 6;
        }

        //StrongEvent�̐��ɉ�����Enemy����������ϐ��𑗂�
        if (StrongEvent >= 0 && StrongEvent <= 1)
        {
            //���߂̏�ԁAEnemy��3����1��|���܂ő���
            //���[���C�͊�{�I�ɕ����ɋ��āA��������͗]�蓮���Ȃ�
            //�ǂ������鑬�x��1.5
            //���m�͈͂�4
            _level = LevelEnum.Level1;
        }
        else if (StrongEvent == 2 || StrongEvent == 3)
        {
            //Enemy��3����1��|�����Ƃł��̒i�K�ɂȂ�
            //Enemy��3����2��|���܂ő���
            //���[���C�͕����̒��������������܂��
            //�ǂ������鑬�x�͐l��菭���x�����炢-- > 2 ?
            //���m�͈͂�5
            _level = LevelEnum.Level2;
        }
        else
        {
            //Enemy��3����2��|�����Ƃł��̒i�K�ɂȂ�
            //���[���C�͕����̒��𕁒ʂɕ������
            //�ǂ������鑬�x�͐l���ق�̋͂��ɒx�����x--> 2.5 ?
            //���m�͈͂�6
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
            //Enemy����̂ł�Human��ǂ������Ă��鎞�AEffect��������
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
