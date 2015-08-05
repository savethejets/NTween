
#if HOTWEEN_TYPE

using NTweenPackage;
using Holoville.HOTween;

namespace NTweenPackage
{
	public class HOTweenPlugin : TweenPlugin
	{

		public HOTweenPlugin ()
		{
			HOTween.Init ();
		}

		#region TweenPlugin implementation

		public object CreateTweenTo (NTweenBuilder builder, NTweenAttributes attributes)
		{
			var tweenParms = SetAttributes (attributes);
				
			tweenParms.Prop (builder.GetProperty (), builder.GetTweenValueTo ());

			return HOTween.To (builder.GetObject (), builder.GetDuration (), tweenParms);
		}

		static TweenParms SetAttributes (NTweenAttributes attributes)
		{
			var tweenParms = new TweenParms ();

			if (attributes != null) {

				tweenParms.AutoKill (attributes.GetAutoKill ());

				if (attributes.GetDelay () > 0) {
					tweenParms.Delay (attributes.GetDelay ());
				}
					
				if (attributes.GetLoops () != 0) {
					tweenParms.Loops (attributes.GetLoops (), GetLoopType (attributes.GetLoopType ()));
				}

				if (attributes.GetEaseType () != NTweenEaseType.Linear) {
					tweenParms.Ease (GetEaseType (attributes.GetEaseType ()));
				}

				if (attributes.GetOnComplete () != null) {
					tweenParms.OnComplete (attributes.GetOnComplete ().Invoke);
				}
				if (attributes.GetOnPlay () != null) {
					tweenParms.OnStart (attributes.GetOnPlay ().Invoke);
				}
				if (attributes.GetOnUpdate () != null) {
					tweenParms.OnUpdate (attributes.GetOnUpdate ().Invoke);
				}
			}

			return tweenParms;
		}

		public object CreateShake (NTweenBuilder builder, NTweenAttributes attributes)
		{
			TweenParms parms = SetAttributes (attributes);

			parms.Prop (builder.GetProperty (), builder.GetTweenValueTo ());

			return HOTween.Shake (builder.GetObject (), builder.GetDuration (), parms, attributes.GetAmplitude (), attributes.GetFrequency ());
		}

		public void StopAnyTweens (NTweenBuilder builder)
		{
			foreach (var tween in HOTween.GetTweenersByTarget (builder.GetObject (), false)) {
				tween.Kill ();
			}
		}

		public object CreateSequence (NTweenSequence sequence)
		{
			Sequence hoTweenSequence = new Sequence ();

			foreach (var tween in sequence.GetTweens()) {

				// yuck
				if (tween is SequenceTweenHolder) {
					var nTween = (SequenceTweenHolder)tween;

					var tweenParms = SetAttributes (nTween.attribute);
					
					tweenParms.Prop (nTween.builder.GetProperty (), nTween.builder.GetTweenValueTo ());
					
					var hoTween = HOTween.To (nTween.builder.GetObject (), nTween.builder.GetDuration (), tweenParms);
					
					hoTweenSequence.Append (hoTween);
				} else if (tween is SequenceCallBack) {
					hoTweenSequence.AppendCallback (((SequenceCallBack)tween).Callback.Invoke);
				} else if (tween is SequenceInterval) {
					hoTweenSequence.AppendInterval (((SequenceInterval)tween).Duration);
				}
			}

			return hoTweenSequence;
		}

		#endregion


		public static LoopType GetLoopType (NTweenLoopType loop)
		{
			switch (loop) {
			case NTweenLoopType.Restart:
				return LoopType.Restart;
			case NTweenLoopType.YoyoInverse:
				return LoopType.YoyoInverse;
			case NTweenLoopType.Incremental:
				return LoopType.Incremental;
			case NTweenLoopType.Yoyo:
				return LoopType.Yoyo;
			}

			return LoopType.Incremental;
		}

		public static EaseType GetEaseType (NTweenEaseType ease)
		{
			switch (ease) {
			case  NTweenEaseType.Linear:
				return EaseType.Linear;
			case  NTweenEaseType.EaseInSine:
				return EaseType.EaseInSine;
			case  NTweenEaseType.EaseOutSine:
				return EaseType.EaseOutSine;
			case  NTweenEaseType.EaseInOutSine:
				return EaseType.EaseInOutSine;
			case  NTweenEaseType.EaseInQuad:
				return EaseType.EaseInQuad;
			case  NTweenEaseType.EaseOutQuad:
				return EaseType.EaseOutQuad;
			case  NTweenEaseType.EaseInOutQuad:
				return EaseType.EaseInOutQuad;
			case  NTweenEaseType.EaseInCubic:
				return EaseType.EaseInCubic;
			case  NTweenEaseType.EaseOutCubic:
				return EaseType.EaseOutCubic;
			case  NTweenEaseType.EaseInOutCubic:
				return EaseType.EaseInOutCubic;
			case  NTweenEaseType.EaseInQuart:
				return EaseType.EaseInQuart;
			case  NTweenEaseType.EaseOutQuart:
				return EaseType.EaseOutQuart;
			case  NTweenEaseType.EaseInOutQuart:
				return EaseType.EaseInOutQuart;
			case  NTweenEaseType.EaseInQuint:
				return EaseType.EaseInQuint;
			case  NTweenEaseType.EaseOutQuint:
				return EaseType.EaseOutQuint;
			case  NTweenEaseType.EaseInOutQuint:
				return EaseType.EaseInOutQuint;
			case  NTweenEaseType.EaseInExpo:
				return EaseType.EaseInExpo;
			case  NTweenEaseType.EaseOutExpo:
				return EaseType.EaseOutExpo;
			case  NTweenEaseType.EaseInOutExpo:
				return EaseType.EaseInOutExpo;
			case  NTweenEaseType.EaseInCirc:
				return EaseType.EaseInCirc;
			case  NTweenEaseType.EaseOutCirc:
				return EaseType.EaseOutCirc;
			case  NTweenEaseType.EaseInOutCirc:
				return EaseType.EaseInOutCirc;
			case  NTweenEaseType.EaseInElastic:
				return EaseType.EaseInElastic;
			case  NTweenEaseType.EaseOutElastic:
				return EaseType.EaseOutElastic;
			case  NTweenEaseType.EaseInOutElastic:
				return EaseType.EaseInOutElastic;
			case  NTweenEaseType.EaseInBack:
				return EaseType.EaseInBack;
			case  NTweenEaseType.EaseOutBack:
				return EaseType.EaseOutBack;
			case  NTweenEaseType.EaseInOutBack:
				return EaseType.EaseInOutBack;
			case  NTweenEaseType.EaseInBounce:
				return EaseType.EaseInBounce;
			case  NTweenEaseType.EaseOutBounce:
				return EaseType.EaseOutBounce;
			case  NTweenEaseType.EaseInOutBounce:			
				return EaseType.EaseInOutBounce;
			case  NTweenEaseType.EaseInStrong:
				return EaseType.EaseInStrong;
			case  NTweenEaseType.EaseOutStrong:
				return EaseType.EaseOutStrong;
			case  NTweenEaseType.EaseInOutStrong:
				return EaseType.EaseInOutStrong;
			}

			return EaseType.Linear;
		}
	}
}

#endif
