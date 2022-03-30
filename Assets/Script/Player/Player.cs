using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField]
    float m_moveSpeed = 1.0f;
    [SerializeField]
    bool m_isChargeMode = false;
    [SerializeField]
    public bool m_isGhostObject = false;
    [SerializeField]
    float m_chargePowerAddSpeed = 1.0f;
    [SerializeField]
    float m_changingActionTime = 1.0f;
    AudioSource m_audioSource;

    // チャージ中か
    public bool IsChargeMode { get { return m_isChargeMode; } }
    public float GetChargePower { get { return m_chargePower; } }
    public const float MaxChargePower = 100.0f;
    public bool EnableMove { private set; get; } = false;
    public bool IsDead { private set; get; } = false;
    public bool IsGhostMode { private set; get; } = false;
    public bool IsDirection = false;//プレイヤーの向き Trueなら右向き

    public float m_chargePower = 0;
    float m_changingTimer = -1.0f;
    float m_changingTimer2 = -1.0f;

    Rigidbody m_rigidbody = null;
    Animator m_animator = null;
    SpriteRenderer m_spriteRenderer = null;
    GameObject m_playerGhost = null;
    GameObject m_playerHuman = null;

    Player _player;

    //PlayerAttackに関するもの
    PlayerAttack _attack;

    //これがTrueでGhostへ変身する
    private bool ChangeGhost = false;

    //PlayerのHP
    [SerializeField] private int Life = 10;
    public float _life => Life;
    //敵からくらうゲージダメージ(EnemyLevelで変える)
    public int GaugeDamage = 10;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponentInChildren<Animator>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_playerHuman = GameObject.FindGameObjectWithTag("Player");
        _player = m_playerHuman.GetComponent<Player>();
        m_playerGhost = GameObject.FindGameObjectWithTag("PlayerGhost");
        m_audioSource = GetComponent<AudioSource>();
        _attack = m_playerGhost.GetComponent<PlayerAttack>();
    }

    private void Start()
    {
        EnableMove = true;
        m_isChargeMode = false;

        if (m_isGhostObject)
        {
            m_playerGhost.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        updateMove();
        if (Life > 0)
        {
            updateCharge();
            updateAttack();
        }
        else
        {
            updateLife();
        }
        
    }

    void updateMove()
    {
        if (EnableMove)
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");
            var input = new Vector3(x, 0, y);
            var velocity = input * m_moveSpeed;

            m_rigidbody.velocity = velocity;
            if (m_spriteRenderer != null && input.magnitude > 0.2f)
            {
                if (x > 0.0f)
                {
                    IsDirection = true;
                    m_spriteRenderer.flipX = true;
                }
                else
                {
                    IsDirection = false;
                    m_spriteRenderer.flipX = false;
                }
                m_animator.SetFloat("Walk", 1.0f);
            }
            else
            {
                m_animator.SetFloat("Walk", 0.0f);
            }
        }
        else
        {
            m_rigidbody.velocity = Vector3.zero;
        }
    }

    void updateCharge()
    {
        // プレイヤーのチャージ操作
        if (m_isGhostObject == false && IsDead == false)
        {
            //仕様変更　クリックではなく、特定のエリアに入ることでチャージ
            //var isCharging = Input.GetButton("Charge");
            var isCharging = PlayerCharge.IsCharging;
            if (isCharging)
            {

                if (m_audioSource.isPlaying == false)
                {
                    m_audioSource.Play();
                }

                // チャージ中
                m_animator.SetBool("Charge", true);
                //EnableMove = false;
                m_isChargeMode = true;

                if (m_chargePower < MaxChargePower)
                {
                    // 回復
                    m_chargePower += m_chargePowerAddSpeed * Time.deltaTime;
                    if (m_chargePower > MaxChargePower)
                    {
                        m_chargePower = MaxChargePower;
                    }
                }
            }
            else
            {
                // チャージなし
                m_animator.SetBool("Charge", false);
                EnableMove = true;
                m_isChargeMode = false;

                if (m_audioSource.isPlaying)
                {
                    m_audioSource.Stop();
                }
            }
            if (m_chargePower >= MaxChargePower)
            {
                if (m_audioSource.isPlaying)
                {
                    m_audioSource.Stop();
                }
                if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) || Input.GetButton("Charge"))
                {
                    ChangeGhost = true;
                }
                if (ChangeGhost == true)
                {
                    EnableMove = false;
                    // チャージ最大
                    if (m_changingTimer < 0.0f)
                    {
                        // 返信ポーズ
                        m_animator.SetBool("SwitchGhost", true);
                        m_changingTimer = m_changingActionTime;
                    }
                    else
                    {
                        m_changingTimer -= Time.deltaTime;
                        if (m_changingTimer < 0.0f)
                        {
                            // 変身ポーズ後、ゴースト化
                            m_animator.SetBool("SwitchGhost", false);
                            m_animator.SetBool("Charge", false);

                            StartCoroutine("ReduceChargeGauge");
                            StartGhostMode();
                            m_changingTimer = -1.0f;
                        }
                    }
                }
            }
        }
        //幽体から人間へ戻る
        if(m_isGhostObject == false && IsDead == true)
        {
            if(m_chargePower <= 0)
            {
                // チャージが0になった
                if (m_changingTimer2 < 0.0f)
                {
                    // 変身ポーズ
                    //m_animator.SetBool("SwitchGhost", true);
                    m_changingTimer2 = m_changingActionTime;
                }
                else
                {
                    m_changingTimer2 -= Time.deltaTime;
                    if (m_changingTimer2 < 0.0f)
                    {
                        // 変身ポーズ後、ゴースト化
                        //m_animator.SetBool("SwitchGhost", false);
                        //m_animator.SetBool("Charge", false);
                        
                        StartBodyMode();
                        m_changingTimer2 = -1.0f;
                    }
                }
            }
        }
    }

    void StartGhostMode()
    {
        // ゴーストモード開始
        if (m_isGhostObject == false)
        {
            // にんげん側処理
            m_animator.SetBool("Dead", true);
            EnableMove = false;
            IsDead = true;

            // ゴースト側処理
            m_playerGhost.SetActive(true);
            var ghostAnimator = m_playerGhost.GetComponentInChildren<Animator>();
            ghostAnimator.SetBool("Ghost", true);
            var player = m_playerGhost.GetComponent<Player>();
            player.IsGhostMode = true;
            m_playerGhost.transform.position = transform.position;

            _attack.attackcol = true;
            _attack.attackone = true;
        }
    }

    private IEnumerator ReduceChargeGauge()
    {
        yield return new WaitForSeconds(1f);
        while (m_chargePower > 0)
        {
            m_chargePower--;
            yield return new WaitForSeconds(0.2f);
        }
        yield break;
    }

    private void StartBodyMode()
    {
        StopCoroutine("ReduceChargeGauge");
        ChangeGhost = false;
        // ゴースト側処理
        _attack.attackcol = true;
        _attack.attackone = true;
        //var ghostAnimator = m_playerGhost.GetComponentInChildren<Animator>();
        //ghostAnimator.SetBool("Ghost", false);
        //var player = m_playerGhost.GetComponent<Player>();
        //m_playerGhost.transform.position = transform.position;
        m_playerGhost.SetActive(false);

        // にんげん側処理
        m_animator.SetBool("Dead", false);
        EnableMove = true;
        IsDead = false;
    }

    //体への攻撃か幽体への攻撃かで分ける
    public void DamageBody()
    {
        Life-=5;
    }

    public void DamageGhost()
    {
        m_chargePower -= GaugeDamage;
    }

    private void updateAttack()
    {
        //ゴーストの攻撃の処理
        //攻撃中は動けない
        //EnemyAttackを元にしたPlayerAttackを作成->ここではそれを呼ぶ処理のみ
        //取り合えずクリックで攻撃できるように
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(0) || Input.GetButton("Charge"))
        {
            if (_attack.attackcol == true && m_isGhostObject == true)
            {
                if (_player.m_chargePower > 0)
                {
                    Debug.Log("Enemyに攻撃します1");
                    _attack.AttackIfPossible();
                    m_animator.SetTrigger("Attack");
                }
            }
        }
        if(m_isGhostObject == true)
        {
            if(_attack.attackcol == false)
            {
                EnableMove = false;
            }
            else
            {
                EnableMove = true;
            }
        }
    }

    private void updateLife()
    {
        //Lifeが0の時に動けなくなる＋死ぬ
        if (Life <= 0)
        {
            if (m_isGhostObject == false)
            {
                EnableMove = false;
                IsDead = false;
                _attack.attackcol = false;
                if (m_changingTimer < 0.8f)
                {
                    m_animator.SetBool("SwitchGhost", true);
                    m_changingTimer = m_changingActionTime;
                }
                else
                {
                    m_changingTimer -= Time.deltaTime;
                    if (m_changingTimer < 0.8f)
                    {
                        // 変身ポーズ後、ゴースト化
                        m_animator.SetBool("SwitchGhost", false);
                        m_animator.SetBool("Charge", false);
                        m_animator.SetBool("Dead", true);
                        m_changingTimer = -1.0f;
                    }
                }
            }
        }
    }
}
