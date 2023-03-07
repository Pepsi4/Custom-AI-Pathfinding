using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPathfindingMovement
{
    void SetPathVectorList(List<Vector3> list);
    Vector2 GetPosition();
}
