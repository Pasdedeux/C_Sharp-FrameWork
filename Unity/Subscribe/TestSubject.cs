using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BaseClassKit;
using InterfaceKit.Observer;

namespace Assets.Scripts.Observer
{
    class TestSubject : BaseISingleton<TestSubject>, ISubject
    {
        private Dictionary<string, IObserver> _dict = new Dictionary<string, IObserver>();
        public Dictionary<string, IObserver> ObserverPool
        {
            get { return _dict; }
        }

        public void AddObserver( IObserver observer )
        {
            if(!ObserverPool.ContainsKey(observer.Name))
            {
                ObserverPool.Add( observer.Name, observer );
                UnityEngine.Debug.Log( "Observer added succesfully! Now there are Observer num" + ObserverPool.Count );
            }
        }

        public void RemoveObserver( IObserver observer )
        {
            if( ObserverPool.ContainsKey( observer.Name ) )
            {
                ObserverPool.Remove( observer.Name );
                UnityEngine.Debug.Log( "Observer removed succesfully! Now there are Observer num" + ObserverPool.Count );
            }
        }

        public void Notify( string subject )
        {
            foreach( IObserver item in ObserverPool.Values )
            {
                item.IUpdate( subject );
            }
        }
    }
}
