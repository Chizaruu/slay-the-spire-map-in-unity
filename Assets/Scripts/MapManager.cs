﻿using System.Linq;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapConfig config;
    public MapView view;
    
    private void Start()
    {
        if (PlayerPrefs.HasKey("Map"))
        {
            var mapJson = PlayerPrefs.GetString("Map");
            var map = JsonUtility.FromJson<Map>(mapJson);
            // using this instead of .Contains()
            if (map.path.Any(p => p.Equals(map.GetBossNode().point)))
            {
                // payer has already reached the boss, generate a new map
                GenerateNewMap();
            }
            else
            {
                // player has not reached the boss yet, load the current map
                view.ShowMap(map);
            }
        }
        else
        {
            GenerateNewMap();
        }
    }

    public void GenerateNewMap()
    {
        var map = MapGenerator.GetMap(config);
        Debug.Log(map.ToJson());
        view.ShowMap(map);
    }
}
