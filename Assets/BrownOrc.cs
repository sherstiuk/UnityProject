using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownOrc : Orc {
	public GameObject prefabCarrot;

	public float throwInterval = 3f;
	float last_carrot = 0;
	float Wait;
	
	protected override void noticed() {
		float value = this.getDirection();
		mode = Mode.Throwing; 
		if (mode != Mode.Death) {
			animator.SetBool("walk", false);
			throwCarrots();
		}
	}

	protected override void setFightMode() {
		fightMode = Mode.Throwing;
		Wait = throwInterval;
	} 

	private void throwCarrots() {
        if (mode == fightMode && Wait >= throwInterval) {
            Debug.Log("BROWN 2");
			StartCoroutine(launchCarrot());
            Wait = 0f;
        }
        else Wait += Time.deltaTime;
    }

	private IEnumerator launchCarrot() {
        Animator animator = GetComponent<Animator>();
        animator.SetBool("attack", true);
        launchCarrot(getDirection());
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("attack", false);
    }
	
    void launchCarrot(float direction) {
		GameObject obj = GameObject.Instantiate(this.prefabCarrot);
		obj.transform.position = this.transform.position + (new Vector3(0f,1f,0f));
		Carrot carrot = obj.GetComponent<Carrot> ();
		carrot.launch(direction);
	}
}
