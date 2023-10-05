using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
  

    private int randomNum;

    public GridElement RightGridElement;

    public GridElement downGridElement;

    public GridElement upGridElement;


    public void SpawnTile(List<GameObject> Tile)
    {
        randomNum = Random.Range(0, 4);
        Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);

        ////CONTINUE IF IT DOESNT HAVE ANY TILE
        //if (!hasTile)
        //{

        //    randomNum = Random.Range(0, 4);
        //    Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);

        //    hasTile = true;

        //    SetID();
        //}

        //// REPLACE THE TILE WITH CLICKABLE OBJECT 
        //else
        //{

        //    RemoveTile();
        //    randomNum = Random.Range(0, 2);
        //    Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);

        //    hasTile = true;
        //    StartCoroutine(WaitAndGetId());

        //}
    }
}
