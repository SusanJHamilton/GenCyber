using UnityEngine;

public class EffectToggleManager : MonoBehaviour
{
    private bool effectEnabled = false;

    void Updated()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            effectEnabled = !effectEnabled;

            if (effectEnabled)
            {
                Shader.EnableKeyword("EFFECT_ON");



            }
            else
            {
                Shader.DisableKeyword("EFFECT_ON");
            }

            Debug.Log("Effect Toggled: " + effectEnabled);
        }
    }  
}
