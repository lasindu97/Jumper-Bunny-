using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpPower = 5.0f;

    private Rigidbody2D _rBody;
    private Animator _animator;

    public Transform groundCheckPosition;
    public LayerMask whatIsGround;

    private bool _isGrounded;
    private bool _jumped;
    [SerializeField]
    private bool _isPowerupActive;

    private void Awake()
    {
        _rBody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }// Awake

    private void Update()
    {
        CheckIfGrounded();
        Playerjump();

    }// Update

    private void FixedUpdate()
    {
        float _horizontalInput = Input.GetAxisRaw("Horizontal");
        
        // 0 --> no movement
        // -1 --> go left (left arrow / A)
        // +1 --> go right (right arrow / D) 

        if(_horizontalInput > 0)
        {
            // go right
            ChangeDirection(0.25f);

            // current velocity = new velocity
            if (_isPowerupActive)
            {
                _rBody.velocity = new Vector2(speed * 2, _rBody.velocity.y);
            }
            else
            {
                _rBody.velocity = new Vector2(speed, _rBody.velocity.y);
            }
            

        }
        else if(_horizontalInput < 0)
        {
            // go left
            ChangeDirection(-0.25f);

            if (_isPowerupActive)
            {
                _rBody.velocity = new Vector2(-speed * 2, _rBody.velocity.y);
            }
            else
            {
                _rBody.velocity = new Vector2(-speed, _rBody.velocity.y);
            }

            

        }
        else
        {
            // _horizontalInput = 0
            _rBody.velocity = new Vector2(0f, _rBody.velocity.y);

        }

        _animator.SetInteger("Speed", Mathf.Abs((int)_rBody.velocity.x));

    }// FixedUpdate

    private void ChangeDirection(float direction)
    {
        Vector3 _tempScale = transform.localScale;
        _tempScale.x = direction;
        transform.localScale = _tempScale;
    
    }// ChangeDirection

    private void CheckIfGrounded()
    {
        _isGrounded = Physics2D.Raycast(groundCheckPosition.position, Vector2.down, 0.1f, whatIsGround);

        if (_isGrounded)
        {
            if (_jumped)
            {
                _jumped = false;
                _animator.SetBool("Jump", false);
            }
        }

    }// CheckIfGrounded

    private void Playerjump()
    {
        if(_isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _jumped = true;
                _rBody.velocity = new Vector2(_rBody.velocity.x, jumpPower);

                _animator.SetBool("Jump", true);
            }
        }

    }// Playerjump

    private void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Carrot")
        {
            _isPowerupActive = true;

            StartCoroutine(PowerDownRoutine());

            target.gameObject.SetActive(false);

        }else if(target.tag == "Finish")
        {
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator PowerDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        _isPowerupActive = false;
        print("Power has down!");
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Gameplay 2");
    }


}// class
