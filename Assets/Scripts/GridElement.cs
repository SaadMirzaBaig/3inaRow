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

            } while (leftTile.GetComponent<Tile>().id == randomNum);

            Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);

        }
        else
        {
            randomNum = Random.Range(0, 4);
            Instantiate(Tile[randomNum], transform.position, Quaternion.identity, transform);
        }

    }

    public int GetTileId
    {
        get
        {
            if (GetHasTile)
            {
                return transform.GetComponentInChildren<Tile>().id;

            }
            else
            {
                return -1;
            }

        }
    }
    
    public bool GetHasTile
    {
        get { return transform.childCount > 0; }
        set {; }
    }

    //On mouse click event
    //Invokes method to remove tile
    private void OnMouseDown ()
    {

        RemoveTile();
    }


    //Remove tiles from child
    public void RemoveTile()
    {
        //if the grid has a child
        if (GetHasTile)
        {
            //destroy the tile
            Destroy(transform.GetChild(0).gameObject);
            GetHasTile = false;

            //Wait to Rearrange the column after tile got removed
            StartCoroutine(WaitToReArrangeColumn());


        }
    }

    IEnumerator WaitToReArrangeColumn()
    {

        yield return new WaitForSecondsRealtime(0.2f);

        EventManager.onReArrangeColumn?.Invoke((int)transform.position.x, (int)transform.position.y);

    }

}
