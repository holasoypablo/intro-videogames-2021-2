using UnityEngine;

public class PlayerMovementController : Movement
{
    private Rigidbody _rb;
    private Transform _body;
    
    private Vector3 _targetVelocity;
    private Quaternion _targetRotation;
    private float _targetRotationSpeed;

    public override void Move(Vector3 velocity)
    {
        _targetVelocity = velocity;
    }

    public override void RotateTo(Quaternion rotation, float rotationSpeed)
    {
        _targetRotation = rotation;
        _targetRotationSpeed = rotationSpeed;
    }

    private void Start()
    {
        _rb = gameObject.AddComponent<Rigidbody>();
        _body = transform.Find("PlayerBody");
    }

    void Update()
    {
        _body.rotation = Quaternion.RotateTowards(_body.rotation,  _targetRotation, _targetRotationSpeed);
    }

    void FixedUpdate()
    {
        _rb.velocity = _targetVelocity;
    }
}
