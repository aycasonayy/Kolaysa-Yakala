using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;
    public float speed = 2;

    public bool isAppleCollected;

    private Rigidbody _rb;

    private bool _isCharacterRunning;

    public Animator animator;

    public void RestartPlayer()
    {
        gameObject.SetActive(true);
        _rb = GetComponent<Rigidbody>();
        _rb.position = new Vector3 (0,0.36f,-3);
        isAppleCollected = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collectable"))
        {
            other.gameObject.SetActive(false);
            gameDirector.levelManager.AppleCollected();
            isAppleCollected = true;
        }
        if (other.CompareTag("Door") && isAppleCollected)
        {
            print("Win the Level!");
            gameDirector.LevelCompleted();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.transform.CompareTag("Enemy"))
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        var direction = Vector3.zero;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = 6;
            SetRunAnimationSpeed(2f);
        }
        else
        {
            speed = 3;
            SetRunAnimationSpeed(1f);
        }
        if (Input.GetKey(KeyCode.W))
        {
            direction += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            direction += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            direction += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            direction += Vector3.right;
        }

        if (direction.magnitude < .1f)
        {
            TrigerIdleAnimation();
        }
        else
        {
            
            TriggerRunAnimation();
        }

            transform.LookAt(transform.position + direction);
        _rb.linearVelocity = direction.normalized * speed;
    }

    void TriggerRunAnimation()
    {
        if (!_isCharacterRunning)
        {
            animator.SetTrigger("RUN");
            _isCharacterRunning = true;
        }

    }

    void TrigerIdleAnimation()
    {
        if (_isCharacterRunning)
        {
            animator.SetTrigger("IDLE");
            _isCharacterRunning = false;
        }
    }

    void SetRunAnimationSpeed(float s)
    {
        animator.SetFloat("RunSpeedMultiplier",s);

    }
}
