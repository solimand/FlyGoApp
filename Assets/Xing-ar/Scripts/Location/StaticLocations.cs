using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StaticLocations
{
    // Allowed Geofence Single Cells
    public static string[] fixedPos2mt = new string[14] {
        testgo1Cell17, testgo2Cell17,
        ArciereCell17, AlberoMuscolosoCell17, ChimeraCell17, Chimera2Cell17, Chimera3Cell17, 
        MedusaCell17, SerpentePietraCell17, SpiritoFuocoCell17, BuddhaCell17,
        ScagliaPietraCell17, ScagliaPietra2Cell17, "",
    };

    public static string NO_ZONE = "----------";

    // TEST 44.48256081468141, 11.375324649466517 (18-477e2b3a19) ; 44.48316908981307, 11.37570335190113 (18-477e2b39f5)
    // 44.48737759542787, 11.330448004800237 (17-477fd4ee74) ; 44.487504223125164, 11.329400995370172 (17-477fd4ee04)
    public const float testgo1Lat = 44.48737759542787f; public const float testgo1Lon = 11.330448004800237f;
    public const string testgo1Cell18 = "477e2b3a1b"; public const string testgo1Cell17 = "477e2b3a1c";
    public const float testgo1Alt = 0f; public const int testgo1Dist = 3;
    public const float testgo2Lat = 44.487504223125164f; public const float testgo2Lon = 11.329400995370172f;
    public const string testgo2Cell18 = "477e2b39f5"; public const string testgo2Cell17 = "477fd4ee04";
    public const float testgo2Alt = 0f; public const int testgo2Dist = 5;

    // ARCIERE    
    public const float ArciereLat = 44.499365736248f; public const float ArciereLon = 11.2823648750782f;
    public const string ArciereCell18 = "477fd4191d";
    public const string ArciereCell17 = "477fd4191c";
    public const int ArciereDist = 5;
    public const float ArciereAlt = 0f;
    public const bool ArciereFront = true;    

    // SCAGLIA PIETRA
    public const float ScagliaPietraLat = 44.50077821742219f; public const float ScagliaPietraLon = 11.28382072857177f;
    public const string ScagliaPietraCell18 = "477fd4199d";
    public const string ScagliaPietraCell17 = "477fd4199c";
    public const int ScagliaPietraDist = 3;
    public const float ScagliaPietraAlt = 2.0f;
    public const bool ScagliaPietraFront = true;
    
    // SCAGLIA PIETRA 2
    public const float ScagliaPietra2Lat = 44.504700192729f; public const float ScagliaPietra2Lon = 11.2899675965309f;
    public const string ScagliaPietra2Cell18 = "477fd4103f";
    public const string ScagliaPietra2Cell17 = "477fd4103c";
    public const int ScagliaPietra2Dist = 5;
    public const float ScagliaPietra2Alt = 3.0f;
    public const bool ScagliaPietra2Front = true;

    // PIETRE FORATE 
    //public const float PietreForateLat = 44.5006762002241f; public const float PietreForateLon = 11.2847795337439f;
    public const float PietreForateLat = 44.50102928390615f; public const float PietreForateLon = 11.285103906468683f;
    public const string PietreForateCell18 = "477fd41991";
    public const string PietreForateCell17 = "477fd4198c";
    public const int PietreForateDist = 8; 
    public const float PietreForateAlt = 3.0f;
    public const bool PietreForateFront = false;

    // ALBERO
    public const float AlberoMuscolosoLat = 44.5023606292688f; public const float AlberoMuscolosoLon = 11.2877142056823f;
    public const string AlberoMuscolosoCell18 = "477fd410a1";
    public const string AlberoMuscolosoCell17 = "477fd4109c";
    public const int AlberoMuscolosoDist = 5;
    public const float AlberoMuscolosoAlt = 0.0f;
    public const bool AlberoMuscolosoFront = false;

    // MEDUSA
    public const float MedusaLat = 44.5047288867109f; public const float MedusaLon = 11.2908041104674f;
    public const string MedusaCell18 = "477fd41031";
    public const string MedusaCell17 = "477fd41034";
    public const int MedusaDist = 5;
    public const float MedusaAlt = 2.0f;
    public const bool MedusaFront = false;

    // CHIMERA
    //public const float ChimeraLat = 44.50463673428272f; public const float ChimeraLon = 11.291060010707026f;
    public const float ChimeraLat = 44.50422737686775f; public const float ChimeraLon = 11.290670288738715f;
    public const string ChimeraCell18 = "477fd41033";
    public const string ChimeraCell17 = "477fd4104c";
    public const int ChimeraDist = 8;
    public const float ChimeraAlt = -2.0f;
    public const bool ChimeraFront = true;

    // CHIMERA 2
    public const float Chimera2Lat = 44.4991385542784f; public const float Chimera2Lon = 11.286333873868f;
    public const string Chimera2Cell18 = "477fd4177f";
    public const string Chimera2Cell17 = "477fd4177c";
    public const int Chimera2Dist = 8;
    public const float Chimera2Alt = -2.0f;
    public const bool Chimera2Front = false;

    // CHIMERA 3
    public const float Chimera3Lat = 44.496118145665f; public const float Chimera3Lon = 11.2779060378671f;
    public const string Chimera3Cell18 = "477fd42195";
    public const string Chimera3Cell17 = "477fd42194";
    public const int Chimera3Dist = 8;
    public const float Chimera3Alt = -2.0f;
    public const bool Chimera3Front = true;

    // SPIRITO FUOCO
    public const float SpiritoFuocoLat = 44.4980892012309f; public const float SpiritoFuocoLon = 11.2794315442443f;
    public const string SpiritoFuocoCell18 = "477fd41f4f";
    public const string SpiritoFuocoCell17 = "477fd41f4c";
    public const int SpiritoFuocoDist = 2;
    public const float SpiritoFuocoAlt = 2.0f;
    public const bool SpiritoFuocoFront = false;

    // SERPENTE PIETRA
    public const float SerpentePietraLat = 44.4971220961484f; public const float SerpentePietraLon = 11.2771144509315f;
    public const string SerpentePietraCell18 = "477fd421d1";
    public const string SerpentePietraCell17 = "477fd421dc";
    public const int SerpentePietraDist = 3;
    public const float SerpentePietraAlt = 2.0f;
    public const bool SerpentePietraFront = true;

    // BUDDHA
    public const float BuddhaLat = 0.0f; public const float BuddhaLon = 0.0f;
    public const string BuddhaCell18 = "___";
    public const string BuddhaCell17 = "___";
    public const int BuddhaDist = 3;
    public const int BuddhaAlt = 5;
    public const bool BuddhaFront = false;
}