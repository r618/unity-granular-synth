using UnityEngine;

public class PositionView : MonoBehaviour
{
	public float position = 0f;
	public float width = 0f;

	void Update()
	{
		var localPos = new Vector3(3.4f * (position + width / 2f) - 1.7f, this.transform.localPosition.y, this.transform.localPosition.z);
		this.transform.localPosition = localPos;
		var localSc = new Vector3(3.4f * width, this.transform.localScale.y, this.transform.localScale.z);
		this.transform.localScale = localSc;
	}
}