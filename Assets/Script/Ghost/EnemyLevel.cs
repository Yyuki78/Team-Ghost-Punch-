using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLevel : MonoBehaviour
{
    //EnemyManger����EnemyLevel���󂯎���Ă���ɉ����ē�Փx��ς���
    [SerializeField] GameObject EnemyManager;
    private EnemyManager _manager;
    [SerializeField] GameObject CollisionDetector;
    private SphereCollider _collider;
    private EnemyMove _move;
    private EnemyStatus _status;
    private EnemyAttack _attack;
    [SerializeField] GameObject Player;
    private Player _player;
    public bool Charge = false; //Charge�����ǂ���
    private NavMeshAgent _agent;
    //RandomEnemyWalk�ɓn���Ĝp�j���邽�߂̕ϐ�
    public bool level2 = false;
    public bool level3 = false;
    //�`���[�WArea��������܂ł̎��Ԃ�ς���p
    [SerializeField] GameObject Mirror;
    MirrorManager _mirror;
    private bool once = true;

    private void Awake()
    {
        _manager = EnemyManager.GetComponent<EnemyManager>();
        _collider = CollisionDetector.GetComponent<SphereCollider>();
        _move = GetComponent<EnemyMove>();
        _status = GetComponent<EnemyStatus>();
        _attack = GetComponent<EnemyAttack>();
        _agent = GetComponent<NavMeshAgent>();
        _player = Player.GetComponent<Player>();
        _mirror = Mirror.GetComponent<MirrorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsChargeMode == true)
        {
            ChargeTrue();
        }else if (_player.IsDead == true)
        {
            PlayerGhostMode();
        }else if (_status.IsRunState)
        {
            RunMode();
        }
        else
        {
            Charge = false;
            //Level�ɉ����ē�Փx��؂�ւ���
            if (_manager.IsLevel1)
            {
                Level1();
            }
            else if (_manager.IsLevel2)
            {
                Level2();
            }
            else
            {
                Level3();
            }
        }
    }

    void ChargeTrue()
    {
        //Player���`���[�W���Ă�����
        //���Ȃ�͈̔͂���Enemy�����m����
        Charge = true;
        if (_manager.IsLevel1)
        {
            _collider.radius = 10.0f;
        }
        else if (_manager.IsLevel2)
        {
            _collider.radius = 12.0f;
        }
        else
        {
            _collider.radius = 14.0f;
        }
        //level2 = false;
        //level3 = false;
    }

    void PlayerGhostMode()
    {
        //Player��GhostMode�ɂȂ��Ă�����
        //���Ȃ�͈̔͂���Enemy�����m����
        Charge = true;
        if (_manager.IsLevel1)
        {
            _collider.radius = 10.0f;
        }
        else if (_manager.IsLevel2)
        {
            _collider.radius = 12.0f;
        }
        else
        {
            _collider.radius = 14.0f;
        }
        //level2 = false;
        //level3 = false;
    }

    void RunMode()
    {
        //Ghost��Player��ǂ��Ă�����
        //���ɂ����瑤�ł��邱�Ƃ͂Ȃ�
        Charge = true;
        if (_manager.IsLevel1)
        {
            _collider.radius = 4.0f;
        }
        else if (_manager.IsLevel2)
        {
            _collider.radius = 6.0f;
        }
        else
        {
            _collider.radius = 8.0f;
        }
    }

    void Level1()
    {
        //���߂̏�ԁAEnemy��3����1��|���܂ő���
        //���[���C�͊�{�I�ɕ����ɋ��āA��������͗]�蓮���Ȃ�
        //�ǂ������鑬�x�͒x��-->1.5?
        //���m�͈͂�4
        Debug.Log("EnemyLevel1");
        _agent.speed = 1.5f;
        _collider.radius = 4.0f;
        //NormalState�̎��͜p�j����
        if (_move.RanWalk == true)
        {
            level2 = true;
            level3 = false;
        }
        else
        {
            level2 = false;
            level3 = false;
        }
        //Player�ւ̃Q�[�W�_���[�W��10
        _player.GaugeDamage = 10;
        //�U���̃N�[���_�E����1.5f
        _attack.attackCooldown = 1.5f;
    }

    void Level2()
    {
        //Enemy��3����1��|�����Ƃł��̒i�K�ɂȂ�
        //Enemy��3����2��|���܂ő���
        //���[���C�͕����̒��������������܂��
        //�ǂ������鑬�x�͐l��菭���x�����炢-- > 2 ?
        //���m�͈͂�5
        _agent.speed = 2.0f;
        _collider.radius = 6.0f;
        //NormalState�̎��͜p�j����(Level2�͂̂�т�s��)
        if (_move.RanWalk == true)
        {
            level2 = true;
            level3 = false;
        }
        else
        {
            level2 = false;
            level3 = false;
        }
        //Player�ւ̃Q�[�W�_���[�W��15
        _player.GaugeDamage = 13;
        //�U���̃N�[���_�E����1.5f
        _attack.attackCooldown = 1.5f;
    }

    void Level3()
    {
        //Enemy��3����2��|�����Ƃł��̒i�K�ɂȂ�
        //���[���C�͕����̒��𕁒ʂɕ������
        //�ǂ������鑬�x�͐l���ق�̋͂��ɒx�����x--> 2.2
        //���m�͈͂�6
        if (Timer.TimeOut == true)//���Ԑ؂�
        {
            _agent.speed = 2.5f;
        }
        else
        {
            _agent.speed = 2.2f;
        }
        _collider.radius = 8.0f;
        //NormalState�̎��͜p�j����(Level3�͕��ʂɓ������)
        if (_move.RanWalk == true)
        {
            level2 = false;
            level3 = true;
        }
        else
        {
            level2 = false;
            level3 = false;
        }
        //Player�ւ̃Q�[�W�_���[�W��20
        _player.GaugeDamage = 16;
        //�U���̃N�[���_�E����1.0f
        _attack.attackCooldown = 1.0f;
        //�`���[�W�G���A�������ɏ�����悤�ɂȂ�
        if (once == true)
        {
            _mirror.Level3();
            once = false;
        }
    }
}