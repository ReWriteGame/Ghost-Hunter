using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;
public class CinemachineShake : MonoBehaviour
{
   [SerializeField] private float amplitudeGainMax = 1;
   [SerializeField] private float frequencyGainMax = 1;
   
   [SerializeField] private AnimationCurve amplitudeGainCurveIncrease = AnimationCurve.Constant(0,1,1);
   [SerializeField] private AnimationCurve amplitudeGainCurveDecrease = AnimationCurve.Constant(0,1,1);
   [SerializeField] private AnimationCurve frequencyGainCurveIncrease = AnimationCurve.Constant(0,1,1);
   [SerializeField] private AnimationCurve frequencyGainCurveDecrease = AnimationCurve.Constant(0,1,1);
   [SerializeField] private AnimationCurve impulseAmplitudeGainCurve = AnimationCurve.Constant(0,1,1);
   [SerializeField] private AnimationCurve impulseFrequencyGainCurve = AnimationCurve.Constant(0,1,1);

   [SerializeField] private bool resetOnStart = true;
   
   public UnityEvent startShakeEvent;
   public UnityEvent stopShakeEvent;
   public UnityEvent isMinShakeEvent;
   public UnityEvent isMaxShakeEvent;
   public UnityEvent changeShakeEvent;
   public UnityEvent stopChangeShakeEvent;
   
   public bool IsShaking => isShaking;

   private bool isShaking = true;
   private Coroutine shakeCoroutine;
   private CinemachineVirtualCamera cinemachineVirtualCamera;
   private CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin;
   
   private void Awake()
   {
      cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
      cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

      if(resetOnStart)NullShake();
   }
   
 
   public void MoveToMin(float time)
   {
      SetNullCoroutine();
      changeShakeEvent?.Invoke();
      float currentTimeCorrection = time - cinemachineBasicMultiChannelPerlin.m_AmplitudeGain / amplitudeGainMax * time;
      shakeCoroutine = StartCoroutine( ShakeCameraTimerCor(time, amplitudeGainCurveDecrease, frequencyGainCurveDecrease,isMinShakeEvent, currentTimeCorrection));
   }
   
   public void MoveToMax(float time)
   {
      SetNullCoroutine();
      changeShakeEvent?.Invoke();
      float currentTimeCorrection = cinemachineBasicMultiChannelPerlin.m_AmplitudeGain / amplitudeGainMax * time;
      shakeCoroutine = StartCoroutine( ShakeCameraTimerCor(time, amplitudeGainCurveIncrease,frequencyGainCurveIncrease,isMaxShakeEvent, currentTimeCorrection));
   }
   
   public void StopChangeShake()
   { 
     stopChangeShakeEvent?.Invoke();
     SetNullCoroutine();
   }

   private void SetNullCoroutine()
   {
      if(shakeCoroutine != null) StopCoroutine(shakeCoroutine);
   }

   public void ImpulseShake(float time)
   {
      // возвращает тряску в ту позицию с котрой начал но при очень быстром использовании имеет накопительный эффект и камера может улететь 
      /*int keysLastIndexAmp = impulseAmplitudeGainCurve.length - 1;
      impulseAmplitudeGainCurve.MoveKey(0,new Keyframe(impulseAmplitudeGainCurve[0].time, cinemachineBasicMultiChannelPerlin.m_AmplitudeGain));
      impulseAmplitudeGainCurve.MoveKey(keysLastIndexAmp,new Keyframe(impulseAmplitudeGainCurve[keysLastIndexAmp].time, cinemachineBasicMultiChannelPerlin.m_AmplitudeGain));

      int keysLastIndexFreq = impulseFrequencyGainCurve.length - 1;
      impulseFrequencyGainCurve.MoveKey(0,new Keyframe(impulseFrequencyGainCurve[0].time, cinemachineBasicMultiChannelPerlin.m_FrequencyGain));
      impulseFrequencyGainCurve.MoveKey(keysLastIndexFreq,new Keyframe(impulseFrequencyGainCurve[keysLastIndexFreq].time, cinemachineBasicMultiChannelPerlin.m_FrequencyGain));
      */
      SetNullCoroutine();
      shakeCoroutine = StartCoroutine( ShakeCameraTimerCor(time, impulseAmplitudeGainCurve,impulseFrequencyGainCurve));
   }
 
   public void NullShake()
   {
      isShaking = false;
      cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0;
      cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 0;
   }
 
   private IEnumerator ShakeCameraTimerCor(float seconds, AnimationCurve amplitudeCurve, AnimationCurve frequencyCurve, UnityEvent unityEvent = null, float startTime = 0)
   {
      float elapsedTime = startTime;
      while (elapsedTime < seconds)
      {
         elapsedTime += Time.deltaTime;
         float percent = elapsedTime / seconds;
         
         float amplitude = amplitudeGainMax * amplitudeCurve.Evaluate(percent);
         cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
         
         float frequency = frequencyGainMax * frequencyCurve.Evaluate(percent);
         cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
         
         yield return new WaitForEndOfFrame();
      }
      unityEvent?.Invoke();
   }

   private void FixedUpdate()
   {
      if (cinemachineBasicMultiChannelPerlin.m_AmplitudeGain > 0 &&
          cinemachineBasicMultiChannelPerlin.m_FrequencyGain > 0)
      {
         if(!isShaking)startShakeEvent?.Invoke();
         isShaking = true;
      }
      else
      {
         if(isShaking)stopShakeEvent?.Invoke();
         isShaking = false;
      }
   }
}

//info: точно ставить параметры тряски в кривых иначе может слегка подергивать камеру 
