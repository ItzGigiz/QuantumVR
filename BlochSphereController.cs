using UnityEngine;
using TMPro;

public class BlochSphereController : MonoBehaviour
{
    [Header("Riferimenti Visuali")]
    public Transform vettoreStato;      // Assegna il GameObject VettoreDiStato
    public TextMeshProUGUI txtDisplay; // Display di testo sulla console

    private Quaternion targetRotation = Quaternion.identity;
    private bool inSovrapposizione = false;

    void Start()
    {
        ResetState();
    }

    // Porta X (NOT): ruota sempre lo stato ATTUALE di 180° attorno all'asse X
    public void ApplyGateX()
    {
        targetRotation = Quaternion.Euler(180f, 0f, 0f) * targetRotation;
        UpdateVisualState();
    }

    // Porta H (Hadamard): porta il vettore sull'equatore (in avanti o di lato così si vede bene)
    public void ApplyGateH()
    {
        if (!inSovrapposizione)
        {
            // Se era su |0> (in alto) va in avanti (-90° su X) o verso destra (-90° su Z).
            // Usiamo -90° su X così punta verso l'utente ed è ben visibile!
            float dir = (Vector3.Dot(targetRotation * Vector3.up, Vector3.up) > 0) ? -90f : 90f;
            targetRotation = Quaternion.Euler(dir, 0f, 0f);
            inSovrapposizione = true;
        }
        else
        {
            // Applicare H due volte riporta allo stato puro iniziale
            ResetState();
        }
        UpdateVisualState();
    }

    // Reset: riporta al Polo Nord |0>
    public void ResetState()
    {
        targetRotation = Quaternion.identity;
        inSovrapposizione = false;
        if (txtDisplay != null) txtDisplay.text = "Stato: |0>";
        UpdateVisualState();
    }

    // Misura: aziona il collasso probabilistico
    public void MeasureState()
    {
        if (inSovrapposizione)
        {
            int result = Random.Range(0, 2);
            targetRotation = (result == 0) ? Quaternion.identity : Quaternion.Euler(180f, 0f, 0f);
            inSovrapposizione = false;
            if (txtDisplay != null) txtDisplay.text = "Risultato Misurato: |" + result + ">";
        }
        else
        {
            int state = (Vector3.Dot(targetRotation * Vector3.up, Vector3.up) > 0) ? 0 : 1;
            if (txtDisplay != null) txtDisplay.text = "Stato Determinato: |" + state + ">";
        }
        UpdateVisualState();
    }

    private void UpdateVisualState()
    {
        if (vettoreStato != null)
        {
            vettoreStato.localRotation = targetRotation;
        }
    }
}