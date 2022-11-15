using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    [SerializeField]
    GameObject guaracy;

    private AudioClip[,] orientationMessagesFromGuaracyToPlayer;

    private int orientationIndiceMessage;
    private int[] orientationIndiceRepetition;

    [SerializeField]
    private int maxTalksPerOrientation = 1;

    public AudioSource CallAudioSourcGuaracy;
    public AudioClip[] OrientationFrente;
    public AudioClip[] OrientationFrenteEsquerda;
    public AudioClip[] OrientationFrenteDireita;
    public AudioClip[] OrientationEsquerda;
    public AudioClip[] OrientationDireita;
    public AudioClip[] OrientationTras;

    void Start()
    {
        orientationIndiceRepetition = new int[6];
        orientationMessagesFromGuaracyToPlayer = new AudioClip[6, maxTalksPerOrientation];
        //FuncForTestCreateMessagesOrientationMessagesFromGuaracy();
        fillMessagesOrientationAudioClip();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(guaracy.transform);
    }

    private void WhenCollidedFrente(Collider collider)
    {
        if (collider.tag == "Frente")
        {
            //Debug.Log("Estou na sua frente");
            orientationIndiceMessage = 0;
        }
    }
    private void WhenFrenteEsquerda(Collider collider)
    {
        if (collider.tag == "FrenteEsquerda")
        {
            //Debug.Log("Estou na sua frente um pouco para a esquerda");
            orientationIndiceMessage = 1;
        }
    }
    private void WhenCollidedEsquerda(Collider collider)
    {
        if (collider.tag == "Esquerda")
        {
            //Debug.Log("Estou a sua esquerda");
            orientationIndiceMessage = 2;
        }
    }
    private void WhenCollidedTras(Collider collider)
    {
        if (collider.tag == "Tras")
        {
            //Debug.Log("Estou atras de você");
            orientationIndiceMessage = 3;
        }
    }

    private void WhenCollidedDireita(Collider collider)
    {
        if (collider.tag == "Direita")
        {
            //Debug.Log("Estou a sua direita");
            orientationIndiceMessage = 4;
        }
    }

    private void WhenCollidedFrenteDireita(Collider collider)
    {
        if (collider.tag == "FrenteDireita")
        {
            //Debug.Log("Estou na sua frente um pouco para a direita");
            orientationIndiceMessage = 5;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        WhenCollidedFrente(other);
        WhenFrenteEsquerda(other);
        WhenCollidedFrenteDireita(other);
        WhenCollidedTras(other);
        WhenCollidedEsquerda(other);
        WhenCollidedDireita(other);
    }

    public void MessageGuaracySolicited()
    {
        //Debug.Log(orientationMessagesFromGuaracyToPlayer[orientationIndiceMessage, orientationIndiceRepetition[orientationIndiceMessage]]);
        CallAudioSourcGuaracy.PlayOneShot(orientationMessagesFromGuaracyToPlayer[orientationIndiceMessage, orientationIndiceRepetition[orientationIndiceMessage]]);
        addOrientationIndiceRepetition();
    }

    private void addOrientationIndiceRepetition()
    {
        orientationIndiceRepetition[orientationIndiceMessage]++;
        if (orientationIndiceRepetition[orientationIndiceMessage] >= maxTalksPerOrientation)
            orientationIndiceRepetition[orientationIndiceMessage] = 0;
    }

    /*private void FuncForTestCreateMessagesOrientationMessagesFromGuaracy()
    {
        orientationMessagesFromGuaracyToPlayer = new string[6,5];
        orientationMessagesFromGuaracyToPlayer[0,0] = "Frente 0 ";
        orientationMessagesFromGuaracyToPlayer[0,1] = "Frente 1 ";
        orientationMessagesFromGuaracyToPlayer[0,2] = "Frente 2 ";
        orientationMessagesFromGuaracyToPlayer[0,3] = "Frente 3 ";

        orientationMessagesFromGuaracyToPlayer[1,0] = "Frente Esquerda 0 ";
        orientationMessagesFromGuaracyToPlayer[1,1] = "Frente Esquerda 1 ";
        orientationMessagesFromGuaracyToPlayer[1,2] = "Frente Esquerda 2 ";
        orientationMessagesFromGuaracyToPlayer[1,3] = "Frente Esquerda 3 ";

        orientationMessagesFromGuaracyToPlayer[2,0] = "Esquerda 0 ";
        orientationMessagesFromGuaracyToPlayer[2,1] = "Esquerda 1 ";
        orientationMessagesFromGuaracyToPlayer[2,2] = "Esquerda 2 ";
        orientationMessagesFromGuaracyToPlayer[2,3] = "Esquerda 3 ";

        orientationMessagesFromGuaracyToPlayer[3,0] = "Tras 0 ";
        orientationMessagesFromGuaracyToPlayer[3,1] = "Tras 1 ";
        orientationMessagesFromGuaracyToPlayer[3,2] = "Tras 2 ";
        orientationMessagesFromGuaracyToPlayer[3,3] = "Tras 3 ";

        orientationMessagesFromGuaracyToPlayer[4,0] = "Direita 0 ";
        orientationMessagesFromGuaracyToPlayer[4,1] = "Direita 1 ";
        orientationMessagesFromGuaracyToPlayer[4,2] = "Direita 2 ";
        orientationMessagesFromGuaracyToPlayer[4,3] = "Direita 3 ";

        orientationMessagesFromGuaracyToPlayer[5,0] = "Frente Direita 0 ";
        orientationMessagesFromGuaracyToPlayer[5,1] = "Frente Direita 1 ";
        orientationMessagesFromGuaracyToPlayer[5,2] = "Frente Direita 2 ";
        orientationMessagesFromGuaracyToPlayer[5,3] = "Frente Direita 3 ";
    }*/

    private void fillMessagesOrientationAudioClip()
    {
        fillOneMessageOrientationAudioClip(0, OrientationFrente);
        fillOneMessageOrientationAudioClip(1, OrientationFrenteEsquerda);
        fillOneMessageOrientationAudioClip(2, OrientationEsquerda);
        fillOneMessageOrientationAudioClip(3, OrientationTras);
        fillOneMessageOrientationAudioClip(4, OrientationDireita);
        fillOneMessageOrientationAudioClip(5, OrientationFrenteDireita);
    }

    private void fillOneMessageOrientationAudioClip(int indice, AudioClip[] audioClip)
    {
        for(int i = 0; i < maxTalksPerOrientation; i++)
        {
            orientationMessagesFromGuaracyToPlayer[indice, i] = audioClip[i];
        }
    }
}
