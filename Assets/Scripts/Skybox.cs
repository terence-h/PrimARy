using UnityEngine;

public class Skybox : MonoBehaviour
{
    // From: https://www.youtube.com/watch?v=HEHn4EUUyBk

    // Start is called before the first frame update
    void Start()
    {
        // Get the mesh of the skybox sphere
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        // Get the normals of the skybox sphere
        Vector3[] normals = mesh.normals;

        // Loop every normal
        for (int i = 0; i < normals.Length; i++)
            normals[i] = -1 * normals[i]; // Multiply the normal by -1 inverts the normal

        // Set the normals to the inverted normals
        // We are not done yet as the triangles in the mesh is still in clockwise order
        mesh.normals = normals;

        // Loop through mesh again
        for (int i = 0; i < mesh.subMeshCount; i++)
        {
            // Get the triangles of the mesh
            int[] tris = mesh.GetTriangles(i);

            // Loop every triangles (+3 cause triangle has 3 sides)
            for (int j = 0; j < tris.Length; j += 3)
            {
                // Swap order of tri vertices
                // Basically putting the 1st triangle of the loop to the back
                // Thus, making it anti-clockwise
                int temp = tris[j];
                tris[j] = tris[j + 1];
                tris[j + 1] = temp;
            }

            // Set the triangles to the anti-clockwise triangles
            mesh.SetTriangles(tris, i);
        }
    }
}