using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> players;
    private float _offset_z;
    void Start()
    {
        _offset_z = transform.position.z;
    }

    void LateUpdate()
    {
        Vector3 middle = new Vector3();
        float maxR = 0;
        for (int i = 0; i < players.Count; ++i)
        {
            if (i > 0) maxR = Mathf.Max(maxR, (players[i].position - players[i - 1].position).magnitude);
            middle += players[i].position;
        }
        if (players.Count > 0) transform.position = new Vector3(middle.x / players.Count, middle.y / players.Count, _offset_z - maxR * 0.75f);
    }
}
