using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour {

    public GameObject player;
    public float fovAngle;
    public float fovRange;

    public delegate void AlertStatusDelegate();
    public static event AlertStatusDelegate alertStatus;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //Check if Player is in range
        bool charInRange;
        if (Vector3.Distance(transform.position, player.transform.position) < fovRange)
        {
            charInRange = true;
        }
        else charInRange = false;

        //Check if player is in unbroken line of sight
        bool unbrokenLineOfSight;
        RaycastHit hit;
        Physics.Linecast(transform.position, player.transform.position, out hit);

        if (hit.collider.tag == "Player")
        {

            unbrokenLineOfSight = true;
        }
        else unbrokenLineOfSight = false;

        //Check if Player is within angle/fov
        //float dot = player.transform.position * player.transform.position * Mathf.Cos(player.transform.position, transform.position);

        bool inFOV;
        Vector3 direction = ZeroOutThisVectorY(player.transform.position) - ZeroOutThisVectorY(transform.position);
        float angle = Vector3.Angle(direction, transform.forward);

        //this one works
        //print(angle + " Unity Angle");
        if (angle < fovAngle * .5f)
        {
            //print("in fov");
            inFOV = true;
        }
        else inFOV = false;

        if (inFOV && unbrokenLineOfSight && charInRange) {
            print("Player seen");
            alertStatus();
        }
            

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 playerDir = ZeroOutThisVectorY(player.transform.position) - ZeroOutThisVectorY(transform.position);
            float targAng = AngleBetweenTwoVectors(direction, transform.forward);
        }

        if (Input.GetKey(KeyCode.N))
        {
            Vector3 playerDir = ZeroOutThisVectorY(player.transform.position) - ZeroOutThisVectorY(transform.position);
            float targAng = SimpleAngle(direction, transform.forward);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees)
    {
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));

    }

    public float Find2DAngleBetweenForwardAndTarget(Vector3 target) {

        //Get the 2D versions of both Vectors
        Vector2 base2DVector = ConvertToVector2(transform.position);
        Vector2 target2DVector = ConvertToVector2(target);

        //Get the Dot product of the vectors------------

        float baseMag = Mathf.Pow(base2DVector.x, 2) + Mathf.Pow(base2DVector.y, 2); //getting the magnitude of this vector by squaring each scalar and adding them together
        float targetMag = Mathf.Pow(target2DVector.x, 2) + Mathf.Pow(target2DVector.y, 2); //getting the magnitude of this vector by squaring each scalar and adding them together
        
        Vector2 base2DVectorNormalized = new Vector2(base2DVector.x * (1/Mathf.Sqrt(baseMag)), base2DVector.y * (1/Mathf.Sqrt(baseMag))); //normalizing the magnitude by dividing the original vector by the square root of it's magnitude
        Vector2 target2DVectorNormalized = new Vector2(target2DVector.x * (1 / Mathf.Sqrt(targetMag)), target2DVector.y * (1 / Mathf.Sqrt(targetMag))); //normalizing the magnitude by dividing the original vector by the square root of it's magnitude

        float dotProduct = base2DVectorNormalized.x * target2DVectorNormalized.x + base2DVectorNormalized.y * target2DVectorNormalized.y; //get the dot of the two vectors

        float dot = dotProduct / (baseMag * targetMag);
        //Multiply it by acos to find the degrees
        return Mathf.Acos(dot);
    }

    public float AngleBetweenTwoVectors(Vector3 vec1, Vector3 vec2) {
        Vector3 originVec = ZeroOutThisVectorY(vec1);
        Vector3 targetVec = ZeroOutThisVectorY(vec2);

        float dot = DotProduct(originVec, targetVec);

        //float normalizedDot = dot / (Magnitude(originVec) * Magnitude(targetVec));

        float acos = Mathf.Acos(dot);

        float angle = acos * 180 / Mathf.PI;
        return angle;
    }

    public float SimpleAngle(Vector3 vec1, Vector3 vec2) {
        Vector3 dir = ZeroOutThisVectorY(player.transform.position) - ZeroOutThisVectorY(transform.position);

        //normalize both vectors
        Vector3 vec1Norm = ThisVectorNormalized(vec1);
        Vector3 vec2Norm = ThisVectorNormalized(vec2);
        
        //run them through atan2
        float ang = Mathf.Atan2(vec2.z - vec1.z, vec2.x - vec1.x) * 180 / Mathf.PI;

        return ang;
    }

    public Vector2 ConvertToVector2(Vector3 vec3) {
        return new Vector2(vec3.x, vec3.z);
    }

    public Vector3 ZeroOutThisVectorY(Vector3 vec3) {
        return new Vector3(vec3.x, 0, vec3.z);
    }

    public float DotProduct(Vector3 vec1, Vector3 vec2) {
        return (vec1.x * vec2.x) + (vec1.y * vec2.y) + (vec1.z * vec2.z);
    }

    public float Magnitude(Vector3 vec) {
        return Mathf.Pow(vec.x,2) + Mathf.Pow(vec.y,2) + Mathf.Pow(vec.z, 2);
    }

    public Vector3 ThisVectorNormalized(Vector3 vec) {
        return new Vector3(vec.x * (1 / Mathf.Sqrt(Magnitude(vec))), vec.y * (1 / Mathf.Sqrt(Magnitude(vec))), vec.z * (1/Mathf.Sqrt(Magnitude(vec))));
    }
    /*
    public Vector3 SeaBassFind2DAngleBetweenForwardAndTarget(Vector3 target) {
        Vector2 targetVect = new Vector2(transform.position.x,transform.position.z) - new Vector2(target.x, target.z); //Get the vector (displacement) of the target from this position - subtract target from this position

        float targetMag = Mathf.Sqrt(Mathf.Pow(targetVect.x, 2) + Mathf.Pow(targetVect.y, 2)); //Get magnitude of that vector - magnitude equation: sqrt(x**2 + y**2)

        Vector2 targetVectNormalized = new Vector2(targetVect.x / targetMag, targetVect.y / targetMag); //normalizing vector by dividing each scalar by the vector's magnitude

        
    }
    */
}
