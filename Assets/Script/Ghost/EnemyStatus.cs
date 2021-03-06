using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敵の状態管理スクリプト
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
        // NavMeshAgentのvelocityで移動速度のベクトルが取得できる
        _animator.SetFloat("MoveSpeed", _agent.velocity.magnitude);
        if (Timer.TimeOut == true && once == false)
        {
            once = true;
            FullRecovery();
        }
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
        Debug.Log("追いかけます");
        _animator.SetBool("Run", true);
        base.GoToRunStateIfPossible();
    }

    //後でRunからNormalに戻る処理を追加する
    /*
    public void StandUp()
    {
        Debug.Log("立ち上がります");
        SitEnable = false;
        if (once == true)
        {
            once = false;
            base.GoToNormalStateIfPossible();
        }
        _animator.SetBool("Sit", false);
        StartCoroutine(SitEnableCoroutine());
    }*/

    /// <summary>
    /// 倒された時の消滅コルーチンです。
    /// </summary>
    /// <returns></returns>
    private IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private IEnumerator RunEnableCoroutine()
    {
        yield return new WaitForSeconds(2);
        RunEnable = true;
        yield break;
    }
}