using System.Runtime.InteropServices;
using System.Text;

namespace Sonic_Heroes_AP_Client;

public enum MusicType
{
    Music,
    BossMusic,
    MenuMusic,
    ShortMusic,
    
    Jingle,
    LongJingle,
    Ignored,
}

public static class MusicShuffleData
{
    public static string HeroesBGMFolder { get; set; } = Mod.Configuration.HeroesMusicShuffleOptions.MusicShuffleHeroesBGMFolder.Replace("/", "\\");
    
    public static string SA2ADXFolder { get; set; } = Mod.Configuration.SA2MusicShuffleOptions.MusicShuffleSA2ADXFolder.Replace("/", "\\");
    
    public static string SADXWMAFolder { get; set; } = Mod.Configuration.SADXMusicShuffleOptions.MusicShuffleSADXWMAFolder.Replace("/", "\\");
    
    public static string CustomFolder { get; set; } = Mod.Configuration.CustomMusicShuffleOptions.MusicShuffleCustomFolder.Replace("/", "\\");
    
    
    public static List<(string name, MusicType type)> HeroesSongs = new()
    {
        (Path.Combine(HeroesBGMFolder, "SNG_BTL01.adx"), MusicType.ShortMusic),             //2P Battle Sea Area
        (Path.Combine(HeroesBGMFolder, "SNG_BTL02.adx"), MusicType.ShortMusic),             //2P Battle City Area
        (Path.Combine(HeroesBGMFolder, "SNG_BTL03.adx"), MusicType.ShortMusic),             //2P Battle Casino Area
        (Path.Combine(HeroesBGMFolder, "SNG_BTL04.adx"), MusicType.ShortMusic),             //2P Battle Ring Race
        (Path.Combine(HeroesBGMFolder, "SNG_BTL05.adx"), MusicType.ShortMusic),             //2P Battle Quick Race
        (Path.Combine(HeroesBGMFolder, "SNG_E0017.adx"), MusicType.LongJingle),             //"We Can" Credits (Sonic Theme)
        (Path.Combine(HeroesBGMFolder, "SNG_E0117.adx"), MusicType.LongJingle),             //"This Machine" Credits (Dark Theme) Rose and Chaotix Don't have one but have the full theme
        (Path.Combine(HeroesBGMFolder, "SNG_E0404.adx"), MusicType.LongJingle),             //"Sonic Heroes" Credits (Final Story)
        (Path.Combine(HeroesBGMFolder, "SNG_EV10.adx"), MusicType.LongJingle),              //Event 9 "No Past To Remember" 1 min no loop
        (Path.Combine(HeroesBGMFolder, "SNG_EV11.adx"), MusicType.LongJingle),              //Event 10 "Monkey Business" 31 secs no loop
        (Path.Combine(HeroesBGMFolder, "SNG_EV12.adx"), MusicType.LongJingle),              //Event 11 "My World"       42 secs no loop
        (Path.Combine(HeroesBGMFolder, "SNG_EV13.adx"), MusicType.LongJingle),              //Event 12 "My Ambition"    50 secs no loop
        (Path.Combine(HeroesBGMFolder, "SNG_INVNCBL.adx"), MusicType.Jingle),               //Invincible Jingle
        (Path.Combine(HeroesBGMFolder, "SNG_RNDCLR.adx"), MusicType.Jingle),                //Round Clear Jingle
        (Path.Combine(HeroesBGMFolder, "SNG_SPEEDUP.adx"), MusicType.Jingle),               //Speed Shoes Jingle
        (Path.Combine(HeroesBGMFolder, "SNG_SPSTG1.adx"), MusicType.Music),                 //Bonus Stage (No Emerald)
        (Path.Combine(HeroesBGMFolder, "SNG_SPSTG2.adx"), MusicType.Music),                 //Bonus Stage (Emerald)
        (Path.Combine(HeroesBGMFolder, "SNG_STG01.adx"), MusicType.Music),                  //Seaside Hill
        (Path.Combine(HeroesBGMFolder, "SNG_STG02.adx"), MusicType.Music),                  //Ocean Palace
        (Path.Combine(HeroesBGMFolder, "SNG_STG03.adx"), MusicType.Music),                  //Grand Metropolis
        (Path.Combine(HeroesBGMFolder, "SNG_STG04.adx"), MusicType.Music),                  //Power Plant
        (Path.Combine(HeroesBGMFolder, "SNG_STG05.adx"), MusicType.Music),                  //Casino Park
        (Path.Combine(HeroesBGMFolder, "SNG_STG06.adx"), MusicType.Music),                  //Bingo Park
        (Path.Combine(HeroesBGMFolder, "SNG_STG07.adx"), MusicType.Music),                  //Rail Canyon
        (Path.Combine(HeroesBGMFolder, "SNG_STG08.adx"), MusicType.Music),                  //Bullet Station
        (Path.Combine(HeroesBGMFolder, "SNG_STG09.adx"), MusicType.Music),                  //Frog Forest
        (Path.Combine(HeroesBGMFolder, "SNG_STG10.adx"), MusicType.Music),                  //Lost Jungle
        (Path.Combine(HeroesBGMFolder, "SNG_STG11A.adx"), MusicType.Music),                 //Hang Castle Regular Gravity
        (Path.Combine(HeroesBGMFolder, "SNG_STG11B.adx"), MusicType.Music),                 //Hang Castle Upside Down Gravity
        (Path.Combine(HeroesBGMFolder, "SNG_STG12.adx"), MusicType.Music),                  //Mystic Mansion (Entire)
        (Path.Combine(HeroesBGMFolder, "SNG_STG12A.adx"), MusicType.Jingle),                //Mystic Mansion Part
        (Path.Combine(HeroesBGMFolder, "SNG_STG12B.adx"), MusicType.Jingle),                //Mystic Mansion Part
        (Path.Combine(HeroesBGMFolder, "SNG_STG12C.adx"), MusicType.Jingle),                //Mystic Mansion Part
        (Path.Combine(HeroesBGMFolder, "SNG_STG12D.adx"), MusicType.Jingle),                //Mystic Mansion Part
        (Path.Combine(HeroesBGMFolder, "SNG_STG12E.adx"), MusicType.Jingle),                //Mystic Mansion Part
        (Path.Combine(HeroesBGMFolder, "SNG_STG12F.adx"), MusicType.Jingle),                //Mystic Mansion Part
        (Path.Combine(HeroesBGMFolder, "SNG_STG13.adx"), MusicType.Music),                  //Egg Fleet
        (Path.Combine(HeroesBGMFolder, "SNG_STG14.adx"), MusicType.Music),                  //Final Fortress
        (Path.Combine(HeroesBGMFolder, "SNG_STG20.adx"), MusicType.BossMusic),              //Egg Hawk
        (Path.Combine(HeroesBGMFolder, "SNG_STG20PI.adx"), MusicType.Jingle),               //Egg Hawk Intro (Cutscene)
        (Path.Combine(HeroesBGMFolder, "SNG_STG21.adx"), MusicType.BossMusic),              //Team Fight 1 (City) (prob not used)
        (Path.Combine(HeroesBGMFolder, "SNG_STG22.adx"), MusicType.BossMusic),              //Robot Carnival and Robot Storm
        (Path.Combine(HeroesBGMFolder, "SNG_STG23.adx"), MusicType.BossMusic),              //Egg Albatross
        (Path.Combine(HeroesBGMFolder, "SNG_STG24TC.adx"), MusicType.BossMusic),            //Team Fight Chaotix
        (Path.Combine(HeroesBGMFolder, "SNG_STG24TD.adx"), MusicType.BossMusic),            //Team Fight Dark
        (Path.Combine(HeroesBGMFolder, "SNG_STG24TR.adx"), MusicType.BossMusic),            //Team Fight Rose
        (Path.Combine(HeroesBGMFolder, "SNG_STG24TS.adx"), MusicType.BossMusic),            //Team Fight Sonic (instrumental?)
        (Path.Combine(HeroesBGMFolder, "SNG_STG26.adx"), MusicType.BossMusic),              //Egg Emperor
        (Path.Combine(HeroesBGMFolder, "SNG_STG27.adx"), MusicType.BossMusic),              //Metal Madness
        (Path.Combine(HeroesBGMFolder, "SNG_STG28.adx"), MusicType.BossMusic),              //Metal Overlord
        (Path.Combine(HeroesBGMFolder, "SNG_STG29.adx"), MusicType.Music),                  //Sea Gate
        (Path.Combine(HeroesBGMFolder, "SNG_SYS1.adx"), MusicType.MenuMusic),               //Menu Main (after Save File Selected)
        (Path.Combine(HeroesBGMFolder, "SNG_SYS2.adx"), MusicType.MenuMusic),               //Menu 1P (includes Level Select)
        (Path.Combine(HeroesBGMFolder, "SNG_SYS3.adx"), MusicType.MenuMusic),               //Menu 2P
        (Path.Combine(HeroesBGMFolder, "SNG_SYS4.adx"), MusicType.MenuMusic),               //Menu File Select Options
        (Path.Combine(HeroesBGMFolder, "SNG_TBSFA1.adx"), MusicType.Jingle),                //Team Blast Sonic
        (Path.Combine(HeroesBGMFolder, "SNG_TBSFA2.adx"), MusicType.Jingle),                //Team Blast Dark
        (Path.Combine(HeroesBGMFolder, "SNG_TBSFA3.adx"), MusicType.Jingle),                //Team Blast Rose
        //(Path.Combine(HeroesBGMFolder, "SNG_TBSFA4.adx"), MusicType.Ignored),               //duplicate of TBSFA4J (Japanese Voices)
        (Path.Combine(HeroesBGMFolder, "SNG_TBSFA4E.adx"), MusicType.Jingle),               //Team Blast Chaotix (English)
        (Path.Combine(HeroesBGMFolder, "SNG_TBSFA4J.adx"), MusicType.Jingle),               //Team Blast Chaotix (Japanese)
        (Path.Combine(HeroesBGMFolder, "SNG_TBSFA5.adx"), MusicType.Jingle),                //Team Blast Super Sonic
        (Path.Combine(HeroesBGMFolder, "SNG_TITLE1.adx"), MusicType.LongJingle),                //Title Theme (with Intro) before cutscene
        (Path.Combine(HeroesBGMFolder, "SNG_TITLE2.adx"), MusicType.Jingle),                //Title Theme (without Intro) after cutscene
        (Path.Combine(HeroesBGMFolder, "SNG_V01_MAIN.adx"), MusicType.LongJingle),          //"Sonic Heroes" Main Theme
        (Path.Combine(HeroesBGMFolder, "SNG_V02_TS.adx"), MusicType.LongJingle),            //"We Can" Sonic Theme
        (Path.Combine(HeroesBGMFolder, "SNG_V03_TD.adx"), MusicType.LongJingle),            //"This Machine" Dark Theme
        (Path.Combine(HeroesBGMFolder, "SNG_V04_TR.adx"), MusicType.LongJingle),            //"Follow Me" Rose Theme
        (Path.Combine(HeroesBGMFolder, "SNG_V05_TC.adx"), MusicType.LongJingle)             //"Team Chaotix" Chaotix Theme
    };

    public static List<(string name, MusicType type)> SADXSongs = new()
    {
        (Path.Combine(SADXWMAFolder, "advamy.adx"), MusicType.LongJingle),                  //Amy Theme (Short)
        (Path.Combine(SADXWMAFolder, "advbig.adx"), MusicType.LongJingle),                  //Big Theme (Short)
        (Path.Combine(SADXWMAFolder, "adve102.adx"), MusicType.LongJingle),                 //Gamma Theme (Short)
        (Path.Combine(SADXWMAFolder, "advknkls.adx"), MusicType.LongJingle),                //Knuckles Theme (Short)
        (Path.Combine(SADXWMAFolder, "advmiles.adx"), MusicType.LongJingle),                //Tails Theme (Short)
        (Path.Combine(SADXWMAFolder, "advsonic.adx"), MusicType.LongJingle),                //Sonic Theme (Short)
        (Path.Combine(SADXWMAFolder, "amy.adx"), MusicType.LongJingle),                     //Amy Theme
        (Path.Combine(SADXWMAFolder, "big.adx"), MusicType.LongJingle),                     //Big Theme
        (Path.Combine(SADXWMAFolder, "bossall.adx"), MusicType.BossMusic),                  //Boss Theme
        (Path.Combine(SADXWMAFolder, "bosse101.adx"), MusicType.BossMusic),                 //Beta Boss Theme
        (Path.Combine(SADXWMAFolder, "bossevnt.adx"), MusicType.BossMusic),                 //
        (Path.Combine(SADXWMAFolder, "bosstrgt.adx"), MusicType.BossMusic),                 //Boss Fight Against Playable Char (Sonic vs Knuckles)
        (Path.Combine(SADXWMAFolder, "casino1.adx"), MusicType.Music),                      //Casinopolis
        (Path.Combine(SADXWMAFolder, "casino2.adx"), MusicType.Music),                      //Casinopolis Sewers
        (Path.Combine(SADXWMAFolder, "casino3.adx"), MusicType.Music),                      //Pinball Table
        (Path.Combine(SADXWMAFolder, "casino4.adx"), MusicType.Music),                      //Nights Pinball Table
        (Path.Combine(SADXWMAFolder, "chao.adx"), MusicType.Music),                         //Chao
        (Path.Combine(SADXWMAFolder, "chaogoal.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "chaohall.adx"), MusicType.Ignored),                   //Chao Hall (Not in Sadx)
        (Path.Combine(SADXWMAFolder, "chaorace.adx"), MusicType.Music),                     //Chao Race
        (Path.Combine(SADXWMAFolder, "chaos.adx"), MusicType.BossMusic),
        (Path.Combine(SADXWMAFolder, "chaos_6.adx"), MusicType.BossMusic),
        (Path.Combine(SADXWMAFolder, "chaos_p1.adx"), MusicType.BossMusic),
        (Path.Combine(SADXWMAFolder, "chaos_p2.adx"), MusicType.BossMusic),
        (Path.Combine(SADXWMAFolder, "chao_g_born_c.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_born_d.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_born_d2.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_born_h.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_born_h2.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_dance.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_dead.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "chao_g_iede.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "CHAO_K_M.adx"), MusicType.MenuMusic),                 //Black Market
        (Path.Combine(SADXWMAFolder, "chao_k_net_fine.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "CHAO_R_E.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "chao_r_gate_open.adx"), MusicType.Ignored),           //
        (Path.Combine(SADXWMAFolder, "charactr.adx"), MusicType.MenuMusic),                 //Choose Character
        (Path.Combine(SADXWMAFolder, "circuit.adx"), MusicType.ShortMusic),
        (Path.Combine(SADXWMAFolder, "continue.adx"), MusicType.Ignored),                   //Run Out of Lives
        (Path.Combine(SADXWMAFolder, "c_btl_cv.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "e102.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "ecoast1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "ecoast2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "ecoast3.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "egcarer1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "egcarer2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "eggman.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "eggmbl23.adx"), MusicType.BossMusic),
        (Path.Combine(SADXWMAFolder, "eggrobo.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "evtbgm00.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "evtbgm01.adx"), MusicType.LongJingle),                //
        (Path.Combine(SADXWMAFolder, "evtbgm02.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "evtbgm03.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "evtbgm04.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "evtbgm05.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "finaleg1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "finaleg2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "fishget.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "fishing.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "fishmiss.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "hammer.adx"), MusicType.ShortMusic),
        (Path.Combine(SADXWMAFolder, "highway1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "highway2.adx"), MusicType.ShortMusic),
        (Path.Combine(SADXWMAFolder, "highway3.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "hurryup.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "icecap1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "icecap2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "icecap3.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "invncibl.adx"), MusicType.Jingle),                    //
        (Path.Combine(SADXWMAFolder, "item1.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "jingle_1.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "jingle_2.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "jingle_3.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "jingle_4.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "jingle_5.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "KNUCKLES.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "lstwrld1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "lstwrld2.adx"), MusicType.ShortMusic),
        (Path.Combine(SADXWMAFolder, "lstwrld3.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "mainthem.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "MCLEAR_44.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "MSTART_44.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "mstcln.adx"), MusicType.Music),
        //(Path.Combine(SADXWMAFolder, "nights_k.adx"), MusicType.Ignored),                 //empty file
        (Path.Combine(SADXWMAFolder, "one_up.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "option.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "redmntn1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "redmntn2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "rndclear.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "sandhill.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "scramble.adx"), MusicType.ShortMusic),                //Tornado Music
        (Path.Combine(SADXWMAFolder, "shelter1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "shelter2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "skydeck1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "skydeck2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "sonic.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "speedup.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "sprsonic.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "s_square.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "tails.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "theamy.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "thebig.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "thee102.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "theknkls.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "themiles.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "thesonic.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "tical.adx"), MusicType.LongJingle),
        (Path.Combine(SADXWMAFolder, "timer.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "title.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "title2.adx"), MusicType.Jingle),
        (Path.Combine(SADXWMAFolder, "titl_egg.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "titl_mr1.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "titl_mr2.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "titl_ss.adx"), MusicType.Ignored),
        (Path.Combine(SADXWMAFolder, "trial.adx"), MusicType.MenuMusic),
        (Path.Combine(SADXWMAFolder, "twnklpk1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "twnklpk2.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "twnklpk3.adx"), MusicType.ShortMusic),
        (Path.Combine(SADXWMAFolder, "wndyvly1.adx"), MusicType.Music),
        (Path.Combine(SADXWMAFolder, "wndyvly2.adx"), MusicType.ShortMusic),
        (Path.Combine(SADXWMAFolder, "wndyvly3.adx"), MusicType.Music)
    };

    public static List<(string name, MusicType type)> SA2Songs = new()
    {
        (Path.Combine(SA2ADXFolder, "advsng_1.adx"), MusicType.MenuMusic),                  //6 secs
        (Path.Combine(SA2ADXFolder, "advsng_2.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "advsng_3.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "advsng_4.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "advsng_5.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "a_mine.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "boss_01.adx"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "BOSS_02A.ADX"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "BOSS_02B.ADX"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "boss_03.adx"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "boss_04.adx"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "boss_05.adx"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "boss_06.adx"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "BOSS_07.ADX"), MusicType.BossMusic),
        (Path.Combine(SA2ADXFolder, "btl_ce.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_hb.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_ig.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_mh.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_opng.adx"), MusicType.LongJingle),                 //no loop
        (Path.Combine(SA2ADXFolder, "btl_pc.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_rh.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_sel.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "btl_so.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_wb.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "btl_wj.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "chao_g_bgm_d.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "chao_g_bgm_h.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "chao_g_bgm_n.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "chao_g_born_c.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_born_d.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_born_d2.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_born_h.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_born_h2.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_born_hc.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_dance.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_dead.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_gate_open.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_iede.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_new_garden.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_radicase1.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_radicase2.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_radicase3.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_radicase4.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_tv_cartoon.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_tv_drama.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_g_tv_sports.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_hall.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "CHAO_K_M.ADX"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "chao_k_m2.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "chao_k_net_connect.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "chao_k_net_fault.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_net_fine.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_sing_dark1.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_sing_dark2.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_sing_dark3.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_sing_hero1.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_sing_hero2.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_k_sing_hero3.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_l_m.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "chao_new_garden.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_r_a.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "chao_r_b.adx"), MusicType.Ignored),                    //short version of chao_r_j
        (Path.Combine(SA2ADXFolder, "chao_r_c.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "chao_r_d.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "CHAO_R_E.ADX"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "chao_r_gate_open.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "chao_r_h.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "chao_r_item_get.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "chao_r_j.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "continue.adx"), MusicType.Ignored),
        (Path.Combine(SA2ADXFolder, "c_btl_ch.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "c_btl_cv.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "c_btl_dr.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "c_btl_gm.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "c_btl_ls.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "c_btl_sl.adx"), MusicType.MenuMusic),
        (Path.Combine(SA2ADXFolder, "c_btl_wn.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "c_core_1.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "c_core_2.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "c_core_5.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "C_CORE_6.ADX"), MusicType.Ignored),                    //loops? 0:18
        (Path.Combine(SA2ADXFolder, "c_escap1.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "c_escap2.adx"), MusicType.ShortMusic),                 //truck chase
        (Path.Combine(SA2ADXFolder, "c_escap3.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "c_gadget.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "c_wall.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "d_chambr.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "d_lagoon.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "e000_sng.adx"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E006_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "e019_sng.adx"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "e021_sng.adx"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E027_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E028_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E101_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E106_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E111_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E112_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E119_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "e127_sng.adx"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E130_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E205_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "e207_sng.adx"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E208_SNG.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E210_SN1.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "E210_SN2.ADX"), MusicType.LongJingle),
        (Path.Combine(SA2ADXFolder, "e350_sng.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "e_engine.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "e_quart.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "f_chase.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "f_rush.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "g_fores.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "g_hill.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "h_base.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "invncibl.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "item_get.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "i_gate.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "kart.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "l_colony.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "m_harb1.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "m_harb2.adx"), MusicType.ShortMusic),
        (Path.Combine(SA2ADXFolder, "m_herd.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "m_space.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "m_street.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "one_up.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "p_cave.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "p_hill.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "p_lane.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "rndclear.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "r_hwy.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "speedup.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "s_hall.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "s_ocean.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "s_rail.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "t1_shado.adx"), MusicType.LongJingle),                 //no loop 2:53
        (Path.Combine(SA2ADXFolder, "t1_sonic.adx"), MusicType.LongJingle),                 //no loop 2:45
        (Path.Combine(SA2ADXFolder, "T2_MILES.ADX"), MusicType.ShortMusic),                 //loop 1:09
        (Path.Combine(SA2ADXFolder, "T2_ROUGE.ADX"), MusicType.Music),                      //loop 2:01
        (Path.Combine(SA2ADXFolder, "t3_eggma.adx"), MusicType.Ignored),                    //loop 0:17 (might need to be ignored)
        (Path.Combine(SA2ADXFolder, "T3_KNUCK.ADX"), MusicType.Music),                      //loop 1:41
        (Path.Combine(SA2ADXFolder, "T3_MILES.ADX"), MusicType.ShortMusic),                 //loop 0:36
        (Path.Combine(SA2ADXFolder, "T3_SHADO.ADX"), MusicType.Music),                      //loop 1:45
        (Path.Combine(SA2ADXFolder, "T4_KNUCK.ADX"), MusicType.ShortMusic),                 //loop 1:09
        (Path.Combine(SA2ADXFolder, "T4_ROUGE.ADX"), MusicType.Music),                      //loop 1:59
        (Path.Combine(SA2ADXFolder, "T4_SONIC.ADX"), MusicType.LongJingle),                 //no loop 0:38
        (Path.Combine(SA2ADXFolder, "t9_amy.adx"), MusicType.Music),                        //loop 1:29 (this one is good)
        (Path.Combine(SA2ADXFolder, "T9_EGGMA.ADX"), MusicType.ShortMusic),                 //loop 0:48
        (Path.Combine(SA2ADXFolder, "T9_KNUCK.ADX"), MusicType.LongJingle),                 //no loop 1:07
        (Path.Combine(SA2ADXFolder, "T9_MILES.ADX"), MusicType.Music),                      //loop 1:26
        (Path.Combine(SA2ADXFolder, "T9_ROUGE.ADX"), MusicType.Music),                      //loop 1:41
        (Path.Combine(SA2ADXFolder, "T9_SHADO.ADX"), MusicType.Music),                      //loop 2:11
        (Path.Combine(SA2ADXFolder, "T9_SONIC.ADX"), MusicType.Music),                      //loop 1:19
        (Path.Combine(SA2ADXFolder, "timer.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "title.adx"), MusicType.Jingle),
        (Path.Combine(SA2ADXFolder, "w_bed.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "w_canyon.adx"), MusicType.Music),
        (Path.Combine(SA2ADXFolder, "w_jungl.adx"), MusicType.Music)
    };
}

public class MusicShuffleHandler
{
    public static Dictionary<string, string> Map;

    public void Shuffle(int seed)
    {
        if (!Mod.Configuration!.HeroesMusicShuffleOptions.MusicShuffleHeroes 
            && !Mod.Configuration.SA2MusicShuffleOptions.MusicShuffleSA2 
            && !Mod.Configuration.SADXMusicShuffleOptions.MusicShuffleSADX
            && !Mod.Configuration.CustomMusicShuffleOptions.MusicShuffleCustom)
            return;
        
        //MusicShuffleData.HeroesBGMFolder = MusicShuffleData.HeroesBGMFolder.Replace("/", "\\");
        //MusicShuffleData.SA2ADXFolder = MusicShuffleData.SA2ADXFolder.Replace("/", "\\");
        //MusicShuffleData.SADXWMAFolder = MusicShuffleData.SADXWMAFolder.Replace("/", "\\");
        //MusicShuffleData.CustomFolder = MusicShuffleData.CustomFolder.Replace("/", "\\");
        
        //Console.WriteLine($"Shuffle Here: {seed}");
        Map = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        var allSongs = new List<(string name, MusicType type)>();
        var heroesSongs = MusicShuffleData.HeroesSongs;
        
        if (Mod.Configuration!.HeroesMusicShuffleOptions.MusicShuffleHeroes)
            allSongs.AddRange(heroesSongs);
        if (Mod.Configuration.SADXMusicShuffleOptions.MusicShuffleSADX)
            allSongs.AddRange(MusicShuffleData.SADXSongs);
        if (Mod.Configuration.SA2MusicShuffleOptions.MusicShuffleSA2)
            allSongs.AddRange(MusicShuffleData.SA2Songs);
        
        try
        {
            if (Mod.Configuration.CustomMusicShuffleOptions.MusicShuffleCustom)
            {
                if (Directory.Exists(MusicShuffleData.CustomFolder))
                {
                    allSongs.AddRange(
                        from type in Enum.GetValues(typeof(MusicType)).Cast<MusicType>()
                        where Directory.Exists(
                            Path.Combine(MusicShuffleData.CustomFolder, type.ToString()))
                        from file in Directory.GetFiles(Path.Combine(MusicShuffleData.CustomFolder,
                            type.ToString()))
                        select (file, type));
                }
            }
        
        
            //change types here
            
            var heroesSongsMerged = new List<(string name, MusicType type)>();

            foreach (var hero in heroesSongs)
            {
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleBossMusic && hero.type is MusicType.BossMusic)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleMenuMusic && hero.type is MusicType.MenuMusic)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleShortMusic && hero.type is MusicType.ShortMusic)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleLongJingle && hero.type is MusicType.LongJingle)
                {
                    heroesSongsMerged.Add((hero.name, MusicType.Jingle));
                    continue;
                }
                heroesSongsMerged.Add((hero.name, hero.type));
                
            }
            
            var allSongsMerged = new List<(string name, MusicType type)>();

            foreach (var hero in allSongs)
            {
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleBossMusic && hero.type is MusicType.BossMusic)
                {
                    allSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleMenuMusic && hero.type is MusicType.MenuMusic)
                {
                    allSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleShortMusic && hero.type is MusicType.ShortMusic)
                {
                    allSongsMerged.Add((hero.name, MusicType.Music));
                    continue;
                }
                if (!Mod.Configuration.MusicShuffleOptions.MusicShuffleLongJingle && hero.type is MusicType.LongJingle)
                {
                    allSongsMerged.Add((hero.name, MusicType.Jingle));
                    continue;
                }
                allSongsMerged.Add((hero.name, hero.type));
            }
            
            

            var heroesGroups = heroesSongsMerged.GroupBy(s => s.type);
            var allGroups = allSongsMerged.GroupBy(s => s.type).ToDictionary(g => g.Key, g => g.Select(x => x.name).ToArray());
            var random = new Random(seed);
            foreach (var group in heroesGroups)
            {
                var songs = group.Select(x => x.name).ToList();
                var type = group.Key;
                var shuffled = allGroups[type].OrderBy(_ => random.Next()).ToList();
                
                if (songs.Count > shuffled.Count)
                {
                    for (var i = 0; i < shuffled.Count; i++)
                    {
                        Map[songs[i]] = shuffled[i];
                        Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping[songs[i].Split('\\').Last()] = shuffled[i].Split('\\').Last();
                        //Mod.SaveDataHandler.CustomSaveData.MusicRandoMapping[songs[i]] = shuffled[i];
                    }

                    for (var i = shuffled.Count; i < songs.Count; i++)
                    {
                        Map[songs[i]] = songs[i];
                        Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping[songs[i].Split('\\').Last()] = songs[i].Split('\\').Last();
                        //Mod.SaveDataHandler.CustomSaveData.MusicRandoMapping[songs[i]] = songs[i];
                    }
                    continue;
                }

                for (var i = 0; i < songs.Count; i++)
                {
                    Map[songs[i]] = shuffled[i];  
                    Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping[songs[i].Split('\\').Last()] = shuffled[i].Split('\\').Last();
                    //Mod.SaveDataHandler.CustomSaveData.MusicRandoMapping[songs[i]] = shuffled[i];
                }
                    
            }
            
            
            //Handle Mystic Mansion Here
            //var tempStr = Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12.adx")];
            //Console.WriteLine($"Mystic Mansion Should Now Be: {tempStr}");
            Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12A.adx")] = Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12.adx")];
            Mod.SaveDataHandler!.CustomSaveData!.MusicRandoMapping["SNG_STG12A.adx"] = Map[Path.Combine(MusicShuffleData.HeroesBGMFolder, "SNG_STG12A.adx")].Split('\\').Last();
            

            unsafe
            {
                //MusicShuffleSpecialCases* shuffleSpecialCases = (MusicShuffleSpecialCases*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();

                //IntPtr mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                //var shuffleSpecialCasesAddr = (IntPtr)mysticMansion;
                
                //Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1090D5, BitConverter.GetBytes((int)(string*)mysticMansion));
                //Memory.Instance.SafeWrite(Mod.ModuleBase + 0x3FB3F, BitConverter.GetBytes((int)(string*)mysticMansion));
                
                /*
                //Try Individual Here
                tempStr = Map["SNG_STG12A.adx"];
                mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1090D5, BitConverter.GetBytes((int)(string*)mysticMansion));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x3FB3F, BitConverter.GetBytes((int)(string*)mysticMansion));
                
                tempStr = Map["SNG_STG12B.adx"];
                mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x109103, BitConverter.GetBytes((int)(string*)mysticMansion));
                
                tempStr = Map["SNG_STG12C.adx"];
                mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x109131, BitConverter.GetBytes((int)(string*)mysticMansion));
                
                tempStr = Map["SNG_STG12D.adx"];
                mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x10915F, BitConverter.GetBytes((int)(string*)mysticMansion));
                
                tempStr = Map["SNG_STG12E.adx"];
                mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x109185, BitConverter.GetBytes((int)(string*)mysticMansion));
                
                tempStr = Map["SNG_STG12F.adx"];
                mysticMansion = (IntPtr)(string*)Marshal.AllocHGlobal(tempStr.Length + 1).ToPointer();
                Memory.Instance.SafeWrite((UIntPtr)mysticMansion, Encoding.ASCII.GetBytes((tempStr + '\0').ToArray()));
                Memory.Instance.SafeWrite(Mod.ModuleBase + 0x1091AB, BitConverter.GetBytes((int)(string*)mysticMansion));
                */
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }



    public unsafe void HandleBGMFilePathHook(int filePathAddr)
    {
        try
        {
            byte[] filePath = new byte[256];
            Marshal.Copy(filePathAddr, filePath, 0, filePath.Length);
            
            var oldFileFullPath = Encoding.ASCII.GetString(filePath, 0, filePath.Length).Trim('\0');
            
            
            
            //Console.WriteLine($"HandleBGMFilePathHook oldFileFullPath: {oldFileFullPath}");
            
            var success = Map.TryGetValue(oldFileFullPath, out var newName);
            if (!success)
                return;
            //Console.WriteLine($"HandleBGMFilePathHook Success: newName: {newName}");
            newName += '\0';
            var newNameBytes = Encoding.ASCII.GetBytes(newName.ToArray());
            for (var i = 0; i < newName.Length; i++)
                *(byte*)(filePathAddr + i) = newNameBytes[i];
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}