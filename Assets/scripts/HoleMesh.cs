using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class HoleMesh : MonoBehaviour {
	
	public Mesh mesh;

	Vector3[] vertices;
	int[] triangles;

	void Awake() {
		mesh = GetComponent<MeshFilter> ().mesh;
	}

	// Use this for initialization
	void Start () {
		MakeMesh ();
	}

	void MakeMesh () {
		vertices = new Vector3[] { 
			new Vector3 (1, 0, 0), new Vector3 (0, 0, 0), new Vector3 (0, 0, 1), 
			new Vector3 (0, 0, 2), new Vector3 (1, 0, 2), new Vector3 (2, 0, 2),   
			new Vector3 (2, 0, 1), new Vector3 (2, 0, 0), new Vector3 (1, 0, 0)


		};
					
		triangles = new int[] {
			0, 1, 2, 
			2, 3, 4,
			4, 5, 6,
			6, 7, 8
		};

		mesh.Clear();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.RecalculateNormals();
	}
}
