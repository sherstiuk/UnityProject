using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc : MonoBehaviour {
	public enum Mode {
		GoToA,
		GoToB,
		Attack,
		Death,
		Throwing,
		//...
	}
	public Mode mode = Mode.GoToA;
	protected Mode fightMode;

	public float speed = 1.5f;
	public float runAcceleraion = 1.5f;
	protected Rigidbody2D myBody = null;
	public BoxCollider2D Head;
 	public BoxCollider2D Body;
	protected Vector3 pointA;
    protected Vector3 pointB;
	public Vector3 relocation = Vector3.one;
	public bool dying = false;
	public bool attacking = false;
	
    protected Animator animator;
	protected Vector3 my_pos;
	protected Vector3 rabbit_pos;

	// Use this for initialization
	protected void Start () {
		myBody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		relocation.y = relocation.z = 0;
		pointA = this.transform.position;
		if (relocation.x >= 0) pointB = pointA + relocation;
		else pointA += relocation;
		setFightMode();
	}
	
	// Update is called once per frame
	protected void Update () {
		
	}

	protected void FixedUpdate () {
		rabbit_pos = HeroRabbit.lastRabbit.transform.position;
        my_pos = this.transform.position;
		float value = this.getDirection();

		if (mode == Mode.Death || attacking) {
			animator.SetBool("walk", false);
			return;
		}
		else {
			if (mode == fightMode) {
				noticed();
			} else {
				if (Mathf.Abs(value) > 0 && mode != Mode.Death) {
					animator.SetBool("walk", true);
					Vector2 vel = myBody.velocity;
					vel.x = value * speed;
					myBody.velocity = vel;
				}
				else animator.SetBool("walk", false);
			}

			SpriteRenderer sr = GetComponent<SpriteRenderer>();

			if (value < 0) sr.flipX = false;
			else if (value > 0) sr.flipX = true;
		}
	}

	protected float getDirection() {
		if (rabbit_pos.x >= Mathf.Min (pointA.x, pointB.x)&& rabbit_pos.x <= Mathf.Max (pointA.x, pointB.x)){
			mode = fightMode;
		} else {
				if(my_pos.x >= pointB.x || 
				(Mathf.Abs(my_pos.x-pointB.x) > Mathf.Abs(my_pos.x-pointA.x) && mode == fightMode))
					this.mode = Mode.GoToA;
				if(my_pos.x <= pointA.x ||
				(Mathf.Abs(my_pos.x-pointB.x) <= Mathf.Abs(my_pos.x-pointA.x) && mode == fightMode))
					this.mode = Mode.GoToB;
		}
		if(mode == fightMode) {
			if(my_pos.x < rabbit_pos.x) return 1;
			else return -1;
		}
		if(this.mode == Mode.GoToB) {
			if(my_pos.x >= pointB.x) this.mode = Mode.GoToA;
		}
		else if(this.mode == Mode.GoToA) {
			if(my_pos.x <= pointA.x) this.mode = Mode.GoToB;
		}
		if(mode == Mode.GoToB) {
			if(my_pos.x <= pointB.x) return 1;
			else return -1;
		} else if(mode == Mode.GoToA) {
			if(my_pos.x >= pointA.x) return -1;
			else return 1;
		}
		return 0;
	}

	protected bool isArrived(Vector3 pos, Vector3 target) {
        pos.z = 0;
        target.z = 0;
        return Vector3.Distance(pos, target) < 0.2f;
    }

	protected virtual void noticed() {
		float value = this.getDirection();
		if (Mathf.Abs(value) > 0 && mode != Mode.Death) {
			animator.SetBool("run", true);
			Vector2 vel = myBody.velocity;
			vel.x = value * speed * runAcceleraion;
			myBody.velocity = vel;
		}
		else animator.SetBool("run", false);
	}

	protected virtual void setFightMode() {
		fightMode = Mode.Attack;
	} 

	public IEnumerator death() {
		mode = Mode.Death;
		dying = true;
		animator.SetBool("death", true);
		yield return new WaitForSeconds(0.3f);
		this.GetComponent<BoxCollider2D>().isTrigger = true;
		if (myBody != null) Destroy(myBody);
		animator.SetBool("death", false);
		Destroy(this.gameObject);   
    }

	public IEnumerator attack(HeroRabbit rabbit) {
		attacking = true;
		mode = Mode.Attack;
		animator.SetTrigger("attack");
		rabbit.death();
		yield return new WaitForSeconds(0.5f);
		animator.SetBool("walk", true);
		attacking = false;    
    }

}
