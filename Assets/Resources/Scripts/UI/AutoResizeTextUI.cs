using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AutoResizeTextUI : MonoBehaviour
{
    public TMP_InputField _inputField; 
    public TextMeshProUGUI _textMeshPro;
    public RectTransform _rectTransform;
    private string _previousText;
    public bool shoudTheParentBeResized = true;
    public bool shouldTheParentReAnchored = false;
    // Start is called before the first frame update
    void Start()
    { 
        if (_textMeshPro != null)
            _previousText = _textMeshPro.text; 
        
        if (_inputField != null){
            // _inputField.onValueChanged.AddListener(delegate { 
            //     _inputField.textComponent.ForceMeshUpdate();
            //     float height = _inputField.textComponent.textBounds.size.y;
            //     _inputField.GetComponent<RectTransform>().sizeDelta = new Vector2(_inputField.GetComponent<RectTransform>().sizeDelta.x, height);
            //     if (shoudTheParentBeResized)
            //     {
            //         var parentRectTransform = _inputField.transform.parent.GetComponent<RectTransform>();
            //         parentRectTransform.sizeDelta = new Vector2(parentRectTransform.sizeDelta.x, height); 
            //         //set enable false and then true to force the parent to resize
            //         parentRectTransform.gameObject.SetActive(false);
            //         parentRectTransform.gameObject.SetActive(true);   
            //     }
            // });
        }
    }

    void Update(){
        if (_textMeshPro != null)
        {
            if (_textMeshPro.text != _previousText)
            {
                AdjustHeightToFitText();
                _previousText = _textMeshPro.text;
            }  
        }
    }

    public void forceUpdate(){
        AdjustHeightToFitText();
    }

    void AdjustHeightToFitText()
    {
        if (_textMeshPro == null)
        {
            Debug.LogWarning("Missing TextMeshPro component on " + gameObject.name);
            return;
        }

        _textMeshPro.ForceMeshUpdate(); // Force the text to update its mesh
        var textBounds = _textMeshPro.textBounds; // Get the text bounds

        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, textBounds.size.y);
        
        //the position should be adjusted as well
        _rectTransform.anchoredPosition = new Vector2(_rectTransform.anchoredPosition.x, -textBounds.size.y / 2);

        if (shoudTheParentBeResized)
        {
            var parentRectTransform = transform.parent.GetComponent<RectTransform>();
            parentRectTransform.sizeDelta = new Vector2(parentRectTransform.sizeDelta.x, textBounds.size.y); 
            //set enable false and then true to force the parent to resize
            parentRectTransform.gameObject.SetActive(false);
            parentRectTransform.gameObject.SetActive(true);
            
        }

        if (shouldTheParentReAnchored)
        {
            var parentRectTransform = transform.parent.GetComponent<RectTransform>();
            float height = parentRectTransform.sizeDelta.y;
            // parentRectTransform.anchoredPosition = new Vector2(parentRectTransform.anchoredPosition.x, -height / 2);
            //new Pos Y
            parentRectTransform.anchoredPosition = new Vector2(parentRectTransform.anchoredPosition.x, -height / 2);
        }        
    }
 
}
