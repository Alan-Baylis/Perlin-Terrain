using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject terrainPanel;
    public GameObject cloudsPanel;

    private MapGenerator mapGenerator;
    private Clouds cloudGenerator;
    private bool menuOpened;
    
	void Start () {
        mapGenerator = GetComponent<MapGenerator>();
        cloudGenerator = GameObject.Find("Clouds").GetComponent<Clouds>();
        menuOpened = false;
	}

    public void MenuButtonPressed() {
        if (menuOpened) {
            terrainPanel.SetActive(false);
            cloudsPanel.SetActive(false);
        } else {
            terrainPanel.SetActive(true);
            cloudsPanel.SetActive(false);
        }
        menuOpened = !menuOpened;
    }

    public void TerrainButtonPressed() {
        menuOpened = true;
        terrainPanel.SetActive(true);
        cloudsPanel.SetActive(false);
    }

    public void CloudsButtonPressed() {
        menuOpened = true;
        terrainPanel.SetActive(false);
        cloudsPanel.SetActive(true);
    }
	
    /****************************************************************************************************************************
        TERRAIN SECTION
    ****************************************************************************************************************************/
	public void ScaleEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.noiseScale = float.Parse(input.text);
        mapGenerator.GenerateMap();
    }

    public void OctavesEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.octaves = int.Parse(input.text);
        mapGenerator.GenerateMap();
    }

    public void PersistanceEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.persistance = float.Parse(input.text);
        mapGenerator.GenerateMap();
    }

    public void LacunarityEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.lacunarity = float.Parse(input.text);
        mapGenerator.GenerateMap();
    }

    public void SeedEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.seed = int.Parse(input.text);
        mapGenerator.GenerateMap();
    }

    public void OffsetXEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.offset = new Vector2(float.Parse(input.text), mapGenerator.offset.y);
        mapGenerator.GenerateMap();
    }

    public void OffsetYEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.offset = new Vector2(mapGenerator.offset.x, float.Parse(input.text));
        mapGenerator.GenerateMap();
    }

    public void HeightEdited(InputField input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.heightMultiplier = float.Parse(input.text);
        mapGenerator.GenerateMap();
    }

    public void LodEdited(Slider input) {
        mapGenerator.SetRecompute(true);
        mapGenerator.lod = (int)input.value;
        mapGenerator.GenerateMap();
    }

    /****************************************************************************************************************************
        Clouds SECTION
    ****************************************************************************************************************************/

    public void CloudScaleEdited(InputField input) {
        cloudGenerator.scale = float.Parse(input.text);
    }

    public void CloudOctavesEdited(InputField input) {
        cloudGenerator.octaves = int.Parse(input.text);
    }

    public void CloudPersistanceEdited(InputField input) {
        cloudGenerator.persistance = float.Parse(input.text);
    }

    public void CloudLacunarityEdited(InputField input) {
        cloudGenerator.lacunarity = float.Parse(input.text);
    }

    public void CloudSeedEdited(InputField input) {
        cloudGenerator.seed = int.Parse(input.text);
    }

    public void CloudOffsetXEdited(InputField input) {
        cloudGenerator.offset = new Vector2(float.Parse(input.text), mapGenerator.offset.y);
    }

    public void CloudOffsetYEdited(InputField input) {
        cloudGenerator.offset = new Vector2(mapGenerator.offset.x, float.Parse(input.text));
    }

    public void CloudContrastLowEdited(InputField input) {
        cloudGenerator.contrastLow = float.Parse(input.text);
    }

    public void CloudContrastHighEdited(InputField input) {
        cloudGenerator.contrastHigh = float.Parse(input.text);
    }

    public void CloudBrightnessEdited(Slider input) {
        cloudGenerator.brightnessOffset = input.value;
        cloudGenerator.SetShouldUpdate(true);
    }

    public void CloudSpeedXEdited(InputField input) {
        cloudGenerator.xSpeed = float.Parse(input.text);
    }

    public void CloudSpeedYEdited(InputField input) {
        cloudGenerator.ySpeed = float.Parse(input.text);
    }

    public void CloudColorREdited(Slider input) {
        Color old = cloudGenerator.cloudColor;
        cloudGenerator.cloudColor = new Color(input.value, old.g, old.b);
        cloudGenerator.SetShouldUpdate(true);
    }

    public void CloudColorGEdited(Slider input) {
        Color old = cloudGenerator.cloudColor;
        cloudGenerator.cloudColor = new Color(old.r, input.value, old.b);
        cloudGenerator.SetShouldUpdate(true);
    }

    public void CloudColorBEdited(Slider input) {
        Color old = cloudGenerator.cloudColor;
        cloudGenerator.cloudColor = new Color(old.r, old.g, input.value);
        cloudGenerator.SetShouldUpdate(true);
    }

}
