#if DOTWEEN_TYPE

using UnityEngine;
using System.Collections;

namespace NTweenPackage
{
	public class DOTweenPlugin : TweenPlugin {
		#region TweenPlugin implementation

		public object CreateTweenTo (NTweenBuilder builder, NTweenAttributes attributes)
		{
			throw new System.NotImplementedException ();
		}

		public object CreateShake (NTweenBuilder builder, NTweenAttributes attributes)
		{
			throw new System.NotImplementedException ();
		}

		public void StopAnyTweens (NTweenBuilder builder)
		{
			throw new System.NotImplementedException ();
		}

		public object CreateSequence (NTweenSequence sequence)
		{
			throw new System.NotImplementedException ();
		}

		#endregion


	}
}

#endif
