using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class AreaPartPaneController : MonoBehaviour
    {
        private BuildModeLevelModel _levelModel;
        public int areaPartIndex;

        void Start()
        {
            this._levelModel = FindObjectOfType<BuildModeLevelModel>();
        }

        void Update()
        {
            SetPaneText(_levelModel.getNumberOfPartsUsed(this.areaPartIndex), _levelModel.numberOfPartsRequired[this.areaPartIndex], _levelModel.numberOfPartsAllowed[this.areaPartIndex]);
        }
        
        private void SetPaneText(int used, int required, int availableTotal)
        {
            Text[] componentsInChildren = this.gameObject.GetComponentsInChildren<Text>();
            componentsInChildren[componentsInChildren.Length - 1].text =
                used + " USED\r\n" + required + " REQUIRED\r\n" + availableTotal + " MAXIMUM";
        }
    }
}