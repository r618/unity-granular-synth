using UnityEngine;

public class GranularSynth : MonoBehaviour
{
	public AudioClip clip;

	public int playbackSpeed = 1;
    public int grainSize = 1000;
    public int grainStep = 1;

	int sampleLength;
	float[] samples;

	int position = 0;
	float interval = 0;

	void Awake()
	{
		sampleLength = clip.samples;
		samples = new float[clip.samples * clip.channels];
		clip.GetData(samples, 0);
	}

	void Update()
	{
		var cursor = FindObjectOfType<PositionView>();
		cursor.position = 1.0f / sampleLength * position;
		cursor.width = 1.0f / sampleLength * interval * playbackSpeed;
	}

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(16, 16, Screen.width - 32, Screen.height - 32));
		GUILayout.FlexibleSpace();
		GUILayout.Label("Playback Speed: " + playbackSpeed);
		this.playbackSpeed = (int)GUILayout.HorizontalSlider(this.playbackSpeed, -4.0f, 4.0f);
		GUILayout.FlexibleSpace();
		GUILayout.Label("Grain Size: " + grainSize);
		this.grainSize = (int)GUILayout.HorizontalSlider(this.grainSize, 2.0f, 10000.0f);
		GUILayout.FlexibleSpace();
		GUILayout.Label("Grain Step: " + grainStep);
		this.grainStep = (int)GUILayout.HorizontalSlider(this.grainStep, -3000.0f, 3000.0f);
		GUILayout.FlexibleSpace();
		if (GUILayout.Button("RANDOMIZE!"))
		{
            this.playbackSpeed = Random.Range(-2, 2);
            this.grainSize = Random.Range(200, 1000);
            this.grainStep = Random.Range(-1500, 1500);
		}
		GUILayout.FlexibleSpace();
		GUILayout.EndArea();

		if (playbackSpeed == 0) playbackSpeed = 1;
	}

	void OnAudioFilterRead(float[] data, int channels)
	{
		for (var i = 0; i < data.Length; i += 2) {
			data[i] = samples[position * 2];
			data[i + 1] = samples[position * 2 + 1];

			if (--interval <= 0) {
				interval = grainSize;
				position += grainStep;
			} else {
				position += playbackSpeed;
			}

			while (position >= sampleLength) {
				position -= sampleLength;
			}
			while (position < 0) {
				position += sampleLength;
			}
		}
	}
}