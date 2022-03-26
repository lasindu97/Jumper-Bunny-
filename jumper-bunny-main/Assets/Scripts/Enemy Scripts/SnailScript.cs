using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailScript : MonoBehaviour
{
    public Transform topCollision, leftCollision, rightCollision, bottomCollision;
 
    public float speed = 1f;

    public LayerMask playerLayer;

    private Vector3 _leftCollisionPos, _rightCollisionPos;

    private bool _isMoveLeft; // To Do --> check again (true / false)
    private bool _canMove;
    private bool _stunned;

    private Rigidbody2D _rBody;
    private Animator _animator;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();

        _leftCollisionPos = leftCollision.position;
        _rightCollisionPos = rightCollision.position;
    }// Awake

    private void Start()
    {
        _canMove = true;
        _isMoveLeft = true;
    }// Start

    private void Update()
    {
        if (_canMove)
        {
            if (_isMoveLeft)
            {
                // go left
                _rBody.velocity = new Vector2(-speed, _rBody.velocity.y);
            }
            else
            {
                // go right
                _rBody.velocity = new Vector2(speed, _rBody.velocity.y);
            }
        }

        CheckCollision();

    }// Update

    private void CheckCollision()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftCollision.position, Vector2.left, 0.1f, playerLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightCollision.position, Vector2.right, 0.1f, playerLayer);

        Collider2D topHit = Physics2D.OverlapCircle(topCollision.position, 0.2f, playerLayer);

        if (topHit != null)
        {
            if(topHit.gameObject.tag == "Player")
            {
                if (!_stunned)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
                        topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x, 9.0f);

                    _canMove = false;
                    _rBody.velocity = new Vector2(0f, 0f);

                    _animator.Play("Stunned");

                    _stunned = true;

                    if(tag == "Beetle")
                    {
                        _animator.Play("Stunned"); 
                        StartCoroutine(Dead(0.5f));
                    }
                }
            }
        }

        if (leftHit)
        {
            if (leftHit.collider.gameObject.tag == "Player")
            {
                if (!_stunned)
                {
                    // apply damage to player
                    leftHit.collider.gameObject.GetComponent<DamagePlayer>().DealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        _rBody.velocity = new Vector2(15f, _rBody.velocity.y);
                        StartCoroutine(Dead(3.0f));
                    }
                    
                }
            }
        }

        if (rightHit)
        {
            if (rightHit.collider.gameObject.tag == "Player")
            {
                if (!_stunned)
                {
                    // apply damage to player
                    rightHit.collider.gameObject.GetComponent<DamagePlayer>().DealDamage();
                }
                else
                {
                    if(tag != "Beetle")
                    {
                        _rBody.velocity = new Vector2(-15f, _rBody.velocity.y);
                        StartCoroutine(Dead(3.0f));
                    }
                    
                }
            }
        }

        if (!Physics2D.Raycast(bottomCollision.position, Vector2.down, 0.1f)) // if we do not detect collision any more
        {
            ChangeDirection();
        }

    }// CheckCollision

    private void ChangeDirection()
    {
        _isMoveLeft = !_isMoveLeft;

        Vector3 tempScale = transform.localScale;

        if (_isMoveLeft)
        {
            tempScale.x = Mathf.Abs(tempScale.x);

            leftCollision.position = _leftCollisionPos;
            rightCollision.position = _rightCollisionPos;
        }
        else
        {
            tempScale.x = -Mathf.Abs(tempScale.x);

            leftCollision.position = _rightCollisionPos;
            rightCollision.position = _leftCollisionPos;
        }

        transform.localScale = tempScale;

    }// ChangeDirection


    private IEnumerator Dead(float timer)
    {
        yield return new WaitForSeconds(timer);
        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Bullet")
        {
            if(tag == "Beetle")
            {
                _animator.Play("Stunned");

                _canMove = false;
                _rBody.velocity = new Vector2(0, 0);

                StartCoroutine(Dead(0.25f));
            }

            if (tag == "Snail")
            {
                if (!_stunned)
                {
                    _animator.Play("Stunned");
                    _stunned = true;
                    _canMove = false;

                    _rBody.velocity = new Vector2(0, 0);
                }
                else
                {
                    this.gameObject.SetActive(false);
                }
            }
        }

        
    }

}// class
