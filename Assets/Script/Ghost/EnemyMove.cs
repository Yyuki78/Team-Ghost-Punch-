using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask; // ���C���[�}�X�N

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
        _status = GetComponent<EnemyStatus>();
    }

    // CollisionDetector��onTriggerStay�ɃZ�b�g���A�Փ˔�����󂯎�郁�\�b�h
    public void OnDetectObject(Collider collider)
    {
        if (!_status.IsMovable)
        {
            _agent.isStopped = true;
            return;
        }

        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player"))
        {
            var positionDiff = collider.transform.position - transform.position; // ���g�ƃv���C���[�̍��W�������v�Z
            var distance = positionDiff.magnitude; // �v���C���[�Ƃ̋������v�Z
            var direction = positionDiff.normalized; // �v���C���[�ւ̕���

            // _raycastHits�ɁA�q�b�g����Collider����W���Ȃǂ��i�[�����
            // RaycastAll��RaycastNonAlloc�͓����̋@�\�����ARaycastNonAlloc���ƃ������ɃS�~���c��Ȃ��̂ł�����𐄏�
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);
            Debug.Log("hitCount: " + hitCount);
            if (hitCount == 0)
            {
                // �{��̃v���C���[��CharacterController���g���Ă��āACollider�͎g���Ă��Ȃ��̂�Raycast�̓q�b�g���Ȃ�
                // �܂�A�q�b�g����0�ł���΃v���C���[�Ƃ̊Ԃɏ�Q���������Ƃ������ƂɂȂ�
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                // �����������~����
                _agent.isStopped = true;
            }
        }
    }
}