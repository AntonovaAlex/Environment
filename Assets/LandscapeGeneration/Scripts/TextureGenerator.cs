using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TextureGenerator {

	public static Texture2D TextureFromColourMap (Color[] colourMap, int mapSize)
	{
		int width = mapSize;
		int height = mapSize;

		Texture2D texture = new Texture2D(width, height);
		//texture.filterMode = FilterMode.Point;
		//texture.wrapMode = TextureWrapMode.Clamp;

		texture.SetPixels(colourMap);
		texture.Apply();
		return texture;
	}

	public static Texture2D TextureFromColourMapShade(Color[] colourMap, int mapSize)
	{
		int width = mapSize;
		int height = mapSize;

		Texture2D texture = new Texture2D(width, height);
		//texture.filterMode = FilterMode.Point;
		//texture.wrapMode = TextureWrapMode.Clamp;

		texture.SetPixels(colourMap);

	    //Renderer rend = new Renderer;
	    //Shader shader1;
	    //Shader shader2;
	    //rend = GetComponent<Renderer>();
		//shader1 = Shader.Find("Standard");
		//shader2 = Shader.Find("Custom/Terrain");
		//rend.sharedMaterial.shader = shader2;


		texture.Apply();
		return texture;
	}

	public static Texture2D TextureFromHeightMap (float[] heightMap)
	{
		int width = (int)Mathf.Sqrt(heightMap.GetLength(0));
		int height = (int)Mathf.Sqrt(heightMap.GetLength(0));

		

		Color[] colourMap = new Color[width * height];
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[y * width + x]);
			}
		}

		return TextureFromColourMap(colourMap, width);
	}

}
