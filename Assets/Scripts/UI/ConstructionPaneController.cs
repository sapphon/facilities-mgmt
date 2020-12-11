using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI
{
    public class ConstructionPaneController : MonoBehaviour
    {
        private BuildModeLevelModel _levelModel;
        public int areaPartIndex;

        void Start()
        {
            void buttonClicked(int index)
            {
                FindObjectOfType<ConstructionPicker>().SetSelectedArea(index);
            }
            this._levelModel = FindObjectOfType<BuildModeLevelModel>();
            SetButtonText(_levelModel.areaParts[areaPartIndex].name, GetComponentInChildren<Button>().gameObject);
            SetButtonCallback(() => buttonClicked(areaPartIndex), GetComponentInChildren<Button>());
        }

        void Update()
        {
            
            SetPaneText(_levelModel.getNumberOfPartsUsed(this.areaPartIndex), _levelModel.numberOfPartsRequired[this.areaPartIndex], _levelModel.numberOfPartsAllowed[this.areaPartIndex]);
        }
        
        private void SetButtonCallback(UnityAction action, Button button)
        {
            button.onClick.AddListener(action);
        }
        
        private void SetPaneText(int used, int required, int availableTotal)
        {
            Text[] componentsInChildren = this.gameObject.GetComponentsInChildren<Text>();
            componentsInChildren[componentsInChildren.Length - 1].text =
                used + " USED\r\n" + required + " REQUIRED\r\n" + availableTotal + " MAXIMUM";
        }
        
        private static void SetButtonText(string text, GameObject buttonObject)
        {
            Text buttonText = buttonObject.GetComponentInChildren<Text>();
            buttonText.text = text;
        }
    }
}