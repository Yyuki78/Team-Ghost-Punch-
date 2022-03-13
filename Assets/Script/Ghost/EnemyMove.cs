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
    const string LayerName2 = "EnemySerch";
    private int mask;

    private EnemyLevel _level;
    public bool RanWalk = true;//�p�j�o���邩�ǂ�����EnemyLevel�Ɏ󂯓n��

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    //���ŏ����̊Ԓ�~����p
    [SerializeField] GameObject StageEffect;
    private ThunderEffect _thunder;
    public bool thunder = false;//RandomEnemyMove�Ŏ~�߂�p

    public void Start()
    {
        mask = LayerMask.GetMask(new string[] { LayerName1 });
        _level = GetComponent<EnemyLevel>();
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgent��ێ����Ă���
        _status = GetComponent<EnemyStatus>();
        _thunder = StageEffect.GetComponent<ThunderEffect>();
    }

    public void Update()
    {
        if (_level.Charge==false)
        {
            mask = LayerMask.GetMask(new string[] { LayerName1 });
            raycastLayerMask = mask;
            if (!_status.IsMovable || thunder == true)
            {
                RanWalk = true;
            }
        }
        else
        {
            Debug.Log("�`���[�W���܂��̓S�[�X�g���[�h�ł�");
            mask = LayerMask.GetMask(new string[] { LayerName2 });
            raycastLayerMask = mask;
        }
        if (_thunder.canSee == true)
        {
            StartCoroutine("StopMoving");
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

        if (thunder == true)
        {
            _agent.isStopped = true;
            return;
        }

        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A���̃I�u�W�F�N�g��ǂ�������
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerGhost"))
        {
            //Enemy�̃X�e�[�^�X��Run�ɕύX
            _status.GoToRunStateIfPossible();
            RanWalk = false;

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

    // CollisionDetector��onTriggerExit�ɃZ�b�g���A�Փ˔�����󂯎�郁�\�b�h
    public void GoLoiterState(Collider collider)
    {
        // ���m�����I�u�W�F�N�g�ɁuPlayer�v�̃^�O�����Ă���΁A�p�j���[�h�ɐ؂�ւ���
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerGhost"))
        {
            //������{�ǂ������Ă�����ԂłȂ��Ȃ�return
            if (!_status.IsMovable)
            {
                _agent.isStopped = true;
                return;
            }
            if (!_status.IsRunState) return;

            _status.GoToNormalStateIfPossible();
            RanWalk = true;
        }
    }

    private IEnumerator StopMoving()
    {
        thunder = true;
        _agent.isStopped = true;
        yield return new WaitForSeconds(1f);
        _agent.isStopped = false;
        thunder = false;
        yield break;
    }
}