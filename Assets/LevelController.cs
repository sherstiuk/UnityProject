using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour {

	public static LevelController current;
	Vector3 startingPosition;
	public UILabel CoinsCollected;
    public UILabel FruitCollected;
	public int totalFruit = 0;
	int totalCrystals = 3;
	int totalLives = 3;
	int fruit = 0;
	int coins = 0;
    public UI2DSprite Heart1;
	public UI2DSprite Heart2;
	public UI2DSprite Heart3;
	public UI2DSprite Gem1;
    public UI2DSprite Gem2;
	public UI2DSprite Gem3;
	public UI2DSprite EmptyHeart;
	public UI2DSprite Blue;
	public UI2DSprite Green;
	public UI2DSprite Red;
	
	void Start() {
		CoinsCollected.text = "0000";
		FruitCollected.text = "0/" + totalFruit.ToString();
	}

	void Awake() {
		current = this;
	}
	
	public void setStartPosition(Vector3 pos) {
		this.startingPosition = pos;
	}

	public void onRabbitDeath(HeroRabbit rabbit) {
		if (totalLives >= 0) {
			--totalLives;
			switch (totalLives) {
				case 2 : Heart3.gameObject.GetComponent<UI2DSprite> ().sprite2D = EmptyHeart.gameObject.GetComponent<UI2DSprite> ().sprite2D; break;
				case 1 : Heart2.gameObject.GetComponent<UI2DSprite> ().sprite2D = EmptyHeart.gameObject.GetComponent<UI2DSprite> ().sprite2D; break;
				case 0 : Heart1.gameObject.GetComponent<UI2DSprite> ().sprite2D = EmptyHeart.gameObject.GetComponent<UI2DSprite> ().sprite2D; break;
			}
			rabbit.big = false;
			rabbit.shiny = false;
			rabbit.transform.localScale =  Vector3.one;
			rabbit.transform.position = this.startingPosition;
		} else {
			//
		}
	}

	public void addCoins(int value) {
        this.coins += value;
        string coinsNum = coins.ToString();
        string formatted = "";
        for (int i = 0; i < 4 - coinsNum.Length; ++i) formatted += "0";
        formatted += coinsNum;
        CoinsCollected.text = formatted;
    }

    public void addFruit(int value) {
        this.fruit += value;
        FruitCollected.text = fruit.ToString() + '/' + totalFruit.ToString();
    }

    public void addCrystal(int colorCode){
		UI2DSprite found = Red;
		switch (colorCode) {
				case 1 : found = Blue; break;
				case 2 : found = Green; break;
		}
		switch (totalCrystals) {
				case 3 : Gem1.gameObject.GetComponent<UI2DSprite> ().sprite2D = found.gameObject.GetComponent<UI2DSprite> ().sprite2D; break;
				case 2 : Gem2.gameObject.GetComponent<UI2DSprite> ().sprite2D = found.gameObject.GetComponent<UI2DSprite> ().sprite2D; break;
				case 1 : Gem3.gameObject.GetComponent<UI2DSprite> ().sprite2D = found.gameObject.GetComponent<UI2DSprite> ().sprite2D; break;
		}
        --totalCrystals;
    }
}
