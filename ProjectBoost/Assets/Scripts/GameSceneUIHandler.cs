using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/*
 * Many events will occur with the UI here. Below are all the events that will happen
 * 
 * 1) Player position will update the distance traveled text. This will be achieved by cashing a reference to the player object & getting its
 *    y-position. The game will be set up such that the player's y position at start will be at 0, and since the game will only be going up,
 *    The y-position will not need to worry about negative values.
 *    
 * 2) Player heath bar will update when the player takes damage. this will be done by changing the width of the health bar image to 0. The
 *    width will be calculated based on the proportion of player health being set to the proportion of health bar width.
 *    
 * 3) Fuel bar will update when the player moves upward (not side to side). This will be done by changing the width of the fuel bar image to 0.
 *    The width will be calculated based on the proportion of fuel remaining being set to the proportion of the fuel bar width.
 */

public class GameSceneUIHandler : MonoBehaviour
{
    //Serialized fields
    [SerializeField] TextMeshProUGUI textDistanceTraveled, textTargetDistance;
    [SerializeField] Image imageFuelBar, imageHealthBar;
    [SerializeField][Range(0, 1)] float fltProportionSlider; //Test if fuel bar & health bar image does change

    //Attributes
    float fltFuelBarMaxWidth; //Multiply this value by the proportion of remaining fuel to accurately show remaining fuel
    float fltHealthBarMaxWidth; //Multiply this value by the proportion of remaining health to accurately show remaining health

    void Awake()
    {
        //Set fuel bar & health var max widths based on current width of the resective image widths
        fltFuelBarMaxWidth = imageFuelBar.GetComponent<RectTransform>().sizeDelta.x;
        fltHealthBarMaxWidth = imageHealthBar.GetComponent<RectTransform>().sizeDelta.x;

        //Debug.Log("fuel bar max width: " + fltFuelBarMaxWidth);
        //Debug.Log("health bar max width: " + fltHealthBarMaxWidth);
    }

    void Start()
    {
        textTargetDistance.text = "500"; //Change this when adding level system
    }

    void Update()
    {
        //UpdateFuelImage(fltProportionSlider);
        //UpdateHealthImage(fltProportionSlider);
    }

    public void UpdateDistanceTraveled(float fltDistanceTraveled)
    {
        textDistanceTraveled.text = fltDistanceTraveled.ToString("F0");
    }

    public void UpdateFuelImage(float fltRemaingFuelProportion)
    {
        RectTransform fuelBarRectTransform = imageFuelBar.GetComponent<RectTransform>(); //Get the rect transform component

        //The width and height of the image is stored in the RectTransform's sizeDelta property as a Vector2.
        //Change the width of the image to the proportion of remaining fuel, keep height the same
        float fltNewWidth = fltRemaingFuelProportion * fltFuelBarMaxWidth;
        float fltNewHeight = fuelBarRectTransform.sizeDelta.y;

        fuelBarRectTransform.sizeDelta = new Vector2(fltNewWidth, fltNewHeight);
    }

    public void UpdateHealthImage(float fltRemainingHealthProportion)
    {
        RectTransform healthBarRectTransform = imageHealthBar.GetComponent<RectTransform>(); //Get the rect transform component

        //The width and height of the image is stored in the RectTransform's sizeDelta property as a Vector2.
        //Change the width of the image to the proportion of remaining health, keep height the same
        float fltNewWidth = fltRemainingHealthProportion * fltHealthBarMaxWidth;
        float fltNewHeight = healthBarRectTransform.sizeDelta.y;

        healthBarRectTransform.sizeDelta = new Vector2(fltNewWidth, fltNewHeight);
    }
}
