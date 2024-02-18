using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// GUI of the android app
/// </summary>
public class MobileGUI : MonoBehaviour
{
    [SerializeField] private AppClient client;

    [Header("Unconnected")]
    [SerializeField] private GameObject unconnectedRoot;

    [Header("Connected")]
    [SerializeField] private GameObject connectedRoot;
    [SerializeField] private TextMeshProUGUI connectedIpText;

    [Header("Form")]
    [SerializeField] private GameObject formRoot;
    [SerializeField] private Transform gridRoot;
    [SerializeField] private GameObject descriptionPrefab;
    [SerializeField] private GameObject casePrefab;
    private Form form;


    /// <summary>
    /// Initialize the grid with a form
    /// </summary>
    /// <param name="form">The form</param>
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

    /// <summary>
    /// OnClick event to try to find the server
    /// </summary>
    public void Click_TryConnection()
    {
        client.FindServer();
    }

    /// <summary>
    /// Opens the connected tab
    /// </summary>
    /// <param name="ip">The IP the app is connected to</param>
    public void OpenConnectedTab(string ip)
    {
        unconnectedRoot.SetActive(false);
        connectedRoot.SetActive(true);
        formRoot.SetActive(false);
        connectedIpText.text = "Connecté à : " + ip;
    }

    /// <summary>
    /// OnClick event for asking the form
    /// </summary>
    public void Click_AskForForm()
    {
        client.AskForForm();
    }

    /// <summary>
    /// OnClick event for sending the form
    /// </summary>
    public void Click_SendForm()
    {
        client.SendForm(form);
    }

    /// <summary>
    /// OnClick event for pausing the game
    /// </summary>
    public void Click_TogglePause()
    {
        client.PauseApp();
    }
}
