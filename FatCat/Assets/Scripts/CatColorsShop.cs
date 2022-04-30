using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CatColorsShop : MonoBehaviour
{
    private Button _randomButton;
    private int _costBuying = 10;
    private float _timeWhenPause = 0.2f;
    private Button _newRandomButton;
    private Color _normalColor;
    private Color _disabledColor;
    [SerializeField] private AnimationCurve _curve;
    private void Awake()
    {
        ColorBlock buttonColors = GameObject.FindObjectOfType<CatColor>().GetComponent<Button>().colors;
        _normalColor = buttonColors.normalColor;
        _disabledColor = buttonColors.disabledColor;
        _randomButton = GameObject.FindObjectOfType<RandomFoodButton>().GetComponent<Button>();
        ProcessingPlayerData.SetDelegateShowMessage(new ProcessingPlayerData.ShowMessage(ShowWhenNoMoney));
    }
    private void ShowWhenNoMoney(string message)
    {
        Resources.FindObjectsOfTypeAll<YouDoNotHaveEnoughMoneyMessage>()[0].gameObject.SetActive(true);
    }
    private void OnEnable()
    {
        _newRandomButton = Resources.FindObjectsOfTypeAll<RandomFoodButton>()[0].GetComponent<Button>();
        CheckIfBuying();
    }
    private void CheckIfBuying()
    {
        // if we exit before animation end, button will be doesn't interactible
        _newRandomButton.interactable = true;
        // hide button for buying new colors
        if (CheckIfListEmpty(GetNotAllowedColors()))
        {
            _randomButton.gameObject.SetActive(false);
        }
        List<int> avaibleCat = ProcessingPlayerData.GetDataFile().avaibleCat;
        CatColor[] catColors = GameObject.FindObjectsOfType<CatColor>();
        foreach (CatColor _catColor in catColors)
        {
            Button _catColorButton = _catColor.GetComponent<Button>();
            ColorBlock cb = _catColorButton.colors;
            cb.normalColor = _normalColor;
            cb.disabledColor = _disabledColor;
            _catColorButton.colors = cb;
            if (avaibleCat.Contains(_catColor.Index))
            {
                _catColorButton.interactable = true;
            }
            else
            {
                _catColorButton.interactable = false;
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
            ProcessingPlayerData.BuyCatColor(_costBuying, NotBoughtColors[indexOfElement]);
            if (coins >= _costBuying)
            {
                //randomButton.gameObject.SetActive(false);
                // user can't again tap on this button while fortune is starting
                _newRandomButton.interactable = false;
                foreach (CatColor catColor in GameObject.FindObjectsOfType<CatColor>())
                {
                    if (NotBoughtColors.Contains(catColor.Index))
                    {
                        colorsButtons.Add(catColor.GetComponent<Button>());
                    }
                }
                StartCoroutine(FortuneEffect(NotBoughtColors[indexOfElement], colorsButtons));
            }
        }
        else
        {
            ProcessingPlayerData.BuyCatColor(0, 0);
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
                if (button.GetComponent<CatColor>().Index == indexOfWinItem && seconds > _timeWhenPause)
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
                _newRandomButton.interactable = true;
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
        List<int> avaibleCat = ProcessingPlayerData.GetDataFile().avaibleCat;
        List<int> AllIndexes = new List<int> { };
        CatColor[] catColors = GameObject.FindObjectsOfType<CatColor>();
        int lastColorIndex = catColors.Length;
        for (int i = 0; i < lastColorIndex; i++)
        {
            AllIndexes.Add(i);
        }

        foreach (int avaibleIndexes in avaibleCat)
        {
            AllIndexes.Remove(avaibleIndexes);
        }
        Debug.Log(AllIndexes);
        return AllIndexes;
    }
}
