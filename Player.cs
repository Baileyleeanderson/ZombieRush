using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Player : MonoBehaviour
{

    [SerializeField] private float _jumpForce = 100f;
    [SerializeField] private AudioClip _sfxJump;
    [SerializeField] private AudioClip _sfxDeath;

    private Animator _anim;
    private Rigidbody _rigidbody;
    private bool _jump = false;
    private AudioSource _audioSource;

    private void Awake()
    {
        Assert.IsNotNull(_sfxJump);
        Assert.IsNotNull(_sfxDeath);
    }

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        
        if (!GameManager.instance.GameOver && GameManager.instance.GameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("jumpclick");
                GameManager.instance.PlayerStartedGame();
                _anim.Play("Jump");
                _jump = true;
            }
        }
        else if (GameManager.instance.GameOver){
            StartCoroutine(ResetPlayerPos());
        }
    }

    private void FixedUpdate()
    {
        
        if (_jump == true)
        {
            _jump = false;
            _rigidbody.useGravity = true;
            _rigidbody.velocity = new Vector2(0, 0);
            _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode.Impulse);
            _audioSource.PlayOneShot(_sfxJump);
            if (transform.position.y >= 12.26f)
            {
                Death();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "obstacle" || collision.gameObject.tag == "ground")
        {
            Death();

        }
    }

    IEnumerator ResetPlayerPos(){
        yield return new WaitForSeconds(3.0f);
    }

    void Death(){
        _rigidbody.AddForce(new Vector2(-100, 20), ForceMode.Impulse);
        _rigidbody.detectCollisions = false;
        _audioSource.PlayOneShot(_sfxDeath);
        GameManager.instance.PlayerCollided();
        _jump = false;
    }
}
