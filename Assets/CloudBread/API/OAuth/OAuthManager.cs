using UnityEngine;
using System.Collections;

namespace CloudBread.OAuth
{
    public class OAuthManager 
    {
        public BaseConnector Connector;

        private bool isUseOAuth = true;

        public OAuthManager()
        {

        }

        public OAuthManager(BaseConnector _connector)
        {
            Connector = _connector;

        }


        public void Clear()
        {

        }

        
        public void ReGet()
        {

        }
    }

}