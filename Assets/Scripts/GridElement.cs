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

    //On mouse click event
    //Invokes method to remove tile
    private void OnMouseDown ()
    {
        RemoveTile();
    }


    //Remove tiles from child
    private void RemoveTile()
    {
        if (transform.childCount > 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            StartCoroutine(WaitToArrangeTile());
        }
    }

    void SwapTile()
    {
        if (upGridElement != null && upGridElement.transform.childCount > 0)
        {
            upGridElement.transform.GetChild(0).SetParent(transform);

            //RESET THE LOCAL POSITION OF THE CHILD
            transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
            Debug.Log(transform.GetChild(0).localPosition);
        }
    }


    IEnumerator WaitToArrangeTile()
    {

        yield return new WaitForSecondsRealtime(0.2f);


        if (upGridElement != null && upGridElement.transform.childCount > 0)
        {
            upGridElement.transform.GetChild(0).SetParent(transform);

            //RESET THE LOCAL POSITION OF THE CHILD
            transform.GetChild(0).localPosition = new Vector3(0, 0, 0);
        }
    }
}
