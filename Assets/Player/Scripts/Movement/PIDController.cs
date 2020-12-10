using UnityEngine;

[System.Serializable]
public class PidController
{
    [SerializeField] private float linearCorrection = 0.2f;
    [SerializeField] private float integralCorrection = 0.05f;
    [SerializeField] private float derivativeCorrection = 1f;

    private float lastError;
    private float integral;

    public void Reset()
    {
        lastError = 0;
        integral = 0;
    }

    public float Update(float error, float deltaTime)
    {
        var derivative = (error - lastError) / deltaTime;
        integral += error * deltaTime;
        lastError = error;

        return linearCorrection * error + integralCorrection * integral + derivativeCorrection * derivative;
    }
    
}
