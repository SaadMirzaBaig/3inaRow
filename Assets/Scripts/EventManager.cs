using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnInitialzieGrid { get; set; }
    public static Action OnAssignNeighbours { get; set; }

    public static Action<int, int> OnSpawnGrid { get; set; }
    public static Action OnPopulateGrid { get; set; }


}


