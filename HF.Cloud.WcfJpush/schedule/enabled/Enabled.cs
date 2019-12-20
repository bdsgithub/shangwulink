using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HF.Cloud.WcfJpush.util;
using Newtonsoft.Json;
namespace HF.Cloud.WcfJpush.schedule
{
    public class Enabled
    {
        [JsonProperty]
        private bool enable;

        public void setEnable(bool enable) { 
            this.enable = enable;  
        }
        public bool getEnable()
        {
            return enable;
        }
    }
}
