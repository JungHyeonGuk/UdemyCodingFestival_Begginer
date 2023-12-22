using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	public bool isMoveable;
    public float speed;

	GameManager gameManager;
    TileManager tileManager;
   



	public void Init(GameManager gameManager, TileManager tileManager) 
    {
        this.gameManager = gameManager;
        this.tileManager = tileManager;

        TileData startTileData = tileManager.GetTileData("Start");
        transform.position = startTileData.coord;
	}


    void InputKeyDown(Vector3Int dir) 
    {
        Vector3Int curCoord = Vector3Int.RoundToInt(transform.position);
        Vector3Int targetCoord = curCoord;

        for (int i = 0; i < 1000; i++) 
        {
            Vector3Int checkCoord = curCoord + dir * i;

            if (tileManager.HasTileData(checkCoord, "Wall")) break;
            if (tileManager.HasTileData(checkCoord, "End"))
            {
				targetCoord = checkCoord;
                break;
			}
			targetCoord = checkCoord;
		}

        StartCoroutine(MoveCo(targetCoord));
        Rotate(dir);
	}

    IEnumerator MoveCo(Vector3Int targetCoord) 
    {
        isMoveable = false;
        while (transform.position != targetCoord) 
        {
            yield return null;
            transform.position = Vector3.MoveTowards(transform.position, targetCoord, Time.deltaTime * speed);
        }
        isMoveable = true;
	}

    void Rotate(Vector3Int dir) 
    {
        float angleZ = 0;

        if (dir == Vector3Int.up) angleZ = 180;
		else if (dir == Vector3Int.down) angleZ = 0;
		else if (dir == Vector3Int.left) angleZ = 270;
		else if (dir == Vector3Int.right) angleZ = 90;

        transform.rotation = Quaternion.Euler(0, 0, angleZ);
	}


	void Update()
	{
        if (isMoveable) 
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)) InputKeyDown(Vector3Int.up);
			else if (Input.GetKeyDown(KeyCode.DownArrow)) InputKeyDown(Vector3Int.down);
			else if (Input.GetKeyDown(KeyCode.LeftArrow)) InputKeyDown(Vector3Int.left);
			else if (Input.GetKeyDown(KeyCode.RightArrow)) InputKeyDown(Vector3Int.right);

		}
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
        gameManager.PlayerTriggerEnter2D(collision.gameObject);
	}
}
