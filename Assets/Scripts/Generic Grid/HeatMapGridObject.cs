using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMapGridObject
{
    private const int MIN = 0;
    private const int MAX = 100;
    public int Value { get; set; }


    private Grid<HeatMapGridObject> grid;
    private int value;
    private int x;
    private int y;


    public HeatMapGridObject(Grid<HeatMapGridObject> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void AddValue(int addValue)
    {
        Value += addValue;
        Mathf.Clamp(Value, MIN, MAX);
        grid.TriggerGridObjectChanged(x, y);
    }

    public float GetValueNormalized()
    {
        return (float)Value / MAX;
    }

    public override string ToString()
    {
        return Value.ToString();
    }

}
