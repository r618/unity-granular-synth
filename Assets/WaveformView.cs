using UnityEngine;

public class WaveformView : MonoBehaviour
{

	public AudioClip clip;
	public int resolution = 256;

	MeshFilter meshFilter;

	void Awake()
	{
		meshFilter = GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
	}

	void Start()
	{
		var samples = new float[clip.channels * clip.samples];
		clip.GetData(samples, 0);

		var vertices = new Vector3[resolution];
		for (var i = 0; i < resolution; i++)
		{
			var level = samples[samples.Length * i / resolution];
			vertices[i] = new Vector3(3.4f / (float)resolution * i - 1.7f, level / 2, 0);
		}
		meshFilter.mesh.vertices = vertices;

		var indices = new int[resolution];
		for (var i = 0; i < resolution; i++)
		{
			indices[i] = i;
		}
		meshFilter.mesh.SetIndices(indices, MeshTopology.LineStrip, 0);
	}
}