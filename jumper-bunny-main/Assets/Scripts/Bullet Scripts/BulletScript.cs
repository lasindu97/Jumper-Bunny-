using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 10f;

    private bool _canMove;

    void Awake()
    {
        _canMove = true;
    }
    private void Update()
    {
        MoveBullet();
    }

    private void MoveBullet()
    {
        if (_canMove)
        {
            Vector3 _tempPos = transform.position;
            _tempPos.x += Speed * Time.deltaTime;
            transform.position = _tempPos;
        }
        
        Destroy(this.gameObject, 0.5f);
    }

    public float Speed
    {
        get
        {
            return _bulletSpeed;
        }
        set
        {
            _bulletSpeed = value;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Snail")
        {
            _canMove = false;
            Destroy(this.gameObject, 0.05f);
        }
    }
}// class
