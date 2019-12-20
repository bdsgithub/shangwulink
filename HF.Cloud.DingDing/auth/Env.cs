using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HF.Cloud.DingDing.auth
{
    public class Env
    {



        //获取企业的access_token  Https请求方式: GET
        //https://oapi.dingtalk.com/gettoken?corpid=id&corpsecret=secrect


        //获取企业的jsticket  Https请求方式：GET
        //https://oapi.dingtalk.com/get_jsapi_ticket?access_token=ACCESS_TOKE&type=jsapi




        public const String OAPI_HOST = "https://oapi.dingtalk.com";

        public const String CORP_ID = "ding8fd03d105d14827d";
        public const String SECRET = "fSnF7_rVZHW3qnazrQFbR1KyKprpOixXcwKdPirftvFJz0dEoGyel2h0StyEK6Fa";
        //微应用AgentID
        public const String agentId = "9162364";


        //当前的access_token
        public static String access_token = "0a583a6c112a3c4c94831194fb7d9a0a";









    }
}