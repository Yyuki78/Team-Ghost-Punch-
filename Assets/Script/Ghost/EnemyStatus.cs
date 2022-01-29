using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// �G�̏�ԊǗ��X�N���v�g
/// </summary>
[RequireComponent(typeof(NavMeshAgent))]
public class EnemyStatus : MobStatus
{
    private NavMeshAgent _agent;
    Vector3 _linkEndPos;
    private bool RunEnable = true;
    private bool once = false;

    protected override void Start()
    {
        base.Start();

        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Debug.Log(_state);
        // NavMeshAgent��velocity�ňړ����x�̃x�N�g�����擾�ł���
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
    }

    protected override void OnDie()
    {
        base.OnDie();
        StartCoroutine(DestroyCoroutine());
    }

    public void Run()
    {
        if (!base.IsRunnable) return;
        if (RunEnable == false) return;
        Debug.Log("�ǂ������܂�");
        _animator.SetBool("Run", true);
        once = true;
        base.GoToRunStateIfPossible();
    }

    //���Run����Normal�ɖ߂鏈����ǉ�����
    public void StandUp()
    {
        Debug.Log("���������̂Œʏ��ԂɂȂ�܂�");
        RunEnable = false;
        if (once == true)
        {
            once = false;
            base.GoToNormalStateIfPossible();
        }
        _animator.SetBool("Run", false);
        StartCoroutine(RunEnableCoroutine());
    }

    /// <summary>
    /// �|���ꂽ���̏��ŃR���[�`���ł��B
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private IEnumerator RunEnableCoroutine()
    {
        yield return new WaitForSeconds(2);
        RunEnable = true;
        yield break;
    }
}