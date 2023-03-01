using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGrid : MonoBehaviour
{
    Grid<HeatMapGridObject> grid;
    void Start()
    {
        grid = new Grid<HeatMapGridObject>(4, 4, 2f, new Vector3(-3, -3), (Grid<HeatMapGridObject> g, int x, int y) => new HeatMapGridObject(g, x, y));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //grid.SetValue(UtilsClass.GetMouseWorldPosition(), true);

            HeatMapGridObject heatMapGridObject = grid.GetGridObject(UtilsClass.GetMouseWorldPosition());

            if (heatMapGridObject != null)
            {
                heatMapGridObject.AddValue(5);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(grid.GetGridObject(UtilsClass.GetMouseWorldPosition()));
        }
    }
}
