using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Common.Geometry;


class S2Geofence
{
    private const int DESIRED_LVL = 20;
    //const double latTest = 44.482672;
    //const double lonTest = 11.375569;

    private static ILogger mLogger = Debug.unityLogger;
    private const string kTAG = "";

    public S2Geofence()
    {
        mLogger = new Logger(new MyLogHandler());
        mLogger.Log(kTAG, "Start");
    }

    public string CellIdFromCoord(double lat, double lon)
    {
        S2LatLng s2latlon = S2LatLng.FromDegrees(lat, lon);
        S2CellId cellid = S2CellId.FromLatLng(s2latlon); //prec 30
        S2CellId parentCellid = cellid.ParentForLevel(DESIRED_LVL); //prec desired
        string cellidDesiredPrec = parentCellid.ToToken();
        Debug.Log($"I am in S2 Cell id {cellidDesiredPrec} with preciosion {parentCellid.Level}");
        return cellidDesiredPrec;
    }
}
