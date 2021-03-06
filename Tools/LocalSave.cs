﻿//--------------------------------------------------------
//    [Author]:                   Wu Xijin
//    [Date]   :           Sunday, May 08, 2016
//--------------------------------------------------------
using UnityEngine;
using System.Text;

public static class LocalSave {

    public static void SetInt (string key, int value) {

        PlayerPrefs.SetInt(key, value);
    }

    public static int GetInt (string key) {
        if (!PlayerPrefs.HasKey(key)) {
            return 0;
        }
        else {
            return PlayerPrefs.GetInt(key);
        }
    }

    public static void SetFloat (string key, float value) {

        PlayerPrefs.SetFloat(key, value);
    }

    public static float GetFloat (string key) {
        if (!PlayerPrefs.HasKey(key)) {
            return 0f;
        }
        else {
            return PlayerPrefs.GetFloat(key);
        }
    }

    public static void SetString (string key, string value) {

        PlayerPrefs.SetString(key, value);
    }

    public static string GetString (string key) {
        if (!PlayerPrefs.HasKey(key)) {
            return string.Empty;
        }
        else {
            return PlayerPrefs.GetString(key);
        }
    }

    public static void SetVector3 (string key, Vector3 value) {

        StringBuilder sb = new StringBuilder();
        sb.Append(value.x);
        sb.Append(";");
        sb.Append(value.y);
        sb.Append(";");
        sb.Append(value.z);

        PlayerPrefs.SetString(key, sb.ToString());
    }

    public static Vector3 GetVector3 (string key) {

        if (!PlayerPrefs.HasKey(key)) {
            return Vector3.zero;
        }
        else {
            Vector3 v;
            string[] strArray = PlayerPrefs.GetString(key).Split(';');
            v.x = float.Parse(strArray[0]);
            v.y = float.Parse(strArray[2]);
            v.z = float.Parse(strArray[4]);

            return v;
        }
    }

    public static void SetIntArray (string key, int[] value) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < value.Length; i++) {
            sb.Append(value[i]);
            sb.Append(';');
        }

        sb.Remove(sb.Length - 1, 1);

        PlayerPrefs.SetString(key, sb.ToString());
    }

    public static int[] GetIntArray (string key) {
        if (!PlayerPrefs.HasKey(key)) {
            return null;
        }
        else {
            int[] intArray = null;
            string value = PlayerPrefs.GetString(key);

            string[] strArray = value.Split(';');
            intArray = new int[strArray.Length];
            for (int i = 0; i < strArray.Length; i++) {
                intArray[i] = int.Parse(strArray[i]);
            }

            return intArray;
        }
    }

    public static void SetFloatArray (string key, float[] value) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < value.Length; i++) {
            sb.Append(value[i]);
            sb.Append(";");
        }

        sb.Remove(sb.Length - 1, 1);

        PlayerPrefs.SetString(key, sb.ToString());
    }

    public static float[] GetFloatArray (string key) {

        if (!PlayerPrefs.HasKey(key)) {
            return null;
        }
        else {
            float[] array = null;
            string value = PlayerPrefs.GetString(key);

            string[] strArray = value.Split(';');
            array = new float[strArray.Length];
            for (int i = 0; i < strArray.Length; i++) {
                array[i] = float.Parse(strArray[i]);
            }

            return array;
        }

    }

    public static void SetStringArray (string key, string[] value) {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < value.Length; i++) {
            sb.Append(value[i]);
            sb.Append(";");
        }

        sb.Remove(sb.Length - 1, 1);

        PlayerPrefs.SetString(key, sb.ToString());
    }

    public static string[] GeStringArray (string key) {
        if (!PlayerPrefs.HasKey(key)) {
            return null;
        }
        else {
            string value = PlayerPrefs.GetString(key);
            return value.Split(';');
        }
    }
}



