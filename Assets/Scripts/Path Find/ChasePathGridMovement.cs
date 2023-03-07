using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChasePathGridMovement : VelocityMovement
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _updatePathDelaySecond = 1f;
    private PathFinding _pathFinding;
    [SerializeField] private GridPathfindingController _testPathFinding;

    List<PathNode> _path;
    private List<Vector3> MovementList;

    private IEnumerator UpdatePathTo(Transform target)
    {
        _currentPathIndex = 0;
        MovementList = new List<Vector3>();
        _pathFinding.Grid.GetXY(target.position, out int x, out int y);

        if (_path != null)
        {
            _path = _pathFinding.FindPath(_pathFinding.Grid.GetGridObject(transform.localPosition).x, _pathFinding.Grid.GetGridObject(transform.localPosition).y, x, y);
        }
        else
        {
            _path = _pathFinding.FindPath(0, 0, x, y);
        }

        if (_path != null)
        {
            for (int i = 0; i < _path.Count; i++)
            {
                if (_pathFinding.Grid.GetGridObject(this.transform.position) != _path[i])
                    MovementList.Add(new Vector3(_path[i].x * _pathFinding.Grid.CellSize + _pathFinding.Grid.OriginPosition.x + _pathFinding.Grid.CellSize * .5f,
                            _path[i].y * _pathFinding.Grid.CellSize + _pathFinding.Grid.OriginPosition.y + _pathFinding.Grid.CellSize * .5f));
            }
        }
        yield return new WaitForSeconds(_updatePathDelaySecond);
        StartCoroutine(UpdatePathTo(_target));
    }

    public int _currentPathIndex = 0;
    public override void Move()
    {
        if (MovementList != null)
        {
            if (MovementList.Count > 0 && Vector2.Distance(this.transform.position, MovementList[_currentPathIndex]) > 0.05f)  //error argument out of range
            {
                MoveVector = (MovementList[_currentPathIndex] - this.transform.position).normalized;
                _rigidbody2D.velocity = MoveVector * Time.fixedDeltaTime * _speed;
            }
            else
            {
                _currentPathIndex++;
                if (_currentPathIndex >= MovementList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    private void StopMoving()
    {
        _currentPathIndex = 0;
        MoveVector = Vector2.zero;
        MovementList = null;
        _rigidbody2D.velocity = Vector2.zero;
    }

    private void Start()
    {
        _pathFinding = _testPathFinding._pathFinding;
        StartCoroutine(UpdatePathTo(_target));
    }
}
