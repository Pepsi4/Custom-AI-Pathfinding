using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GridPathfindingController : MonoBehaviour
{
    public PathFinding _pathFinding;

    [SerializeField] private bool _isDebug = false;
    [SerializeField] private Color PathSuccessfulColor;
    [SerializeField] private Color NoPathColor;
    [SerializeField] private Color DefaultPathColor;
    [SerializeField] private GridPathfindingMovement _pathfindingMovement;
    [SerializeField] private MeshFactory _meshFactory;

    private PathNode _nodeToRemove;
    private List<PathNode> _path;

    void Awake()
    {
        _pathFinding = new PathFinding(10, 10, new Vector2(-5, -5), this.transform, 1f);
    }

    private void CreatePath(Vector3 endPos)
    {
        _pathFinding.Grid.GetXY(endPos, out int x, out int y);

        if (_path != null && _pathfindingMovement != null)
        {
            _pathFinding.Grid.SetListDebugTextColor(DefaultPathColor, _pathFinding.Grid.DebugTextArray, _path);
            try { _path = _pathFinding.FindPath(_pathFinding.Grid.GetGridObject(_pathfindingMovement.GetPosition()).x, _pathFinding.Grid.GetGridObject(_pathfindingMovement.GetPosition()).y, x, y); }
            catch (System.NullReferenceException) { Debug.Log("<color=orange> Target is not on a walkable grid. </color>"); }
        }
        else
        {
            _path = _pathFinding.FindPath(0, 0, x, y);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CreatePath(UtilsClass.GetMouseWorldPosition());

            if (_path != null)
            {
                List<Vector3> playerMovementList = new List<Vector3>();
                for (int i = 0; i < _path.Count; i++)
                {
                    if (_isDebug)
                        Debug.DrawLine(new Vector3(_path[i].x + _pathFinding.Grid.OriginPosition.x, _path[i].y + _pathFinding.Grid.OriginPosition.y) * _pathFinding.Grid.CellSize + Vector3.one * .5f * _pathFinding.Grid.CellSize,
                            new Vector3(_path[i + 1].x + _pathFinding.Grid.OriginPosition.x, _path[i + 1].y + _pathFinding.Grid.OriginPosition.y) * _pathFinding.Grid.CellSize + Vector3.one * .5f * _pathFinding.Grid.CellSize,
                            Color.green, 1000f);


                    playerMovementList.Add(new Vector3(_path[i].x * _pathFinding.Grid.CellSize + _pathFinding.Grid.OriginPosition.x + _pathFinding.Grid.CellSize * .5f,
                            _path[i].y * _pathFinding.Grid.CellSize + _pathFinding.Grid.OriginPosition.y + _pathFinding.Grid.CellSize * .5f));
                }
                playerMovementList.RemoveRange(0, 1);
                _pathfindingMovement.SetPathVectorList(playerMovementList);
                // _pathFinding.Grid.SetListDebugTextColor(PathSuccessfulColor, _pathFinding.Grid.DebugTextArray, _path);
            }
        }

        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = UtilsClass.GetMouseWorldPosition();

            _pathFinding.Grid.GetXY(mouseWorldPos, out int x, out int y);
            bool isWalkable;
            try
            {
                isWalkable = _pathFinding.GetNode(x, y).IsWalkable;
            }
            catch (System.NullReferenceException) { Debug.Log("<color=orange> Clicked not on the grid! </color>"); return; }

            _pathFinding.GetNode(x, y).IsWalkable = !isWalkable;

            if (isWalkable)
            {
                _pathFinding.Grid.SetDebugTextColor(NoPathColor, x, y);
                try { _nodeToRemove = _path.FirstOrDefault(node => node.Equals(_pathFinding.GetNode(x, y))); }
                catch (System.ArgumentNullException) { }

               // _meshFactory.CreateSquare(_pathFinding.Grid.GetWorldPosition(x, y) + Vector3.back, new Vector3(_pathFinding.Grid.CellSize, _pathFinding.Grid.CellSize));

                if (_nodeToRemove != null)
                {
                    _path.Remove(_nodeToRemove);
                }
            }
            else
                _pathFinding.Grid.SetDebugTextColor(DefaultPathColor, x, y);
        }
    }
}

