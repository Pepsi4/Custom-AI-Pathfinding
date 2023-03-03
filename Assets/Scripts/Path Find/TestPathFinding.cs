using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestPathFinding : MonoBehaviour
{
    private PathFinding _pathFinding;
    List<PathNode> _path;
    private bool _isDebug = false;

    [SerializeField] Color PathSuccessfulColor;
    [SerializeField] Color NoPathColor;
    [SerializeField] Color DefaultPathColor;

    // Start is called before the first frame update
    void Start()
    {
        _pathFinding = new PathFinding(10, 10, new Vector2(-5, -5), this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPos = UtilsClass.GetMouseWorldPosition();
            _pathFinding.Grid.GetXY(mouseWorldPos, out int x, out int y);

            if (_path != null)
                _pathFinding.Grid.SetListDebugTextColor(DefaultPathColor, _pathFinding.Grid.DebugTextArray, _path);

            _path = _pathFinding.FindPath(0, 0, x, y);



            if (_path != null)
            {
                for (int i = 0; i < _path.Count - 1; i++)
                {

                    //Debug.Log(x + "," + y);
                    if (_isDebug)
                        Debug.DrawLine(new Vector3(_path[i].x + _pathFinding.Grid.OriginPosition.x, _path[i].y + _pathFinding.Grid.OriginPosition.y) * _pathFinding.Grid.CellSize + Vector3.one * .5f * _pathFinding.Grid.CellSize,
                            new Vector3(_path[i + 1].x + _pathFinding.Grid.OriginPosition.x, _path[i + 1].y + _pathFinding.Grid.OriginPosition.y) * _pathFinding.Grid.CellSize + Vector3.one * .5f * _pathFinding.Grid.CellSize,
                            Color.green, 1000f);

                    _pathFinding.Grid.SetListDebugTextColor(PathSuccessfulColor, _pathFinding.Grid.DebugTextArray, _path);
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 mouseWorldPos = UtilsClass.GetMouseWorldPosition();

            _pathFinding.Grid.GetXY(mouseWorldPos, out int x, out int y);

            bool isWalkable = _pathFinding.GetNode(x, y).IsWalkable;
            _pathFinding.GetNode(x, y).IsWalkable = !isWalkable;

            if (isWalkable)
            {
                _pathFinding.Grid.SetDebugTextColor(NoPathColor, x, y);
                var nodeToRemove = _path.FirstOrDefault(node => node.Equals(_pathFinding.GetNode(x, y)));
                if (nodeToRemove != null)
                {
                    Debug.Log(nodeToRemove);
                    _path.Remove(nodeToRemove);
                }
            }

            else

                _pathFinding.Grid.SetDebugTextColor(DefaultPathColor, x, y);




        }
    }
}
