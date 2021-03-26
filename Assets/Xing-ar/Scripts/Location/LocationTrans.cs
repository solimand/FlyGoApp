using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationTrans
{

    private Vector2 _localOrigin = Vector2.zero;
    private float _LatOrigin { get { return _localOrigin.x; } }
    private float _LonOrigin { get { return _localOrigin.y; } }
    private float metersPerLat;
    private float metersPerLon;
    
    // TODO thread-safe singleton
    #region Singleton
    private static LocationTrans _singleton;

    private LocationTrans()
    {

    }

    public static LocationTrans GetInstance()
    {
        if (_singleton == null)
        {
            _singleton = new LocationTrans();
        }
        return _singleton;
    }
    #endregion

    #region Public Methods
    /**From UCS (X,Y,Z) to GPS (Lat, Lon)*/
    public static Vector2 UCSToGPS(Vector3 position)
    {
        return GetInstance().ConvertUCStoGPS(position);
    }

    /**From GPS (Lat, Lon) to UCS (X,Y,Z)*/
    public static Vector3 GPSToUCS(Vector2 gps)
    {
        return GetInstance().ConvertGPStoUCS(gps);
    }

    /**From GPS (Lat, Lon) to UCS (X,Y,Z)*/
    public static Vector3 GPSToUCS(float latitude, float longitude)
    {
        return GetInstance().ConvertGPStoUCS(new Vector2(latitude, longitude));
    }

    /**Change the relative GPS offset (Lat, Lon), Default (0,0), 
     * used to bring a local area to (0,0,0) in UCS coordinate system */
    public static void SetLocalOrigin(Vector2 localOrigin)
    {
        GetInstance()._localOrigin = localOrigin;
    }
    #endregion

    #region PrivFunctions
    private void FindMetersPerLat(float lat) // Compute lengths of degrees
    {
        float m1 = 111132.92f;    // latitude calculation term 1
        float m2 = -559.82f;        // latitude calculation term 2
        float m3 = 1.175f;      // latitude calculation term 3
        float m4 = -0.0023f;        // latitude calculation term 4
        float p1 = 111412.84f;    // longitude calculation term 1
        float p2 = -93.5f;      // longitude calculation term 2
        float p3 = 0.118f;      // longitude calculation term 3

        lat = lat * Mathf.Deg2Rad;

        // Calculate the length of a degree of latitude and longitude in meters
        metersPerLat = m1 + (m2 * Mathf.Cos(2 * (float)lat)) + (m3 * Mathf.Cos(4 * (float)lat)) + (m4 * Mathf.Cos(6 * (float)lat));
        metersPerLon = (p1 * Mathf.Cos((float)lat)) + (p2 * Mathf.Cos(3 * (float)lat)) + (p3 * Mathf.Cos(5 * (float)lat));
    }

    private Vector3 ConvertGPStoUCS(Vector2 gps)
    {
        FindMetersPerLat(_LatOrigin);
        float zPosition = metersPerLat * (gps.x - _LatOrigin); //Calc current lat
        float xPosition = metersPerLon * (gps.y - _LonOrigin); //Calc current lat
        return new Vector3((float)xPosition, 0, (float)zPosition);
    }

    private Vector2 ConvertUCStoGPS(Vector3 position)
    {
        FindMetersPerLat(_LatOrigin);
        Vector2 geoLocation = new Vector2(0, 0);
        geoLocation.x = (_LatOrigin + (position.z) / metersPerLat); //Calc current lat
        geoLocation.y = (_LonOrigin + (position.x) / metersPerLon); //Calc current lon
        return geoLocation;
    }
    #endregion

    #region ToTest
    /*
    // TODO to test 
    void SpawnObject()
    {
        // Real world position of object. Need to update with something near your own location.
        float latitude = -27.469093;
        float longitude = 153.023394;

        // Conversion factors
        float degreesLatitudeInMeters = 111132;
        float degreesLongitudeInMetersAtEquator = 111319.9f;

        // Real GPS Position - This will be the world origin.
        var gpsLat = GPSManager.Instance.latitude;
        var gpsLon = GPSManager.Instance.longitude;
        // GPS position converted into unity coordinates
        var latOffset = (latitude - gpsLat) * degreesLatitudeInMeters;
        var lonOffset = (longitude - gpsLon) * 
            degreesLongitudeInMetersAtEquator * Mathf.Cos(latitude * (Mathf.PI / 180)); ;

        // Create object at coordinates
        var obj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        obj.transform.position = new Vector3(latOffset, 0, lonOffset);
        obj.transform.localScale = new Vector3(4, 4, 4);
    }
    */
    #endregion
}
