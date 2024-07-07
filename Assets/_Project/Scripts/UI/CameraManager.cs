using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _altitude = 20f;

    public Transform groundTarget;
    
    private Vector3 _projectedPosition;
    
    private Ray _ray;
    private RaycastHit _hit;

    private void Awake()
    {
        _projectedPosition = Vector3.ProjectOnPlane(transform.forward, Vector3.up).normalized;
        groundTarget.position = SelectionUtil.MiddleOfScreenPointToWorld();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Move(1);
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Move(2);
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Move(3);
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Move(4);
        }
    }

    private void Move(int dir)
    {
        switch (dir)
        {
            case 1:
                transform.Translate(_projectedPosition * (_speed * Time.deltaTime), Space.World);
                break;
            case 2:
                transform.Translate(-_projectedPosition * (_speed * Time.deltaTime), Space.World);
                break;
            case 3: 
                transform.Translate(-transform.right * (_speed * Time.deltaTime), Space.World);
                break;
            case 4:
                transform.Translate(transform.right * (_speed * Time.deltaTime), Space.World);
                break;
            default:
                break;
        }

        _ray = new Ray(transform.position, Vector3.up * -1000f);
        if (Physics.Raycast(_ray, out _hit, 1000f, Globals.TERRAIN_LAYER_MASK))
        {
            transform.position = _hit.point + Vector3.up * _altitude;
        }
        Vector3 middleOfScreen = SelectionUtil.MiddleOfScreenPointToWorld();
        groundTarget.position = middleOfScreen;
    }
}
