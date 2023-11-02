using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using JetBrains.Annotations;

public class QuestionTwo : MonoBehaviour
{
    public TextAsset Input;

    public int time;
    public int shoesCount;

    public struct Shoes
    {
        /// <summary>
        /// A : 신발이 생성되는 시간
        /// </summary>
        public int A;

        /// <summary>
        /// B : 신발을 착용하기 위해 필요한 시간
        /// </summary>
        public int B;

        /// <summary>
        /// C : 신발의 지속 시간
        /// </summary>
        public int C;

        /// <summary>
        /// D : 신발을 장착했을 때의 속도
        /// </summary>
        public int D;

        public Shoes(int a, int b, int c, int d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }
    }

    public void Start()
    {
        // TextAsset 받아오기
        string[] inputs = TextToStringArray(Input);

        // 기본 값 저장
        time = Int32.Parse(inputs[0]);
        shoesCount = Int32.Parse(inputs[1]);

        // 신발 등록 및 저장
        Shoes[] shoesDatas = new Shoes[inputs.Length - 2];
        for (int i = 0; i < shoesCount; i++)
        {
            shoesDatas[i] = StringToShoes(inputs[i + 2]);
        }

        // 실제 풀이
        int output = Solution(time, shoesCount, shoesDatas);

        // Output.txt로 저장
        PrintToOutput(output.ToString());

    }

    // TextAsset 을 string[] 값으로 변경
    public string[] TextToStringArray(TextAsset asset)
    {
        string[] inputs = asset.text.ToString().Split("\n");
        return inputs;
    }

    // 초기 설정을 제외한 string 값을 split 해서 shoes 로 저장
    public Shoes StringToShoes(string str)
    {
        string[] stringArr = str.Split(' ');

        Shoes tempShoes = new Shoes();

        tempShoes.A = Int32.Parse(stringArr[0]);
        tempShoes.B = Int32.Parse(stringArr[1]);
        tempShoes.C = Int32.Parse(stringArr[2]);
        tempShoes.D = Int32.Parse(stringArr[3]);

        return tempShoes;
    }

    public int Solution(int x, int shoesCount, Shoes[] shoes)
    {
        // 이동한 거리
        int MovedDistance = 0;
        int currentSpeed = 1;

        // 신발 사이의 효율을 비교했을 때 가장 효율적인 신발
        int bestDistance;
        int bestShoes;

        int[][] calculatedDistance = new int[shoesCount][];

        for (int i = 0; i < shoesCount; i++)
        {
            calculatedDistance[i] = CalculateShoes(shoes[i]);
        }

        for (int i = 0; i < x; i++)
        {

            MovedDistance += currentSpeed;
        }

        return MovedDistance;
    }

    public int[] CalculateShoes(Shoes shoes)
    {
        int[] ints = new int[shoes.B + shoes.C];
        int tempDistance = 0;

        for (int i = 0, imax = shoes.B + shoes.C; i < imax; i++)
        {
            // 신발의 착용 시간 중에는 배열에 0 넣기
            if (i < shoes.B)
                tempDistance += 0;

            // 신발을 착용한 후 지속시간 동안에는 배열에 신발 속도 넣기
            else if (i - shoes.B < shoes.C)
                tempDistance += shoes.D;

            // 신발의 지속시간이 끝난 후에는 배열에 1 넣기
            else
                tempDistance += 1;


            ints[i] = tempDistance;
        }

        return ints;
    }

    // Output으로 내보내기
    public void PrintToOutput(string output)
    {
        File.WriteAllText(Application.persistentDataPath + @"Output.txt", output);
        Debug.Log($"{Application.persistentDataPath + @"/Output.txt"}에 {output} 저장");
    }
}
