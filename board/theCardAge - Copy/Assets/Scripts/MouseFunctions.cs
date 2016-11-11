using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]

public class MouseFunctions : MonoBehaviour
{
    public Image defaultImage;
    public Image easyImage;
    public Image mediumImage;
    public Image hardImage;
    private Image currentImage;
    public Button easyButton;
    public Button mediumButton;
    public Button hardButton;
    private bool pointerOnAButton;
    
    void Start()
    {
        currentImage = defaultImage;
        defaultImage.CrossFadeAlpha(1.0f, 1.0f, true);
        easyImage.CrossFadeAlpha(0.0f, 0.0f, true);
        mediumImage.CrossFadeAlpha(0.0f, 0.0f, true);
        hardImage.CrossFadeAlpha(0.0f, 0.0f, true);


    }
    void Update()
    {
        if (pointerOnAButton == true)
        {
            defaultImage.CrossFadeAlpha(0.0f, 0.0f, true);
            currentImage.CrossFadeAlpha(1.0f, 1.0f, true);
            
        }
        else if (pointerOnAButton == false)
        {
            defaultImage.CrossFadeAlpha(1.0f, 1.0f, true);
            currentImage.CrossFadeAlpha(0.0f, 0.0f, true);
        }
    }
    
    public void OnEasyButton()
    {
        currentImage = easyImage;
        pointerOnAButton = true;
    }
    public void OnMediumButton()
    {
        currentImage = mediumImage;
        pointerOnAButton = true;
    }
    public void OnHardButton()
    {
        currentImage = hardImage;
        pointerOnAButton = true;
    }
    public void OutButton()
    {
        pointerOnAButton = false;
    }
}
