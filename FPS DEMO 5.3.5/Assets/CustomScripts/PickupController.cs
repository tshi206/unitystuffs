using UnityEngine;
using System.Collections;

public class PickupController : MonoBehaviour
{

	public float PickableRange = 10f;
 
	private Camera fpsCam; 
 
 	private Vector3 objectPos;
 	private Quaternion objectRot;
 	private GameObject pickObj;
 	
 	private bool canpick = true;
	private bool picking = false;
 	private bool guipick = false;
	private bool picked = false;

 	private GameObject pickref;

	private Color objcolor;
 
 // Use this for initialization
	void Start () {
  	 pickref = GameObject.FindWithTag("pickedref");
	 pickObj = pickref;
	 fpsCam = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
	 Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
	 RaycastHit hit;
	 if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, PickableRange))
	 {
	  if (hit.collider.gameObject.CompareTag("pickable"))
	  {
	      guipick = true;
	  }
	  else
	  {
	      guipick = false;
	  }
	 }
	    objectPos = transform.position;
 
	    objectRot = transform.rotation;
		if (Input.GetKeyDown(KeyCode.F) && canpick && guipick)
		{
			guipick = false;
			picking = true;
			rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));
			if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, PickableRange) && hit.collider.gameObject.CompareTag("pickable"))
			{
				
				pickObj = hit.collider.gameObject;

				hit.rigidbody.useGravity = false;

				hit.rigidbody.isKinematic = true;

				hit.collider.isTrigger = true;

				hit.transform.parent = gameObject.transform;

				hit.transform.position = objectPos;
   
				hit.transform.rotation = objectRot;

				objcolor = hit.collider.GetComponent<Renderer>().material.color;
				
				Color transparentColor = new Color(objcolor.r, objcolor.g, objcolor.b, 0f);

				hit.collider.gameObject.GetComponent<Renderer>().material.color = transparentColor;

				//Debug.Log("change color" + transparentColor);
			}
		}
		
		if(Input.GetKeyUp(KeyCode.F) && picking){

			picking = false;

			canpick = false;

			picked = true;

		}
		
		if(Input.GetKeyDown(KeyCode.F) && !canpick && !pickObj.GetComponent<PickableObjController>().isRefusethrow()){
 
			canpick = true;
  
			pickObj.GetComponent<Rigidbody>().useGravity = true;

			pickObj.GetComponent<Rigidbody>().isKinematic = false;

			pickObj.transform.parent = null;

			pickObj.GetComponent<Collider>().isTrigger = false;
  
			pickObj.GetComponent<Rigidbody>().AddForce (transform.forward * 8000);
  
			pickObj.GetComponent<Renderer>().material.color = objcolor;
			
			pickObj = pickref;
 
			picked = false;
			
		}
		
		if(Input.GetKeyDown(KeyCode.R) && !canpick && !pickObj.GetComponent<PickableObjController>().isRefusethrow()){
 
			canpick = true;
  
			pickObj.GetComponent<Rigidbody>().useGravity = true;

			pickObj.GetComponent<Rigidbody>().isKinematic = false;

			pickObj.transform.parent = null;

			pickObj.GetComponent<Collider>().isTrigger = false;
  
			pickObj.GetComponent<Renderer>().material.color = objcolor;
			
			pickObj = pickref;
			
			picked = false;
		}
	}

	void OnGUI()
	{
		if (guipick && canpick){
			GUI.Label (new Rect (Screen.width/2,Screen.height/2,Screen.width/2,Screen.height/2), "press F to pick"); 
		}
		else if (picked)
		{
			GUI.Label (new Rect (Screen.width/2,Screen.height/2,Screen.width/2,Screen.height/2), "press F to throw or R to release"); 
		}
	}
}
