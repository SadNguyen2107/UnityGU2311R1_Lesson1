using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Path", menuName = "Path")]
public class PathSO : ScriptableObject
{
    [SerializeField] Transform _path;

    public IEnumerator<Transform> GetWaypoints()
    {
        for (int waypoint_index = 0; waypoint_index < _path.childCount; waypoint_index++)
        {
            yield return _path.GetChild(waypoint_index);
        }
    }
}
