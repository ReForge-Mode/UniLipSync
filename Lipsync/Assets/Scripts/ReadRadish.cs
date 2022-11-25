using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ReadRadish : MonoBehaviour
{
    public List<PhonemeData> phonemeDataList;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F11))
        {
            GetNameList();
        }
    }


    /// <summary>
    /// This is the function called to start reading names from text files
    /// </summary>
    /// <returns></returns>
    public void GetNameList()
    {
        //In case the list is not empty, clear it all out
        //nameList.Clear();

        string path = OpenFileExplorer();
        if (!string.IsNullOrEmpty(path))
        {
            ReadTextFile(path);
        }
    }

    /// <summary>
    /// This is the function to read the file line by line
    /// </summary>
    /// <param name="file_path"></param>
    private void ReadTextFile(string file_path)
    {
        StreamReader inputStream = new StreamReader(file_path);
        bool hasSkippedMetadata = false;

        while (!inputStream.EndOfStream)
        {
            string inputLine = inputStream.ReadLine();

            //Skip metadata
            if (hasSkippedMetadata == false)
            {
                if (inputLine.Contains("-----"))
                {
                    hasSkippedMetadata = true;
                    continue;
                }
            }
            else
            {
                // Do Something with the input. 
                HandleInputPerLine(inputLine);
            }
        }

        inputStream.Close();
    }

    /// <summary>
    /// This is the function that will open the windows dialogue to find the text file
    /// </summary>
    /// <returns></returns>
    private string OpenFileExplorer()
    {
        return EditorUtility.OpenFilePanel("Find Text File (.phonemes)", "", "phonemes");
    }

    /// <summary>
    /// This is the function to handle what to do per line in the text file
    /// </summary>
    /// <param name="inputLine"></param>
    private void HandleInputPerLine(string inputLine)
    {
        //Skip spacebars and :_ for some reason
        if (inputLine[0] == '-' || inputLine.Contains(";_"))
        {
            return;
        }

        PhonemeData temp = new PhonemeData("", 0, 0);

        //Read the Phoneme Symbol
        temp.phoneme = inputLine[0] + "" + inputLine[1];

        #region Find Start number
        //Skip until we found numbers
        int i = 2;
        while (inputLine[i] != '|')
        {
            i++;
            continue;
        }
        i++;

        //Get the number
        string numberString = "";

        while (inputLine[i] != '|')
        {
            if (inputLine[i] != ' ')
            {
                numberString += inputLine[i];
            }
            i++;
        }

        //Convert string to number
        int.TryParse(numberString, out temp.start);
        #endregion

        #region Find End Number
        //Skip until we found numbers
        while (inputLine[i] != '|')
        {
            i++;
            continue;
        }
        i++;

        //Get the number
        numberString = "";

        while (inputLine[i] != '|')
        {
            if (inputLine[i] != ' ')
            {
                numberString += inputLine[i];
            }
            i++;
        }
        i++;

        Debug.Log(numberString);

        //Convert string to number
        int.TryParse(numberString, out temp.end);
        #endregion

        //Add to the list
        phonemeDataList.Add(temp);
    }

}

[System.Serializable]
public struct PhonemeData
{
    public string phoneme;
    public int start;
    public int end;

    public PhonemeData(string _phoneme, int _start, int _end)
    {
        phoneme = _phoneme;
        start = _start;
        end = _end;
    }
}