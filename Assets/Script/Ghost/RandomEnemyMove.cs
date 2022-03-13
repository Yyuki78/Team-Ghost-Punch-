using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomEnemyMove : MonoBehaviour
{
    //�@�ړI�n
    private Vector3 destination;
    //�@�����X�s�[�h
    private NavMeshAgent _agent;
    //�@���x
    private Vector3 velocity;
    //�@�ړ�����
    private Vector3 direction;
    //�@�����t���O
    private bool arrived;
    //�@SetPosition�X�N���v�g
    private SetPosition setPosition;
    //�@�҂�����
    [SerializeField]
    private float waitTime = 3f;
    //�@�o�ߎ���
    private float elapsedTime;

    //�p�j�̃��x��
    private EnemyLevel _level;

    //���p�j�ł��邩�ǂ���
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
        if (_level.level2 == true || _level.level3 == true)
        {
            if (_level.level2 == true)
            {
                setPosition.distance = 4;
            }else if (_level.level3 == true)
            {
                setPosition.distance = 6;
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

                /*
                //�@�ړI�n�ɓ����������ǂ����̔���
                if (Vector3.Distance(transform.position, destination) < 0.5f)
                {
                    arrived = true;
                }*/
                //�@�������Ă�����

                elapsedTime += Time.deltaTime;
                if (elapsedTime > waitTime)
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
                //�@�҂����Ԃ��z�����玟�̖ړI�n��ݒ�
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
