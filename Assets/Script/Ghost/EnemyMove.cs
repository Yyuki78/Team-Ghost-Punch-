using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(EnemyStatus))]
public class EnemyMove : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask; // レイヤーマスク
    const string LayerName1 = "Default";
    const string LayerName2 = "EnemySerch";
    private int mask;

    private EnemyLevel _level;
    public bool RanWalk = true;//徘徊出来るかどうかをEnemyLevelに受け渡す

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    //雷で少しの間停止する用
    [SerializeField] GameObject StageEffect;
    private ThunderEffect _thunder;
    public bool thunder = false;//RandomEnemyMoveで止める用

    public void Start()
    {
        mask = LayerMask.GetMask(new string[] { LayerName1 });
        _level = GetComponent<EnemyLevel>();
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
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
            Debug.Log("チャージ中またはゴーストモードです");
            mask = LayerMask.GetMask(new string[] { LayerName2 });
            raycastLayerMask = mask;
        }
        if (_thunder.canSee == true)
        {
            StartCoroutine("StopMoving");
        }
    }

    // CollisionDetectorのonTriggerStayにセットし、衝突判定を受け取るメソッド
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

        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerGhost"))
        {
            //EnemyのステータスをRunに変更
            _status.GoToRunStateIfPossible();
            RanWalk = false;

            var positionDiff = collider.transform.position - transform.position; // 自身とプレイヤーの座標差分を計算
            var distance = positionDiff.magnitude; // プレイヤーとの距離を計算
            var direction = positionDiff.normalized; // プレイヤーへの方向

            // _raycastHitsに、ヒットしたColliderや座標情報などが格納される
            // RaycastAllとRaycastNonAllocは同等の機能だが、RaycastNonAllocだとメモリにゴミが残らないのでこちらを推奨
            var hitCount = Physics.RaycastNonAlloc(transform.position, direction, _raycastHits, distance, raycastLayerMask);
            Debug.Log("hitCount: " + hitCount);
            if (hitCount == 0)
            {
                // 本作のプレイヤーはCharacterControllerを使っていて、Colliderは使っていないのでRaycastはヒットしない
                // つまり、ヒット数が0であればプレイヤーとの間に障害物が無いということになる
                _agent.isStopped = false;
                _agent.destination = collider.transform.position;
            }
            else
            {
                // 見失ったら停止する
                _agent.isStopped = true;
            }
        }
    }

    // CollisionDetectorのonTriggerExitにセットし、衝突判定を受け取るメソッド
    public void GoLoiterState(Collider collider)
    {
        // 検知したオブジェクトに「Player」のタグがついていれば、徘徊モードに切り替える
        if (collider.CompareTag("Player") || collider.CompareTag("PlayerGhost"))
        {
            //動ける＋追いかけていた状態でないならreturn
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