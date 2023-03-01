using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathNode
{
    private Grid<PathNode> _grid;
    private int _x;
    private int _y;

    public PathNode(Grid<PathNode> grid, int x, int y)
    {
        _grid = grid;
        _x = x;
        _y = y;
    }
}
