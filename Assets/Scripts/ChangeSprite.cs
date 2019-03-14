using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public delegate void ButtonHasBeenPressed(bool state, string map);

public class ChangeSprite : MonoBehaviour
{
    public event ButtonHasBeenPressed ButtonHasBeenPressedRequest;

    [HideInInspector]
    public bool state = true;
    public string map = "";

    private GameObject spriteOn;
    private GameObject spriteOff;

	void Start ()
    {
        spriteOn = transform.Find("IMG_On").gameObject;
        spriteOff = transform.Find("IMG_Off").gameObject;

        GetComponent<Button>().onClick.AddListener(() => ChangeState());
    }
	
	public void ChangeState()
    {
        state = !state;
        spriteOn.SetActive(state);
        spriteOff.SetActive(!state);

        if (ButtonHasBeenPressedRequest != null)
            ButtonHasBeenPressedRequest(state, map);
        else
            Debug.Log("NULLLLL");
    }
}
