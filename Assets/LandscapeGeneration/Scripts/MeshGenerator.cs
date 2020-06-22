using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MeshGenerator
{

	public static MeshData GenerateTerrainMesh(float[] heightMap, float heightMultiplier, AnimationCurve heightCurve)
	{
		//int width = (int)Mathf.Sqrt(heightMap.GetLength(0));
		//int height = (int)Mathf.Sqrt(heightMap.GetLength(0));
		int mSize = (int)Mathf.Sqrt(heightMap.GetLength(0));
		float topLeftX = (mSize - 1) / -2f;
		float topLeftZ = (mSize - 1) / -2f;

		MeshData meshData = new MeshData(mSize);
		int vertexIndex = 0;

		for (int y = 0; y < mSize; y++)
		{
			for (int x = 0; x < mSize; x++)
			{
				//meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightCurve.Evaluate(heightMap[y * mSize + x]) * heightMultiplier, topLeftZ - y);
				meshData.vertices[vertexIndex] = new Vector3(topLeftX + x, heightMap[y * mSize + x] * heightMultiplier, topLeftZ - y);
				meshData.uvs[vertexIndex] = new Vector2(x / (float)mSize, y / (float)mSize);

				if (x < mSize - 1 && y < mSize - 1)
				{
					meshData.AddTriangle(vertexIndex, vertexIndex + mSize + 1, vertexIndex + mSize); //обозначаем 2 треугольника в каждом квадрате меша
					meshData.AddTriangle(vertexIndex + mSize + 1, vertexIndex, vertexIndex + 1);

				}

				vertexIndex++;
			}
		}

		return meshData;
	}
}

	public class MeshData
{
	public Vector3[] vertices;
	public int[] triangles;
	public Vector2[] uvs;

	int triangleIndex;

	public MeshData(int mSize)
	{
		vertices = new Vector3[mSize * mSize];
		uvs = new Vector2[mSize * mSize];
		triangles = new int[(mSize - 1) * (mSize - 1) * 6];
	}

	public void AddTriangle(int a, int b, int c)
	{
		triangles[triangleIndex] = a;
		triangles[triangleIndex + 1] = b;
		triangles[triangleIndex + 2] = c;
		triangleIndex += 3;
	}

	public Mesh CreateMesh()
	{
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.uv = uvs;
		AssignMeshComponents();
		//Transform transformMesh = mesh.transform;
		//transformMesh.localPosition = Vector3.zero;
		//transform.Translate(0, 0, 0);
		mesh.RecalculateNormals();
		
		//mesh.localPosition = Vector3.zero;

		return mesh;
	}

	void AssignMeshComponents()
	{
		//transform.Translate(0, 0, 0);
		//	// Find/creator mesh holder object in children
		//	string meshHolderName = "Mesh Holder";
		//	Transform meshHolder = transform.Find(meshHolderName);
		//	if (meshHolder == null)
		//	{
		//		meshHolder = new GameObject(meshHolderName).transform;
		//		meshHolder.transform.parent = transform;
		//		meshHolder.transform.localPosition = Vector3.zero;
		//		meshHolder.transform.localRotation = Quaternion.identity;
		//	}

		//	// Ensure mesh renderer and filter components are assigned
		//	if (!meshHolder.gameObject.GetComponent<MeshFilter>())
		//	{
		//		meshHolder.gameObject.AddComponent<MeshFilter>();
		//	}
		//	if (!meshHolder.GetComponent<MeshRenderer>())
		//	{
		//		meshHolder.gameObject.AddComponent<MeshRenderer>();
		//	}

		//	meshRenderer = meshHolder.GetComponent<MeshRenderer>();
		//	meshFilter = meshHolder.GetComponent<MeshFilter>();
	}


}