using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyLevel : MonoBehaviour
{
    //EnemyManger‚©‚çEnemyLevel‚ğó‚¯æ‚Á‚Ä‚»‚ê‚É‰‚¶‚Ä“ïˆÕ“x‚ğ•Ï‚¦‚é
    [SerializeField] GameObject EnemyManager;
    private EnemyManager _manager;
    [SerializeField] GameObject CollisionDetector;
    private SphereCollider _collider;
    private EnemyMove _move;
    [SerializeField] GameObject Player;
    private Player _player;
    public bool Charge = false; //Charge’†‚©‚Ç‚¤‚©
    private NavMeshAgent _agent;
    //RandomEnemyWalk‚É“n‚µ‚Äœpœj‚·‚é‚½‚ß‚Ì•Ï”
    public bool level2 = false;
    public bool level3 = false;

    private void Awake()
    {
        _manager = EnemyManager.GetComponent<EnemyManager>();
        _collider = CollisionDetector.GetComponent<SphereCollider>();
        _move = GetComponent<EnemyMove>();
        _agent = GetComponent<NavMeshAgent>();
        _player = Player.GetComponent<Player>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_player.IsChargeMode == true)
        {
            ChargeTrue();
        }
        else
        {
            Charge = false;
            //Level‚É‰‚¶‚Ä“ïˆÕ“x‚ğØ‚è‘Ö‚¦‚é
            if (_manager.IsLevel1)
            {
                Level1();
            }
            else if (_manager.IsLevel2)
            {
                Level2();
            }
            else
            {
                Level3();
            }
        }
    }

    void ChargeTrue()
    {
        //Player‚ªƒ`ƒƒ[ƒW‚µ‚Ä‚¢‚éó‘Ô
        //‚©‚È‚è‚Ì”ÍˆÍ‚©‚çEnemy‚ªŒŸ’m‚·‚é
        Charge = true;
        _collider.radius = 10.0f;
    }

    void Level1()
    {
        //‰‚ß‚Ìó‘ÔAEnemy‚Ì3•ª‚Ì1‚ğ“|‚·‚Ü‚Å‘±‚­
        //ƒ†[ƒŒƒC‚ÍŠî–{“I‚É•”‰®‚É‹‚ÄA©•ª‚©‚ç‚Í—]‚è“®‚©‚È‚¢
        //’Ç‚¢‚©‚¯‚é‘¬“x‚Í’x‚¢-->1.5?
        //ŒŸ’m”ÍˆÍ‚Í4
        Debug.Log("EnemyLevel1");
        _agent.speed = 1.5f;
        _collider.radius = 4.0f;
        //NormalState‚Ì‚Íœpœj‚·‚é(Level1‚Í‚µ‚È‚¢)
        if (_move.RanWalk == true)
        {
            level2 = true;
            level3 = false;
        }
        else
        {
            level2 = false;
            level3 = false;
        }
    }

    void Level2()
    {
        //Enemy‚Ì3•ª‚Ì1‚ğ“|‚·‚±‚Æ‚Å‚±‚Ì’iŠK‚É‚È‚é
        //Enemy‚Ì3•ª‚Ì2‚ğ“|‚·‚Ü‚Å‘±‚­
        //ƒ†[ƒŒƒC‚Í•”‰®‚Ì’†‚ğ‚ä‚Á‚­‚è•à‚«‚Ü‚í‚é
        //’Ç‚¢‚©‚¯‚é‘¬“x‚Íl‚æ‚è­‚µ’x‚¢‚­‚ç‚¢-- > 2 ?
        //ŒŸ’m”ÍˆÍ‚Í5
        _agent.speed = 2.0f;
        _collider.radius = 5.0f;
        //NormalState‚Ì‚Íœpœj‚·‚é(Level2‚Í‚Ì‚ñ‚Ñ‚ès‚¤)
        if (_move.RanWalk == true)
        {
            level2 = true;
            level3 = false;
        }
        else
        {
            level2 = false;
            level3 = false;
        }
    }

    void Level3()
    {
        //Enemy‚Ì3•ª‚Ì2‚ğ“|‚·‚±‚Æ‚Å‚±‚Ì’iŠK‚É‚È‚é
        //ƒ†[ƒŒƒC‚Í•”‰®‚Ì’†‚ğ•’Ê‚É•à‚«‰ñ‚é
        //’Ç‚¢‚©‚¯‚é‘¬“x‚Íl‚æ‚è‚Ù‚ñ‚Ì‹Í‚©‚É’x‚¢’ö“x--> 2.5 ?
        //ŒŸ’m”ÍˆÍ‚Í6
        _agent.speed = 2.5f;
        _collider.radius = 6.0f;
        //NormalState‚Ì‚Íœpœj‚·‚é(Level3‚Í•’Ê‚É“®‚«‰ñ‚é)
        if (_move.RanWalk == true)
        {
            level2 = false;
            level3 = true;
        }
        else
        {
            level2 = false;
            level3 = false;
        }
    }
}