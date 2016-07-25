using UnityEngine;
using System.Collections;

public class PlayerShoot : MonoBehaviour {

	RaycastHit shootHit;
	Ray shootRay;
	LineRenderer laserLine;
	int shootableMask;
	GameObject laserBeamOrigin;
	bool isShooting = false;

	// Use this for initialization
	void Start () {
		shootableMask = LayerMask.GetMask ("Enemies");
		laserLine = GetComponentInChildren<LineRenderer> ();
		laserBeamOrigin = GameObject.FindGameObjectWithTag ("LaserBeamOrigin");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1") && isShooting == false) {
			Shoot ();
			isShooting = true;
			Invoke ("StopShooting", 0.15f);
		}
	}

	void Shoot() {
		laserLine.enabled = true;
		//laserLine.SetPosition (0, transform.position); // todo: fix this to com out of the eyes
		laserLine.SetPosition(0, laserBeamOrigin.transform.position);

		// shootRay.origin = transform.position;
		shootRay.origin = laserBeamOrigin.transform.position;
		shootRay.direction = transform.forward;
		Debug.Log("shootRay.direction = " + shootRay.direction.ToString());
		//if(
		Physics.Raycast(shootRay, out shootHit, 100.0f, shootableMask);//){
			laserLine.SetPosition(1, shootHit.point);
		//}
	}

	void StopShooting() {
		laserLine.enabled = false;
		isShooting = false;
	}
}
