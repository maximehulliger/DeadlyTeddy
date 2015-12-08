using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
[RequireComponent( typeof(Canvas) )]
public class AdaptUISize : MonoBehaviour {
	private static AdaptUISize instance; 

	public static Vector2 scale { get {return instance.scaleI; } }


	[SerializeField]
	private Rect initialRect;
	[SerializeField]
	private Rect oldRect;
	[SerializeField]
	private RectTransform rTransform;
	[SerializeField]
	private Vector2 scaleI;

	[Tooltip("set the default screen size")]
	public bool reset = false;


	void Start() {
		instance = this;
	}

	// Update is called once per frame
	void Update () {
		if (!rTransform || reset) {
			rTransform = GetComponent<RectTransform>();
			initialRect = rTransform.rect;
			print ("reseted :)");
		}

		if (!rTransform.rect.Equals(oldRect) || reset) {
			reset = false;
			oldRect = rTransform.rect;
			scaleI = Vector2.Scale(oldRect.size, new Vector2(1f/initialRect.width, 1f/initialRect.height));

			for (int i=0; i<transform.childCount; i++) {
				Transform child = transform.GetChild(i);
				child.localScale = scaleI;
			}

		}
	}
}
