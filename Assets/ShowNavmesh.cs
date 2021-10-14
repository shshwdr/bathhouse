using Pool;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShowNavmesh : Singleton<ShowNavmesh>
{
	bool isShowMesh = false;
	void Start()
	{
		EventPool.OptIn("changeItem", ShowMesh);
		//ShowMesh();
	}

	public IEnumerator show()
    {
		yield return new WaitForSeconds(1f);
		// NavMesh.CalculateTriangulation returns a NavMeshTriangulation object.
		NavMeshTriangulation meshData = NavMesh.CalculateTriangulation();

		// Create a new mesh and chuck in the NavMesh's vertex and triangle data to form the mesh.
		Mesh mesh = new Mesh();
		mesh.vertices = meshData.vertices;
		mesh.triangles = meshData.indices;

		// Assigns the newly-created mesh to the MeshFilter on the same GameObject.
		GetComponent<MeshFilter>().mesh = mesh;

	}
	// Generates the NavMesh shape and assigns it to the MeshFilter component.
	public void ShowMesh()
	{

		StartCoroutine(show());
	}

	public void Hide()
    {

		GetComponent<MeshFilter>().mesh = null;
	}

    private void Update()
    {


		if (Input.GetKeyDown(KeyCode.P))
		{
			isShowMesh = !isShowMesh;
            if (isShowMesh)
            {

				ShowMesh();

            }
            else
            {
				GetComponent<MeshFilter>().mesh = null;

			}

		}
    }
}