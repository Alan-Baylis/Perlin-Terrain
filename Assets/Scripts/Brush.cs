using UnityEngine;
using System.Collections;

public class Brush : MonoBehaviour {

    private MapGenerator mapGenerator;
    public int brushSize = 10;
    public float increaseAmount = 0.01f;
    public float decreaseAmount = 0.01f;

    void Start() {
        mapGenerator = GameObject.Find("Map Generator").GetComponent<MapGenerator>();
    }

	void Update () {
	    if (Input.GetMouseButton(0) && !Input.GetKey(KeyCode.LeftAlt)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            LayerMask terrainMask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(mouseRay, out rayHit, 10000.0f, terrainMask)) {
                float width = rayHit.collider.bounds.size.x;
                int tx = (int)((rayHit.point.x + (width / 2.0f)) / width * 241);
                int ty = 241 - (int)((rayHit.point.z + (width / 2.0f)) / width * 241);
                IncreaseHeight(tx, ty); 
            }
        } else if (Input.GetMouseButton(1) && !Input.GetKey(KeyCode.LeftAlt)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayHit;
            LayerMask terrainMask = LayerMask.GetMask("Terrain");
            if (Physics.Raycast(mouseRay, out rayHit, 10000.0f, terrainMask)) {
                float width = rayHit.collider.bounds.size.x;
                int tx = (int)((rayHit.point.x + (width / 2.0f)) / width * 241);
                int ty = 241 - (int)((rayHit.point.z + (width / 2.0f)) / width * 241);
                DecreaseHeight(tx, ty);
            }
        }
	}

    private void IncreaseHeight(int tx, int ty) {
        float[,] nm = mapGenerator.GetNoiseMap();
        for (int i = -brushSize; i < brushSize; ++i) {
            for (int j = -brushSize; j < brushSize; ++j) {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= brushSize) {
                    if (tx + i >= 0 && tx + i < 241 && ty + j >= 0 && ty + j < 241) {
                        int sum = Mathf.Abs(i) + Mathf.Abs(j);

                        float inc = increaseAmount * ((float)(brushSize - sum) / brushSize);
                        nm[tx + i, ty + j] += inc;
                        if (nm[tx + i, ty + j] > 1.0f) {
                            nm[tx + i, ty + j] = 1.0f;
                        }
                    }
                }
            }
        }

        mapGenerator.SetNoiseMap(nm);
        mapGenerator.GenerateMap();
    }

    private void DecreaseHeight(int tx, int ty) {
        float[,] nm = mapGenerator.GetNoiseMap();
        for (int i = -brushSize; i < brushSize; ++i) {
            for (int j = -brushSize; j < brushSize; ++j) {
                if (Mathf.Abs(i) + Mathf.Abs(j) <= brushSize) {
                    if (tx + i >= 0 && tx + i < 241 && ty + j >= 0 && ty + j < 241) {
                        int sum = Mathf.Abs(i) + Mathf.Abs(j);

                        float dec = decreaseAmount * ((float)(brushSize - sum) / brushSize);
                        nm[tx + i, ty + j] -= dec;
                        if (nm[tx + i, ty + j] < 0.0f) {
                            nm[tx + i, ty + j] = 0.0f;
                        }
                    }
                }
            }
        }

        mapGenerator.SetNoiseMap(nm);
        mapGenerator.GenerateMap();
    }
}
