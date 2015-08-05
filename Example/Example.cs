using UnityEngine;
using System.Collections;
using NTweenPackage;

public class Example : MonoBehaviour {

	public Vector3 dimensions;

	private Vector3 _openDimensions = new Vector3(1,1,1);
	private float _delay = 1.4f;

	void Start() {

		// moves the position to 1,1,1 in 0.5 seconds
		transform.TweenPosition (new Vector3(1,1,1), 0.5f);

		// scales to 2,2,2 in 1 second with a 1.5 second delay using the EaseInSin ease type.
		transform.TweenLocalScale (new Vector3 (2, 2, 2), 1f, new NTweenAttributes ().Delay (1.5f).Ease (NTweenEaseType.EaseInSine));

		// tweens the dimension vector to 1,1,1 and when done runs the DoOnComplete method.
		NTween.Instance.CreateTween (new NTweenBuilder (this, 0.1f, "dimensions", _openDimension), new NTweenAttributes ().Delay (_delay).OnComplete(DoOnComplete));

	}

	public void DoOnComplete() {
		// do on completion of the tween.
	}
}