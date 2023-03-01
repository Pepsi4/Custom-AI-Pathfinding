using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour, IRoaming
{

    private Vector2 _startPos;
    // Start is called before the first frame update
    void Start()
    {
        _startPos = this.transform.position;
    }

    public Vector2 GetRoamingPosition()
    {
        return _startPos + UtilsClass.GetRandomDirection() * Random.Range(10f, 70f);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
