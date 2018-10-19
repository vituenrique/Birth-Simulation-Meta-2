using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ModelRotation : MonoBehaviour
{

    bool left = false, right = false, up = false, down = false, expand = false, shrink = false;

    Vector3 meshesCenter;
    FrameByFrameAnimation animationScript;

    public float rotationSpeed;

    public Vector3 scale;

    public Button play, back, forward, rLeft, rRight, rUp, rDown, sPlus, sMinus;

    public BoxCollider boxCol;

    void Start()
    {
        meshesCenter = GetCenterOfMultipleBounds();
        boxCol.enabled = false;
        animationScript = gameObject.GetComponentInChildren<FrameByFrameAnimation>();
    }

    void Update()
    {
        inputEventRotate();
        inputEventScale();
    }

    /// <summary>
    /// Desabilita a interação com todos os elementos da UI, a animação e o box colider do objeto.
    /// </summary>
    public void disableUI()
    {
        play.enabled = !play.enabled;
        back.enabled = !back.enabled;
        forward.enabled = !forward.enabled;
        rLeft.enabled = !rLeft.enabled;
        rRight.enabled = !rRight.enabled;
        rUp.enabled = !rUp.enabled;
        rDown.enabled = !rDown.enabled;
        sPlus.enabled = !sPlus.enabled;
        sMinus.enabled = !sMinus.enabled;
        animationScript.disableSlider();
        boxCol.enabled = !boxCol.enabled;
    }

    /// <summary>
    /// Sinaliza rotação para esquerda.
    /// </summary>
    public void rotateLeft()
    {
        left = !left;
    }

    /// <summary>
    /// Sinaliza rotação para direita.
    /// </summary>
    public void rotateRight()
    {
        right = !right;
    }

    /// <summary>
    /// Sinaliza rotação para cima.
    /// </summary>
    public void rotateUp()
    {
        up = !up;
    }

    /// <summary>
    /// Sinaliza rotação para baixo.
    /// </summary>
    public void rotateDown()
    {
        down = !down;
    }

    /// <summary>
    /// Sinaliza escala positiva.
    /// </summary>
    public void scalePlus()
    {
        expand = !expand;
    }

    /// <summary>
    /// Sinaliza escala negativa.
    /// </summary>
    public void scaleMinus()
    {
        shrink = !shrink;
    }

    /// <summary>
    /// Aplica as rotações no objeto a partir da entrada do teclado ou click nos botões da simulação. 
    /// </summary>
    void inputEventRotate()
    {
        if (Input.GetKey(KeyCode.RightArrow) || (right == true && rRight.enabled == true))
        {
            gameObject.transform.Rotate(new Vector3(Time.deltaTime * -rotationSpeed, 0, 0));
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || (left == true && rLeft.enabled == true))
        {
            gameObject.transform.Rotate(new Vector3(Time.deltaTime * rotationSpeed, 0, 0));
        }
        else if (Input.GetKey(KeyCode.UpArrow) || (up == true && rUp.enabled == true))
        {
            gameObject.transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
        }
        else if (Input.GetKey(KeyCode.DownArrow) || (down == true && rDown.enabled == true))
        {
            gameObject.transform.Rotate(new Vector3(0, Time.deltaTime * rotationSpeed, 0));
        }
    }

    /// <summary>
    /// Aplica as escalas no objeto a partir da entrada do teclado ou click nos botões da simulação. 
    /// </summary>
    void inputEventScale()
    {
        if (Input.GetKey(KeyCode.D) || (expand == true && sPlus.enabled == true))
        { // Expand
            if (isScalingTooBig(0.009f)) return;
            Vector3 offset = gameObject.transform.position;
            scaleAroundCenter(gameObject.transform, offset, scale);
        }
        else if (Input.GetKey(KeyCode.A) || (shrink == true && sMinus.enabled == true))
        { // Shrink
            if (isScalingTooSmall(0.001f)) return;
            Vector3 offset = gameObject.transform.position;
            scaleAroundCenter(gameObject.transform, -1 * offset, -1 * scale);
        }
    }

    /// <summary>
    /// Calcula, a partir dos bounds do objeto, o centro do mesh.
    /// </summary>
    /// <returns>Retorna um Vector3 sendo ele o centro do mesh.</returns>
    Vector3 GetCenterOfMultipleBounds()
    {
        Bounds bigBound = new Bounds();
        foreach (Renderer r in GetComponentsInChildren<Renderer>())
        {
            bigBound.Encapsulate(r.bounds);
        }
        return bigBound.center;
    }

    /// <summary>
    /// Escala um objeto mantendo ele na mesma posição.
    /// </summary>
    /// <param name="target">Objeto a ser escalado.</param>
    /// <param name="offset">Offset a ser aplicado para manter o objeto na mesma posição após a escala ser aplicada.</param>
    /// <param name="scale">Vetor da escala a ser aplicada.</param>
    void scaleAroundCenter(Transform target, Vector3 offset, Vector3 scale)
    {
        target.position -= offset;
        target.localScale += scale;
        target.position += offset;
    }

    /// <summary>
    /// Verifica se o objeto está numa escala muito pequena.
    /// </summary>
    /// <param name="scalingThreshold">Limite de escala.</param>
    /// <returns>Retorna 'true' se a escala esta muito pequana em algum dos eixos e 'false' caso ainda possa ser aplicada a escala.</returns>
    bool isScalingTooSmall(double scalingThreshold)
    {
        Vector3 target = gameObject.transform.localScale;
        if (target.x < scalingThreshold)
        {
            return true;
        }
        else if (target.y < scalingThreshold)
        {
            return true;
        }
        else if (target.z < scalingThreshold)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Verifica se o objeto está numa escala muito grande.
    /// </summary>
    /// <param name="scalingThreshold">Limite de escala.</param>
    /// <returns>Retorna 'true' se a escala esta muito pequgrandeana em algum dos eixos e 'false' caso ainda possa ser aplicada a escala.</returns>
    bool isScalingTooBig(double scalingThreshold)
    {
        Vector3 target = gameObject.transform.localScale;
        if (target.x > scalingThreshold)
        {
            return true;
        }
        else if (target.y > scalingThreshold)
        {
            return true;
        }
        else if (target.z > scalingThreshold)
        {
            return true;
        }
        return false;
    }

}
