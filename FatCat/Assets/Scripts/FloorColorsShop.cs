using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloorColorsShop : MonoBehaviour
{
    private Button _randomButton;
    private float _timeWhenPause=0.2f;
    private int _costBuying = 10;
    private Color _normalColor;
    private Color _disabledColor;
    [SerializeField] private AnimationCurve _curve;
    private void Awake()
    {
        ColorBlock buttonColors = GameObject.FindObjectOfType<FloorColor>().GetComponent<Button>().colors;
        _normalColor = buttonColors.normalColor;
        _disabledColor = buttonColors.disabledColor;
        ProcessingPlayerData.SetDelegateShowMessage(new ProcessingPlayerData.ShowMessage(ShowWhenNoMoney));
    }
    private void ShowWhenNoMoney(string message)
    {
        Resources.FindObjectsOfTypeAll<YouDoNotHaveEnoughMoneyMessage>()[0].gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        _randomButton = Resources.FindObjectsOfTypeAll<RandomFloorColorButton>()[0].GetComponent<Button>();
        CheckIfBuying();
    }
    private void CheckIfBuying()
    {
        _randomButton.interactable = true;
        if (CheckIfListEmpty(GetNotAllowedColors()))
        {
            _randomButton.gameObject.SetActive(false);
        }
        List<int> allowedFloorColor = ProcessingPlayerData.GetDataFile().avaibleMaterialFloor;
        foreach (FloorColor floorColor in GameObject.FindObjectsOfType<FloorColor>())
        {
            Button _floorColorButton = floorColor.GetComponent<Button>();
            ColorBlock cb = _floorColorButton.colors;
            cb.normalColor = _normalColor;
            cb.disabledColor = _disabledColor;
            _floorColorButton.colors = cb;
            if (allowedFloorColor.Contains(floorColor.Index))
            {
                _floorColorButton.interactable = true;
            }
            else
            {
                _floorColorButton.interactable = false;
            }
        }
    }
    public void BuyColor()
    {
        List<int> NotBoughtColors = GetNotAllowedColors();
        if (NotBoughtColors.Count != 0)
        {
            int count = NotBoughtColors.Count;
            int indexOfElement = Random.Range(0, count);
            List<Button> colorsButtons = new List<Button> { };
            int coins = (int)ProcessingPlayerData.GetDataFile().coins;
            ProcessingPlayerData.BuyFloorColor(_costBuying, NotBoughtColors[indexOfElement]);
            if (coins >= _costBuying)
            {
                //randomButton.gameObject.SetActive(false);
                // user can't again tap on this button while fortune is starting
                _randomButton.interactable = false;
                foreach (FloorColor floorColor in GameObject.FindObjectsOfType<FloorColor>())
                {
                    if (NotBoughtColors.Contains(floorColor.Index))
                    {
                        colorsButtons.Add(floorColor.GetComponent<Button>());
                    }
                }
                StartCoroutine(FortuneEffect(NotBoughtColors[indexOfElement], colorsButtons));
            }
        }
        else
        {
            ProcessingPlayerData.BuyFloorColor(0, 0);
        }
    }
    private IEnumerator FortuneEffect(float indexOfWinItem, List<Button> buttons)
    {
        float seconds = 0;
        while (true)
        {
            seconds += Time.deltaTime;
            float value = _curve.Evaluate(seconds);
            foreach (Button button in buttons)
            {
                ColorBlock cb = button.colors;
                cb.disabledColor = _normalColor;
                cb.normalColor = _disabledColor;
                button.colors = cb;
                yield return new WaitForSeconds(value);
                if (button.GetComponent<FloorColor>().Index == indexOfWinItem && seconds > _timeWhenPause)
                {
                    ColorBlock colorBlock1 = button.colors;
                    colorBlock1.normalColor = _normalColor;
                    colorBlock1.disabledColor = _disabledColor;
                    button.colors = colorBlock1;
                    button.interactable = true;
                    break;
                }
                ColorBlock colorBlock = button.colors;
                colorBlock.normalColor = _normalColor;
                colorBlock.disabledColor = _disabledColor;
                button.colors = colorBlock;
            }
            if (seconds > _timeWhenPause)
            {
                _randomButton.interactable = true;
                break;
            }
        }
    }

    private bool CheckIfListEmpty(List<int> checkList)
    {
        if (checkList.Count == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    private List<int> GetNotAllowedColors()
    {
        List<int> avaibleFloors = ProcessingPlayerData.GetDataFile().avaibleMaterialFloor;
        List<int> AllIndexes = new List<int> { };
        FloorColor[] floorColors = GameObject.FindObjectsOfType<FloorColor>();
        int lastColorIndex = floorColors.Length;
        for (int i = 0; i < lastColorIndex; i++)
        {
            AllIndexes.Add(i);
        }
        foreach (int avaibleIndexes in avaibleFloors)
        {
            AllIndexes.Remove(avaibleIndexes);
        }
        return AllIndexes;
    }
}
