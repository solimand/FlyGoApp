using System.Collections.Generic;
using UnityEngine;
using Google.Common.Geometry;
using System.Linq; //for ToArray()


class S2Geofence
{
    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "S2Geofence";

    //const double latTest = 44.482672;
    //const double lonTest = 11.375569;
    Dictionary<string, string[]> geofenceAreas = new Dictionary<string, string[]>();
    string[] keys;
    //string[][] values;

    // S2 GEOFENCING DATA
    string [] fixedPos = new string[2] { "477e2b39f45", "477e2b3a1b9" };
    //string[] fixedPos2mt = new string[6] { "477e2b3a1c1", "477e2b3a1bf", "477e2b3a1eb","477e2b3a1ed", "477e2b3a195", "477e2b3a193" };
    string[] fixedPos2mt = new string[3] { "477e2b39f44", "477e2b398ac", "477e2b3a1bc" };
    string[,] geoFencesMatrix = new string[2, 8] {
        {"477e2b39f41", "477e2b39f43", "477e2b39f47",
            "477e2b39f49", "477e2b39f4f", "477e2b39f51", "477e2b39f5b", "477e2b39f5d"},        
        { "477e2b3a1b1", "477e2b3a1b7", "477e2b3a1bb",
            "477e2b3a1bd", "477e2b3a1bf", "477e2b3a1c1", "477e2b3a1c7", "477e2b3a1c9"}
    };

    
    //TODO change geofence from dictionary to struct
    private struct GeofenceRow
    {
        string charName;
        string geofencePlace;
        string[] geofenceNeighbourhood;
    }
    private GeofenceRow[] locations;

    

    public S2Geofence()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
        int i = 0;

        //writing geofenceAreas Dictionary
        foreach (string currPos in fixedPos)
        {
            string[] value = Enumerable.Range(0, geoFencesMatrix.GetLength(1))
                .Select(x => geoFencesMatrix[i, x])
                .ToArray();
            geofenceAreas.Add(currPos, value);
            i++;
        }


        //writing all keys/val in array to future checks
        keys = geofenceAreas.Keys.ToArray();
        
        /* DEBUG TEST Dictionary
        values = geofenceAreas.Values.ToArray();
        foreach (string curFixedPos in keys)
            Debug.Log($"dictionary fixedPos pos {curFixedPos}");
        foreach (string[] currValues in values)
            foreach (string currValue in currValues)
                Debug.Log($"dictionary geofence {currValue}");
        */
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

    public string AmIinCellId(double lat, double lon, int level)
    {
        string currCell = CellIdFromCoord(lat, lon, level);
        if (fixedPos2mt.Contains(currCell))
        {
            //Debug.Log($"You are in the cellid {currCell}");
            return currCell;
        }
        return "N";
    }
}

