using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]private Vector3 _offset;
    [SerializeField]private Transform _playerTrf;

    [Range(0, 1)]
    [SerializeField]private float _distance;


    private void Start() {
        Cursor.lockState = CursorLockMode.Confined;
    }
    private void LateUpdate() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out RaycastHit hit)){
            transform.position = Vector3.Lerp(_playerTrf.position + _offset, hit.point, _distance);

        }
    }
}
