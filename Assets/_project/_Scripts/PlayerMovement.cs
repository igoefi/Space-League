using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{   
    [Header("Movement")]
    [SerializeField]private float _walkSpeed;
    private float _walkSpeedBonus;
    [SerializeField]private float _dashSpeed;
    private float _dashSpeedBonus;
    [SerializeField]private float _drag;
    [SerializeField]private float _maxAngleOnSlop;

    [Header("Dash")]
    [SerializeField]private float _dashForce;
    [SerializeField]private float _dashDuration;
    [SerializeField]private float _dashCooldown;

    private float _speed;
    private float _dashTimer;
    private bool _dashing = false;
    private bool _dashIsReady = true;
    private bool _onGround;


    private Vector3 _inputDir;
    private RaycastHit _slopeHit;
    private RaycastHit _groundHit;
    private Rigidbody _playerRB;
    private Player _player;

    private void Start() {
        _playerRB = GetComponent<Rigidbody>();
        _player = GetComponent<Player>();

        _player.OnNegativeEffectEvent =  Slowing;
    }

    #region Main func
    private void Update() {
        PlayerInput();
        CurrentSpeed();
        SpeedControl();

        _onGround = Physics.Raycast(transform.position, Vector3.down, out _groundHit, 2 * 0.5f + 0.2f);
        _playerRB.useGravity = !OnSlope();

        if(_inputDir == Vector3.zero && !_dashing && _onGround){
            _playerRB.drag = _drag;
        }
        else{
            _playerRB.drag = 0;
        }
    }

    private void PlayerInput(){
        _inputDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if(Input.GetButtonDown("Jump") && _dashIsReady && _player.Stamina > 25){
            StartDash();
        }
    }

    private void FixedUpdate() {
        if(_playerRB.velocity.y > 0){
            _playerRB.AddForce(Vector3.down * 100f, ForceMode.Force);
        }
        if(_dashing){
            Dash();
            return;
        }
        Move();
    }


    private void Move(){
        if(OnSlope()){
            _playerRB.AddForce(Vector3.ProjectOnPlane(_inputDir.ToIso(), _slopeHit.normal).normalized * _speed * 10, ForceMode.Force);
        }
        else{
            _playerRB.AddForce(_inputDir.ToIso() * _speed * 10, ForceMode.Force);
        }
    }
    #endregion
    #region Dash Func
    private void StartDash(){
        _dashing = true; 
        _dashIsReady = false; 

        _dashTimer = _dashDuration;

        _player.DecreaseStamina(25);
    }
    private void Dash(){

        if(OnSlope()){
            _playerRB.AddForce(Vector3.ProjectOnPlane(_inputDir.ToIso(), _slopeHit.normal).normalized * _dashForce, ForceMode.Force);
        }
        else{
            _playerRB.AddForce(_inputDir.ToIso() * _dashForce, ForceMode.Force);
        }

        _dashTimer -= Time.deltaTime;

        if(_dashTimer <= 0){
            StopDash();
        }
    }
    private void StopDash(){
        _dashing = false;

        Invoke(nameof(ResetDash), _dashCooldown);
    }

    private void ResetDash(){
        _dashIsReady = true;
    }
    #endregion
    #region Util
    private void  CurrentSpeed(){
        if(_dashing){
            _speed = _dashSpeed + _dashSpeedBonus;
        }
        else{
            _speed = _walkSpeed + _walkSpeedBonus;
        }
        if(_onGround && _groundHit.collider.CompareTag("SlowZone")){
            Slowing();
        }
    }
    
    private void SpeedControl(){
        Vector3 flowVel = new Vector3(_playerRB.velocity.x, 0, _playerRB.velocity.z);

        if(flowVel.magnitude > _speed){
            Vector3 limetedVel = flowVel.normalized * _speed;
            _playerRB.velocity = new Vector3(limetedVel.x, _playerRB.velocity.y, limetedVel.z);
        }
    }

    public bool OnSlope(){
        if(Physics.Raycast(transform.position, Vector3.down, out  _slopeHit, 2 * 0.5f + 0.3f)){
            float angle = Vector3.Angle(Vector3.up, _slopeHit.normal);
            return (angle < _maxAngleOnSlop && angle != 0);
        }
        return false;
    }
    #endregion
    #region Buff func
    public void Slowing(){
        _speed = _walkSpeed/2;
    }
    public void SetBonusWalkSpeed(float amount){
        _walkSpeedBonus += amount;
    }
    public void SetBonusDashSpeed(float amount){
        _dashSpeedBonus += amount;
    }
    #endregion
}
