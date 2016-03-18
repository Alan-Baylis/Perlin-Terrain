using UnityEngine;
using System.Collections;

public static class MeshGenerator {

    public static MeshData GenerateTerrainMesh(float[,] heightMap, float heightMultiplier, AnimationCurve heightCurve, int lod) {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topLeftX = (width - 1) / -2.0f;
        float topLeftZ = (height - 1) / 2.0f;

        int lodIncrement = (lod == 0) ? 1 : lod * 2;
        int verticesPerWidth = (width - 1) / lodIncrement + 1;

        MeshData meshData = new MeshData(verticesPerWidth, verticesPerWidth);
        int vertexIndex = 0;

        for (int y = 0; y < height; y += lodIncrement) {
            for (int x = 0; x < width; x += lodIncrement) {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[x, y]) * heightMultiplier, topLeftZ - y);
                meshData.uvs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);

                if (x < width - 1 && y < height - 1) {
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerWidth + 1, vertexIndex + verticesPerWidth);
                    meshData.AddTriangle(vertexIndex + verticesPerWidth + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;
    }

}

public class MeshData {
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] uvs;

    int triangleIndex;

    public MeshData(int width, int height) {
        vertices = new Vector3[width * height];
        triangles = new int[(width-1) * (height-1) * 6];
        uvs = new Vector2[width * height];
    }

    public void AddTriangle(int v1, int v2, int v3) {
        triangles[triangleIndex] = v1;
        triangles[triangleIndex + 1] = v2;
        triangles[triangleIndex + 2] = v3;
        triangleIndex += 3;
    }

    public Mesh CreateMesh() {
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
        return mesh;
    }
}
