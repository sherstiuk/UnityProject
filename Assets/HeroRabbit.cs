using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroRabbit : MonoBehaviour {
	public float speed = 2;
	Rigidbody2D myBody = null;
	bool isGrounded = false;
	bool JumpActive = false;
	float JumpTime = 0f;
    float Wait = 0.0f;
	public float MaxJumpTime = 2f;
	public float JumpSpeed = 2f;

    Transform heroParent = null;
    Animator animator;

    bool big = false;
    bool dying = false;
    public bool shiny = false;
    float shineleft = 0f;
    int healths = 1;

	// Use this for initialization
	void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		LevelController.current.setStartPosition(transform.position);
        this.heroParent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate () {
		float value = Input.GetAxis("Horizontal");
        if(dying) {
            animator.SetBool("die", false);
            Wait -= Time.deltaTime;
            if (Wait <= 0) {
                ++healths;
                dying = false;
                LevelController.current.onRabbitDeath(this);
            }
        }
        else {
            if (Mathf.Abs(value) > 0) {
                animator.SetBool("run", true);
                Vector2 vel = myBody.velocity;
                vel.x = value * speed;
                myBody.velocity = vel;
            }
            else animator.SetBool("run", false);

            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (value < 0) sr.flipX = true;
            else if (value > 0) sr.flipX = false;

            Vector3 from = transform.position + Vector3.up * 0.3f;
            Vector3 to = transform.position + Vector3.down * 0.1f;
            int layer_id = 1 << LayerMask.NameToLayer("Ground");

            RaycastHit2D hit = Physics2D.Linecast(from, to, layer_id);
            if (hit) isGrounded = true;
            else isGrounded = false;
            if(hit) {
                if(hit.transform != null && hit.transform.GetComponent<MovingPlatform>() != null)
                    SetNewParent(this.transform, hit.transform);
            } else SetNewParent(this.transform, this.heroParent);

            
            if (Input.GetButtonDown("Jump") && isGrounded) this.JumpActive = true;
            if (this.JumpActive) {
                if (Input.GetButton("Jump")) {
                    this.JumpTime += Time.deltaTime;
                    if (this.JumpTime < this.MaxJumpTime) {
                        Vector2 vel = myBody.velocity;
                        vel.y = JumpSpeed * (1.0f - JumpTime / MaxJumpTime);
                        myBody.velocity = vel;
                    }
                }
                else {
                    this.JumpActive = false;
                    this.JumpTime = 0;
                }
            }
            
            if (this.isGrounded) animator.SetBool("jump", false);
            else animator.SetBool("jump", true);

            if (!shiny) sr.color = Color.white;
            else {
                shineleft -= Time.deltaTime;
                sr.color = Color.red;
                if (shineleft < 0) shiny = false;
            }
        }
	}

    public void Bomb() {
        if(!shiny) {
            if (big) {
                big = false;
                this.transform.localScale =  Vector3.one;
                myBody.velocity /= 2;
                shiny = true;
                shineleft = 4.0f;
            }
            else {
                --healths;
                dying = true;
                animator.SetBool("die", true);
                Wait = 2.0f;
            }
        }
    }
    public void Mushroom() {
        big = true;
        this.transform.localScale =  Vector3.one*2;
        myBody.velocity *= 2;
    }

    static void SetNewParent(Transform obj, Transform new_parent) {
		if(obj.transform.parent != new_parent) {
			Vector3 pos = obj.transform.position;
			obj.transform.parent = new_parent;
			obj.transform.position = pos;
		}
	}
}
