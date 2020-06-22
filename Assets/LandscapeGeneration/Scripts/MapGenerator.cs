using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

	public enum DrawMode {NoiseMap, HeightMap, Mesh};
	public DrawMode drawMode;

	//public int mapSize;
	 int mapSize = 230;
	//public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0, 1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public float meshHightMultiplier;
	public AnimationCurve meshHeightCurve;

	[Header("Erosion Settings")] // заголовок меню редактора
	public int numErosionIterations = 50000; // количество итераций эрозионного процесса

	Erosion erosion; // объект класса Erosion

	public bool autoUpdate;

	public TerrainType[] regions;

	public void GenerateMap()
	{
		float[] noiseMap = Noise.GenerateNoiseMap(mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset);

		Color[] colourMap = new Color[mapSize * mapSize];

		for (int y = 0; y < mapSize; y++)
		{
			for (int x = 0; x < mapSize; x++)
			{
				float currentHeight = noiseMap[y * mapSize + x];
				for (int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						colourMap[y * mapSize + x] = regions[i].colour;
						break;
					}
				}
			}
		}

		MapDisplay display = FindObjectOfType<MapDisplay>();
		if (drawMode == DrawMode.NoiseMap)
		{
			//display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
			display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHightMultiplier, meshHeightCurve), TextureGenerator.TextureFromHeightMap(noiseMap));
		}
		else if (drawMode == DrawMode.HeightMap)
		{
			//display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap, mapSize));
			display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColourMap(colourMap, mapSize));
		}
		else if (drawMode == DrawMode.Mesh)
		{
			
			//display.DrawMeshLand(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHightMultiplier, meshHeightCurve));
			display.DrawMeshLand(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColourMapShade(colourMap, mapSize));
		}


	}

	public void Erode()
	{
		//map = FindObjectOfType<Noise>().Generate(mapSize); // генерация шума на основе параметра mapSize
		float[] noiseMap = Noise.GenerateNoiseMap(mapSize, seed, noiseScale, octaves, persistance, lacunarity, offset);
		Color[] colourMap = new Color[mapSize * mapSize];

		for (int y = 0; y < mapSize; y++)
		{
			for (int x = 0; x < mapSize; x++)
			{
				float currentHeight = noiseMap[y * mapSize + x];
				for (int i = 0; i < regions.Length; i++)
				{
					if (currentHeight <= regions[i].height)
					{
						colourMap[y * mapSize + x] = regions[i].colour;
						break;
					}
				}
			}
		}

		erosion = FindObjectOfType<Erosion>(); // инициализация объекта класса erosion
		erosion.Erode(noiseMap, mapSize, numErosionIterations, true); // реализация функции эрозии
	    //GenerateMesh(); // генерация меша
		MapDisplay display = FindObjectOfType<MapDisplay>();
		display.DrawMeshLand(MeshGenerator.GenerateTerrainMesh(noiseMap, meshHightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColourMap(colourMap, mapSize));
	}

	void OnValidate()
	{
		if (mapSize < 1)
		{
			mapSize = 1;
		}
		if (mapSize < 1)
		{
			mapSize = 1;
		}
		if (lacunarity < 1)
		{
			lacunarity = 1;
		}
		if (octaves < 0)
		{
			octaves = 0;
		}
	}

}

[System.Serializable]
public struct TerrainType
{
	public string name;
	public float height;
	public Color colour;
}
