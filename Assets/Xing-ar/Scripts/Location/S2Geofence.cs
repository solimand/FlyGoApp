using System.Collections.Generic;
using UnityEngine;
using Google.Common.Geometry;
using System.Linq; //for ToArray()


class S2Geofence
{
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "S2Geofence";

    public S2Geofence()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");        
    }
        

    public string CellIdFromCoord(double lat, double lon, int level)
    {
        S2LatLng s2latlon = S2LatLng.FromDegrees(lat, lon);
        S2CellId cellid = S2CellId.FromLatLng(s2latlon); //prec 30
        S2CellId parentCellid = cellid.ParentForLevel(level); //prec desired
        string cellidDesiredPrec = parentCellid.ToToken();
        //Debug.Log($"I am in S2 Cell id {cellidDesiredPrec} with preciosion {parentCellid.Level}");
        return cellidDesiredPrec;
    }

    public string AmIinCellId(double lat, double lon, int level)
    {
        string currCell = CellIdFromCoord(lat, lon, level);
        if (StaticLocations.fixedPos2mt.Contains(currCell))
        {
            //Debug.Log($"You are in the cellid {currCell}");
            return currCell;
        }
        //Debug.Log($"You are in the cellid {currCell}");
        return "N";
    }

    /*
    public bool AmIinGeofence(double lat, double lon, int level)
    {
        string currCell = CellIdFromCoord(lat, lon, level);
        string[] currGeofences;

        //accedi array con ogni fixced pos e ricevi array geofence,
        //if sono dentro uno di questi visualizzo lanim
        foreach (string currFixedPos in fixedPos)
        {
            currGeofences = geofenceAreas[currFixedPos];
            foreach(string currCellGeofence in currGeofences)
            {
                if (currCell == currCellGeofence)
                {
                    Debug.Log($"I am in geofence cellid {currCellGeofence} " +
                        $"and I can see object at {currFixedPos}");
                    return true;
                }
            }
        }
        return false;
    }
    */
}

