using UnityEngine;
 
//  ---  How to use it:
//
//		TimeSince ts;
//
//		void Start () {
//		    ts = 0;
//		}
//
//		void Update () {
//		    if ( ts > 10 ) {
//		    	DoSomethingAfterTenSeconds();
//				ts = 0  // Reset the timer
//		    }
//		}
//
 
public struct TimeSince {
	
    float time;
 
    public static implicit operator float(TimeSince ts) {
        return Time.time - ts.time;
    }
 
    public static implicit operator TimeSince(float ts) {
        return new TimeSince { time = Time.time - ts };
    }
}
