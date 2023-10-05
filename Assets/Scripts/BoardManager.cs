using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{


    private GridElement[,] GridElementComponent;                        //2D array to hold each grid object's element component

    private GameObject gridElement;

    [SerializeField] private GameObject Grid;                           //Main parent of grid

    [Range(3, 9)]
    [SerializeField] private int numberOfRows;

    [Range(3, 9)]
    [SerializeField] private int numberOfColumns;

    // Start is called before the first frame update
    void Start()
    {
        InitializeGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //INITIALZING MAIN GRID
    private void InitializeGrid()
    {

        GridElementComponent = new GridElement[numberOfColumns, numberOfRows];

        for (int column = 0; column < numberOfColumns; column++)
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                SpawnGridElements(column, row);
            }
        }


    }

    //SPAWNING EMPTY GRID ELEMENTS
    private void SpawnGridElements(int x, int y)
    {

        gridElement = new GameObject("x: " + x + "y: " + y);    //naming with respect to x and y position for readablity
        gridElement.transform.position = new Vector3(x, y);     //setting the positions

        gridElement.transform.SetParent(Grid.transform);        //Orgainizing Hierarchy

        gridElement.AddComponent<GridElement>();        // attaching Element properties with each grid element
        gridElement.AddComponent<BoxCollider2D>();      // adding collider for on click detection

        GridElementComponent[x, y] = gridElement.GetComponent<GridElement>();
    }
}
