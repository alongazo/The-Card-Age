using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class RightPanel : MonoBehaviour {

    [SerializeField]
    private RawImage cardImage1;
    [SerializeField]
    private Text cardStrengthTextBox1;
    [SerializeField]
    private Text cardDefenseTextBox1;
    [SerializeField]
    private Text cardMoveTextBox1;
    [SerializeField]
    private Text cardStatusTextBox1;
    [SerializeField]
    private Text cardHPBox1;
    [SerializeField]
    private Text cardMaxHPBox1;


    [SerializeField]
    private RawImage cardImage2;
    [SerializeField]
    private Text cardStrengthTextBox2;
    [SerializeField]
    private Text cardDefenseTextBox2;
    [SerializeField]
    private Text cardMoveTextBox2;
    [SerializeField]
    private Text cardStatusTextBox2;
    [SerializeField]
    private Text cardHPBox2;
    [SerializeField]
    private Text cardMaxHPBox2;


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
    
	private Card currentCard = null;
    private bool rightPanel1On = false;

	private string prevCardTag = "-1";

    private void Awake () {
        anim1 = rightPanel1.GetComponent<Animator>();
        anim2 = rightPanel2.GetComponent<Animator>();
    }
	private void Update()
	{
		if (currentCard != null)
		{
			if (rightPanel1On) {
				cardHPBox1.text = currentCard.Health ();

			} else {
				cardHPBox2.text = currentCard.Health ();
			}
		}
	}


    public void ViewCardStat(Card card)
    {
		// reset previous selected card from the hand;
		prevCardTag = "-1";

        //Debug.Log("is card null: " + (card == null));
        if (card == null)
        {
            prevRightPanelX = -1;
            prevRightPanelY = -1;
            rightPanel1On = false;
            anim1.SetBool(slideIn1, false);
            anim2.SetBool(slideIn2, false);
			currentCard = null;
            return;
        }
        
        if (prevRightPanelX != card.CurrentX || prevRightPanelY != card.CurrentY)
        {
            prevRightPanelX = card.CurrentX;
            prevRightPanelY = card.CurrentY;
			// get pointer to card information so you can update stats on the panel
			currentCard = card;
		
            if (rightPanel1On == false)
            {
                rightPanel1On = true;

                // Display image
                cardImage1.texture = card.Image();

                // Display HP on right panel
                cardHPBox1.text = card.Health();
                cardMaxHPBox1.text = "/" + card.MaxHealth();

                // Display strength on right panel
                cardStrengthTextBox1.text = card.Strength().ToString();

                // Display defense on right panel
                cardDefenseTextBox1.text = card.Defense().ToString();

                // Display move cost on right panel
                cardMoveTextBox1.text = card.Movement();
                
                // Display status on right panel
                cardStatusTextBox1.text = card.Status();

                // play right panel slide in/out animation
                anim1.SetBool(slideIn1, true);
                anim2.SetBool(slideIn2, false);
            }
            else if (rightPanel1On == true)
            {
                rightPanel1On = false;

                // Display image
                cardImage2.texture = card.Image();

                // Display HP on right panel
                cardHPBox2.text = card.Health();
                cardMaxHPBox2.text = "/" + card.MaxHealth();

                // Display strength on right panel
                cardStrengthTextBox2.text = card.Strength().ToString();

                // Display defense on right panel
                cardDefenseTextBox2.text = card.Defense().ToString();

                // Display move cost on right panel
                cardMoveTextBox2.text = card.Movement();

                // Display status on right panel
                cardStatusTextBox2.text = card.Status();


                anim2.SetBool(slideIn2, true);
                anim1.SetBool(slideIn1, false);

            }
            
        }



    }
	public void ViewCardStatOnHand(Card card, string cardTag)
	{
		//Debug.Log ("is card null: " + (card == null));
		if (card == null) {
			prevRightPanelX = -1;
			prevRightPanelY = -1;
			rightPanel1On = false;
			anim1.SetBool (slideIn1, false);
			anim2.SetBool (slideIn2, false);

			return;
		}

		if (prevCardTag != cardTag) {
			//Debug.Log("view");
			prevRightPanelX = card.CurrentX;
			prevRightPanelY = card.CurrentY;
			//Debug.Log("view2");
			if (rightPanel1On == false) {
				rightPanel1On = true;

				// Display image
				cardImage1.texture = card.Image ();

				// Display HP on right panel
				cardHPBox1.text = card.MaxHealth ();
				cardMaxHPBox1.text = "/" + card.MaxHealth ();

				// Display strength on right panel
				cardStrengthTextBox1.text = card.Strength ().ToString ();

				// Display defense on right panel
				cardDefenseTextBox1.text = card.Defense ().ToString ();

				// Display move cost on right panel
				cardMoveTextBox1.text = card.Movement ();

				// Display status on right panel
				cardStatusTextBox1.text = card.Status ();

				// play right panel slide in/out animation
				anim1.SetBool (slideIn1, true);
				anim2.SetBool (slideIn2, false);
			} else if (rightPanel1On == true) {
				rightPanel1On = false;

				// Display image
				cardImage2.texture = card.Image ();

				// Display HP on right panel
				cardHPBox2.text = card.MaxHealth ();

				cardMaxHPBox2.text = "/" + card.MaxHealth ();

				// Display strength on right panel
				cardStrengthTextBox2.text = card.Strength ().ToString ();

				// Display defense on right panel
				cardDefenseTextBox2.text = card.Defense ().ToString ();

				// Display move cost on right panel
				cardMoveTextBox2.text = card.Movement ();

				// Display status on right panel
				cardStatusTextBox2.text = card.Status ();


				anim2.SetBool (slideIn2, true);
				anim1.SetBool (slideIn1, false);

			}
			prevCardTag = cardTag;
		}

	}
}
