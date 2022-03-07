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

    Rigidbody m_rigidbody = null;
    Animator m_animator = null;
    SpriteRenderer m_spriteRenderer = null;
    GameObject m_playerGhost = null;
    GameObject m_playerHuman = null;

    //PlayerAttackに関するもの
    PlayerAttack _attack;

    // Start is called before the first frame update
    void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator = GetComponentInChildren<Animator>();
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        m_playerHuman = GameObject.FindGameObjectWithTag("Player");
        m_playerGhost = GameObject.FindGameObjectWithTag("PlayerGhost");
        m_audioSource = GetComponent<AudioSource>();
        _attack = GetComponent<PlayerAttack>();
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
        updateCharge();

        //ゴーストの攻撃の処理
        //攻撃中は動けない(まだ未実装)
        //EnemyAttackを元にしたPlayerAttackを作成->ここではそれを呼ぶ処理のみ
        //Animetionの部分は入れていない
        //取り合えず右クリックで攻撃できるように
        if (Input.GetMouseButtonDown(1))
        {
            if (_attack.attackcol == true)
            {
                Debug.Log("Enemyに攻撃します1");
                _attack.AttackIfPossible();
                 m_animator.SetTrigger("Attack"); ;
            }
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
            var isCharging = Input.GetButton("Charge");
            if (isCharging)
            {

                if (m_audioSource.isPlaying == false)
                {
                    m_audioSource.Play();
                }

                // チャージ中
                m_animator.SetBool("Charge", true);
                EnableMove = false;
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

                        StartGhostMode();
                        m_changingTimer = -1.0f;
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
        }
    }
}
