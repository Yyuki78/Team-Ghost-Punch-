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
    [SerializeField] GameObject Player;
    private Player _player;
    public bool Charge = false; //Charge中かどうか
    private NavMeshAgent _agent; private void Awake()
    {
        _manager = EnemyManager.GetComponent<EnemyManager>();
        _collider = CollisionDetector.GetComponent<SphereCollider>();
        _move = GetComponent<EnemyMove>();
        _agent = GetComponent<NavMeshAgent>();
        _player = Player.GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsChargeMode == true)
        {
            ChargeTrue();
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
        _collider.radius = 10.0f;
    }

    void Level1()
    {
        //初めの状態、Enemyの3分の1を倒すまで続く
        //ユーレイは基本的に部屋に居て、自分からは余り動かない
        //追いかける速度は遅い-->1.5?
        //検知範囲は4
        _agent.speed = 1.5f;
        _collider.radius = 4.0f;
        //NormalStateの時は徘徊する(Level1はしない)
    }

    void Level2()
    {
        //Enemyの3分の1を倒すことでこの段階になる
        //Enemyの3分の2を倒すまで続く
        //ユーレイは部屋の中をゆっくり歩きまわる
        //追いかける速度は人より少し遅いくらい-- > 2 ?
        //検知範囲は5
        _agent.speed = 2.0f;
        _collider.radius = 5.0f;
        //NormalStateの時は徘徊する(Level2はのんびり行う)

    }

    void Level3()
    {
        //Enemyの3分の2を倒すことでこの段階になる
        //ユーレイは部屋の中を普通に歩き回る
        //追いかける速度は人よりほんの僅かに遅い程度--> 2.5 ?
        //検知範囲は6
        _agent.speed = 2.5f;
        _collider.radius = 6.0f;
        //NormalStateの時は徘徊する(Level3は普通に動き回る)
    }
}