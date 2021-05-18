using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    #region Public Fields
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Text percentageTracker;
   

    #endregion

    #region Private Fields

    #endregion


    #region Unity Callbacks
   
    #endregion

    #region Other Methods
    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
        percentageTracker.text = $"{Mathf.Round(slider.normalizedValue * 100)}%";
    }

    public void SetMaximumHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        var percentage = slider.value / maxHealth;
        fill.color = gradient.Evaluate(percentage);
        percentageTracker.text = $"{Mathf.Round(percentage * 100)}%";
        
    }
    #endregion
}

