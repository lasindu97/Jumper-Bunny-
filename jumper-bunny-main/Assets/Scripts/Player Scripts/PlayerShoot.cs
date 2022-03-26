using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject _bulletPrefab;

    private Animator _animator;

    public float fireRate = 0.25f;
    private float _nextFire = 0f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time > _nextFire)
            {
                GameObject _bullet = Instantiate(_bulletPrefab, transform.position + new Vector3(0, -0.35f, 0), Quaternion.identity);
                _bullet.GetComponent<BulletScript>().Speed *= transform.localScale.x;

                _animator.SetBool("Shoot", true);

                _nextFire = Time.time + fireRate;
            }

        }else
        {
            _animator.SetBool("Shoot", false);
        }

    }
}// class
