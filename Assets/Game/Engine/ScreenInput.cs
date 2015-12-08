using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// Deal with the fingers data to get horizontal moving factor, swipe data ant soon others :)
public class ScreenInput : MonoBehaviour {
	public struct Tap {
		/// position of the tap on the screen in pixels
		public Vector2 screenPos;
		/// position of the tap on the GUI in weird unity shitty canvas "pixels"
		public Vector2 canvasPos;
		public Tap(Vector2 screenPos, Vector2 canvasPos) {
			this.screenPos = screenPos;
			this.canvasPos = canvasPos;
		}
	}
	private struct SwipeContent {
		public Vector2 pos;
		public float time;
		public SwipeContent(Vector2 pos, float time) {
			this.pos = pos;
			this.time = time;
		}
	}

	// moving buttons parameters
	private RawImage leftArrow;
	private RawImage rightArrow;
	private RawImage jumpButton;

	// swipe parameters
	public int k = 10;
	private const float minSwipeDistSq = 5f;


	// screen input (script output)
	/// takes value -1, 0 or 1 depending on the user pressing left or right button.
	public static float horizontal { get; private set; }	
	public static bool hasSwipe { get; private set; }
	/// direction and intensity of the swipe.
	public static Vector2 swipe { get; private set; }		
	/// base position of the swipe (where the finger was before moving)
	public static Vector2 swipeBase { get; private set; }	
	/// true if the jump button was pressed during that frame
	public static bool jumpDown { get; private set; }
	/// used to prevent new touches to be processed into control output (right left arrows, jump button, swipe...)
	public static bool userControl { get { return userControlPr; } set { userControlPr = value;  horizontal = 0; } }
	/// taps (screen click with a finger) that occured the current frame and havent been processed by the GUI
	public static List<Tap> taps { get; private set; }


	private List<SwipeContent> lastSwipes = new List<SwipeContent>();
	private int swipeTouchId = -1;
	private int movingTouchId = -1;
	private Vector2 screenToCanvasRatio;
	private Canvas canvas;
	private Rect leftRect, rightRect, jumpRect;
	private float midMovePosX;			//point in x axis between the left and right arrow
	private static bool userControlPr = true;

	private bool debugSwipeOn = false;
	private bool debugHorizontalOn = false;

	void Start () {
		leftArrow = GameObject.Find("MoveLeftButton").GetComponent<RawImage>();
		rightArrow = GameObject.Find("MoveRightButton").GetComponent<RawImage>();
		jumpButton = GameObject.Find("JumpButton").GetComponent<RawImage>();


		hasSwipe = false;
		horizontal = 0;
		jumpDown = false;
		taps = new List<Tap>();
		canvas = leftArrow.canvas;
		leftRect = extractAbsoluteRect(leftArrow.rectTransform);
		rightRect = extractAbsoluteRect(rightArrow.rectTransform);
		jumpRect = extractAbsoluteRect(jumpButton.rectTransform);
		midMovePosX = (leftRect.xMax + rightRect.xMin)*0.5f;
	}
	
	void Update () {
		jumpDown = Input.GetKeyDown(KeyCode.Space) && userControl;//false;
		taps.Clear();
		foreach (Touch touch in Input.touches)
		{
			Vector2 posOnCanvas = screenToCanvas(touch.position);
			switch (touch.phase)
			{
			case TouchPhase.Began :
				if (userControl && leftRect.Contains(posOnCanvas)) {
					movingTouchId = touch.fingerId;
					horizontal = -1;
				} else if (userControl && rightRect.Contains(posOnCanvas)) {
					movingTouchId = touch.fingerId;
					horizontal = 1;	
				} else if (userControl && jumpRect.Contains(posOnCanvas)) {
					jumpDown = true;
				} else {
					swipeTouchId = touch.fingerId;
					swipeBase = touch.position;
				}
				break;
			case TouchPhase.Moved :
				if (touch.fingerId == movingTouchId) {
					if (userControl) {
						horizontal = posOnCanvas.x > midMovePosX ? 1 : -1;
					}
				} else if (touch.fingerId == swipeTouchId) {
					if (userControl) {
						lastSwipes.Insert (0, new SwipeContent(touch.position, Time.time ));
						if (lastSwipes.Count > 1) {
							int avOver = Mathf.Max (k, lastSwipes.Count);
							Vector2 gestureDiff = lastSwipes[0].pos - lastSwipes[avOver-1].pos;
							float gestureTime = lastSwipes[0].time - lastSwipes[avOver-1].time;
							hasSwipe = true; //(gestureTime < maxSwipeTime) && (gestureDiff.sqrMagnitude > minSwipeDistSq);
							swipe = gestureDiff / gestureTime;
							if (lastSwipes.Count > k) {
								lastSwipes.RemoveRange(k, lastSwipes.Count);
							}
						}
					}
				}
				break;				
			case TouchPhase.Ended :
				if (touch.fingerId == movingTouchId) {
					horizontal = 0;
					movingTouchId = -1;
				} else if (touch.fingerId == swipeTouchId) {
					hasSwipe = false;
					swipeTouchId = -1;
					lastSwipes.Clear();
				}
				break;
			case TouchPhase.Stationary:
				if (touch.fingerId == swipeTouchId) {
					hasSwipe = false;
				}
				break;
			}
		}

		// tap with click (works for mobile)
		if (Input.GetMouseButtonDown(0)) {
			taps.Add(new Tap(Input.mousePosition, screenToCanvas(Input.mousePosition)));
		}

		if (userControl) {
			// simulate swipe with horizontal and vertical axis
			Vector2 keyBoardInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
			if (keyBoardInput != Vector2.zero) {
				if (Mathf.Abs (keyBoardInput.x) > 0)
					keyBoardInput.x = Mathf.Sign(keyBoardInput.x);
				if (Mathf.Abs (keyBoardInput.y) > 0)
					keyBoardInput.y = Mathf.Sign(keyBoardInput.y);
				hasSwipe = true;
				debugSwipeOn = true;
				swipe = keyBoardInput * 2000;
			} else if (debugSwipeOn) {
				debugSwipeOn = false;
				hasSwipe = false;
			}
			// simulate move with q and e
			bool e = Input.GetKey("e");
			bool q = Input.GetKey("q");
			if (e || q) {
				horizontal = (q ? -1 : 0) + (e ? 1 : 0);
				debugHorizontalOn = true;
			} else if (debugHorizontalOn) {
				horizontal = 0;
			}
		}
	}

	private Vector2 screenToCanvas(Vector2 pos) {
		return new Vector2(
			(pos.x / Screen.width - 0.5f )* canvas.pixelRect.width/AdaptUISize.scale.x,
			(pos.y / Screen.height - 0.5f )* canvas.pixelRect.height/AdaptUISize.scale.y
			);
	}

	public static Rect extractAbsoluteRect(RectTransform rt) {
		Vector2 size = rt.rect.size;
		Vector2 pos = rt.anchoredPosition - size / 2;
		return new Rect(pos, size);
	}

	private Vector2 inverseY(Vector2 v) {
		return new Vector2(v.x, Screen.height - v.y);
	}
}
