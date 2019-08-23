using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileController : MonoBehaviour
{

    public bool saveData(string Filename, Config data)
    {
        if (checkExist(Filename))
        {
            StreamWriter sw = null;
            bool res;

            try
            {
                sw = new StreamWriter(@Filename, false);
                string json = JsonUtility.ToJson(data);
                sw.WriteLine(json);
                res = true;
            }
            catch (Exception e)
            {
                Debug.Log("Cannot open config.json");
                Debug.Log(e.Message);
                res = false;
            }
            finally
            {
                if (sw != null)
                    sw.Close();
            }
            return res;
        }
        else
        {
            return false;
        }
    }

    public Config loadData(string Filename)
    {
        StreamReader sr = null;
        Config returnData = new Config();

        try
        {
            sr = new StreamReader(@Filename, false);
            string json = sr.ReadLine();
            returnData = JsonUtility.FromJson<Config>(json);
        }
        catch(Exception e)
        {
            Debug.Log("Cannot open config.json");
            Debug.Log(e.Message);
            returnData = null;
        }
        finally
        {
            if (sr != null)
                sr.Close();
        }
        return returnData;
    }

    public void generateNewData(string Filename)
    {
        //FileStream
        StreamWriter sw = null;
        Config data = new Config();

        try
        {
            sw = new StreamWriter(@Filename, false);
            string json = JsonUtility.ToJson(data);
            sw.WriteLine(json);
        }
        catch(Exception e)
        {
            Debug.Log("Cannot open config.json");
            Debug.Log(e.Message);
        }
        finally
        {
            if (sw != null)
                sw.Close();
        }
    }

    public bool checkExist(string Filename)
    {
        return File.Exists(@Filename);
    }

    public Config resetData()
    {
        Config temp = new Config();
        return temp;
    }

    public class Config
    {
        //VideoSettings
        public string ScreenResolution = "1280x720";
        public int VSync = 0;
        public bool Fullscreen = true;
        public int AntiAliasing = 8; // 0,2,4,8
        public int TextureQuality = 0; //0(x1), 1(x1/2), 2(x1/4)
        public int ShadowDistance = 40;
        public int ShadowResolution = 3; //0(Low), 1(Medium), 2(High), 3(VeryHigh)

        //AudioSettings
        public int BGM = 80;
        public int SE = 50;
        public bool Mute = false;

        //Other

    }
}
