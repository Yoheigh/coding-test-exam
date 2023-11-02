using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Windows;

public class QuestionOne : MonoBehaviour
{
    public TextAsset Input;

    void Start()
    {
        int[] ints = StringToIntArray(Input);

        for (int i = 0, imax = ints.Length; i < imax; i++)
        {
            if (ints[i] == 0)
            {
                Debug.Log("종료");
                break;
            }
            else
                Debug.Log($"{ints[i]}에서 조건을 만족하는 가장 큰 수 : {Solution(ints[i])}");

        }
    }

    public int[] StringToIntArray(TextAsset asset)
    {
        string[] inputs = asset.text.ToString().Split("\n");
        int[] inputsToInt = new int[inputs.Length];

        for (int i = 0; i < inputs.Length; i++)
        {
            inputsToInt[i] = Int32.Parse(inputs[i]);
        }

        return inputsToInt;
    }

    /// <summary>
    /// 주어진 값(0 ~ 2,147,483,647)에서 F(x)를 만족하는 가장 큰 정수를 구한다
    /// </summary>
    /// <param name="input">int 값을 지정</param>
    /// <returns></returns>
    public int Solution(int input)
    {
        for (int x = input; x > 0; x--)
        {
            if (x > 0 && x <= 787109376)
            {
                if (F(x) == 1)
                {
                    return x;
                }
            }

            else if (x <= 1787109376)
                return 787109376;

            // 해당 범위 중 조건을 만족하는 가장 큰 수
            else if (x > 1787109376)
                return 1787109376;
        }

        // 예외처리 상황에 전부 잡히지 않으면
        return -1;
    }

    public int F(int x)
    {
        // x 값을 10진수 string 으로 변환
        string xString = x.ToString("D");

        // x를 10진수로 표현했을 때 마지막 K자리
        int K = xString.Length;

        // x 제곱을 string 으로 변환
        string xStringSquare = ((ulong)x * (ulong)x).ToString();

        string tempString = string.Empty;

        // x 제곱에서 x 값의 자릿수를 구함
        for (int i = xStringSquare.Length - K; i < xStringSquare.Length; i++)
        {
            tempString += xStringSquare[i];
        }

        if (tempString == xString)
            return 1;
        else
            return 0;
    }
}
