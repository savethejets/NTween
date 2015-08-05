/*
	NTWeen 

	The MIT License (MIT)

	Copyright (c) 2015 Kyle Reczek

	Permission is hereby granted, free of charge, to any person obtaining a copy
	of this software and associated documentation files (the "Software"), to deal
	in the Software without restriction, including without limitation the rights
	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
	copies of the Software, and to permit persons to whom the Software is
	furnished to do so, subject to the following conditions:

	The above copyright notice and this permission notice shall be included in all
	copies or substantial portions of the Software.

	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
	SOFTWARE.
*/

using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace NTweenPackage
{
	public enum NTweenEaseType
	{
		Linear,
		EaseInSine,
		EaseOutSine,
		EaseInOutSine,
		EaseInQuad,
		EaseOutQuad,
		EaseInOutQuad,
		EaseInCubic,
		EaseOutCubic,
		EaseInOutCubic,
		EaseInQuart,
		EaseOutQuart,
		EaseInOutQuart,
		EaseInQuint,
		EaseOutQuint,
		EaseInOutQuint,
		EaseInExpo,
		EaseOutExpo,
		EaseInOutExpo,
		EaseInCirc,
		EaseOutCirc,
		EaseInOutCirc,
		EaseInElastic,
		EaseOutElastic,
		EaseInOutElastic,
		EaseInBack,
		EaseOutBack,
		EaseInOutBack,
		EaseInBounce,
		EaseOutBounce,
		EaseInOutBounce,
		AnimationCurve,
		EaseInStrong,
		EaseOutStrong,
		EaseInOutStrong
	}

	public enum NTweenLoopType
	{
		Restart,
		Yoyo,
		YoyoInverse,
		Incremental
	}
		
	[Serializable]
	public class NTweenBuilder
	{
		[SerializeField] private float _duration;
		[SerializeField] private string _property;
		[SerializeField] private object _tweenValueTo;
		[SerializeField] private object _object;

		public NTweenBuilder (object obj, float duration, string property, object value)
		{
			_object = obj;
			_duration = duration;
			_property = property;
			_tweenValueTo = value;
		}

		public float GetDuration ()
		{			
			return this._duration;
		}

		public string GetProperty ()
		{
			return this._property;
		}

		public object GetTweenValueTo ()
		{			
			return this._tweenValueTo;
		}

		public object GetObject ()
		{			
			return this._object;
		}
	}
		
	[Serializable]
	public class NTweenAttributes
	{
		[SerializeField] private float _delay = 0;
		[SerializeField] private float _frequency = 1.2f;
		[SerializeField] private float _amplitude = 12f;
		[SerializeField] private int _loops = 0;
		[SerializeField] private Action _onComplete;
		[SerializeField] private Action _onUpdate;
		[SerializeField] private Action _onPlay;
		[SerializeField] private bool _autoKill = true;
		[SerializeField] private Dictionary<string, object> _otherProperties;
		[SerializeField] private NTweenEaseType _easeType = NTweenEaseType.Linear;
		[SerializeField] private NTweenLoopType _loopType = NTweenLoopType.Restart;

		public NTweenAttributes Delay (float delay)
		{
			this._delay = delay;
			return this;
		}

		public NTweenAttributes Loops (int loops)
		{
			this._loops = loops;
			return this;
		}

		public NTweenAttributes Frequency (float frequency)
		{
			this._frequency = frequency;
			return this;
		}

		public NTweenAttributes Amplitude (float amplitude)
		{
			this._amplitude = amplitude;
			return this;
		}

		public NTweenAttributes OtherProperty(string key, object attribute) {

			if (_otherProperties == null) {
				_otherProperties = new Dictionary<string, object> ();
			}

			_otherProperties.Add (key, attribute);

			return this;
		}

		public NTweenAttributes OnUpdate (Action onUpdate)
		{
			this._onUpdate = onUpdate;
			return this;
		}

		public NTweenAttributes OnComplete (Action onComplete)
		{
			this._onComplete = onComplete;
			return this;
		}

		public NTweenAttributes OnPlay (Action onPlay)
		{
			this._onPlay = onPlay;
			return this;
		}

		public NTweenAttributes AutoKill (bool autoKill)
		{
			this._autoKill = autoKill;
			return this;
		}

		public NTweenAttributes Ease (NTweenEaseType ease)
		{
			this._easeType = ease;
			return this;
		}

		public NTweenAttributes LoopType (NTweenLoopType loop)
		{
			this._loopType = loop;
			return this;
		}

		public float GetDelay ()
		{			
			return this._delay;
		}

		public bool GetAutoKill ()
		{
			return this._autoKill;
		}

		public Dictionary<string, object> OtherProperties() {
			return _otherProperties;
		}

		public int GetLoops ()
		{			
			return this._loops;
		}

		public Action GetOnUpdate ()
		{
			return this._onUpdate;
		}

		public Action GetOnComplete ()
		{			
			return this._onComplete;
		}

		public Action GetOnPlay ()
		{			
			return this._onPlay;
		}

		public NTweenEaseType GetEaseType ()
		{			
			return this._easeType;
		}

		public NTweenLoopType GetLoopType ()
		{			
			return this._loopType;
		}

		public float GetAmplitude ()
		{
			return this._amplitude;
		}

		public float GetFrequency ()
		{
			return this._frequency;
		}
	}

	public interface ISequenceItem
	{

	}

	public class SequenceCallBack : ISequenceItem
	{
		public Action Callback;

		public SequenceCallBack (Action callback)
		{
			this.Callback = callback;
		}
	}

	public class SequenceInterval : ISequenceItem
	{
		public float Duration;

		public SequenceInterval (float duration)
		{
			this.Duration = duration;
		}
	}

	public class SequenceTweenHolder : ISequenceItem
	{
		public NTweenBuilder builder;
		public NTweenAttributes attribute;

		public SequenceTweenHolder (NTweenBuilder builder, NTweenAttributes attributes)
		{
			this.builder = builder;
			this.attribute = attributes;
		}
	}

	public class NTweenSequence
	{

		private List<ISequenceItem> _sequences = new List<ISequenceItem> ();

		public void AddTween (NTweenBuilder builder, NTweenAttributes attributes)
		{
			_sequences.Add (new SequenceTweenHolder (builder, attributes));
		}

		public void AddInterval (float duration)
		{
			_sequences.Add (new SequenceInterval (duration));
		}

		public void AddCallback (Action action)
		{
			_sequences.Add (new SequenceCallBack (action));
		}

		public List<ISequenceItem> GetTweens ()
		{
			return _sequences;
		}
	}

	public interface TweenPlugin
	{
		object CreateTweenTo (NTweenBuilder builder, NTweenAttributes attributes);

		object CreateShake (NTweenBuilder builder, NTweenAttributes attributes);

		void StopAnyTweens (NTweenBuilder builder);

		object CreateSequence (NTweenSequence sequence);
	}

	public class NTween : MonoBehaviour
	{
		#region static methods

		private static NTween _levelController;

		public static NTween Instance {
			get {
				string name = "NTween";

				if (_levelController == null) {

					if (GameObject.Find (name) != null) {
						_levelController = GameObject.Find (name).GetComponent<NTween> ();
					} else {
						var obj = new GameObject ();
						obj.name = name;
						_levelController = obj.AddComponent<NTween> ();
					}
				} 

				return _levelController;
			}
		}

		#endregion

		#if HOTWEEN_TYPE

		private TweenPlugin _plugin = new HOTweenPlugin ();

		#endif

		#if DOTWEEN_TYPE

		private TweenPlugin _plugin = new DOTweenPlugin ();

		#endif

		void Start ()
		{

		}

		public object CreateTween (NTweenBuilder builder, NTweenAttributes attributes)
		{
			return _plugin.CreateTweenTo (builder, attributes);
		}

		public object CreateShake (NTweenBuilder builder, NTweenAttributes attributes)
		{
			return _plugin.CreateShake (builder, attributes);
		}

		public object CreateSequence (NTweenSequence sequence)
		{
			return _plugin.CreateSequence (sequence);
		}

		public void StopAnyTweens (NTweenBuilder builder)
		{
			_plugin.StopAnyTweens (builder);
		}
	}

	//@todo Probably need to add pooling of the builders and such there can be a lot that get created. 	
	//public class NTweenBuilderPool {
	//
	//		List<NTweenBuilder> pool = new List<NTweenBuilder> ();
	//
	//		public NTweenBuilder Create() {
	//			if (pool.Count == 0) {
	//				return new NTweenBuilder ();
	//			} else {
	//				var tween = pool[pool.FindLastIndex()];
	//
	//				pool.RemoveAt(pool.FindLastIndex());
	//
	//				return tween;
	//			}
	//		}
	//
	//		public void Return(NTweenBuilder builder) {
	//			builder.AutoKill (false);
	//			builder.Delay (0);
	//			builder.Duration (0);
	//			builder.EaseType (NTweenEaseType.Linear);
	//			builder.Loops (0);
	//			builder.LoopType (NTweenLoopType.Restart);
	//			builder.Obj (null);
	//			builder.GetObject (null);
	//		}
	//	}


	static class NTweenHelper
	{
	
		public static object TweenPosition (this Transform transform, Vector3 finalPosition, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "position", finalPosition);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}

			return NTween.Instance.CreateTween (builder, attributes);
		}

		public static object TweenLocalPosition (this Transform transform, Vector3 finalPosition, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "localPosition", finalPosition);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}


			return NTween.Instance.CreateTween (builder, attributes);
		}

		public static object TweenRotation (this Transform transform, Vector3 finalPosition, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "rotation", finalPosition);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}


			return NTween.Instance.CreateTween (builder, attributes);

		}

		public static object TweenLocalRotation (this Transform transform, Vector3 finalPosition, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "localRotation", finalPosition);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}
				
			return NTween.Instance.CreateTween (builder, attributes);

		}

		public static object TweenScale (this Transform transform, Vector3 finalPosition, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "scale", finalPosition);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}
				
			return NTween.Instance.CreateTween (builder, attributes);

		}

		public static object TweenLocalScale (this Transform transform, Vector3 finalPosition, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "localScale", finalPosition);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}
				
			return NTween.Instance.CreateTween (builder, attributes);

		}

		public static object TweenColor (this SpriteRenderer transform, Color finalColor, float duration, NTweenAttributes attributes = null)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "color", finalColor);

			if (attributes == null) {
				attributes = new NTweenAttributes ();
			}
				
			return NTween.Instance.CreateTween (builder, attributes);

		}

		public static object TweenShakeRotation (this Transform transform, Vector3 rotation, float duration, float frequency, float amplitude)
		{

			NTweenBuilder builder = new NTweenBuilder (transform, duration, "localRotation", rotation);

			var nTweenAttributes = new NTweenAttributes ();

			nTweenAttributes.Frequency (frequency).Amplitude (amplitude);

			return NTween.Instance.CreateShake (builder, nTweenAttributes);

		}

		public static void StopAnyTweens (this Transform transform)
		{
			NTween.Instance.StopAnyTweens (new NTweenBuilder (transform, 0, null, null));
		}
	}
}