using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharMovementController : Movement
{
    private CharacterController _controller;
    private Transform _body;
    private Vector3 _direccion;
    private bool _fijado;
    private float _gravedad = -9.81f;
    private Quaternion _targetRotation;
    private float _targetRotationSpeed;

    public override void Move(Vector3 move)
    {
        _controller.Move(move * Time.deltaTime);

        if (move != Vector3.zero)
        {
            _body.transform.forward = move;
        }

        _direccion.y += _gravedad * Time.deltaTime;
        _controller.Move(_direccion * Time.deltaTime);
    }

    public override void RotateTo(Quaternion rotation, float rotationSpeed)
    {
        _targetRotation = rotation;
        _targetRotationSpeed = rotationSpeed;
        _body.rotation = Quaternion.RotateTowards(_body.rotation, _targetRotation, _targetRotationSpeed);
    }
    void Start()
    {
        _controller = gameObject.AddComponent<CharacterController>();

        _controller.center = new Vector3(0, 1, 0);
        _body = transform.Find("PlayerBody");
    }

    // Update is called once per frame
    void Update()
    {
        



    }
}
