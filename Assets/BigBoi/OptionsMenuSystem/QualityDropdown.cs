using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NullReEx = System.NullReferenceException;

namespace BigBoi.OptionsSystem
{
    [AddComponentMenu("BigBoi/Options Menu System/Quality Dropdown")]
    [RequireComponent(typeof(Dropdown))]
    public class QualityDropdown : MonoBehaviour
    {
        private Dropdown dropdown;
        private string saveName;

        [SerializeField, Tooltip("Name the quality levels, if you name too many the excess names will be ignored.")]
        private string[] qualityLevelNames;

        void Start()
        {
            saveName = "QualityLevel"; //generate save name

            dropdown = GetComponent<Dropdown>(); //connect to own dropdown

            dropdown.onValueChanged.AddListener(SetGraphics); //add method to event group

            

            //check number of quality levels available
            if (!PlayerPrefs.HasKey("QualityCount")) //if no count saved
            {
                int qualityCount = 0; //set count to 0
                QualitySettings.SetQualityLevel(0); //set quality to lowest
                for (int i = 0; i < 2; i++) //loop time
                {
                    QualitySettings.IncreaseLevel(); //increase quality level by 1
                    if (QualitySettings.GetQualityLevel() > qualityCount) //check if quality level has actually increased
                    {
                        i = 0; //tell loop to go again
                    }
                    else //if quality level has not increased
                    {
                        i = 2; //tell loop this is the last round
                    }
                    qualityCount++; //add 1 to quality count
                }
                PlayerPrefs.SetInt("QualityCount", qualityCount); //save quality level count to playerprefs
            }

            //fill dropdown here
            dropdown.ClearOptions(); //clear options
            List<Dropdown.OptionData> options = new List<Dropdown.OptionData>(); //make list of new items
            Dropdown.OptionData item = new Dropdown.OptionData(); //create empty item

            for (int i = 0; i < PlayerPrefs.GetInt("QualityCount"); i++)
            {
                item = new Dropdown.OptionData(qualityLevelNames[i]); //change empty item to this
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

        void Update()
        {

        }

        void SetGraphics(int _index)
        {
            QualitySettings.SetQualityLevel(_index);
            PlayerPrefs.SetInt(saveName, _index);
        }
    }
}