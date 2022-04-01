using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLevel : MonoBehaviour
{
    //EnemyMangerからEnemyLevelを受け取ってそれに応じて難易度を変える
    [SerializeField] GameObject EnemyManager;
    private EnemyManager _manager;
    [SerializeField] GameObject CollisionDetector;
    private SphereCollider _collider;
    private EnemyMove _move;
    private EnemyStatus _status;
    private EnemyAttack _attack;
    [SerializeField] GameObject Player;
    private Player _player;
    public bool Charge = false; //Charge中かどうか
    private NavMeshAgent _agent;
    //RandomEnemyWalkに渡して徘徊するための変数
    public bool level2 = false;
    public bool level3 = false;
    //チャージAreaが消えるまでの時間を変える用
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
            //Levelに応じて難易度を切り替える
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
        //Playerがチャージしている状態
        //かなりの範囲からEnemyが検知する
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
        //PlayerがGhostModeになっている状態
        //かなりの範囲からEnemyが検知する
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
        //GhostがPlayerを追っている状態
        //特にこちら側ですることはない
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
        //初めの状態、Enemyの3分の1を倒すまで続く
        //ユーレイは基本的に部屋に居て、自分からは余り動かない
        //追いかける速度は遅い-->1.5?
        //検知範囲は4
        Debug.Log("EnemyLevel1");
        _agent.speed = 1.5f;
        _collider.radius = 4.0f;
        //NormalStateの時は徘徊する
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
        //Playerへのゲージダメージは10
        _player.GaugeDamage = 10;
        //攻撃のクールダウンは1.5f
        _attack.attackCooldown = 1.5f;
    }

    void Level2()
    {
        //Enemyの3分の1を倒すことでこの段階になる
        //Enemyの3分の2を倒すまで続く
        //ユーレイは部屋の中をゆっくり歩きまわる
        //追いかける速度は人より少し遅いくらい-- > 2 ?
        //検知範囲は5
        _agent.speed = 2.0f;
        _collider.radius = 6.0f;
        //NormalStateの時は徘徊する(Level2はのんびり行う)
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
        //Playerへのゲージダメージは15
        _player.GaugeDamage = 13;
        //攻撃のクールダウンは1.5f
        _attack.attackCooldown = 1.5f;
    }

    void Level3()
    {
        //Enemyの3分の2を倒すことでこの段階になる
        //ユーレイは部屋の中を普通に歩き回る
        //追いかける速度は人よりほんの僅かに遅い程度--> 2.2
        //検知範囲は6
        if (Timer.TimeOut == true)//時間切れ
        {
            _agent.speed = 2.5f;
        }
        else
        {
            _agent.speed = 2.2f;
        }
        _collider.radius = 8.0f;
        //NormalStateの時は徘徊する(Level3は普通に動き回る)
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
        //Playerへのゲージダメージは20
        _player.GaugeDamage = 16;
        //攻撃のクールダウンは1.0f
        _attack.attackCooldown = 1.0f;
        //チャージエリアがすぐに消えるようになる
        if (once == true)
        {
            _mirror.Level3();
            once = false;
        }
    }
}