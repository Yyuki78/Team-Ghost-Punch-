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
    const string LayerName2 = "Nothing";
    private float step_time;
    private int mask1;
    private int mask2;
    const int LayerNum = 8;

    private NavMeshAgent _agent;
    private RaycastHit[] _raycastHits = new RaycastHit[10];
    private EnemyStatus _status;

    public void Start()
    {
        mask1 = LayerMask.GetMask(new string[] { LayerName1 });
        mask2 = LayerMask.GetMask(new string[] { LayerName2 });
        step_time = 0.0f;
        _agent = GetComponent<NavMeshAgent>(); // NavMeshAgentを保持しておく
        _status = GetComponent<EnemyStatus>();
    }

    public void Update()
    {
        // 経過時間をカウント
        step_time += Time.deltaTime;
        if (Physics.Raycast(transform.position,
                            transform.forward,
                            float.PositiveInfinity,
                            mask1))
            Physics.Raycast(transform.position,
            transform.forward,
            float.PositiveInfinity,
            mask1);
        {

            Debug.Log("Raycast Hit!Mask1");
        }
        // 10秒後にMask2
        if (step_time >= 10.0f)
        {
            Physics.Raycast(transform.position,
            transform.forward,
            float.PositiveInfinity,
            mask2);
            if (Physics.Raycast(transform.position,
                            transform.forward,
                            float.PositiveInfinity,
                            mask2))
            {

                Debug.Log("Raycast Hit!Mask2");
            }
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

        // 検知したオブジェクトに「Player」のタグがついていれば、そのオブジェクトを追いかける
        if (collider.CompareTag("Player"))
        {
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
}