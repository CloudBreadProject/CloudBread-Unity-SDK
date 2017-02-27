using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudBread.OAuth
{
    public interface BaseConnector
    {
        void SessionConfirm();
        void CreateToken();
        void RecreateToken();
    }
}
