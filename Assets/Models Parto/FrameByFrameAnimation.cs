using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FrameByFrameAnimation : MonoBehaviour 
{
	public bool loop = true;
	public bool play = true;
	public float FPS = 60;
	float frameTime = 0;

	int currentFrame =0;
	Transform[] frames;
    public Slider sliderAnimation = null, sliderSpeed;
    //public Slider sliderSpeed = null;
    public int Frame
	{
		get
		{
			return currentFrame;
		}
		set
		{
			if(value >= 0 && value < frames.Length)
			{
				frames[currentFrame].gameObject.SetActive(false);
				currentFrame = value;
				frames[currentFrame].gameObject.SetActive(true);
			}
		}
	}
    public void disableSlider(){
        sliderAnimation.interactable = !sliderAnimation.interactable;
        sliderSpeed.interactable = !sliderSpeed.interactable;    
    }
    public void GoBackwards() {
        Frame--;

    }
    public void SliderPosition(float porcentagem) {
        Frame = (int) (porcentagem * frames.Length);

    }
    public void SliderSpeed(float porcentagem)
    {
        FPS = (int)(porcentagem * 60);

    }
    public void GoForward()
    {
        Frame++;
    }
    public void Play()
    {
        play = !play;
     //   print("Valor de Play "+ play);
    }
    void Start()
	{
		frames = new Transform[this.transform.childCount];
		for(int i = 0; i < frames.Length; i++)
		{
			frames[i] = this.transform.GetChild(i);
			frames[i].gameObject.SetActive(false);
		}
	}
   
    void Update () 
	{
        //  sliderAnimation.value = ((float)currentFrame/(float)frames.Length);
        
        if (play)
		{
            sliderAnimation.value = ((float)currentFrame / (float)frames.Length);
            if (currentFrame != -1)
			{
				frames[currentFrame].gameObject.SetActive(false);
			}
            else
            {
                currentFrame = 0;
            }
			if(FPS <= 0) FPS = 0.1f;
			if(frameTime > (1/FPS))
			{
				currentFrame++;
				frameTime = 0f;
			}
			frameTime += Time.deltaTime;
	
			if(currentFrame >= frames.Length)
			{
				currentFrame = 0;
				if(!loop)
				{
					play = false;
				}
			}
			frames[currentFrame].gameObject.SetActive(true);
            sliderAnimation.value = ((float)currentFrame / (float)frames.Length);
        }
	}
    
}
