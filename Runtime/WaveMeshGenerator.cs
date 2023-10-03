using UnityEngine;

namespace IronMountain.Waves
{
    [RequireComponent(typeof(MeshFilter))]
    public class WaveMeshGenerator : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private int dimensionX = 10;
        [SerializeField] private int dimensionZ = 10;
        [SerializeField] private int densityX = 1;
        [SerializeField] private int densityZ = 1;

        [Header("Cache")]
        private MeshFilter _meshFilter;
        private Mesh _mesh;

        protected Vector3[] Vertices;
        protected int[] Triangles;
        protected Vector2[] Uvs;

        public int DimensionX => dimensionX;
        public int DimensionZ => dimensionZ;

        private MeshFilter MeshFilter
        {
            get
            {
                if (!_meshFilter) _meshFilter = GetComponent<MeshFilter>();
                if (!_meshFilter) _meshFilter = gameObject.AddComponent<MeshFilter>();
                return _meshFilter;
            }
        }

        public void Run()
        {
            _mesh = new Mesh();
            CreateShape();
            FillTriangles();
            FillUVs();
            ApplyMesh();
        }

        protected virtual void CreateShape()
        {
            float halfX = dimensionX / 2f;
            float halfZ = dimensionZ / 2f;
            int cellsX = dimensionX * densityX;
            int cellsZ = dimensionZ * densityZ;
            Vertices = new Vector3[(cellsX + 1) * (cellsZ + 1)];
            for (int i = 0, z = 0; z <= cellsZ; z++)
            {
                for (int x = 0; x <= cellsX; x++)
                {
                    Vertices[i] = new Vector3(
                        Mathf.Lerp(-halfX, halfX, (float) x / cellsX),
                        0,
                        Mathf.Lerp(-halfZ, halfZ, (float) z / cellsZ)
                        );
                    i++;
                }
            }
        }

        protected virtual void FillTriangles()
        {
            int amountX = dimensionX * densityX;
            int amountZ = dimensionZ * densityZ;
            Triangles = new int[amountX * amountZ * 6];
            int vert = 0;
            int tris = 0;

            for (int z = 0; z < amountZ; z++)
            {
                for (int x = 0; x < amountX; x++)
                {
                    Triangles[tris + 0] = vert + 0;
                    Triangles[tris + 1] = vert + amountX + 1;
                    Triangles[tris + 2] = vert + 1;
                    Triangles[tris + 3] = vert + 1;
                    Triangles[tris + 4] = vert + amountX + 1;
                    Triangles[tris + 5] = vert + amountX + 2;

                    vert++;
                    tris += 6;
                }
                vert++;
            }
        }
        
        protected virtual void FillUVs()
        {
            int cellsX = dimensionX * densityX;
            int cellsZ = dimensionZ * densityZ;
            Uvs = new Vector2[(cellsX + 1) * (cellsZ + 1)];
            for (int i = 0, z = 0; z <= cellsZ; z++)
            {
                for (int x = 0; x <= cellsX; x++)
                {
                    Uvs[i] = new Vector2(
                        Mathf.Lerp(0, 1, (float) x / cellsX),
                        Mathf.Lerp(0, 1, (float) z / cellsZ)
                    );
                    i++;
                }
            }
        }

        private void ApplyMesh()
        {
            _mesh.Clear();
     
            _mesh.vertices = Vertices;
            _mesh.triangles = Triangles;
            _mesh.uv = Uvs;

            _mesh.RecalculateNormals();
            
            MeshFilter.mesh = _mesh;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (densityX < 1) densityX = 1;
            if (densityZ < 1) densityZ = 1;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(0f, 1f, 1f, 0.5f);
            Gizmos.DrawCube(transform.position, new Vector3(dimensionX, 0, dimensionZ));
        }

#endif
    }
}
