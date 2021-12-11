using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePointController : MonoBehaviour
{
public bool isLookedAt;
public GameObject detector;
public CameraPickUpSystem pUO;


void Start()
{
    detector = GameObject.Find("Place Point Detector");
    isLookedAt = false;
}
void FixedUpdate()
{ 
if(Time.timeScale == 1)
    {
    isLookedAt = false;
    if(isLookedAt == true)
    {
        pUO.activePlacePoint = this.transform;
    }
    }
    else{}
}


     void LookedAt() 
     {
         isLookedAt = true;
         moveDetector();
     }

     void moveDetector()
     {
      GameObject placePoint = gameObject;   
      Quaternion placePointRotation = placePoint.transform.rotation;
      detector.transform.position = new Vector3((placePoint.transform.position.x), (placePoint.transform.position.y), (placePoint.transform.position.z));
      detector.transform.localRotation = placePointRotation;
      detector.transform.parent = placePoint.transform;
     }

}
