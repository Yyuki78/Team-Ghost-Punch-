using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Mob（動くオブジェクト、MovingObjectの略）の状態管理スクリプト
/// </summary>
public abstract class MobStatus : MonoBehaviour
{
    /// <summary>
    /// 状態の定義
    /// </summary>
    protected enum StateEnum
    {
        Normal, // 通常
        Run,    //追いかけている
        Attack, // 攻撃中
        Die // 死亡
    }
    /// <summary>
    /// 移動可能かどうか
    /// </summary>
    public bool IsMovable => StateEnum.Normal == _state || StateEnum.Run == _state;

    /// <summary>
    /// 攻撃可能かどうか
    /// </summary>
    public bool IsAttackable => StateEnum.Normal == _state || StateEnum.Run == _state;

    /// <summary>
    /// 追いかけられるどうか
    /// </summary>
    public bool IsRunnable => StateEnum.Normal == _state;

    /// <summary>
    /// 追いかけているかどうか
    /// </summary>
    public bool IsRunState => StateEnum.Run == _state;

    /// <summary>
    /// 追いかけているかどうか+攻撃も含む
    /// </summary>
    public bool IsChangeBGM => StateEnum.Run == _state || StateEnum.Attack == _state;

    /// <summary>
    /// 死んでいるかどうか
    /// </summary>
    public bool IsDie => StateEnum.Die == _state;

    /// <summary>
    /// ライフ最大値を返します
    /// </summary>
    public float LifeMax => lifeMax;

    /// <summary>
    /// ライフの値を返します
    /// </summary>
    public float Life => _life;

    [SerializeField] private float lifeMax = 6; // ライフ最大値
    protected Animator _animator;
    protected StateEnum _state = StateEnum.Normal; // Mob状態
    private float _life; // 現在のライフ値（ヒットポイント）

    public AudioClip sound1;
    AudioSource audioSource;

    protected virtual void Start()
    {
        _life = lifeMax; // 初期状態はライフ満タン
        _animator = GetComponentInChildren<Animator>();

        // ライフゲージの表示開始
        LifeGaugeContainer.Instance.Add(this);

        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// キャラが倒れた時の処理を記述します。
    /// </summary>
    protected virtual void OnDie()
    {
        // ライフゲージの表示終了
        LifeGaugeContainer.Instance.Remove(this);
    }

    /// <summary>
    /// 指定値のダメージを受けます。
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        if (_state == StateEnum.Die) return;

        LifeGaugeContainer.Instance.Show(this);
        audioSource.PlayOneShot(sound1);
        _life -= damage;
        if (_life > 0) return;

        _state = StateEnum.Die;
        _animator.SetTrigger("Die");

        OnDie();
    }

    /// <summary>
    /// 可能であれば攻撃中の状態に遷移します。
    /// </summary>
    public void GoToAttackStateIfPossible()
    {
        if (!IsAttackable) return;

        _state = StateEnum.Attack;
        _animator.SetTrigger("Attack");
    }

    /// <summary>
    /// 可能であれば追いかける状態に遷移します。
    /// </summary>
    public void GoToRunStateIfPossible()
    {
        if (!IsMovable) return;

        _state = StateEnum.Run;
    }

    /// <summary>
    /// 可能であればNormalの状態に遷移します。
    /// </summary>
    public void GoToNormalStateIfPossible()
    {
        if (_state == StateEnum.Die) return;

        _state = StateEnum.Normal;
    }

    //HPを全回復する
    public void FullRecovery()
    {
        if (_state == StateEnum.Die) return;

        _life = lifeMax;
    }
}