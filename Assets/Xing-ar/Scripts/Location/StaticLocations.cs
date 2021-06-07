using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StaticLocations
{
    // Allowed Geofence Areas
    public static string[] fixedPos2mt = new string[5] { "477fd4ee07c", "477fd4ee09c", "477e2b3a1bc","477fd4ee07", "477fd4ee09" };

    // TEST Facolta 44.48754455426136, 11.329327606536179 // 44.4873723394221, 11.330171161228847
    public const float alberoLat = 44.48754455426136f; public const float alberoLon = 11.329327606536179f;
    public const string alberoCell19 = "477fd4ee07"; public const float alberoAlt= 0f; public const int alberoDist = 3;
    public const float ragnoLat = 44.4873723394221f; public const float ragnoLon = 11.330171161228847f;
    public const string ragnoCell19 = "477fd4ee09";  public const float ragnoAlt= 3f; public const int ragnoDist = 3;

    // TEST Parco Facolta 44.48754455426136, 11.329327606536179 // 44.4873723394221, 11.330171161228847
    /*public const float alberoLat = 44.48754455426136f; public const float alberoLon = 11.329327606536179f;
    public const string alberoCell19 = "477fd4ee07c"; public const float alberoAlt= 0f; public const int alberoDist = 3;
    public const float ragnoLat = 44.4873723394221f; public const float ragnoLon = 11.330171161228847f;
    public const string ragnoCell19 = "477fd4ee09c";  public const float ragnoAlt= 3f; public const int ragnoDist = 15;*/

    // TEST TETTO HOME 44.482642 11.375178 477e2b3a1bc
    /*public const float alberoLat = 44.48265275117313f; public const float alberoLon = 11.37513649267819f;
    public const string alberoCell19 = "477e2b3a1ec";
    public const float ragnoLat = 44.48277791763526f; public const float ragnoLon = 11.375424708611233f;
    public const string ragnoCell19 = "477e2b3a1bc";*/
    
    // ARCIERE    
    public const float ArciereLat = 44.499365736248f; public const float ArciereLon = 11.2823648750782f;
    public const string ArciereCell19 = "477fd4191dc";
    public const int ArciereDist = 20;
    public const int ArciereAlt = 0;
    public const bool ArciereFront = true;

    // CHIMERA
    public const float ChimeraLat = 44.50463673428272f; public const float ChimeraLon = 11.291060010707026f;
    public const string ChimeraCell19 = "477fd41033c";
    public const int ChimeraDist = 15;
    public const int ChimeraAlt = 0;
    public const bool ChimeraFront = true;

    // SCAGLIA PIETRA (ISSUE = same position of chimera)
    public const float ScagliaPietraLat = 44.50077821742219f; public const float ScagliaPietraLon = 11.28382072857177f;
    public const string ScagliaPietraCell19 = "477fd4199dc";
    public const int ScagliaPietraDist = 10;
    public const int ScagliaPietraAlt = 3;
    public const bool ScagliaPietraFront = true;

    // PIETRE FORATE
    public const float PietreForateLat = 44.5006762002241f; public const float PietreForateLon = 11.2847795337439f;
    public const string PietreForateCell19 = "477fd41991c";
    public const int PietreForateDist = 30;
    public const int PietreForateAlt = 10;
    public const bool PietreForateFront = false;

    // ALBERO
    public const float AlberoMuscolosoLat = 44.5023606292688f; public const float AlberoMuscolosoLon = 11.2877142056823f;
    public const string AlberoMuscolosoCell19 = "477fd410a04";
    public const int AlberoMuscolosoDist = 2;
    public const int AlberoMuscolosoAlt = 0;
    public const bool AlberoMuscolosoFront = false;

    // SCAGLIA PIETRA 2
    public const float ScagliaPietra2Lat = 44.504700192729f; public const float ScagliaPietra2Lon = 11.2899675965309f;
    public const string ScagliaPietra2Cell19 = "477fd4103e4";
    public const int ScagliaPietra2Dist = 10;
    public const int ScagliaPietra2Alt = 3;
    public const bool ScagliaPietra2Front = true;

    // MEDUSA
    public const float MedusaLat = 44.5047288867109f; public const float MedusaLon = 11.2908041104674f;
    public const string MedusaCell19 = "477fd410314";
    public const int MedusaDist = 10;
    public const int MedusaAlt = 3;
    public const bool MedusaFront = false;

    // CHIMERA 2 44.4991385542784,11.286333873868
    public const float Chimera2Lat = 44.4991385542784f; public const float Chimera2Lon = 11.286333873868f;
    public const string Chimera2Cell19 = "477fd4177ec";
    public const int Chimera2Dist = 30;
    public const int Chimera2Alt = 0;
    public const bool Chimera2Front = false;

    // SPIRITO FUOCO
    public const float SpiritoFuocoLat = 44.4980892012309f; public const float SpiritoFuocoLon = 11.2794315442443f;
    public const string SpiritoFuocoCell19 = "477fd41f4ec";
    public const int SpiritoFuocoDist = 20;
    public const int SpiritoFuocoAlt = 3;
    public const bool SpiritoFuocoFront = false;

    // CHIMERA 3
    public const float Chimera3Lat = 44.496118145665f; public const float Chimera3Lon = 11.2779060378671f;
    public const string Chimera3Cell19 = "477fd42194c";
    public const int Chimera3Dist = 10;
    public const int Chimera3Alt = 0;
    public const bool Chimera3Front = true;

    // SERPENTE PIETRA
    public const float SerpentePietraLat = 44.4971220961484f; public const float SerpentePietraLon = 11.2771144509315f;
    public const string SerpentePietraCell19 = "477fd421d0c";
    public const int SerpentePietraDist = 10;
    public const int SerpentePietraAlt = 2;
    public const bool SerpentePietraFront = true;

    // BUDDHA
    public const float BuddhaLat = 0.0f; public const float BuddhaLon = 0.0f;
    public const string BuddhaCell19 = "___";
    public const int BuddhaDist = 10;
    public const int BuddhaAlt = 5;
    public const bool BuddhaFront = false;

}