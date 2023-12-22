using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MobileGUI : MonoBehaviour
{
    [SerializeField] private AppClient client;

    [Header("Unconnected")]
    [SerializeField] private GameObject unconnectedRoot;
    [SerializeField] private Button tryConnectionButton;
    [SerializeField] private TMP_InputField ipInputField;

    [Header("Connected")]
    [SerializeField] private GameObject connectedRoot;
    [SerializeField] private TextMeshProUGUI connectedIpText;

    [Header("Form")]
    [SerializeField] private GameObject formRoot;
    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject descriptionPrefab;
    [SerializeField] private GameObject casePrefab;
    private Form form;


    public void InitializeForm(Form form)
    {
        formRoot.SetActive(true);
        connectedRoot.SetActive(false);
        this.form = form;

        foreach (Transform child in gridRoot)
        {
            Destroy(child.gameObject);
        }

        GridLayoutGroup layout = gridRoot.GetComponent<GridLayoutGroup>();
        gridRoot.GetComponent<RectTransform>().sizeDelta = new Vector2(
            (layout.cellSize.x) * (form.columns.Count + 1) + 10,
            (layout.cellSize.y) * (form.rows.Count + 1) + 10
        );

        print(gridRoot.GetComponent<RectTransform>().sizeDelta);

        Instantiate(descriptionPrefab, gridRoot).GetComponent<FormDescriptionCase>().Init("\\");

        foreach (string column in form.columns)
        {
            Instantiate(descriptionPrefab, gridRoot).GetComponent<FormDescriptionCase>().Init(column);
        }

        foreach (FormRow row in form.rows)
        {
            Instantiate(descriptionPrefab, gridRoot).GetComponent<FormDescriptionCase>().Init(row.username);

            for (int i = 0; i < row.columnsSucceded.Length; i++)
            {
                Instantiate(casePrefab, gridRoot).GetComponent<FormButtonCase>().Init(row, i);
            }
        }
    }

    public void Click_TryConnection()
    {
        client.TryConnectionToIP(ipInputField.text);
    }


    public void OpenConnectedTab(string ip)
    {
        unconnectedRoot.SetActive(false);
        connectedRoot.SetActive(true);
        formRoot.SetActive(false);
        connectedIpText.text = "Connecté à : " + ip;
    }


    public void Click_AskForForm()
    {
        client.AskForForm();
    }

    public void Click_SendForm()
    {
        client.SendForm(form);
    }

    public void Click_TogglePause()
    {
        client.PauseApp();
    }
}
