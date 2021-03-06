using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻撃制御クラス
/// </summary>
[RequireComponent(typeof(MobStatus))]
public class EnemyAttack : MonoBehaviour
{
    public float attackCooldown = 1.5f; // 攻撃後のクールダウン（秒）
    [SerializeField] private Collider attackCollider;
    public bool attckone = true;
    private MobStatus _status;
    private ColorChange _colorChange;

    [SerializeField] GameObject player;
    Player _player;

    private void Start()
    {
        _status = GetComponent<MobStatus>();
        _colorChange = GetComponentInChildren<ColorChange>();
        _player = player.GetComponent<Player>();
    }

    /// <summary>
    /// 攻撃可能な状態であれば攻撃を行います。
    /// </summary>
    public void AttackIfPossible()
    {
        if (!_status.IsAttackable) return; // ステータスと衝突したオブジェクトで攻撃可否を判断
        if (attckone == false) return;
        _status.GoToAttackStateIfPossible();
        _colorChange.canSee();
        //追記
        StartCoroutine(AttackCoroutine());
    }

    /// <summary>
    /// 攻撃対象が攻撃範囲に入った時に呼ばれます。
    /// </summary>
    /// <param name="collider"></param>
    public void OnAttackRangeEnter(Collider collider)
    {
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerGhost"))
        {
            AttackIfPossible();
        }
    }

    /*
    /// <summary>
    /// 攻撃の開始時に呼ばれます。
    /// </summary>
    public void OnAttackStart()
    {
        attackCollider.enabled = true;
    }*/

    /// <summary>
    /// attackColliderが攻撃対象にHitした時に呼ばれます。
    /// </summary>
    /// <param name="collider"></param>
    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<Player>();
        if (null == targetMob) return;

        // プレイヤーにダメージを与える
        Debug.Log("攻撃します");
        if (collider.CompareTag("Player"))
        {
            _player.DamageBody();
        }
        else if (collider.CompareTag("PlayerGhost"))
        {
            _player.DamageGhost();
        }
    }

    /*
    /// <summary>
    /// 攻撃の終了時に呼ばれます。
    /// </summary>
    public void OnAttackFinished()
    {
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
    }*/

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        _status.GoToNormalStateIfPossible();
        attckone = true;
    }

    private IEnumerator AttackCoroutine()
    {
        if (attckone == false) yield break;
        attckone = false;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = true;
        yield return new WaitForSeconds(0.2f);
        attackCollider.enabled = false;
        StartCoroutine(CooldownCoroutine());
        yield break;
    }
}