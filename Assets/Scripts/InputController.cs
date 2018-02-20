﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    private Board boardScript;
    /**
     * LastClickedX represents the object that was last clicked in 3D space. 
     */
	public Tile LastClickedTile{get; private set;}
	public Player LastClickedPlayer{get; private set;}
	public Obstacle LastClickedObstacle{get; private set;}
	//public Camp LastClickedCamp{get; private set;}

    /**
     * Decides what to focus the camera on. Basically last clicked object
     */
    public int CurrentSelected { get; private set; }
    /**
    * SelectedX represents the Player/Obstacle which should be able to move.
    */
    public Player SelectedPlayer{get; set;}
    public Obstacle SelectedObstacle{get; set;}

	void Start ()
    {
        this.boardScript = this.GetComponent<Board>();
		this.LastClickedTile = null;
		this.LastClickedPlayer = null;
		this.LastClickedObstacle = null;
        this.SelectedPlayer = null;
        this.SelectedObstacle = null;
	}
	
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
		{
			this.UpdateLastClickedObjects();
		}
        if (Input.GetKeyDown("m") && this.LastClickedPlayer != null)
        {
            // highlight possible moves
            Board board = this.LastClickedPlayer.GetCamp().GetParent();
            //board.RemoveTileHighlights();
            if (this.LastClickedPlayer.HasControlledObstacle())
            {
                foreach (Tile tile in this.boardScript.Tiles)
                {
                    if (!tile.HasOccupant())
                        tile.gameObject.GetComponent<Renderer>().material.color = Color.green;
                }
            }
            else
                new PlayerControl(this.LastClickedPlayer).HighlightPossibleMoves(board.GetDice.NumberFaceUp(), Color.green);
        }
    }

    /**
	* Update selected Player, Camp etc...
    * Aswin: Also updates the tags on the objects. Useful for controlling camera
    *        Remember to update getLastClicked() in cameraControl.cs if anything new is added here. e.g: Camps...
	*/
    private void UpdateLastClickedObjects()
    {
        Tile currentTile = this.GetMousedTile();
        if (currentTile != null)
        {
            if (this.LastClickedTile != null)
                this.LastClickedTile.tag = "Tiles";
            this.LastClickedTile = currentTile;
            currentTile.tag = "CurrentTile";
            this.CurrentSelected = 1;
        }
        Player currentPlayer = this.GetMousedPlayer();
        if (currentPlayer != null)
        {
            if (this.LastClickedPlayer != null)
                this.LastClickedPlayer.tag = "Players";
            this.LastClickedPlayer = currentPlayer;
            currentPlayer.tag = "CurrentPlayer";
            this.CurrentSelected = 2;
        }

        Obstacle currentObstacle = this.GetMousedObstacle();
        if (currentObstacle != null)
        {
            if (this.LastClickedObstacle != null)
                this.LastClickedObstacle.tag = "Obstacles";
            this.LastClickedObstacle = currentObstacle;
            currentObstacle.tag = "CurrentObstacle";
            this.CurrentSelected = 3;
        }
    }

    /**
     * Returns the gameobject hit by a ray-cast from the camera transform.
     */
	private GameObject GetMousedGameObject()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit target = new RaycastHit();
		if (Physics.Raycast(ray, out target))
			return target.transform.gameObject;
		else
			return null;
	}

    /**
     * Returns the tile that is GetMousedGameObject. If that is not a tile, returns null.
     */
    private Tile GetMousedTile()
    {
		foreach(Tile tile in this.boardScript.Tiles)
			if(tile.gameObject == this.GetMousedGameObject())
				return tile;
		return null;
    }

    /**
     * Returns the player that is GetMousedGameObject. If that is not a player, returns null.
     */
    private Player GetMousedPlayer()
	{
		foreach(Camp camp in this.boardScript.Camps)
		{
			foreach(Player player in camp.TeamPlayers)
				if(player.gameObject == this.GetMousedGameObject())
					return player;
		}
		return null;
	}

    /**
     * Returns the obstacle that is GetMousedGameObject. If that is not a obstacle, returns null.
     */
    private Obstacle GetMousedObstacle()
	{
		foreach(Obstacle obstacle in this.boardScript.Obstacles)
			if(obstacle.gameObject == this.GetMousedGameObject())
				return obstacle;
		return null;
	}
}
