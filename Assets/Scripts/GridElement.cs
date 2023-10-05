using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridElement : MonoBehaviour
{
  
    private int randomNum;

    public GridElement upGridElement;

    public GridElement RightGridElement;
    public GridElement LeftGridElement;

    public GridElement downGridElement;




    public void SpawnTile(List<GameObject> Tile)
    {
       
        // Avoid getting same tile in a row at the begining
        // check if the previous tile has the same value
        // get random value untill its different from the previous one
        // else spawn the tile
        if (LeftGridElement != null)
        {
            Transform leftTile = LeftGridElement.transform.GetChild(0);

            do
            {
                randomNum = Random.Range(0, 4);

            } while (leftTile.GetComponent<Tile>().id != randomNum);

            Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);

        }
        else
        {
            randomNum = Random.Range(0, 4);
            Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);
        }


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
