using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter),typeof(MeshRenderer))]
public class ProceduralGrid : MonoBehaviour {
	Mesh mesh;
	Vector3[] vertices;
	int[] triangles;

	//grid settings
	public Vector3 gridoffset;
	float[] coo;

	// Use this for initialization
	void Awake(){
		mesh = GetComponent<MeshFilter> ().mesh;
	}
	void Start(){
		coo = UDPReceive.ins.cooInt;
		MakeProceduralGrid();
		UpdateMesh();
	}


	void MakeProceduralGrid(){


		//get number of triangles
		int cellCount = coo.Length/9;

		//set array sizes
		vertices = new Vector3[cellCount * 3];
		triangles = new int[cellCount * 3];


		//set tracker integers
		int v = 0;
		int t = 0;

		for(int i = 0; i < cellCount ; i++)
		{
				//populate the vertices and triangles arrays
			vertices [v] = new Vector3 (coo[i*9], coo[i*9+2], coo[i*9+1]) + gridoffset;
			vertices [v+1] = new Vector3 (coo[i*9+3], coo[i*9+5], coo[i*9+4]) + gridoffset;
			vertices [v+2] = new Vector3 (coo[i*9+6], coo[i*9+8], coo[i*9+7]) + gridoffset;

				triangles [t] = v+2;
				triangles [t+1] = v+1;
				triangles [t+2] = v;
				v += 3;
				t += 3;
			}

		}


	void UpdateMesh(){
		mesh.Clear ();

		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals ();

	}
	
	// Update is called once per frame
	void Update() {
		coo = UDPReceive.ins.cooInt;
		MakeProceduralGrid();
		UpdateMesh();
		//print(">> " + UDPReceive.ins.textJoin);
	}
}
