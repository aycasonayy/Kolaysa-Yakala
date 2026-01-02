using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    private Player _player;
    public float speed;
    private Rigidbody _rb;
    public NavMeshAgent navMeshAgent;
    private Animator _animator;
    public Transform zPrefab;

    private bool _isWalking;

    private Transform _z1;
    private Transform _z2;

    public void StartEnemy(Player player)
    {
        _player = player;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
        transform.Rotate(0,Random.Range(-180,180),0);
        createAndAnimateZ();
    }

    private void createAndAnimateZ()
    {
        _z1 = Instantiate(zPrefab);
        _z1.position = transform.position + Vector3.up * 2;
        _z1.localScale = Vector3.zero;
        _z1.DOMoveY(_z1.transform.position.y + 1, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        _z1.DOScale(1, 1f).SetLoops(-1, LoopType.Restart);

        _z2 = Instantiate(zPrefab);
        _z2.position = transform.position + Vector3.up * 2;
        _z2.localScale = Vector3.zero;
        _z2.DOMoveY(_z2.transform.position.y + 1, 1f).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart).SetDelay(.6f);
        _z2.DOScale(1, 1f).SetLoops(-1, LoopType.Restart).SetDelay(.6f);
    }

    private void Update()
    {
        if (_player == null) return;
        if (navMeshAgent == null) return;
        if (_animator == null) return;
        if (_player.isAppleCollected)
        {
            //var direction = (_player.transform.position - transform.position).normalized;
            //direction.y = 0;
            //_rb.position += direction * Time.deltaTime * speed;

            navMeshAgent.destination = _player.transform.position;
            if (!_isWalking)
            {
                _isWalking = true;
                _animator.SetTrigger("WALK");
                _z1.DOKill();
                _z2.DOKill();
                Destroy(_z1.gameObject);
                Destroy(_z2.gameObject);
            }
            
        }


    }

    public void Stop()
    {
        navMeshAgent.speed = 0;
        _animator.SetTrigger("IDLE");
    }
}
