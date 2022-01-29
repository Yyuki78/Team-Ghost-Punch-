using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private LevelEnum _level = LevelEnum.Level1; // Enemy��Level
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

    // Start is called before the first frame update
    void Awake()
    {
        _status1 = Enemy1.GetComponent<EnemyStatus>();
        _status2 = Enemy2.GetComponent<EnemyStatus>();
        _status3 = Enemy3.GetComponent<EnemyStatus>();
        _status4 = Enemy4.GetComponent<EnemyStatus>();
        _status5 = Enemy5.GetComponent<EnemyStatus>();
        _status6 = Enemy6.GetComponent<EnemyStatus>();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy���|�ꂽ���ɋ����p�̕ϐ���+1����
        if (_status1.Life == 0 && once1 == false)
        {
            once1 = true;
            StrongEvent++;
        }
        if (_status2.Life == 0 && once2 == false)
        {
            once2 = true;
            StrongEvent++;
        }
        if (_status3.Life == 0 && once3 == false)
        {
            once3 = true;
            StrongEvent++;
        }
        if (_status4.Life == 0 && once4 == false)
        {
            once4 = true;
            StrongEvent++;
        }
        if (_status5.Life == 0 && once5 == false)
        {
            once5 = true;
            StrongEvent++;
        }
        if (_status6.Life == 0 && once6 == false)
        {
            once6 = true;
            StrongEvent++;
        }

        //StrongEvent�̐��ɉ�����Enemy����������ϐ��𑗂�
        if (StrongEvent >= 0 && StrongEvent <= 2)
        {
            //���߂̏�ԁAEnemy��3����1��|���܂ő���
            //���[���C�͊�{�I�ɕ����ɋ��āA��������͗]�蓮���Ȃ�
            //�ǂ������鑬�x��1.5
            //���m�͈͂�4
            _level = LevelEnum.Level1;
        }
        else if (StrongEvent == 3 || StrongEvent == 4)
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
    }
}
