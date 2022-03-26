using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float resetSpeed = 0.5f;
    public float cameraSpeed = 0.3f;

    public Bounds cameraBounds;

    private Transform _target;

    private float offsetZ;

    private Vector3 _lastTargetPosition;
    private Vector3 _currentVelocity;

    private bool _follwsPlayer;

    private void Awake()
    {
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();
        myCollider.size = new Vector2(Camera.main.aspect * 2f * Camera.main.orthographicSize, 15f);
    }

    // Update is called once per frame
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _lastTargetPosition = _target.position;
        offsetZ = (transform.position - _target.position).z;
        _follwsPlayer = true;
        
    }

    private void FixedUpdate()
    {
        Vector3 aheadTargetPos = _target.position + Vector3.forward * offsetZ;

        if(aheadTargetPos.x >= transform.position.x)
        {
            Vector3 newCameraPosition = Vector3.SmoothDamp(transform.position, aheadTargetPos,
                ref _currentVelocity, cameraSpeed);

            transform.position = new Vector3(newCameraPosition.x, transform.position.y,
                newCameraPosition.z);

            _lastTargetPosition = _target.position;
        }
    }
}// class
