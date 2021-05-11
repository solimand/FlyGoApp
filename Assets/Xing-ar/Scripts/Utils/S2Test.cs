using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Common.Geometry;


public class S2Test : MonoBehaviour
{
    S2RegionCoverer cov;
    const int level = 20;
    const double lat = 44.482672;
    const double lon = 11.375569;
    
    // Start is called before the first frame update
    void Start()
    {
        cov = new S2RegionCoverer();
        //S1Angle s1lat = S1Angle.FromDegrees(lat);
        //S1Angle s1lon = S1Angle.FromDegrees(lon);
        //S2LatLng s2latlon = new S2LatLng(s1lat, s1lon);
                
        S2LatLng s2latlon = S2LatLng.FromDegrees(lat, lon);
        S2CellId cellid = S2CellId.FromLatLng(s2latlon);
        string cellidPrec30 = cellid.ToToken();
        Debug.Log($"I am in S2 Cell id {cellidPrec30} with preciosion {cellid.Level}");
        Debug.Log("exit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
