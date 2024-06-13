
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class ModalAccessGuard : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI questionText;

    [SerializeField] TextMeshProUGUI[] answerOptions;

    private bool isPassed = false;
    string[] questionWithAnswer;

    [SerializeField] UnityEvent OnPassed;
    [SerializeField] UnityEvent OnWrong;


    private void OnEnable()
    {
        GenerateQuiz();
    }

    private void GenerateQuiz()
    {
        // [0] -> first number
        // [1] -> second number
        // [2] -> true answer
        // [3] and so on -> false answer
        questionWithAnswer = GenerateQuestionWithAnswer().Split(";");
        questionText.text = $"{questionWithAnswer[0]} x {questionWithAnswer[1]}";

        // map and obsfuscate 
        int[] numbers = GetShuffledArray(Enumerable.Range(0, 4).ToArray());
        for (int i = 0; i < 4; i++)
        {
            answerOptions[numbers[i]].text = questionWithAnswer[i + 2];
        }

    }

    private int[] GetShuffledArray(int[] arr)
    {
        // Using Fisher-Yates algo
        var rand = new System.Random();
        for (int i = arr.Length - 1; i > 0; i--)
        {
            int j = rand.Next(i + 1);

            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        return arr;
    }

    public string GenerateQuestionWithAnswer()
    {
        int[] questions = new int[2] { Random.Range(1, 10), Random.Range(1, 9) };
        int correctAnswer = questions[0] * questions[1];
        int[] falseAnswer = GenerateFalseAnswers(correctAnswer, 3, 10);

        // format -> num1;num2;answer;false1;false2;false3
        string questionWithAnswers = $"{questions[0]};{questions[1]};{correctAnswer};{falseAnswer[0]};{falseAnswer[1]};{falseAnswer[2]}";

        return questionWithAnswers;
    }

    int[] GenerateFalseAnswers(int referenceNum, int length, int offset)
    {
        int[] generatedFalseAnswers = new int[length];

        for (int i = 0; i < length; i++)
        {
            int temp = Random.Range(referenceNum - offset, referenceNum + offset);
            if (temp == referenceNum)
            {
                temp += Random.Range(1, 5); // obfuscate when same as referenceNum
            }
            generatedFalseAnswers[i] = temp;
        }

        // drawback -> could be one or more duplicates
        return generatedFalseAnswers;
    }

    public void AnswerChecker(TextMeshProUGUI optionText)
    {
        // implemented on gameobject with text as its child
        if (isAnswerTrue(
            int.Parse(questionWithAnswer[0]),
            int.Parse(questionWithAnswer[1]),
            int.Parse(optionText.text)))
        {
            OnPassed?.Invoke();
        }
        else
        {
            OnWrong?.Invoke();
        }
    }

    bool isAnswerTrue(int num1, int num2, int answer)
    {
        return num1 * num2 == answer;
    }

}
