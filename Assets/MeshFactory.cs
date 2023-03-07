using UnityEngine;

public class MeshFactory : MonoBehaviour
{
    [SerializeField] private Material material;

    public void CreateSquare(Vector3 localPosition, Vector3 scale)
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6] { 0, 1, 2, 2, 1, 3 };

        vertices[0] = new Vector3(0, 1);
        vertices[1] = new Vector3(1, 1);
        vertices[2] = new Vector3(0, 0);
        vertices[3] = new Vector3(1, 0);

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        GameObject go = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));
        go.transform.localPosition = localPosition;
        go.transform.localScale = scale;
        go.GetComponent<MeshFilter>().mesh = mesh;
        go.GetComponent<MeshRenderer>().material = material;
    }
}
