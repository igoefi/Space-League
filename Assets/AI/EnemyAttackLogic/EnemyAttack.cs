using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyAttack : MonoBehaviour
{
    protected EnemyAI _AI;

    protected bool _isFrontOfPlayer;
    protected bool _isReadyToAttack;

    //Будет браться из статистик
    [SerializeField] protected float _attackCDTime;
    private void Start()
    {
        _AI = GetComponent<EnemyAI>();

        _isFrontOfPlayer = false;
        _isReadyToAttack = true;

        _AI.FrontOfPlayer.AddListener(SetIsFrontOfPlayer);
    }

    private void Update()
    {
        Debug.Log(_isFrontOfPlayer);
        if (_isReadyToAttack && _isFrontOfPlayer)
            Attack();
    }

    protected void SetIsFrontOfPlayer(bool isFront) =>
        _isFrontOfPlayer=isFront;

    protected abstract void Attack();

    protected IEnumerator AttackCD()
    {
        _isReadyToAttack = false;

        yield return new WaitForSeconds(_attackCDTime);

        _isReadyToAttack = true;
    }

}
