using UnityEngine;
using System.Collections;

public static class Noise {

    public static float[,] GenerateNoiseMap(int width, int height, float scale) {
        float[,] noiseMap = new float[width, height];

        if (scale <= 0.0f) {
            scale = 0.0001f;
        }

        for (int y = 0; y < height; ++y) {
            for (int x = 0; x < width; ++x) {
                float sampleX = x / scale;
                float sampleY = y / scale;

                noiseMap[x, y] = Mathf.PerlinNoise(sampleX, sampleY);

            }
        }

        return noiseMap;
    }


}
