using System.Collections.Generic;
using UnityEngine;
using Google.Common.Geometry;
using System.Linq; //for ToArray()

class S2Geofence
{
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "S2Geofence";
    private const int MIN_CELL_SIZE = 15;
    private const int MAX_CELL_SIZE = 19;

    public S2Geofence()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");        
    }
        

    public static string CellIdFromCoord(double lat, double lon, int level)
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

    // get cells from 15 to 19 (if one of these is a geofence of a GO, instantiate it)
    public string[] AmIinGeoFence(double lat, double lon)
    {
        string[] result = null;
        int count = 0;
        for(int i=MIN_CELL_SIZE; i<MAX_CELL_SIZE; i++)
        {
            string currCell = CellIdFromCoord(lat, lon, i);
            result[count] = currCell;
            count++;
        }
        return result;
    }
}

