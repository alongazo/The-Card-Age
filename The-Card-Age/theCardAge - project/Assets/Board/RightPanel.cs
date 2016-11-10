using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RightPanel : MonoBehaviour {
    [SerializeField]
    private Text cardNameTextBox1;
    [SerializeField]
    private RawImage cardImage1;
    [SerializeField]
    private Text cardStrengthTextBox1;
    [SerializeField]
    private Text cardDefenseTextBox1;
    [SerializeField]
    private Text cardDescriptionTextBox1;
    [SerializeField]
    private Text cardHPBox1;


    [SerializeField]
    private Text cardNameTextBox2;
    [SerializeField]
    private RawImage cardImage2;
    [SerializeField]
    private Text cardStrengthTextBox2;
    [SerializeField]
    private Text cardDefenseTextBox2;
    [SerializeField]
    private Text cardDescriptionTextBox2;
    [SerializeField]
    private Text cardHPBox2;

  
    [SerializeField]
    private RectTransform rightPanel1;
    [SerializeField]
    private RectTransform rightPanel2;


    private Animator anim1;
    private Animator anim2;
    private int prevRightPanelX = -1;
    private int prevRightPanelY = -1;

    int slideIn1 = Animator.StringToHash("slideIn");
    int slideIn2 = Animator.StringToHash("slideIn");
    

    private bool rightPanel1On = false;

    private void Awake () {
        anim1 = rightPanel1.GetComponent<Animator>();
        anim2 = rightPanel2.GetComponent<Animator>();
    }
	


    public void ViewCardStat(Card card)
    {
        Debug.Log("is card null: " + (card == null));
        if (card == null)
        {
            prevRightPanelX = -1;
            prevRightPanelY = -1;
            rightPanel1On = false;
            anim1.SetBool(slideIn1, false);
            anim2.SetBool(slideIn2, false);

            return;
        }
        
        if (prevRightPanelX != card.CurrentX || prevRightPanelY != card.CurrentY)
        {
            prevRightPanelX = card.CurrentX;
            prevRightPanelY = card.CurrentY;
            if (card != null)
            {

                if (rightPanel1On == false)
                {
                    rightPanel1On = true;
                    // Display name on right panel
                    cardNameTextBox1.text = card.Name();

                    // Display image
                    cardImage1.texture = card.Image();

                    // Display HP on right panel
                    cardHPBox1.text = card.Health() + "/" + card.MaxHealth();

                    // Display strength on right panel
                    string[] temp = cardStrengthTextBox1.text.Split(':');  
                    cardStrengthTextBox1.text = temp[0] +": " + card.Strength();
                    // Display defense on right panel
                    temp = cardDefenseTextBox1.text.Split(':');
                    cardDefenseTextBox1.text = temp[0] + ": " + card.Defense();

                    cardDescriptionTextBox1.text = card.Description();

                    // play right panel slide in/out animation
                    anim1.SetBool(slideIn1, true);
                    anim2.SetBool(slideIn2, false);
                }
                else if (rightPanel1On == true)
                {
                    rightPanel1On = false;

                    //cardNameTextBox2.text = card.name;

                    // Display image
                    cardImage2.texture = card.Image();

                    // Display name on right panel
                    cardNameTextBox2.text = card.Name();

                    // Display HP on right panel
                    cardHPBox2.text = card.Health() + "/" + card.MaxHealth();

                    // Display strength on right panel
                    string[] temp = cardStrengthTextBox2.text.Split(':');
                    cardStrengthTextBox2.text = temp[0] + ": " + card.Strength();
                    // Display defense on right panel
                    temp = cardDefenseTextBox2.text.Split(':');
                    cardDefenseTextBox2.text = temp[0] + ": " + card.Defense();

                    cardDescriptionTextBox2.text = card.Description();
                    anim2.SetBool(slideIn2, true);
                    anim1.SetBool(slideIn1, false);

                }
            }
        }



    }
}
