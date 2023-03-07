using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPathfindingMovement : VelocityMovement, IPathfindingMovement
{
    public int _currentPathIndex = 0;
    private List<Vector3> _pathVectorList { get; set; }
    //public Vector2 MoveVector { get; set; }
    public Vector2 GetPosition() => this.transform.position;

    public void SetPathVectorList(List<Vector3> list)
    {
        _pathVectorList = new List<Vector3>();

        _currentPathIndex = 0;
        _pathVectorList = list;
    }

    private void StopMoving()
    {
        Debug.Log("Stop Moving!");
        MoveVector = Vector2.zero;
        _pathVectorList = null;
        _rigidbody2D.velocity = Vector2.zero;
    }

    public override void Move()
    {
        if (_pathVectorList != null)
        {
            if (Vector2.Distance(this.transform.position, _pathVectorList[_currentPathIndex]) > 0.05f)
            {
                MoveVector = (_pathVectorList[_currentPathIndex] - this.transform.position).normalized;
                _rigidbody2D.velocity = MoveVector * Time.fixedDeltaTime * _speed;
            }
            else
            {
                _currentPathIndex++;
                if (_currentPathIndex >= _pathVectorList.Count)
                {
                    StopMoving();
                }
            }
        }
    }

    public void SetTargetPosition(Vector3 targetPos)
    {
        _currentPathIndex = 0;

        if (_pathVectorList != null && _pathVectorList.Count > 1)
        {
            _pathVectorList.RemoveAt(0);
        }
    }
}
