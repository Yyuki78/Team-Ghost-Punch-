using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMoveTest : MonoBehaviour
{
    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
    }

    // CollisionDetector��onTriggerStay�ɃZ�b�g���A�Փ˔�����󂯎�郁�\�b�h
    public void OnDetectObject(Collider collider)
    {
        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player"))
        {
            _agent.destination = collider.transform.position;
        }
    }
}