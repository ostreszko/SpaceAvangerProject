using System.Xml;
using System;
using UnityEngine;
using System.IO;

//Klasa zaczytująca statyski wrogów z XML
public static class StaticDifficulty
{
    static StaticDifficulty()
    {
        GameDifficulty = 1;
        IsXMLValid = true;
        TextAsset xmlFile;
        try
        {
            xmlFile = (TextAsset)Resources.Load("enemiesStats");
            XmlReader reader = XmlReader.Create(new MemoryStream(xmlFile.bytes));
            while (reader.Read())
            {
                //when you find a npc tag do this
                if (reader.IsStartElement("small"))
                {
                    SmallEnemyDamage = System.Convert.ToInt32(reader.GetAttribute("damage"));
                    SmallEnemyFireRateMin = float.Parse(reader.GetAttribute("fireRateMin"));
                    SmallEnemyFireRateMax = float.Parse(reader.GetAttribute("fireRateMax"));
                    SmallEnemyScoreDrop = System.Convert.ToInt32(reader.GetAttribute("scoreDrop"));
                    SmallEnemyHP = System.Convert.ToInt32(reader.GetAttribute("hp"));
                }
                if (reader.IsStartElement("medium"))
                {
                    MediumEnemyDamage = System.Convert.ToInt32(reader.GetAttribute("damage"));
                    MediumEnemyFireRateMin = float.Parse(reader.GetAttribute("fireRateMin"));
                    MediumEnemyFireRateMax = float.Parse(reader.GetAttribute("fireRateMax"));
                    MediumEnemyScoreDrop = System.Convert.ToInt32(reader.GetAttribute("scoreDrop"));
                    MediumEnemyHP = System.Convert.ToInt32(reader.GetAttribute("hp"));
                }
                if (reader.IsStartElement("large"))
                {
                    LargeEnemyDamage = System.Convert.ToInt32(reader.GetAttribute("damage"));
                    LargeEnemyFireRateMin = float.Parse(reader.GetAttribute("fireRateMin"));
                    LargeEnemyFireRateMax = float.Parse(reader.GetAttribute("fireRateMax"));
                    LargeEnemyScoreDrop = System.Convert.ToInt32(reader.GetAttribute("scoreDrop"));
                    LargeEnemyHP = System.Convert.ToInt32(reader.GetAttribute("hp"));
                }
            }
        }
        catch (Exception ex)
        {
            IsXMLValid = false;
            Debug.LogError("XML is missing or invalid, message: " + ex.Message);
        }
    }

    public static float GameDifficulty { get;set;}
    public static bool IsXMLValid { get; set; }
    public static int SmallEnemyDamage { get; set; }
    public static float SmallEnemyFireRateMin { get; set; }
    public static float SmallEnemyFireRateMax { get; set; }
    public static int SmallEnemyScoreDrop { get; set; }
    public static int SmallEnemyHP { get; set; }
    public static int MediumEnemyDamage { get; set; }
    public static float MediumEnemyFireRateMin { get; set; }
    public static float MediumEnemyFireRateMax { get; set; }
    public static int MediumEnemyScoreDrop { get; set; }
    public static int MediumEnemyHP { get; set; }
    public static int LargeEnemyDamage { get; set; }
    public static float LargeEnemyFireRateMin { get; set; }
    public static float LargeEnemyFireRateMax { get; set; }
    public static int LargeEnemyScoreDrop { get; set; }
    public static int LargeEnemyHP { get; set; }

}
