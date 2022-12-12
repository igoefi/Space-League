using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    [Header("Movement")]
    [SerializeField] private float WalkSpeed;
    [SerializeField] private float DashSpeed;
    [SerializeField] private float Drag;
    [SerializeField] private float MaxAngleOnSlop;

    [Header("Dash")]
    [SerializeField] private float DashForce;
    [SerializeField] private float DashDuration;
    [SerializeField] private float DashCooldown;

    private float _speed;
    private float _dashTimer;
    private bool _dashing = false;
    private bool _dashIsReady = true;
    private bool _onGround;


    private Vector3 _inputDir;
    private RaycastHit _slopeHit;
    private Rigidbody _playerRB;

    private void Start() {
        _playerRB = GetComponent<Rigidbody>();
    }


    private void Update() {
        PlayerInput();
        CurrentSpeed();
        SpeedControl();

        _onGround = Physics.Raycast(transform.position, Vector3.down, 2 * 0.5f + 0.2f);

        if(_inputDir == Vector3.zero && !_dashing && _onGround){
            _playerRB.drag = Drag;
        }
        else{
            _playerRB.drag = 0;
        }
    }

    private void PlayerInput(){
        _inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Jump") && _dashIsReady){
            StartDash();
        }
    }

    private void FixedUpdate() {
        if(_dashing){
            Dash();
            return;
        }
        Move();
    }

    private void  CurrentSpeed(){
        if(_dashing){
            _speed = DashSpeed;
        }
        else{
            _speed = WalkSpeed;
        }
    }

    private void Move(){
        if(OnSlope()){
            _playerRB.AddForce(Vector3.ProjectOnPlane(_inputDir.ToIso(), _slopeHit.normal) * _speed * 10, ForceMode.Force);
        }
        else{
            _playerRB.AddForce(_inputDir.ToIso() * _speed * 10, ForceMode.Force);
        }
    }
    #region Dash Func
    private void StartDash(){
        _dashing = true; 
        _dashIsReady = false; 

        _dashTimer = DashDuration;

    }
    private void Dash(){
        if(OnSlope()){
            _playerRB.AddForce(Vector3.ProjectOnPlane(_inputDir.ToIso(), _slopeHit.normal) * DashForce, ForceMode.Force);
        }
        else{
            _playerRB.AddForce(_inputDir.ToIso() * DashForce, ForceMode.Force);
        }

        _dashTimer -= Time.deltaTime;

        if(_dashTimer <= 0){
            StopDash();
        }
    }
    private void StopDash(){
        _dashing = false;

        Invoke(nameof(ResetDash), DashCooldown);
    }

    private void ResetDash(){
        _dashIsReady = true;
    }
    #endregion
    #region Util
    private void SpeedControl(){
        Vector3 flowVel = new Vector3(_playerRB.velocity.x, 0, _playerRB.velocity.z);

        if(flowVel.magnitude > _speed){
            Vector3 limetedVel = flowVel.normalized * _speed;
            _playerRB.velocity = new Vector3(limetedVel.x, _playerRB.velocity.y, limetedVel.z);
        }
    }

    public bool OnSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out  _slopeHit,2 * 0.5f + 0.2f)){
            float angle = Vector3.Angle(Vector3.up, _slopeHit.point);
            return (angle < MaxAngleOnSlop && angle != 0);
        }
        return false;
    }
    #endregion
}
