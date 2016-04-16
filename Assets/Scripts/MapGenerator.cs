using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    public enum DrawMode { Noise, Color, Mesh };
    public DrawMode drawMode;

    const int mapSize = 241;
    [Range(0, 6)] public int lod;
    public float noiseScale;

    public int octaves;
    [Range(0.0f, 1.0f)] public float persistance;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float heightMultiplier;
    public AnimationCurve heightCurve;

    public bool autoUpdate;

    public TerrainType[] regions;

    private float[,] noiseMap;

    private bool recompute = true;

    // TEMP
    void Start() {
        noiseMap = Noise.GenerateNoiseMap(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
        GenerateMap();
    }

    public void GenerateMap() {
        if (recompute) {
            noiseMap = Noise.GenerateNoiseMap(mapSize, mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
            recompute = false;
        }
        
        Color[] colorMap = new Color[mapSize * mapSize];
        for (int y = 0; y < mapSize; ++y) {
            for (int x = 0; x < mapSize; ++x) {
                float currentHeight = noiseMap[x, y];
                for (int i=0; i<regions.Length; ++i) {
                    if (currentHeight <= regions[i].height) {
                        colorMap[mapSize * y + x] = regions[i].color;
                        break;
                    }
                }
            }
        }

        MapDisplay display = FindObjectOfType<MapDisplay>();
        if (drawMode == DrawMode.Noise) {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        } else if (drawMode == DrawMode.Color) {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapSize, mapSize));
        } else if (drawMode == DrawMode.Mesh) {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, heightCurve, lod), TextureGenerator.TextureFromColorMap(colorMap, mapSize, mapSize));
        }
        
    }

    public void SetNoiseMap(float [,] noiseMap) {
        this.noiseMap = noiseMap;
    }

    public float[,] GetNoiseMap() {
        return noiseMap;
    }

    void OnValidate() {
        if (lacunarity < 1) {
            lacunarity = 1;
        }

        if (octaves < 0) {
            octaves = 0;
        }

        if (persistance < 0.0f || persistance > 1.0f) {
            persistance = 0.5f;
        }

        if (lod < 0 || lod > 6) {
            lod = 0;
        }

    }

    public void SetRecompute(bool value) {
        recompute = value;
    }
}

[System.Serializable]
public struct TerrainType {
    public string name;
    public float height;
    public Color color; 
}
