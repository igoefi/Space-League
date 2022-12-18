using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent FrontOfPlayer { get; private set; } = new();
    public UnityEvent MoveToPlayer { get; private set; } = new();

    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    //to check
    [SerializeField] float _maxDistanceToPlayer = 3;

    private bool _isMoving = true;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        FrontOfPlayer.AddListener(Attack);
        MoveToPlayer.AddListener(Move);

        _navMeshAgent.stoppingDistance = _maxDistanceToPlayer;

        MoveToPlayer.Invoke();
    }

    //to check
    private void Attack()
    {
        Debug.Log("Attack");
    }
    private void Move()
    {
        Debug.Log("Move");
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null)
            return;

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _maxDistanceToPlayer)
        {
            if (!_isMoving) return;

            FrontOfPlayer.Invoke();

            _navMeshAgent.isStopped = true;
            _isMoving = false;
        }
        else
        {
            if (!_isMoving)
                MoveToPlayer.Invoke();

            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(_playerTransform.position);

            _isMoving = true;
        }
    }

    public void SetTarget(Transform transform) =>
        _playerTransform = transform;
}
