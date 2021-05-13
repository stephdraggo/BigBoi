using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace BigBoi.Menus.OptionsMenuSystem
{
    [AddComponentMenu("BigBoi/Options Menu System/Quality Dropdown (TMPro)")]
    [RequireComponent(typeof(TMP_Dropdown))]
    public class TMProQualityDropdown : MonoBehaviour
    {
        private TMP_Dropdown dropdown;
        private string saveName;

        private void OnValidate()
        {
            saveName = "QualityLevel"; //generate save name

            dropdown = GetComponent<TMP_Dropdown>(); //connect to own dropdown

            dropdown.onValueChanged.AddListener(SetGraphics); //add method to event group
        }

        private void Start()
        {
            int qualityCount = QualitySettings.names.Length; //get number of quality settings allowed

            //fill dropdown here
            dropdown.ClearOptions(); //clear options
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>(); //make list of new items
            TMP_Dropdown.OptionData item = new TMP_Dropdown.OptionData(); //create empty item

            for (int i = 0; i < qualityCount; i++)
            {
                item = new TMP_Dropdown.OptionData(QualitySettings.names[i]); //change empty item to this
                options.Add(item); //add item to list
            }
            dropdown.AddOptions(options); //add options to dropdown

            if (PlayerPrefs.HasKey(saveName)) //if key saved
            {
                int index = PlayerPrefs.GetInt(saveName); //load quality index
                QualitySettings.SetQualityLevel(index); //set quality
                dropdown.value = index; //update display
            }
            else SetGraphics(0); //else set to lowest value

            dropdown.RefreshShownValue(); //refresh display
        }

        private void SetGraphics(int _index)
        {
            QualitySettings.SetQualityLevel(_index);
            PlayerPrefs.SetInt(saveName, _index);
        }
    }
}