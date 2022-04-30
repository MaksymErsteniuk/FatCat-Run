using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] Rigidbody _rigidbody;
    [SerializeField] SurfaceSlider _slider;
    private float _speedFromSideToSide = 2f;
    private float _speed = 4f;
    public void Move(Vector3 direction)
    {
        Vector3 normalized = direction.normalized;
        normalized.x = 1;
        /*Vector3 directionAlongSurface = _slider.Project(normalized)*/;
        //Vector3 offset = directionAlongSurface * Speed * Time.deltaTime;
        Vector3 offset = normalized * _speed * Time.deltaTime;
        offset.z *= _speedFromSideToSide;
        //Debug.Log(offset);
        _rigidbody.MovePosition(_rigidbody.position + this.transform.TransformDirection(offset));
    }
}