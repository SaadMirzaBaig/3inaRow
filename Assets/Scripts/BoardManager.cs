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


    [SerializeField] private List<GameObject> Tiles;                    //Clickable and non clickable objects


    private void OnEnable()
    {
        EventManager.OnInitialzieGrid += InitializeGrid;
        EventManager.OnAssignNeighbours += AssignNeighbours;
        EventManager.OnSpawnTiles += SpawnGridElements;
        EventManager.OnPopulateGrid += PopulateGridElement;
    }

    private void OnDisable()
    {
        EventManager.OnInitialzieGrid -= InitializeGrid;
        EventManager.OnAssignNeighbours -= AssignNeighbours;
        EventManager.OnSpawnTiles -= SpawnGridElements;
        EventManager.OnPopulateGrid -= PopulateGridElement;

    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnInitialzieGrid?.Invoke();

    }


    //INITIALZING MAIN GRID
    private void InitializeGrid()
    {

        GridElementComponent = new GridElement[numberOfColumns, numberOfRows];

        for (int column = 0; column < numberOfColumns; column++)
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                EventManager.OnSpawnTiles?.Invoke(column, row);
            }
        }

        EventManager.OnAssignNeighbours?.Invoke();

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

    private void AssignNeighbours()
    {


        for (int column = 0; column < numberOfColumns; column++)
        {
            for (int row = 0; row < numberOfRows; row++)
            {

                //track neighbour below

                if (row != 0)
                {
                    GridElementComponent[column, row].downGridElement = GridElementComponent[column, row - 1];
                }

                //keep track of its neigbour to right
                if ((column) != numberOfColumns - 1)
                {
                    GridElementComponent[column, row].RightGridElement = GridElementComponent[column + 1, row];
                }

                //keep track of its neigbour to left
                if ((column) > 0)
                {
                    GridElementComponent[column, row].LeftGridElement = GridElementComponent[column - 1, row];
                }

                //keep track of its neighbour above
                if (row != numberOfRows - 1)
                {
                    GridElementComponent[column, row].upGridElement = GridElementComponent[column, row + 1];
                }
            }

        }

        EventManager.OnPopulateGrid?.Invoke();

    }

    //Populate the each grid element with its neiboughr's information
    private void PopulateGridElement()
    {

        //Spawning objects from DOWN to UP

        for (int column = 0; column < numberOfColumns; column++)
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                
                GridElementComponent[column, row].SpawnTile(Tiles); //SPAWN THE TILE FOR EACH GRID ELEMENT
            }

        }

    }

   

}
