using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathFinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;
    private Grid<PathNode> grid;
    private List<PathNode> openList;
    private HashSet<PathNode> closedList;

    public PathFinding(int width, int height, Vector3 spawnPos, Transform parent, float cellCize = 1f)
    {
        grid = new Grid<PathNode>(width, height, cellCize, spawnPos, (Grid<PathNode> g, int x, int y) => new PathNode(g, x, y), parent);
    }

    public Grid<PathNode> Grid => grid;

    public List<PathNode> FindPath(int startX, int startY, int endX, int endY)
    {
        PathNode startNode = grid.GetGridObject(startX, startY);
        PathNode endNode = grid.GetGridObject(endX, endY);

        openList = new List<PathNode>() { startNode };
        closedList = new HashSet<PathNode>();

        for (int x = 0; x < grid.Width; x++)
        {
            for (int y = 0; y < grid.Height; y++)
            {
                PathNode pathNode = grid.GetGridObject(x, y);
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }

        if (startNode == null || endNode == null)
            return null;

        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            PathNode currentNode = GetLowestFCostNode(openList);
            if (currentNode == endNode)
            {
                return CalculatePath(endNode);
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach (PathNode neighbourNode in GetNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (neighbourNode.IsWalkable == false)
                {
                    closedList.Add(currentNode);
                    continue;
                }

                int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbourNode);
                if (tentativeGCost < neighbourNode.gCost)
                {
                    neighbourNode.cameFromNode = currentNode;
                    neighbourNode.gCost = tentativeGCost;
                    neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                    neighbourNode.CalculateFCost();

                    if (!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        // out of nodes on the open list
        return null;
    }

    public PathNode GetNode(int x, int y)
    {
        return grid.GetGridObject(x, y);
    }

    private List<PathNode> GetNeighboursList(PathNode currentNode)
    {
        List<PathNode> neighboursList = new List<PathNode>();

        if (currentNode.x - 1 >= 0)
        {
            // Left
            neighboursList.Add(GetNode(currentNode.x - 1, currentNode.y));

            // left down
            if (currentNode.y - 1 >= 0) neighboursList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));

            //left up
            if (currentNode.y + 1 < grid.Height) neighboursList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
        }

        if (currentNode.x + 1 < grid.Width)
        {
            //Right
            neighboursList.Add(GetNode(currentNode.x + 1, currentNode.y));

            //right down
            if (currentNode.y - 1 >= 0) neighboursList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));

            //right up
            if (currentNode.y + 1 < grid.Height) neighboursList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
        }

        //Down
        if (currentNode.y - 1 >= 0) neighboursList.Add(GetNode(currentNode.x, currentNode.y - 1));

        //Up
        if (currentNode.y + 1 < grid.Height) neighboursList.Add(GetNode(currentNode.x, currentNode.y + 1));

        return neighboursList;
    }

    private List<PathNode> CalculatePath(PathNode endNode)
    {
        List<PathNode> path = new List<PathNode>();
        path.Add(endNode);
        PathNode currentNode = endNode;
        while (currentNode.cameFromNode != null)
        {
            path.Add(currentNode.cameFromNode);
            currentNode = currentNode.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(PathNode a, PathNode b)
    {
        int xDistance = Mathf.Abs(a.x - b.x);
        int yDistance = Mathf.Abs(a.y - b.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private PathNode GetLowestFCostNode(List<PathNode> pathNodes)
    {
        PathNode lowestFCostNode = pathNodes[0];

        for (int i = 1; i < pathNodes.Count; i++)
        {
            if (pathNodes[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodes[i];
            }
        }

        return lowestFCostNode;
    }
}
