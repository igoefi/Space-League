using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent<bool> FrontOfPlayer{ get; private set; } = new();

    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;

    //to check
    [SerializeField] float _maxDistanceToPlayer = 3;

    private bool _isMoving = true;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();

        _navMeshAgent.stoppingDistance = _maxDistanceToPlayer;
    }

    private void FixedUpdate()
    {
        if (_playerTransform == null)
            return;

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _maxDistanceToPlayer)
        {
            if (!_isMoving) return;

            FrontOfPlayer.Invoke(true);

            _navMeshAgent.isStopped = true;
            _isMoving = false;
        }
        else
        {
            if (!_isMoving)
            {
                FrontOfPlayer.Invoke(false);
                _isMoving = true;
                _navMeshAgent.isStopped = false;
            }

            _navMeshAgent.SetDestination(_playerTransform.position);


        }
    }

    public void SetTarget(Transform transform) =>
        _playerTransform = transform;
}
