using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StaticLocations
{
    // Allowed Geofence Single Cells
    public static string[] fixedPos2mt = new string[14] {
        "477e2b3a19", "477e2b3a1f",
        "","","","",
        "","","","",
        "","","",""
    };

    // TEST 44.48256081468141, 11.375324649466517 (18-477e2b3a19) 
        // 44.482987336249174, 11.375652415964774 (18-477e2b3a1f)
    public const float testgo1Lat = 44.48256081468141f; public const float testgo1Lon = 11.375324649466517f;
    public const string testgo1Cell18 = "477e2b3a19";
    public const float testgo1Alt = 0f; public const int testgo1Dist = 3;
    public const float testgo2Lat = 44.482987336249174f; public const float testgo2Lon = 11.375652415964774f;
    public const string testgo2Cell18 = "477e2b3a1f";
    public const float testgo2Alt = 3f; public const int testgo2Dist = 15;

    // ARCIERE    
    public const float ArciereLat = 44.499365736248f; public const float ArciereLon = 11.2823648750782f;
    public const string ArciereCell19 = "477fd4191dc";
    public const string ArciereCell18 = "477fd4191d";
    public const int ArciereDist = 20;
    public const int ArciereAlt = 0;
    public const bool ArciereFront = true;

    // CHIMERA
    public const float ChimeraLat = 44.50463673428272f; public const float ChimeraLon = 11.291060010707026f;
    public const string ChimeraCell19 = "477fd41033c";
    public const string ChimeraCell18 = "477fd41033";
    public const int ChimeraDist = 15;
    public const int ChimeraAlt = 0;
    public const bool ChimeraFront = true;

    // SCAGLIA PIETRA (ISSUE = same position of chimera)
    public const float ScagliaPietraLat = 44.50077821742219f; public const float ScagliaPietraLon = 11.28382072857177f;
    public const string ScagliaPietraCell19 = "477fd4199dc";
    public const string ScagliaPietraCell18 = "477fd4199d";
    public const int ScagliaPietraDist = 10;
    public const int ScagliaPietraAlt = 3;
    public const bool ScagliaPietraFront = true;

    // PIETRE FORATE
    public const float PietreForateLat = 44.5006762002241f; public const float PietreForateLon = 11.2847795337439f;
    public const string PietreForateCell19 = "477fd41991c";
    public const string PietreForateCell18 = "477fd41991";
    public const int PietreForateDist = 20; //30;
    public const int PietreForateAlt = 6;//10;
    public const bool PietreForateFront = false;

    // ALBERO
    public const float AlberoMuscolosoLat = 44.5023606292688f; public const float AlberoMuscolosoLon = 11.2877142056823f;
    public const string AlberoMuscolosoCell19 = "477fd410a04";
    public const string AlberoMuscolosoCell18 = "477fd410a1";
    public string[] AlberoMuscolosoGeoFenceArea = {"477fd41098c","477fd410994","477fd4109f",
        "477fd410a04","477fd410a1c","477fd410a24",
        "477fd41a0ac","477fd41a754","477fd41a75c"};
    public const int AlberoMuscolosoDist = 2;
    public const int AlberoMuscolosoAlt = 0;
    public const bool AlberoMuscolosoFront = false;

    // SCAGLIA PIETRA 2
    public const float ScagliaPietra2Lat = 44.504700192729f; public const float ScagliaPietra2Lon = 11.2899675965309f;
    public const string ScagliaPietra2Cell19 = "477fd4103e4";
    public const string ScagliaPietra2Cell18 = "477fd4103f";
    public const int ScagliaPietra2Dist = 10;
    public const int ScagliaPietra2Alt = 3;
    public const bool ScagliaPietra2Front = true;

    // MEDUSA
    public const float MedusaLat = 44.5047288867109f; public const float MedusaLon = 11.2908041104674f;
    public const string MedusaCell19 = "477fd410314";
    public const string MedusaCell18 = "477fd41031";
    public const int MedusaDist = 10;
    public const int MedusaAlt = 3;
    public const bool MedusaFront = false;

    // CHIMERA 2 44.4991385542784,11.286333873868
    public const float Chimera2Lat = 44.4991385542784f; public const float Chimera2Lon = 11.286333873868f;
    public const string Chimera2Cell19 = "477fd4177ec";
    public const string Chimera2Cell18 = "477fd4177f";
    public const int Chimera2Dist = 20; //30;
    public const int Chimera2Alt = 0;
    public const bool Chimera2Front = false;

    // SPIRITO FUOCO
    public const float SpiritoFuocoLat = 44.4980892012309f; public const float SpiritoFuocoLon = 11.2794315442443f;
    public const string SpiritoFuocoCell19 = "477fd41f4ec";
    public const string SpiritoFuocoCell18 = "477fd41f4f";
    public const int SpiritoFuocoDist = 20;
    public const int SpiritoFuocoAlt = 3;
    public const bool SpiritoFuocoFront = false;

    // CHIMERA 3
    public const float Chimera3Lat = 44.496118145665f; public const float Chimera3Lon = 11.2779060378671f;
    public const string Chimera3Cell19 = "477fd42194c";
    public const string Chimera3Cell18 = "477fd42195";
    public const int Chimera3Dist = 10;
    public const int Chimera3Alt = 0;
    public const bool Chimera3Front = true;

    // SERPENTE PIETRA
    public const float SerpentePietraLat = 44.4971220961484f; public const float SerpentePietraLon = 11.2771144509315f;
    public const string SerpentePietraCell19 = "477fd421d0c";
    public const string SerpentePietraCell18 = "477fd421d1";
    public const int SerpentePietraDist = 10;
    public const int SerpentePietraAlt = 2;
    public const bool SerpentePietraFront = true;

    // BUDDHA
    public const float BuddhaLat = 0.0f; public const float BuddhaLon = 0.0f;
    public const string BuddhaCell19 = "___";
    public const string BuddhaCell18 = "___";
    public const int BuddhaDist = 10;
    public const int BuddhaAlt = 5;
    public const bool BuddhaFront = false;

}