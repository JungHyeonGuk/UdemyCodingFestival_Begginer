using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public TileManager tileManager;
	public UIManager uIManager;
	public Player player;
	public Cam cam;
	public int gainCoin;
	public int gainStar;


	public void PlayerTriggerEnter2D(GameObject obj) 
	{
		if (obj.name == "Coin") 
		{
			gainCoin++;
			Destroy(obj);
		}
		else if (obj.name == "Star")
		{
			gainStar++;
			Destroy(obj);
		}
		else if (obj.name == "End")
		{
			Success();
		}
	}

	public void ClickRestart() 
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	void Success() 
	{
		uIManager.ShowResultPanel(gainCoin, gainStar);
	}


	void Start()
    {
        tileManager.Init();
		player.Init(this, tileManager);


	}

	void LateUpdate()
	{
		cam.MoveTarget(player.transform.position);
	}
}
