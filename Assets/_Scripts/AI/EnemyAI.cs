using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public UnityEvent FrontOfPlayer { get; private set; } = new();
    public UnityEvent MoveToPlayer { get; private set; } = new();

    private NavMeshAgent _navMeshAgent;
    private Transform _playerTransform;
    
    //to check
    [SerializeField] float _maxDistanceToPlayer = 3;

    private void Start()
    {
        _navMeshAgent= GetComponent<NavMeshAgent>();
        FrontOfPlayer.AddListener(Attack);
        MoveToPlayer.AddListener(Move);

        _navMeshAgent.stoppingDistance= _maxDistanceToPlayer;
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

    private void Update()
    {
        if (_playerTransform == null)
            return;

        if (Vector3.Distance(transform.position, _playerTransform.position) <= _maxDistanceToPlayer)
        {
            FrontOfPlayer.Invoke();

            _navMeshAgent.isStopped= true;
        }
        else
        {
            MoveToPlayer.Invoke();

            _navMeshAgent.isStopped= false;
            _navMeshAgent.SetDestination(_playerTransform.position);
        }
    }

    public void SetTarget(Transform transform) =>
        _playerTransform = transform;
}
