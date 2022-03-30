using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 攻撃制御クラス
/// </summary>
public class PlayerAttack : MonoBehaviour
{
    Player _player;

    [SerializeField] private float attackCooldown = 0.5f; // 攻撃後のクールダウン（秒）
    [SerializeField] private Collider attackColliderRight;
    [SerializeField] private Collider attackColliderLeft;
    public bool attackone = true;
    public bool attackcol = true;

    public AudioClip sound1;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        _player = GetComponent<Player>();
        attackColliderRight.enabled = false;
        attackColliderLeft.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 攻撃可能な状態であれば攻撃を行います。
    /// </summary>
    public void AttackIfPossible()
    {
        //if (!_status.IsAttackable) return; // ステータスと衝突したオブジェクトで攻撃可否を判断

        //_status.GoToAttackStateIfPossible(); //Attackステータスに移行
        //追記
        if (attackcol == true)
        {
            attackcol = false;
            StartCoroutine(AttackCoroutine());
        }
    }
    /// <summary>
    /// 攻撃対象が攻撃範囲に入った時に呼ばれます。
    /// </summary>
    /// <param name="collider"></param>
    public void OnAttackRangeEnter(Collider collider)
    {
        //AttackIfPossible();
    }
    /// <summary>
     /// attackColliderが攻撃対象にHitした時に呼ばれます。
     /// </summary>
     /// <param name="collider"></param>
    public void OnHitAttack(Collider collider)
    {
        var targetMob = collider.GetComponent<EnemyStatus>();
        if (null == targetMob) return;

        // エネミーにダメージを与える
        Debug.Log("Enemyに攻撃します");
        targetMob.Damage(1);
    }
    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(attackCooldown);
        //_status.GoToNormalStateIfPossible();
        attackone = true;
        attackcol = true;
    }

    private IEnumerator AttackCoroutine()
    {
        if (attackone == false) yield break;
        attackone = false;
        yield return new WaitForSeconds(0.01f);
        if (_player.IsDirection == true)
        {
            attackColliderRight.enabled = true;
        }
        else
        {
            attackColliderLeft.enabled = true;
        }
        audioSource.PlayOneShot(sound1);
        yield return new WaitForSeconds(0.5f);
        attackColliderRight.enabled = false;
        attackColliderLeft.enabled = false;
        StartCoroutine(CooldownCoroutine());
        yield break;
    }
}
