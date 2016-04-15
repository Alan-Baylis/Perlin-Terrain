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

    public float xSpeed = 0.001f;
    public float ySpeed = 0.0005f;

	void Start () {
        texture = new Texture2D(size, size);
        texture.filterMode = FilterMode.Bilinear;

        textureRenderer = GetComponent<MeshRenderer>();
        textureRenderer.material.mainTexture = texture;
        GenerateClouds(size, size);
	}
	
	void Update () {
        textureRenderer.material.mainTextureOffset = new Vector2(textureRenderer.material.mainTextureOffset.x + xSpeed,
                                                                 textureRenderer.material.mainTextureOffset.y + ySpeed);
        textureRenderer.transform.localScale = new Vector3(texture.width, 1.0f, texture.height);
    }

    private void GenerateClouds(int width, int height) {
        float[,] noiseMap = Noise.GenerateNoiseMap(width, height, seed, scale, octaves, persistance, lacunarity, offset);
        Color[] colorMap = new Color[width * height];
        float noiseValue;

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                noiseValue = noiseMap[y, x];
                noiseValue = Mathf.Clamp(noiseValue, contrastLow, contrastHigh + contrastLow) - contrastLow;
                noiseValue = Mathf.Clamp(noiseValue, 0.0f, 1.0f);

                float r = Mathf.Clamp(cloudColor.r + brightnessOffset, 0.0f, 1.0f);
                float g = Mathf.Clamp(cloudColor.g + brightnessOffset, 0.0f, 1.0f);
                float b = Mathf.Clamp(cloudColor.b + brightnessOffset, 0.0f, 1.0f);

                colorMap[width * y + x] = new Color(r, g, b, noiseValue);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();
    }
}
