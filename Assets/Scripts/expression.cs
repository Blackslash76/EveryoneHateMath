using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class expression : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI expressionText;
    [SerializeField] TextMeshProUGUI floatingText;
    [SerializeField] Button[] buttons; 
    [SerializeField] int maxNumber = 20;
    [SerializeField] string _operator = string.Empty;

    string[] operators = new string[] { "+", "-", "*", "/" };
    
    List<int> fakeResults = new List<int>();
    int errors = 0;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    public void Init()
    {
        int operatorIndex = Random.Range(0, operators.Length);
        _operator = operators[operatorIndex];

        string expression = CreateSimpleExpression();


        int result = EvaluateSimpleExpression(expression); 
        fakeResults.Clear();
        fakeResults.Add(result);

        errors = 0;

        Debug.Log($"{expression} = {result}");


        //Aggiorno la grafica
        expressionText.text = EncodeExpressionText(expression);

        int buttonWithResult = Random.Range(0, buttons.Length);
        for (int i = 0; i < buttons.Length; i++)
        {
            if (i == buttonWithResult)
            {
                buttons[i].GetComponent<button>().SetButton(true, result.ToString());
            }
            else
            {
                buttons[i].GetComponent<button>().SetButton(false, GenerateFakeResult(result));
            }
        }
    }

    public void WrongAnswerPressed()
    {
        errors++;
        StartCoroutine(DisableButtonsThenReEnable(errors * 2));
    }

    public void CorrectAnswerPressed(button btn)
    {
        
        floatingText.text = "+1";
        floatingText.transform.SetParent(btn.transform);
        floatingText.transform.position = new Vector3(8, 0, 0);
        floatingText.enabled = true;
        floatingText.GetComponent<Animator>().Play("FloatingAndFade");
    }

    string CreateSimpleExpression()
    {
        string expression = string.Empty;
        int leftOperator = Random.Range(1, maxNumber);
        int rightOperator = Random.Range(1, maxNumber);

        switch ( _operator)
        {
            case "+":
                expression = $"{leftOperator} + {rightOperator}";
                break;
            case "-":
                expression = $"{Mathf.Max(leftOperator, rightOperator)} - {Mathf.Min(leftOperator, rightOperator)}";
                break;
            case "*":
                expression = $"{leftOperator} * {rightOperator}";
                break;
            case "/":
                expression = $"{leftOperator * rightOperator} / {rightOperator}";
                break;
        }
        return expression;
    }

    string GenerateFakeResult(int result)
    {
        bool isDup = true;
        int fakeResult = 0;
        while (isDup)
        {
            fakeResult = result + Random.Range(-10, 10);
            isDup = fakeResults.Contains(fakeResult);
        }

        fakeResults.Add(fakeResult);
        return fakeResult.ToString();
    }

    int EvaluateSimpleExpression(string expression)
    {
        int leftOperator, rightOperator;
        int result = 0;

        var s = expression.Split(_operator);
        leftOperator = int.Parse(s[0]);
        rightOperator = int.Parse(s[1]);

        switch (_operator)
        {
            case "+":
                result = leftOperator + rightOperator;
                break;
            case "-":
                result = leftOperator - rightOperator;
                break;
            case "*":
                result = leftOperator * rightOperator;
                break;
            case "/":
                result = leftOperator / rightOperator;
                break;
        }

        return result;
    }

    string EncodeExpressionText(string text)
    {
        text=text.Replace("*", "×");
        text=text.Replace("/", "÷");
        return text;
    }

    IEnumerator DisableButtonsThenReEnable(float timeToWait)
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;            
        }
        yield return new WaitForSeconds(timeToWait);

        foreach (Button button in buttons)
        {
            button.interactable = true;
        }
        yield return null;
    }
}
