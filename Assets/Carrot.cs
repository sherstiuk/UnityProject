using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrot : Collectable {
	public float lifetime = 3f;
	public float speed = 3f;
	float direction = 1f;
	float last_carrot = 0f;
	Vector3 relocation;
	
	void Start() {
		relocation = new Vector3();
		relocation.x = speed;
		relocation.y = 0.5f;
		relocation.z = 0f;
		StartCoroutine (destroyLater ());
	}

	IEnumerator destroyLater() {
		yield return new WaitForSeconds (lifetime);
		Destroy (this.gameObject);
	}

	public void FixedUpdate() {
        if (direction != 0) {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (direction < 0) sr.flipX = true;
			else if (direction > 0) sr.flipX = false;
            if (Mathf.Abs(direction) > 0)
				this.transform.position += relocation * direction * Time.deltaTime;
        }
    }

	public void launch(float direction) {
		this.direction = direction;
	}

	protected override void OnRabbitHit(HeroRabbit rabbit) {
		rabbit.Carrot();
		this.CollectedHide();
	}
}