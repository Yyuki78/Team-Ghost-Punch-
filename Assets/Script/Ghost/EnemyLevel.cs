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
    private NavMeshAgent _agent;

    private void Awake()
    {
        _manager = EnemyManager.GetComponent<EnemyManager>();
        _collider = CollisionDetector.GetComponent<SphereCollider>();
        _move = GetComponent<EnemyMove>();
        _agent = GetComponent<NavMeshAgent>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Level�ɉ����ē�Փx��؂�ւ���
        if (_manager.IsLevel1)
        {
            Level1();
        }else if (_manager.IsLevel2)
        {
            Level2();
        }
        else
        {
            Level3();
        }
    }

    void Level1()
    {
        //���߂̏�ԁAEnemy��3����1��|���܂ő���
        //���[���C�͊�{�I�ɕ����ɋ��āA��������͗]�蓮���Ȃ�
        //�ǂ������鑬�x�͒x��-->1.5?
        //���m�͈͂�4
        _agent.speed = 1.5f;
        _collider.radius = 4.0f;
    }

    void Level2()
    {
        //Enemy��3����1��|�����Ƃł��̒i�K�ɂȂ�
        //Enemy��3����2��|���܂ő���
        //���[���C�͕����̒��������������܂��
        //�ǂ������鑬�x�͐l��菭���x�����炢-- > 2 ?
        //���m�͈͂�5
        _agent.speed = 2.0f;
        _collider.radius = 5.0f;
    }

    void Level3()
    {
        //Enemy��3����2��|�����Ƃł��̒i�K�ɂȂ�
        //���[���C�͕����̒��𕁒ʂɕ������
        //�ǂ������鑬�x�͐l���ق�̋͂��ɒx�����x--> 2.5 ?
        //���m�͈͂�6
        _agent.speed = 2.5f;
        _collider.radius = 6.0f;
    }
}
