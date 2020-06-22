using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class MapGeneratorEditor : Editor {

	public override void OnInspectorGUI()
	{
		MapGenerator mapGen = (MapGenerator)target;

		if(DrawDefaultInspector())
		{
			if (mapGen.autoUpdate)
			{
				mapGen.GenerateMap();
			}
		}

		if (GUILayout.Button("Generate"))
		{
			mapGen.GenerateMap();
		}

		if (GUILayout.Button("Erode (" + mapGen.numErosionIterations + " iterations)"))
		{
			//var sw = new System.Diagnostics.Stopwatch();
			//sw.Start();
			mapGen.Erode();
			//sw.Stop();
			//Debug.Log($"Erosion finished ({m.numErosionIterations} iterations; {sw.ElapsedMilliseconds}ms)");
		}
	}
}
