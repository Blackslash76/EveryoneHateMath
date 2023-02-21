using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class button : MonoBehaviour
{
    [SerializeField] gameManager gameManager;
    [SerializeField] TextMeshProUGUI buttonText;
    [SerializeField] bool isSolution;
    
    [SerializeField] AudioClip[] correctSounds;
    [SerializeField] AudioClip[] wrongSounds;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
            audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButton(bool solutionButton, string value)
    {
        isSolution = solutionButton;
        buttonText.text = value;
    }

    public void OnButtonClick()
    {
        if (isSolution)
        {
            this.transform.parent.GetComponent<expression>().CorrectAnswerPressed(this);
            int sfx = Random.Range(0,correctSounds.Length);
            audioSource.PlayOneShot(correctSounds[sfx]);
            ExpressionComplete();
        }
        else
        {
            int sfx = Random.Range(0, wrongSounds.Length);
            audioSource.PlayOneShot(wrongSounds[sfx]);
            this.transform.parent.GetComponent<expression>().WrongAnswerPressed();
        }
    }

    void ExpressionComplete()
    {
        gameManager.AddScorePoints(1);
        StartCoroutine(WaitAndNext());
    }

    private IEnumerator WaitAndNext()
    {
        yield return new WaitForSeconds(0.5f);
        var parent = this.transform.parent;
        parent.GetComponent<expression>().Init();
    }

}
