using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Observer
{
    class TestObserver : MonoBehaviour, InterfaceKit.Observer.IObserver
    {
        private string _name = "Key";
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public void IUpdate( string subject )
        {
            switch( subject )
            {
                case "keyword1":
                    Debug.Log( "==>keyword1 is found" );
                    break;
                case "keyword2":
                    Debug.Log( "==>keyword2 is found" );
                    break;
                default:
                    Debug.Log( subject + "did't find" );
                    break;
            }
        }

        void Awake()
        {
            TestSubject.Instance.AddObserver( this );

        }


        void Start()
        {
            TestSubject.Instance.AddObserver( this );
        }

        void Update()
        {
            TestSubject.Instance.Notify( "keyword1" );
        }
    }
}
