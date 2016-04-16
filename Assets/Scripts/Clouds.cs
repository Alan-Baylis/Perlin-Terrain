using UnityEngine;
using System.Collections;

public class Clouds : MonoBehaviour {

    private Texture2D texture;
    private Renderer textureRenderer;

    private int size = 241;

    public Color cloudColor = Color.white;

    public float scale = 5.0f;
    public int octaves = 6;
    public float persistance = 0.6f;
    public float lacunarity = 2.0f;
    public int seed = 0;
    public Vector2 offset;

    public float contrastLow = 0.0f;
    public float contrastHigh = 1.0f;
    public float brightnessOffset = 0.0f;

    public float xSpeed = 0.1f;
    public float ySpeed = 0.05f;

    private Color[] colorMap;
    private float r, g, b;
    private bool canOffset = true;

	void Start () {
        texture = new Texture2D(size, size);
        texture.filterMode = FilterMode.Bilinear;

        textureRenderer = GetComponent<MeshRenderer>();
        textureRenderer.material.mainTexture = texture;

        colorMap = new Color[size * size];
        r = Mathf.Clamp(cloudColor.r + brightnessOffset, 0.0f, 1.0f);
        g = Mathf.Clamp(cloudColor.g + brightnessOffset, 0.0f, 1.0f);
        b = Mathf.Clamp(cloudColor.b + brightnessOffset, 0.0f, 1.0f);
    }
	
	void Update () {
        if (canOffset) {
            canOffset = false;
            offset = new Vector2(offset.x + xSpeed, offset.y + ySpeed);
            StartCoroutine(GenerateClouds(size, size));
        }
    }

    IEnumerator GenerateClouds(int width, int height) {
        yield return null;
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lacunarity, offset);
        float noiseValue;

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                noiseValue = noiseMap[y, x];
                noiseValue = Mathf.Clamp(noiseValue, contrastLow, contrastHigh + contrastLow) - contrastLow;
                noiseValue = Mathf.Clamp(noiseValue, 0.0f, 1.0f);
                colorMap[width * y + x] = new Color(r, g, b, noiseValue);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();
        canOffset = true;
    }
}
