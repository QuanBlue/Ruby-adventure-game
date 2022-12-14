using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.5f;
    public bool vertical;
    Rigidbody2D rigidbody2D_;
    Animator animator;
    public ParticleSystem smokeEffect;
    public float changeTime = 3.0f;
    float timer;
    int direction = 1;
    bool broken = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D_ = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;
        if (timer < 0)
        {
            direction = -direction; timer = changeTime;
        }

        if (vertical)
        {
            animator.SetFloat("MoveX", 0); 
            animator.SetFloat("MoveY", direction);
        }
        else
        {
            animator.SetFloat("MoveY", 0); 
            animator.SetFloat("MoveX", direction);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2D_.position;
        if (!broken)
        {
            return;
        }

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction; ;
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction; ;
        }
        
        rigidbody2D_.MovePosition(position);
    }

    public void Fix()
    {
        broken = false;
        rigidbody2D_.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyController player = other.gameObject.GetComponent<RubyController>();
        if (player != null)
        {
            player.ChangeHealth(-1);
        }
    }
}
