using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Besam : MonoBehaviour
{
    public LineRenderer beamPath;
    public List<Transform> points = new List<Transform>();
    public Transform testPointA, testPointB;
    // Start is called before the first frame update
    void Start()
    {
        if(testPointA != null)beamPath.SetPosition(0,testPointA.position);
        if(testPointB != null)beamPath.SetPosition(1,testPointB.position);
    }

    // Update is called once per frame
    void Update()
    {
        if(testPointA != null)beamPath.SetPosition(0,testPointA.position);
        if(testPointB != null)beamPath.SetPosition(1,testPointB.position);
    }
}
