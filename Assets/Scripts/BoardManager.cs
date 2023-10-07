using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{


    private GridElement[,] GridElementComponent;                      //2D array to hold each grid object's element component

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
        EventManager.OnSpawnGrid += SpawnGridElements;
        EventManager.OnPopulateGrid += PopulateGridElement;
        EventManager.onReArrangeColumn += ReArrangeColumn;
        EventManager.onMatchScore += CheckMatch;
    }

    private void OnDisable()
    {
        EventManager.OnInitialzieGrid -= InitializeGrid;
        EventManager.OnAssignNeighbours -= AssignNeighbours;
        EventManager.OnSpawnGrid -= SpawnGridElements;
        EventManager.OnPopulateGrid -= PopulateGridElement;
        EventManager.onReArrangeColumn -= ReArrangeColumn;
        EventManager.onMatchScore -= CheckMatch;

    }
    // Start is called before the first frame update
    void Start()
    {
        EventManager.OnInitialzieGrid?.Invoke();
        EventManager.OnAssignNeighbours?.Invoke();
        EventManager.OnPopulateGrid?.Invoke();
    }


    //INITIALZING MAIN GRID
    private void InitializeGrid()
    {

        GridElementComponent = new GridElement[numberOfColumns, numberOfRows];

        for (int column = 0; column < numberOfColumns; column++)
        {
            for (int row = 0; row < numberOfRows; row++)
            {
                EventManager.OnSpawnGrid?.Invoke(column, row);
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

        //EventManager.OnPopulateGrid?.Invoke();

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


    //ReArrange the column after removing the tile
    //Traverses the only column from where the tiles was removed
    //Gets the position number from onMousClick event in GridElement.cs
    private void ReArrangeColumn(int column,int row)
    {
        for (int i = 0; i < numberOfRows; i++)
        {
            if (GridElementComponent[column, i].upGridElement != null
                && GridElementComponent[column, i].upGridElement.GetHasTile
                && GridElementComponent[column, i].transform.childCount == 0)
            {
                GridElementComponent[column, i].upGridElement.transform.GetChild(0).SetParent(GridElementComponent[column, i].transform);

                //RESET THE LOCAL POSITION OF THE CHILD
                GridElementComponent[column, i].transform.GetChild(0).localPosition = Vector3.zero;
            }
        }

        StartCoroutine(WaitToCheckScore(row));
    }


    private void CheckMatch(int row)
    {
        int matchCount = 1;
        int tempColumnhold = 0;

        for (int c = 0;  c < numberOfColumns;  c++)
        {
            if (GridElementComponent[c, row].RightGridElement != null && 
                GridElementComponent[c, row].GetTileId == GridElementComponent[c, row].RightGridElement.GetTileId)
            {
                matchCount++;
                tempColumnhold = c;
            }
            else
            {
                if (matchCount > 2)
                {
                    do
                    {
                        GridElementComponent[tempColumnhold + 1, row].RemoveTile();
                        tempColumnhold--;
                        matchCount--;
                    } while (matchCount > 0);

                }

                matchCount = 1;
            }
          
        }
    }


    IEnumerator WaitToCheckScore(int row)
    {
        yield return new WaitForFixedUpdate();

        CheckMatch(row);
    }

}
