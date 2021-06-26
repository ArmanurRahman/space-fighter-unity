using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Header("General Setup Settings")]
    [Tooltip("How fast the sheep move up and down ")][SerializeField] float controlSpeed = 20f; 
    [SerializeField] float xRange = 5f; 
    [SerializeField] float yRange = 5f; 

    [Header("Screen position based tuning")]
    [SerializeField] float positionPitchFactor = -4f;
    [SerializeField] float positionYawFactor = 3f;

    [Header("Player input based tuning")]
    [SerializeField] float controlPitchFactor = -10f;    
    [SerializeField] float controlRollFactor = -20f;
    
    [SerializeField] GameObject[] leasers;

    float xThrow;
    float yThrow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();   
        ProcessRotation();    
        ProcessFiring();
    }

    void ProcessTranslation()
    {
        xThrow = Input.GetAxis("Horizontal");
        
        yThrow = Input.GetAxis("Vertical");
        

        float xOffset = xThrow * Time.deltaTime * controlSpeed;
        float newXPos = transform.localPosition.x + xOffset;
        float clampedPosX = Mathf.Clamp(newXPos, -xRange, xRange);

        float yOffset = yThrow * Time.deltaTime * controlSpeed;
        float newYPos = transform.localPosition.y + yOffset;
        float clampedPosY = Mathf.Clamp(newYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedPosX, clampedPosY, transform.localPosition.z);
    }

    void ProcessRotation()
    {
        float pitch = transform.localPosition.y * positionPitchFactor + yThrow * controlPitchFactor;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    void ProcessFiring(){
        if(Input.GetButton("Fire1")){
            SetFiring(true);
        }else{
            SetFiring(false);
        }
    }

    void SetFiring(bool isActive){
        foreach (GameObject leaser  in leasers)
        {
            var emissionModule = leaser.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
    


}
