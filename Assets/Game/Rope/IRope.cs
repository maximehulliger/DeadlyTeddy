using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class IRope : MonoBehaviour
{
	public GameObject m_ropePointPrefab = null;
	public Transform m_rootPoint = null;
	public Transform m_endPoint = null;

	public float m_airFriction = 1.0f;
	public float m_ropeDamping = 7.0f;
	public float m_ropeStiffness = 800.0f;
	public int maxNbOfPoint = 30;


	public Vector2 rootForce { get; private set; }
	public Vector2 endForce { get; private set; }
	public int minNbOfPoint { get; set; }
	
	private List<Rigidbody2D> m_points = new List<Rigidbody2D>();
	private float pointDiameterSqr;
	private RopeMesh meshGenerator = new RopeMesh();


	void Start ()
	{
		minNbOfPoint = 2;
		m_points.Add (newRopePoint(m_rootPoint.position));
		m_points.Add (newRopePoint(m_endPoint.position));
		meshGenerator.GenerateMesh(GetComponent<MeshFilter>().mesh, m_points.Select(p=>p.transform.localPosition).ToList(), true);
		pointDiameterSqr = sqr (((CircleCollider2D)(m_points[0].GetComponent<Collider2D>())).radius*2*m_points[0].transform.localScale.x);
	}

	private float sqr(float f) {
		return f*f;
	}
	
	void FixedUpdate () 
	{
		ConstraintEndsPoints();
		ApplyAirFriction();
		ApplySpringForces();
	}

	void Update() {
		foreach (Rigidbody2D r in m_points) {
			if (r.transform.position.x != r.position.x || r.transform.position.y != r.position.y) {
				r.transform.position = r.position;
			}
		}

		bool needNewRender = false; // <-> #points changed

		// prevent rope loosing bug (remove point when fixed)
		if (MoveHead.fixedd && m_points.Count > 2) {
			m_points.RemoveRange(1, m_points.Count-2);
			needNewRender = true;
		}

		// remove one superfluous points
		for (int i = 0; i < m_points.Count - 2 && m_points.Count >= minNbOfPoint; i++)
		{
			Rigidbody2D p1 = m_points[i];
			Rigidbody2D p2 = m_points[i + 2];
			
			// if 2 points don't need the one in the middle to be physically linked (+/-)
			Vector3 diff = p1.transform.position - p2.transform.position;
			float distSqr = diff.sqrMagnitude;
			if (distSqr*2f < pointDiameterSqr) {
				GameObject.Destroy(m_points[i+1].gameObject);
				m_points.RemoveAt(i+1);
				needNewRender = true;
				break;
			}
		}//*/

		// add the points
		for (int i = m_points.Count - 2; i >= 0 && m_points.Count < maxNbOfPoint; i--)
		{
			Rigidbody2D p1 = m_points[i];
			Rigidbody2D p2 = m_points[i + 1];	
			
			// if there is a hole in the rope
			Vector3 diff = p1.transform.position - p2.transform.position;
			float distSqr = diff.sqrMagnitude;
			if (distSqr > pointDiameterSqr*5f) {
				// we add a new point between the 2 separated points
				Vector2 middle = p2.transform.position + diff/2;
				m_points.Insert(i, newRopePoint(middle));
				// and set the new relative distance
				needNewRender = true;
				break;
			}
		}//*/

		if (needNewRender)
			gameObject.name = "IRope ("+m_points.Count+")";
		meshGenerator.GenerateMesh(GetComponent<MeshFilter>().mesh, m_points.Select(p=>p.transform.localPosition).ToList(), needNewRender);
	}
	
	void ConstraintEndsPoints() {
		m_points[0].position = m_points[0].transform.position = m_rootPoint.position;
		m_points[m_points.Count-1].position = m_points[m_points.Count-1].transform.position = m_endPoint.position;
	}
	
	void ApplyAirFriction() {
		foreach (var point in m_points) {
			point.AddForce(-point.velocity * m_airFriction);
		}
	}

	void ApplySpringForces()
	{
		for (int i = 0; i < m_points.Count - 1; i++)
		{
			Rigidbody2D p1 = m_points[i];
			Rigidbody2D p2 = m_points[i + 1];
			
			Vector3 diff = p1.position - p2.position;
			Vector3 v = p1.velocity - p2.velocity;
			
			Vector3 Fp1 = (m_ropeStiffness * diff + m_ropeDamping * v);

			if (i != 0)
				p1.AddForce(-Fp1);
			else
				rootForce = -Fp1;

			if (i != m_points.Count - 2)
				p2.AddForce(Fp1);
			else 
				endForce = Fp1;
		}
	}

	private Rigidbody2D newRopePoint(Vector2 position) {
		Rigidbody2D point = ((GameObject)Instantiate(m_ropePointPrefab, position, Quaternion.identity)).GetComponent<Rigidbody2D>();
		point.transform.parent = transform;
		return point;
	}
}
