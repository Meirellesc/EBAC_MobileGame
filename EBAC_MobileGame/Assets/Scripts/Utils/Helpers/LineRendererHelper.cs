using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LineRendererHelper : MonoBehaviour
{
    public LineRenderer LineRenderer;

    public List<Transform> Positions;

    void Start()
    {
        LineRenderer.positionCount = Positions.Count;
    }

    void Update()
    {
        LineRenderer.SetPositions(Positions.Select(data => data.position).ToArray());
    }
}
