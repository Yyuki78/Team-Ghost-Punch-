using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask; // ���C���[�}�X�N
    const string LayerName1 = "Default";
    const string LayerName2 = "Nothing";
    private float step_time;
    private int mask1;
    private int mask2;
    const int LayerNum = 8;

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    public void Start()
    {
        mask1 = LayerMask.GetMask(new string[] { LayerName1 });
        mask2 = LayerMask.GetMask(new string[] { LayerName2 });
        step_time = 0.0f;
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
        _status = GetComponent<EnemyStatus>();
    }

    public void Update()
    {
        // �o�ߎ��Ԃ��J�E���g
        step_time += Time.deltaTime;
        if (Physics.Raycast(transform.position,
                            transform.forward,
                            float.PositiveInfinity,
                            mask1))
            Physics.Raycast(transform.position,
            transform.forward,
            float.PositiveInfinity,
            mask1);
        {

            Debug.Log("Raycast Hit!Mask1");
        }
        // 10�b���Mask2
        if (step_time >= 10.0f)
        {
            Physics.Raycast(transform.position,
            transform.forward,
            float.PositiveInfinity,
            mask2);
            if (Physics.Raycast(transform.position,
                            transform.forward,
                            float.PositiveInfinity,
                            mask2))
            {

                Debug.Log("Raycast Hit!Mask2");
            }
        }
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