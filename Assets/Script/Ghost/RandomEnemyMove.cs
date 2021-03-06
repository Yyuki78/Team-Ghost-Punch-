using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomEnemyMove : MonoBehaviour
{
    //　目的地
    private Vector3 destination;
    //　歩くスピード
    private NavMeshAgent _agent;
    //　速度
    private Vector3 velocity;
    //　移動方向
    private Vector3 direction;
    //　到着フラグ
    private bool arrived;
    //　SetPositionスクリプト
    private SetPosition setPosition;
    //　待ち時間
    [SerializeField]
    private float waitTime = 3f;
    //　経過時間
    private float elapsedTime;

    //徘徊のレベル
    private EnemyLevel _level;

    //今徘徊できるかどうか
    private EnemyStatus _status;
    private EnemyMove _move;

    // Use this for initialization
    void Start()
    {
        setPosition = GetComponent<SetPosition>();
        destination = this.transform.position;
        setPosition.CreateRandomPosition();
        velocity = Vector3.zero;
        arrived = false;
        elapsedTime = 0f;
        _level = GetComponent<EnemyLevel>();
        _agent = GetComponent<NavMeshAgent>();
        _status = GetComponent<EnemyStatus>();
        _move = GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_status.IsRunnable) return;
        if (!_move.RanWalk) return;
        if (_move.thunder) return;
        if (_level.level2 == true || _level.level3 == true)
        {
            if (_level.level2 == true)
            {
                setPosition.distance = 4;
                waitTime = 3f;
            }
            else if (_level.level3 == true)
            {
                setPosition.distance = 6;
                waitTime = 2f;
            }
            if (!arrived)
            {
                velocity = Vector3.zero;
                direction = (destination - transform.position).normalized;
                //transform.LookAt(new Vector3(destination.x, transform.position.y, destination.z));
                velocity = direction * (_agent.speed + 1f);
                //Debug.Log(destination);

                velocity.y += Physics.gravity.y * Time.deltaTime;
                //enemyController.Move(velocity * Time.deltaTime);
                transform.position += velocity * Time.deltaTime;

                elapsedTime += Time.deltaTime;
                //　目的地に到着したかどうかの判定
                if (Vector3.Distance(transform.position, destination) < 0.5f)
                {
                    arrived = true;
                }//時間切れでも次に行く
                else if(elapsedTime > waitTime)
                {
                    arrived = true;
                }
            }
            else
            {
                //elapsedTime += Time.deltaTime;
                setPosition.CreateRandomPosition();
                destination = setPosition.GetDestination();
                elapsedTime = 0;
                arrived = false;
                /*
                //　待ち時間を越えたら次の目的地を設定
                if (elapsedTime > waitTime)
                {
                    setPosition.CreateRandomPosition();
                    destination = setPosition.GetDestination();
                    arrived = false;
                    elapsedTime = 0f;
                }
                //Debug.Log(elapsedTime);
                */
            }
        }
    }
}
