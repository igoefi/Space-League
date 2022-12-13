using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Transform _playerTransform;

    bool _isAgressive;
    private void Start()
    {
        _isAgressive = false;
    }
}
