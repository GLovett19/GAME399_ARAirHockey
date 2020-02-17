using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectionHitBoxScript : MonoBehaviour
{
    //Self Assigning Components
    Camera cam;
    EventSystem es_;
    Button but_MyButton;
    Material m_MyMaterial;
    Renderer r_MyRenderer;

    //Inspector Assigned Components
    

    //Private Fields
    float f_counter;

    //Public Fields
    public float f_SelectionThreshold;
    public float f_ShaderSelectionValue;
    public Vector3 buttonPosition;


    // Start is called before the first frame update
    void Start()
    {
        //World Components 
        cam = Camera.main;
        es_ = FindObjectOfType<EventSystem>();       
        
        //Instance Components
        r_MyRenderer = gameObject.GetComponent<Renderer>();
        m_MyMaterial = r_MyRenderer.material;
        but_MyButton = GetComponentInParent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        m_MyMaterial.SetFloat("_Vector1", f_ShaderSelectionValue);

        buttonPosition = but_MyButton.transform.localPosition;
        // Debug.Log(buttonPosition.ToString());

        //transform.position = buttonPosition;


        if (es_.currentSelectedGameObject != null)
        {
            if (es_.currentSelectedGameObject.name == but_MyButton.gameObject.name)
            {
                Debug.Log("Button Selected");
                

                f_counter += Time.deltaTime;
                f_ShaderSelectionValue = 2 * (f_counter / f_SelectionThreshold) - 1;
                
                if (f_counter > f_SelectionThreshold)
                {
                    f_counter = 0;
                    //but_MyButton.onClick.Invoke();
                    
                    
                }
            }
            
        }
    }

    // when this object collides with a designated "selector object" it will be selected by the eventmanager
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("Collision Detected");
        if (other.CompareTag("Selector"))
        {
            //Debug.Log("Tag Identified");
            es_.SetSelectedGameObject(but_MyButton.gameObject);
        }
        
    }
    void OnTriggerExit(Collider other)
    {
        
        f_counter = 0;
        //Debug.Log("Button deselected");
        es_.SetSelectedGameObject(null);
    }
}
