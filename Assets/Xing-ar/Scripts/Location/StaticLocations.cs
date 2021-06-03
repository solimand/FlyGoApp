using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StaticLocations{
    // Allowed Geofence Areas
    public static string[] fixedPos2mt = new string[3] { "477fd4ee07c", "477e2b398ac", "477e2b3a1bc" };  
    
    // TEST
    public const float alberoLat = 44.48744696f; public const float alberoLon = 11.32981088f;
    public const string alberoCell19 = "477fd4ee07c";

    // ARCIERE    
    public const float ArciereLat = 44.499365736248f; public const float ArciereLon = 11.2823648750782f;
    public const string ArciereCell19 = "477e2b3a1bc";
    public const int ArciereDist = 20;
    public const int ArciereAlt = 0;
    public const bool ArciereFront = true;
    
    // CHIMERA
    public const float ChimeraLat = 44.5008017449531f; public const float ChimeraLon = 11.2838089093566f;
    public const string ChimeraCell19 = "___";
    public const int ChimeraDist = 15;
    public const int ChimeraAlt = 0;
    public const bool ChimeraFront = true;
    
    // SCAGLIA PIETRA (ISSUE = same position of chimera)
    public const float ScagliaPietraLat = 44.5008017449531f; public const float ScagliaPietraLon = 11.2838089093566f;
    public const string ScagliaPietraCell19 = "___";
    public const int ScagliaPietraDist = 10;
    public const int ScagliaPietraAlt = 3;
    public const bool ScagliaPietraFront = true;
    
    // PIETRE FORATE
    public const float PietreForateLat = 44.5006762002241f; public const float PietreForateLon = 11.2847795337439f;
    public const string PietreForateCell19 = "___";
    public const int PietreForateDist = 30;
    public const int PietreForateAlt = 10;
    public const bool PietreForateFront = false;
    
    // ALBERO
    public const float AlberoMuscolosoLat = 44.5023606292688f; public const float AlberoMuscolosoLon = 11.2877142056823f;
    public const string AlberoMuscolosoCell19 = "___";
    public const int AlberoMuscolosoDist = 2;
    public const int AlberoMuscolosoAlt = 0;
    public const bool AlberoMuscolosoFront = false;
    
    // SCAGLIA PIETRA 2
    public const float ScagliaPietra2Lat = 44.504700192729f; public const float ScagliaPietra2Lon = 11.2899675965309f;
    public const string ScagliaPietra2Cell19 = "___";
    public const int ScagliaPietra2Dist = 10;
    public const int ScagliaPietra2Alt = 3;
    public const bool ScagliaPietra2Front = true;
    
    // MEDUSA
    public const float MedusaLat = 44.5047288867109f; public const float MedusaLon = 11.2908041104674f;
    public const string MedusaCell19 = "___";
    public const int MedusaDist = 10;
    public const int MedusaAlt = 3;
    public const bool MedusaFront = false;
}
