using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
	public Renderer textureRender;
	public MeshFilter meshFilter;
	public MeshRenderer meshRenderer;

	//public Renderer rend;
	Shader shader1;
	Shader shader2;

	//public Material[] mat;




	//shader1 = new Shader(Shader.Find("UI/Default"));

	public void DrawTexture(Texture2D texture)
	{
		textureRender.sharedMaterial.mainTexture = texture;
		textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);
	}

	public void DrawMesh(MeshData meshData, Texture2D texture)
	{
		//rend = GetComponent<Renderer>();
		shader1 = Shader.Find("Standard");
		//var invincibleShader = Shader.Find("Terrain");
		meshFilter.sharedMesh = meshData.CreateMesh();
		meshRenderer.sharedMaterial.mainTexture = texture;
		//textureRender.material.shader = invincibleShader;
		textureRender.material.shader = shader1;
		meshRenderer.sharedMaterial.shader = shader1;

	}
	public void DrawMeshLand(MeshData meshData, Texture2D texture)
	{
		//Renderer rend;
		//rend = GetComponent<Renderer>();
		shader1 = Shader.Find("Standard");
		shader2 = Shader.Find("Custom/Terrain");
		//var invincibleShader = Shader.Find("Terrain");
		meshFilter.sharedMesh = meshData.CreateMesh();
		//textureRender.sharedMaterial.shader = shader2;
		meshRenderer.sharedMaterial.shader = shader2;


	}



}
