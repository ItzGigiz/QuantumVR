using UnityEngine;
using TMPro;

public class BlochSphereController : MonoBehaviour
{
    [Header("Riferimenti Visuali")]
    public Transform vettoreStato;      // Assegna il GameObject VettoreDiStato
    public TextMeshProUGUI txtDisplay; // Display di testo sulla console

    // Angolo theta attuale (0 = |0>, 180 = |1>)
    private float currentTheta = 0f;

    void Start()
    {
        UpdateVisualState();
    }

    // Porta X (NOT): inverte lo stato tra |0> e |1>
    public void ApplyGateX()
    {
        currentTheta = (currentTheta == 0f) ? 180f : 0f;
        UpdateVisualState();
    }

    // Porta H (Hadamard): porta il vettore in sovrapposizione sull'equatore (90°)
    public void ApplyGateH()
    {
        currentTheta = 90f;
        UpdateVisualState();
    }

    // Reset: riporta al Polo Nord |0>
    public void ResetState()
    {
        currentTheta = 0f;
        UpdateVisualState();
    }

    // Misura: aziona il collasso probabilistico
    public void MeasureState()
    {
        if (currentTheta == 90f) // Se è in sovrapposizione
        {
            // 50% di probabilità di collassare a 0 o 1
            int result = Random.Range(0, 2); 
            currentTheta = (result == 0) ? 0f : 180f;
            if (txtDisplay != null) txtDisplay.text = "Risultato Misurato: |" + result + ">";
        }
        else
        {
            int state = (currentTheta == 0f) ? 0 : 1;
            if (txtDisplay != null) txtDisplay.text = "Stato Determinato: |" + state + ">";
        }
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        // Ruota la freccia attorno all'asse X per riflettere l'angolo theta
        if (vettoreStato != null)
        {
            vettoreStato.localRotation = Quaternion.Euler(currentTheta, 0f, 0f);
        }
    }
}